using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos del programa   

namespace SIS_BODEGA
{
    public class ConexionReportes
    {
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

        public static DataTable DetalleReporte()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conn = new SqlConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT
                            id_detalle,
                            id_venta,
                            id_producto,
                            cantidad,
                            subtotal
                         FROM DetalleVenta";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(tabla);
                }
            }

            return tabla;
        }
    }
}
