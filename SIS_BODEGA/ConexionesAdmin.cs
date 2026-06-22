using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos del programa

namespace SIS_BODEGA
{
    public class ConexionesAdmin
    {
        // OBTENER EL CIERRE DE CAJA DEL DÍA
        public static decimal ObtenerVentasDelDia()
        {
            decimal totalHoy = 0;
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Suma el total de las ventas realizadas en la fecha de hoy
            // NOTA: Ajusta 'total' y 'fecha' si tus columnas se llaman distinto en la tabla Ventas
            string query = "SELECT ISNULL(SUM(total), 0) FROM Ventas WHERE CAST(fecha AS DATE) = CAST(GETDATE() AS DATE)";

            SqlCommand cmd = new SqlCommand(query, conexion);
            totalHoy = Convert.ToDecimal(cmd.ExecuteScalar());

            conexion.Close();
            return totalHoy;
        }

        // OBTENER EL TOP DE PRODUCTOS VENDIDOS
        public static DataTable ObtenerTopProductos()
        {
            DataTable tabla = new DataTable();
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Consulta corregida con INNER JOIN explícito para que reconozca 'p' y 'v'
            string query = @"SELECT TOP 5 
                        v.id_producto AS [Código Producto], 
                        p.nombre AS [Nombre del Producto], 
                        SUM(cantidad) AS [Total Unidades Vendidas]
                     FROM DetalleVenta v
                     INNER JOIN Productos p ON v.id_producto = p.id_producto
                     GROUP BY v.id_producto, p.nombre
                     ORDER BY SUM(cantidad) DESC";

            SqlCommand cmd = new SqlCommand(query, conexion);
            SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
            adaptador.Fill(tabla);

            conexion.Close();
            return tabla;
        }

    }
}
