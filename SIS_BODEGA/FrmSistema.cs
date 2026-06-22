using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos del programa

namespace SIS_BODEGA
{
    public partial class FrmSistema : Form
    {
        decimal acumuladorTotal = 0;
        int acumuladorCantidad = 0;
        public FrmSistema()
        {
            InitializeComponent();
            CargarProductos();

        }
        // Este método se encarga de cargar los productos desde la base de datos y mostrarlos en el ComboBox
        private void CargarProductos()
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            string query = "SELECT nombre FROM Productos";
            SqlCommand cmd = new SqlCommand(query, conexion);
            SqlDataReader lector = cmd.ExecuteReader();

            // Limpiamos los combos
            cmbProducto.Items.Clear();
            cmbNombre.Items.Clear();

            // Creamos una colección especial para el autocompletado
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

            while (lector.Read())
            {
                string nombreProd = lector["nombre"]?.ToString() ?? "";

                // Añadimos a las listas normales
                cmbProducto.Items.Add(nombreProd);
                cmbNombre.Items.Add(nombreProd);

                // Añadimos a la colección de autocompletado
                coleccion.Add(nombreProd);
            }

            lector.Close();
            conexion.Close();

            // ASIGNACIÓN CLAVE: Le inyectamos la memoria de autocompletado al combo de inventario
            cmbNombre.AutoCompleteCustomSource = coleccion;
            cmbNombre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbNombre.AutoCompleteSource = AutoCompleteSource.CustomSource; // Cambia a CustomSource
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            string query = "SELECT precio FROM Productos WHERE nombre = @prod";
            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@prod", cmbProducto.Text);

            object resultado = cmd.ExecuteScalar();
            if (resultado != null)
            {
                txtMonto.Text = resultado.ToString();
            }
            conexion.Close();
        }
        private void btnVenta_Click(object sender, EventArgs e)
        {
            // Validar que haya productos agregados
            if (acumuladorTotal <= 0)
            {
                MessageBox.Show("Debe añadir productos antes de cobrar.");
                return;
            }

            // Validar el monto con el que paga el cliente
            decimal montoPagado;

            if (string.IsNullOrWhiteSpace(txtPagoCon.Text))
            {
                MessageBox.Show("Ingrese el monto con el que paga el cliente.");
                return;
            }

            if (!decimal.TryParse(txtPagoCon.Text, out montoPagado))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            // Calcular el vuelto
            decimal vueltoFinal = montoPagado - acumuladorTotal;

            if (vueltoFinal < 0)
            {
                MessageBox.Show("El dinero ingresado es insuficiente.");
                lblVuelto.Text = "Falta Dinero";
                return;
            }

            lblVuelto.Text = "S/. " + vueltoFinal.ToString("N2");

            // Registrar la venta
            string nuevoId = ConexionVentas.ObtenerSiguienteId();
            ConexionVentas.InsertarVenta(nuevoId, acumuladorTotal, acumuladorCantidad);

            // Descontar stock
            string productoSeleccionado = cmbNombre.Text;
            int cantidadVendida = acumuladorCantidad;

            using (SqlConnection conexion = new SqlConnection(Conexion.Cadena))
            {
                conexion.Open();

                string querySelect =
                    "SELECT stock_actual FROM Productos WHERE nombre = @nom";

                SqlCommand cmdSelect =
                    new SqlCommand(querySelect, conexion);

                cmdSelect.Parameters.AddWithValue("@nom", productoSeleccionado);

                object resultado = cmdSelect.ExecuteScalar();

                if (resultado != null)
                {
                    int stockActual = Convert.ToInt32(resultado);
                    int nuevoStock = stockActual - cantidadVendida;

                    if (nuevoStock < 0)
                    {
                        MessageBox.Show("No hay suficiente stock.");
                        return;
                    }

                    string queryUpdate =
                        "UPDATE Productos SET stock_actual = @stock WHERE nombre = @nom";

                    SqlCommand cmdUpdate =
                        new SqlCommand(queryUpdate, conexion);

                    cmdUpdate.Parameters.AddWithValue("@stock", nuevoStock);
                    cmdUpdate.Parameters.AddWithValue("@nom", productoSeleccionado);

                    cmdUpdate.ExecuteNonQuery();
                }
            }

            MessageBox.Show(
                "Venta guardada con éxito.\n\n" +
                "ID Venta: " + nuevoId +
                "\nCantidad de Productos: " + acumuladorCantidad +
                "\nTotal Cobrado: S/. " + acumuladorTotal.ToString("N2") +
                "\nVuelto: " + lblVuelto.Text
            );

            // Reiniciar la venta
            acumuladorTotal = 0;
            acumuladorCantidad = 0;

            txtCantidad.Clear();
            txtMonto.Clear();
            txtPagoCon.Clear();
            cmbProducto.SelectedIndex = -1;
            cmbNombre.SelectedIndex = -1;

        }
        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            return;
        }

        private void bntAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Ingrese una cantidad.");
                return;
            }

            int cantidadIngresada;

            if (!int.TryParse(txtCantidad.Text, out cantidadIngresada))
            {
                MessageBox.Show("Cantidad inválida.");
                return;
            }

            decimal precioUnitario = Convert.ToDecimal(txtMonto.Text);

            decimal subtotal = precioUnitario * cantidadIngresada;

            acumuladorTotal += subtotal;
            acumuladorCantidad += cantidadIngresada;

            MessageBox.Show(
                "Producto agregado.\n" +
                "Subtotal: S/. " + subtotal.ToString("N2") +
                "\nTotal acumulado: S/. " + acumuladorTotal.ToString("N2")
            );

            cmbProducto.SelectedIndex = -1;
            txtCantidad.Clear();
            txtMonto.Clear();
            cmbProducto.Focus();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNombre.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto antes de consultar.");
                return;
            }
            string producto = cmbNombre.Text;
            int stockReal = ConexionInventario.ConsultarStockActual(producto);
            txtStock.Text = stockReal.ToString();
        }

        private void btnInvModificar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cmbNombre.Text) || string.IsNullOrEmpty(txtNuevaCantidad.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto e ingrese la cantidad a añadir.");
                return;
            }

            string producto = cmbNombre.Text;
            int cantidadAIngresar = Convert.ToInt32(txtNuevaCantidad.Text);
            ConexionInventario.SurtirMercaderia(producto, cantidadAIngresar);

            MessageBox.Show("¡Stock modificado con éxito! Se añadieron " + cantidadAIngresar + " unidades.");
            int nuevoStock = ConexionInventario.ConsultarStockActual(producto);
            txtStock.Text = nuevoStock.ToString();
            txtNuevaCantidad.Clear();
        }

        private void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si el combo de inventario está vacío, no hace nada
            if (string.IsNullOrEmpty(cmbNombre.Text)) return;

            string productoSeleccionado = cmbNombre.Text;
            // Buscamos el stock real en la base de datos
            int stockReal = ConexionInventario.ConsultarStockActual(productoSeleccionado);

            txtStock.Text = stockReal.ToString();
        }

        private void cmbNombre_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            string query = "SELECT precio FROM Productos WHERE nombre = @prod";
            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@prod", cmbProducto.Text);

            object resultado = cmd.ExecuteScalar();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            dgvReportes.DataSource = ConexionVentas.TraerReporte();
        }


        private void btnVerProductos_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            string query = @"SELECT 
                        id_producto, 
                        nombre, 
                        precio, 
                        stock_actual, 
                        stock_minimo, 
                        unidad_medida, 
                        categoria,
                        CASE 
                            WHEN stock_actual <= (stock_minimo + 1) THEN 'Reponer' 
                            ELSE 'Disponible' 
                        END AS [estado] 
                     FROM Productos";

            SqlCommand cmd = new SqlCommand(query, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            DataTable datos = new DataTable();

            adaptador.Fill(datos);
            conexion.Close();

            dgvReportes.DataSource = datos;
            dgvReportes.AutoResizeColumns();
        }

        private void btnCierre_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Suma el total de la tabla Ventas, pero SÓLO si corresponden al día de hoy.
            // Usamos CURRENT_TIMESTAMP o GETDATE() que son las funciones nativas de SQL Server.
            string query = @"SELECT ISNULL(SUM(total), 0) 
                     FROM Ventas 
                     WHERE DATEDIFF(day, fecha_venta, GETDATE()) = 0";

            SqlCommand cmd = new SqlCommand(query, conexion);
            decimal totalCobradoHoy = Convert.ToDecimal(cmd.ExecuteScalar());

            conexion.Close();

            // Muestra el resultado real acumulado solo durante el día de hoy
            MessageBox.Show("=== CIERRE DE CAJA DIARIO ===\n\n" +
                            "Fecha: " + DateTime.Today.ToShortDateString() + "\n" +
                            "Total Efectivo de Hoy: S/. " + totalCobradoHoy.ToString("N2"),
                            "Cierre de Caja");
        }

        private void btnMasVendidos_Click(object sender, EventArgs e)
        {
            DataTable datosTop = ConexionesAdmin.ObtenerTopProductos();
            dgvReportes.DataSource = datosTop;
            dgvReportes.AutoResizeColumns();
        }

        private void btnVuelto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPagoCon.Text))
            {
                lblVuelto.Text = "S/. 0.00";
                return;
            }

            decimal montoPagado;

            if (!decimal.TryParse(txtPagoCon.Text, out montoPagado))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            decimal vuelto = montoPagado - acumuladorTotal;

            if (vuelto >= 0)
            {
                lblVuelto.Text = "S/. " + vuelto.ToString("N2");
            }
            else
            {
                lblVuelto.Text = "Falta Dinero";
            }
        }

        private void txtPagoCon_TextChanged(object sender, EventArgs e)
        {
            return;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // 1. Validación rápida: que no esté vacío
            if (string.IsNullOrWhiteSpace(txtIdEliminar.Text))
            {
                MessageBox.Show("Escribe un ID para borrar.");
                return;
            }

            // 2. Conexión y ejecución directa a SQL
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Primero borramos el detalle (por la llave foránea)
            string query1 = "DELETE FROM DetalleVenta WHERE id_venta = @id";
            SqlCommand cmd1 = new SqlCommand(query1, conexion);
            cmd1.Parameters.AddWithValue("@id", txtIdEliminar.Text.Trim());
            cmd1.ExecuteNonQuery();

            // Luego borramos la venta principal
            string query2 = "DELETE FROM Ventas WHERE id_venta = @id";
            SqlCommand cmd2 = new SqlCommand(query2, conexion);
            cmd2.Parameters.AddWithValue("@id", txtIdEliminar.Text.Trim());
            cmd2.ExecuteNonQuery();

            conexion.Close();

        
        }
    }
}
