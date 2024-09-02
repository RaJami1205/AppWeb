﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Gestor_de_inventario_Super_Los_Patitos
{
    public class Conexion
    {
        string cadena = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";

        public SqlConnection ConectarBD = new SqlConnection();

        public Conexion()
        {
            ConectarBD.ConnectionString = cadena;

        }

        public void abrir()
        {
            try
            {
                ConectarBD.Open();
                Console.WriteLine("Conexion abierta");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: No se pudo abrir la BD" + ex.Message);
            }
        }

        public void cerrar()
        {
            ConectarBD.Close();
        }
    }
}
