using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos local

namespace SIS_BODEGA
{
    public class ConexionVentas
    {
        /// <summary>
        /// Genera de forma automática el identificador consecutivo para la siguiente venta.
        /// Cuenta los registros actuales y le suma 1, anteponiendo la letra 'V'.
        /// </summary>
        /// <returns>Una cadena de texto con el formato "V" seguido del número correlativo (ej. "V15").</returns>
        public static string ObtenerSiguienteId()
        {
            // Se inicializa y abre la conexión con la base de datos
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Consulta SQL para contar la cantidad total de registros en la tabla Ventas
            string query = "SELECT COUNT(*) FROM Ventas";
            SqlCommand cmd = new SqlCommand(query, conexion);

            // ExecuteScalar devuelve un objeto con la primera columna de la primera fila; se convierte a entero
            int cantidadFilas = Convert.ToInt32(cmd.ExecuteScalar());

            // Se cierra la conexión para liberar recursos
            conexion.Close();

            // Retorna el prefijo estático concatenado con el número incremental calculado
            return "V" + (cantidadFilas + 1);
        }

        /// <summary>
        /// Inserta un nuevo registro de venta con sus totales acumulados en la base de datos.
        /// </summary>
        public static void InsertarVenta(string id, decimal total, int cantidad)
        {
            // Se establece y abre la conexión a la base de datos
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Sentencia SQL de inserción utilizando GETDATE() para asignar la fecha y hora actual del servidor
            string query = "INSERT INTO Ventas (id_venta, fecha_venta, total, cantidad_productos) VALUES (@id, GETDATE(), @total, @cantidad)";
            SqlCommand cmd = new SqlCommand(query, conexion);

            // Definición de parámetros para evitar la inyección de código SQL malicioso y asegurar los tipos de datos
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@cantidad", cantidad);

            // Ejecuta la sentencia INSERT que altera los datos de la tabla sin retornar filas
            cmd.ExecuteNonQuery();

            // Cierre de la conexión
            conexion.Close();
        }

        /// <summary>
        /// Obtiene todos los registros de la tabla Ventas estructurados para la generación de reportes.
        /// </summary>
        /// <returns>Un objeto DataTable que contiene el listado histórico de todas las ventas.</returns>
        public static DataTable TraerReporte()
        {
            // Se inicializa y abre la conexión a la base de datos
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Consulta SQL para extraer los campos requeridos en el reporte
            string query = "SELECT id_venta, fecha_venta, total, cantidad_productos FROM Ventas";
            SqlCommand cmd = new SqlCommand(query, conexion);

            // Se utiliza SqlDataAdapter como intermediario para extraer y formatear los datos de la consulta
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();

            // Se rellena el contenedor DataTable en memoria con la información recolectada por el adaptador
            adaptador.Fill(tabla);

            // Se finaliza la conexión a la base de datos
            conexion.Close();

            return tabla;
        }

        /// <summary>
        /// Consulta el stock actual disponible de un producto específico mediante su nombre.
        /// </summary>
        /// <param name="nombreProducto">Nombre del producto a consultar.</param>
        /// <returns>La cantidad de unidades disponibles en stock. Retorna -1 si el producto no existe.</returns>
        public static int ObtenerStockProducto(string nombreProducto)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.Cadena))
            {
                conexion.Open();
                string query = "SELECT stock_actual FROM Productos WHERE nombre = @nom";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@nom", nombreProducto);
                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        return Convert.ToInt32(resultado);
                    }
                }
            }
            return -1;
        }
    }
}