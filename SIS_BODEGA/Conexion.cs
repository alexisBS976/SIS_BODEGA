using System;
using System.Collections.Generic;
using System.Text;

namespace SIS_BODEGA
{
    public class Conexion
    {
        /// <summary>
        /// Clase centralizada para gestionar la conexión a la base de datos local.
        /// Al estar en una clase aparte, evitamos repetir la cadena de conexión en cada formulario.
        /// </summary>
        public static string Cadena =
       @"Data Source=(LocalDB)\MSSQLLocalDB;
        AttachDbFilename=|DataDirectory|\BD_BodeguitaKevin.mdf;
        Integrated Security=True;
        Connect Timeout=30;";
    }
}
