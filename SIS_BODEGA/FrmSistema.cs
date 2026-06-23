using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos local

namespace SIS_BODEGA
{
    C#
public partial class FrmSistema : Form
    {
        // Variables globales para rastrear el subtotal acumulado y la cantidad de artículos en la transacción actual
        decimal acumuladorTotal = 0;
        int acumuladorCantidad = 0;

        /// <summary>
        /// Constructor del formulario. Inicializa los componentes visuales y carga el catálogo de productos.
        /// </summary>
        public FrmSistema()
        {
            InitializeComponent();
            CargarProductos();
        }

        /// <summary>
        /// Carga los nombres de los productos desde la base de datos a los ComboBoxes correspondientes
        /// y configura la colección para el autocompletado del buscador.
        /// </summary>
        private void CargarProductos()
        {
            // Se establece y abre la conexión con la base de datos
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Consulta SQL para obtener únicamente los nombres de los productos registrados
            string query = "SELECT nombre FROM Productos";
            SqlCommand cmd = new SqlCommand(query, conexion);
            SqlDataReader lector = cmd.ExecuteReader();

            // Se limpian los elementos previos de ambos ComboBoxes para evitar duplicaciones
            cmbProducto.Items.Clear();
            cmbNombre.Items.Clear();

            // Estructura especial optimizada para almacenar los datos de sugerencia de texto (Autocompletado)
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

            // Recorrido de los registros devueltos por la base de datos
            while (lector.Read())
            {
                // Operador de coalescencia nula para mitigar valores nulos de la base de datos
                string nombreProd = lector["nombre"]?.ToString() ?? "";

                // Se añade el nombre del producto a las listas de los elementos de interfaz
                cmbProducto.Items.Add(nombreProd);
                cmbNombre.Items.Add(nombreProd);

                // Se agrega el nombre a la colección encargada del filtro predictivo
                coleccion.Add(nombreProd);
            }

            // Cierre preventivo de los flujos de datos abiertos
            lector.Close();
            conexion.Close();

            // Configuración de las propiedades del control cmbNombre para habilitar la predicción y autocompletado
            cmbNombre.AutoCompleteCustomSource = coleccion;
            cmbNombre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        /// <summary>
        /// Evento que se dispara al seleccionar un producto en el ComboBox de ventas.
        /// Recupera y muestra el precio unitario del producto seleccionado.
        /// </summary>
        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Consulta parametrizada para buscar el precio unitario exacto según el nombre seleccionado
            string query = "SELECT precio FROM Productos WHERE nombre = @prod";
            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@prod", cmbProducto.Text);

            object resultado = cmd.ExecuteScalar();
            if (resultado != null)
            {
                // Asigna el precio recuperado al campo de texto del monto
                txtMonto.Text = resultado.ToString();
            }
            conexion.Close();
        }

        /// <summary>
        /// Gestiona la confirmación del pago, cálculo del vuelto, inserción de la venta en la base de datos
        /// y la deducción física del stock de inventario.
        /// </summary>
        private void btnVenta_Click(object sender, EventArgs e)
        {
            // Validación: Verifica que el usuario haya acumulado subtotal agregando productos antes de intentar pagar
            if (acumuladorTotal <= 0)
            {
                MessageBox.Show("Debe añadir productos antes de cobrar.");
                return;
            }

            decimal montoPagado;

            // Validación: Verifica la presencia de caracteres en el campo de pago
            if (string.IsNullOrWhiteSpace(txtPagoCon.Text))
            {
                MessageBox.Show("Ingrese el monto con el que paga el cliente.");
                return;
            }

            // Validación: Intenta convertir de forma segura el texto a un tipo decimal decimal válido
            if (!decimal.TryParse(txtPagoCon.Text, out montoPagado))
            {
                MessageBox.Show("Ingrese un monto válido.");
                return;
            }

            // Cálculo aritmético del vuelto final que se le otorgará al comprador
            decimal vueltoFinal = montoPagado - acumuladorTotal;

            // Validación: El dinero entregado por el cliente es menor que el total de la compra
            if (vueltoFinal < 0)
            {
                MessageBox.Show("El dinero ingresado es insuficiente.");
                lblVuelto.Text = "Falta Dinero";
                return;
            }

            // Muestra el vuelto con formato estándar de dos decimales
            lblVuelto.Text = "S/. " + vueltoFinal.ToString("N2");

            // Persistencia: Invoca el método para generar el ID autoincremental e insertar la cabecera de la venta
            string nuevoId = ConexionVentas.ObtenerSiguienteId();
            ConexionVentas.InsertarVenta(nuevoId, acumuladorTotal, acumuladorCantidad);

            // Bloque lógico encargado de controlar el decremento del stock remanente
            string productoSeleccionado = cmbNombre.Text;
            int cantidadVendida = acumuladorCantidad;

            // Uso de bloque using para garantizar el cierre automático de la conexión ante cualquier excepción interna
            using (SqlConnection conexion = new SqlConnection(Conexion.Cadena))
            {
                conexion.Open();

                // Consulta inicial para validar las existencias del artículo seleccionado antes de alterarlo
                string querySelect = "SELECT stock_actual FROM Productos WHERE nombre = @nom";
                SqlCommand cmdSelect = new SqlCommand(querySelect, conexion);
                cmdSelect.Parameters.AddWithValue("@nom", productoSeleccionado);

                object resultado = cmdSelect.ExecuteScalar();

                if (resultado != null)
                {
                    int stockActual = Convert.ToInt32(resultado);
                    int nuevoStock = stockActual - cantidadVendida;

                    // Validación: Impide que el stock de inventario se reduzca a números negativos
                    if (nuevoStock < 0)
                    {
                        MessageBox.Show("No hay suficiente stock.");
                        return;
                    }

                    // Consulta de actualización: Asigna el valor resultante de la resta al registro del producto
                    string queryUpdate = "UPDATE Productos SET stock_actual = @stock WHERE nombre = @nom";
                    SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexion);
                    cmdUpdate.Parameters.AddWithValue("@stock", nuevoStock);
                    cmdUpdate.Parameters.AddWithValue("@nom", productoSeleccionado);

                    cmdUpdate.ExecuteNonQuery();
                }
            }

            // Notificación de éxito detallando los datos clave resumidos de la transacción de venta completada
            MessageBox.Show(
                "Venta guardada con éxito.\n\n" +
                "ID Venta: " + nuevoId +
                "\nCantidad de Productos: " + acumuladorCantidad +
                "\nTotal Cobrado: S/. " + acumuladorTotal.ToString("N2") +
                "\nVuelto: " + lblVuelto.Text
            );

            // Reinicio de estados: Resetea los acumuladores a su valor por defecto para procesar una nueva venta limpia
            acumuladorTotal = 0;
            acumuladorCantidad = 0;

            // Limpieza visual completa de los controles e inputs del formulario
            txtCantidad.Clear();
            txtMonto.Clear();
            txtPagoCon.Clear();
            cmbProducto.SelectedIndex = -1;
            cmbNombre.SelectedIndex = -1;
        }

        /// <summary>
        /// Evento auxiliar para el control del cambio de texto en la cantidad. Sin comportamiento asignado.
        /// </summary>
        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        /// Agrega el producto seleccionado con su respectiva cantidad al carrito virtual, actualizando los acumuladores globales.
        /// </summary>
        private void bntAgregar_Click(object sender, EventArgs e)
        {
            // Validación: Controla que se haya seleccionado un elemento de la lista desplegable
            if (cmbProducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            // Validación: Controla que el cuadro de cantidad no contenga valores en blanco o nulos
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Ingrese una cantidad.");
                return;
            }

            int cantidadIngresada;

            // Validación: Controla que los datos ingresados en cantidad correspondan a un entero válido
            if (!int.TryParse(txtCantidad.Text, out cantidadIngresada))
            {
                MessageBox.Show("Cantidad inválida.");
                return;
            }

            // Conversión del precio unitario desde el control y cálculo aritmético del subtotal de la línea
            decimal precioUnitario = Convert.ToDecimal(txtMonto.Text);
            decimal subtotal = precioUnitario * cantidadIngresada;

            // Suma acumulativa a las variables de estado que controlan la venta global en progreso
            acumuladorTotal += subtotal;
            acumuladorCantidad += cantidadIngresada;

            MessageBox.Show(
                "Producto agregado.\n" +
                "Subtotal: S/. " + subtotal.ToString("N2") +
                "\nTotal acumulado: S/. " + acumuladorTotal.ToString("N2")
            );

            // Restablecimiento de los controles de selección rápida listos para un nuevo ingreso
            cmbProducto.SelectedIndex = -1;
            txtCantidad.Clear();
            txtMonto.Clear();
            cmbProducto.Focus();
        }

        /// <summary>
        /// Botón manual para consultar directamente el stock real de un producto por su nombre desde la interfaz de inventario.
        /// </summary>
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

        /// <summary>
        /// Añade un volumen extra de mercadería al stock actual de un artículo determinado y refresca el valor en pantalla.
        /// </summary>
        private void btnInvModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNombre.Text) || string.IsNullOrEmpty(txtNuevaCantidad.Text))
            {
                MessageBox.Show("Por favor, seleccione un producto e ingrese la cantidad a añadir.");
                return;
            }

            string producto = cmbNombre.Text;
            int cantidadAIngresar = Convert.ToInt32(txtNuevaCantidad.Text);

            // Ejecuta el proceso dinámico de reabastecimiento en la capa de datos
            ConexionInventario.SurtirMercaderia(producto, cantidadAIngresar);

            MessageBox.Show("¡Stock modificado con éxito! Se añadieron " + cantidadAIngresar + " unidades.");

            // Se vuelve a consultar la base de datos para mostrar el stock consolidado final
            int nuevoStock = ConexionInventario.ConsultarStockActual(producto);
            txtStock.Text = nuevoStock.ToString();
            txtNuevaCantidad.Clear();
        }

        /// <summary>
        /// Evento reactivo que actualiza el casillero de stock en tiempo real al seleccionar un ítem del buscador.
        /// </summary>
        private void cmbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbNombre.Text)) return;

            string productoSeleccionado = cmbNombre.Text;
            int stockReal = ConexionInventario.ConsultarStockActual(productoSeleccionado);

            txtStock.Text = stockReal.ToString();
        }

        /// <summary>
        /// Evento duplicado o alternativo para el control del ComboBox. Ejecuta una lectura escalar aislada del precio.
        /// </summary>
        private void cmbNombre_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            string query = "SELECT precio FROM Productos WHERE nombre = @prod";
            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@prod", cmbProducto.Text);

            object resultado = cmd.ExecuteScalar();
            // Nota: El valor recuperado aquí no se asigna a ningún control visual.
        }

        /// <summary>
        /// Carga el reporte total e histórico de las ventas ejecutadas en el contenedor DataGridView de reportes.
        /// </summary>
        private void btnReporte_Click(object sender, EventArgs e)
        {
            dgvReportes.DataSource = ConexionVentas.TraerReporte();
        }

        /// <summary>
        /// Extrae de la base de datos la lista completa de artículos junto a una columna calculada dinámicamente (CASE)
        /// que evalúa si el stock se encuentra por debajo de su límite permitido para alertar la reposición.
        /// </summary>
        private void btnVerProductos_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Consulta que evalúa lógicamente si (stock_actual <= stock_minimo + 1) para etiquetarlo como 'Reponer'
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

            // Se enlazan los resultados formateados al DataGridView y se auto-ajustan las columnas visualmente
            dgvReportes.DataSource = datos;
            dgvReportes.AutoResizeColumns();
        }

        /// <summary>
        /// Ejecuta el proceso de arqueo o cierre de caja sumando los importes financieros de todas las ventas del día corriente.
        /// </summary>
        private void btnCierre_Click(object sender, EventArgs e)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // DATEDIFF(day, fecha_venta, GETDATE()) = 0 asegura filtrar los registros cuya diferencia en días con hoy sea nula
            string query = @"SELECT ISNULL(SUM(total), 0) 
                         FROM Ventas 
                         WHERE DATEDIFF(day, fecha_venta, GETDATE()) = 0";

            SqlCommand cmd = new SqlCommand(query, conexion);
            decimal totalCobradoHoy = Convert.ToDecimal(cmd.ExecuteScalar());

            conexion.Close();

            // Despliegue de un cuadro informativo formal con los totales económicos calculados del día
            MessageBox.Show("=== CIERRE DE CAJA DIARIO ===\n\n" +
                            "Fecha: " + DateTime.Today.ToShortDateString() + "\n" +
                            "Total Efectivo de Hoy: S/. " + totalCobradoHoy.ToString("N2"),
                            "Cierre de Caja");
        }

        /// <summary>
        /// Consulta el top global de productos con mayor rotación e inserta su estructura estadística en la rejilla de datos.
        /// </summary>
        private void btnMasVendidos_Click(object sender, EventArgs e)
        {
            DataTable datosTop = ConexionesAdmin.ObtenerTopProductos();
            dgvReportes.DataSource = datosTop;
            dgvReportes.AutoResizeColumns();
        }

        /// <summary>
        /// Realiza un cálculo interactivo instantáneo y aislado del vuelto que le corresponde al cliente según el texto editado.
        /// </summary>
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

        /// <summary>
        /// Evento auxiliar para el cambio de texto del pago del cliente. Sin comportamiento asignado.
        /// </summary>
        private void txtPagoCon_TextChanged(object sender, EventArgs e)
        {
            return;
        }

        /// <summary>
        /// Realiza un borrado físico definitivo en cascada de un registro de venta.
        /// Elimina primero sus dependencias en DetalleVenta antes de remover la cabecera en Ventas.
        /// </summary>
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // 1. Validación rápida: Verifica que el campo contenedor del identificador a suprimir no esté vacío
            if (string.IsNullOrWhiteSpace(txtIdEliminar.Text))
            {
                MessageBox.Show("Escribe un ID para borrar.");
                return;
            }

            // 2. Conexión y ejecución directa a SQL
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Operación Preventiva: Se purga primero la relación foránea (DetalleVenta) para evadir restricciones de integridad referencial
            string query1 = "DELETE FROM DetalleVenta WHERE id_venta = @id";
            SqlCommand cmd1 = new SqlCommand(query1, conexion);
            cmd1.Parameters.AddWithValue("@id", txtIdEliminar.Text.Trim());
            cmd1.ExecuteNonQuery();

            // Operación Principal: Se remueve el registro matriz de la tabla Ventas una vez liberadas sus restricciones
            string query2 = "DELETE FROM Ventas WHERE id_venta = @id";
            SqlCommand cmd2 = new SqlCommand(query2, conexion);
            cmd2.Parameters.AddWithValue("@id", txtIdEliminar.Text.Trim());
            cmd2.ExecuteNonQuery();

            conexion.Close();
        }
    }