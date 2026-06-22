using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Data.SqlClient; // Usar Microsoft.Data.SqlClient para usar la base de datos del programa

namespace SIS_BODEGA
{
    public class ConexionInventario
    {
        public static int ConsultarStockActual(string nombreProducto)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();
            string query = "SELECT * FROM Productos WHERE nombre = @nom";
            SqlCommand cmd = new SqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@nom", nombreProducto);

            SqlDataReader lector = cmd.ExecuteReader();
            int stock = 0;

            if (lector.Read())
            {
                stock = Convert.ToInt32(lector[3]);
            }

            lector.Close();
            conexion.Close();
            return stock;
        }

        public static void SurtirMercaderia(string nombreProducto, int cantidadNueva)
        {
            SqlConnection conexion = new SqlConnection(Conexion.Cadena);
            conexion.Open();

            // Primero leemos la fila para saber el stock actual y cómo se llama la columna mágicamente
            string querySelect = "SELECT * FROM Productos WHERE nombre = @nom";
            SqlCommand cmdSelect = new SqlCommand(querySelect, conexion);
            cmdSelect.Parameters.AddWithValue("@nom", nombreProducto);

            SqlDataReader lector = cmdSelect.ExecuteReader();
            int stockActual = 0;
            string nombreColumnaCantidad = "";

            if (lector.Read())
            {
                int posicionColumna = 3; 
                stockActual = Convert.ToInt32(lector[posicionColumna]);
                nombreColumnaCantidad = lector.GetName(posicionColumna); // Descubre el nombre real 
            }
            lector.Close();

            // Si descubrió la columna, guarda la suma total de forma segura
            if (!string.IsNullOrEmpty(nombreColumnaCantidad))
            {
                int nuevoStockTotal = stockActual + cantidadNueva;

                string queryUpdate = $"UPDATE Productos SET {nombreColumnaCantidad} = @nuevoStock WHERE nombre = @nom";
                SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conexion);
                cmdUpdate.Parameters.AddWithValue("@nuevoStock", nuevoStockTotal);
                cmdUpdate.Parameters.AddWithValue("@nom", nombreProducto);

                cmdUpdate.ExecuteNonQuery();
            }

            conexion.Close();
        }
    }

}