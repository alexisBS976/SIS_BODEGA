using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos del programa

namespace SIS_BODEGA
{
    public class ConexionVentas
    {
        // Realiza en automatico los ID
        public static string ObtenerSiguienteId()
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Ventas";
            SqlCommand cmd = new SqlCommand(query, conexion);
            int cantidadFilas = Convert.ToInt32(cmd.ExecuteScalar());

            conexion.Close();
            return "V" + (cantidadFilas + 1);
        }

        // Guarda los totales acumulados de la venta en la base de datos
        public static void InsertarVenta(string id, decimal total, int cantidad)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            string query = "INSERT INTO Ventas (id_venta, fecha_venta, total, cantidad_productos) VALUES (@id, GETDATE(), @total, @cantidad)";
            SqlCommand cmd = new SqlCommand(query, conexion);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.Parameters.AddWithValue("@cantidad", cantidad);

            cmd.ExecuteNonQuery();
            conexion.Close();
        }

        // Trae toda la tabla de ventas para el reporte
        public static DataTable TraerReporte()
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();
            string query = "SELECT id_venta, fecha_venta, total, cantidad_productos FROM Ventas";
            SqlCommand cmd = new SqlCommand(query, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);

            conexion.Close();
            return tabla;
        }
    }
}