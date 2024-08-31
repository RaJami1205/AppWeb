using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppWeb.Pages.Proyectos
{
    public class Proyect_formModel : PageModel
    {
        [BindProperty]
        public ProyectoInfo Proyecto { get; set; }

        private string connectionString;

        public Proyect_formModel()
        {
            connectionString = "Data Source=.\\mysqlserver;Initial Catalog=GestionProyectosTareas;Persist Security Info=True;User ID=sa;Password=***********;Encrypt=True;Trust Server Certificate=True";
        }

        public void OnPost()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Proyecto (nombre_proyecto, nombre_portafolio, descripcion, tipo, año, trimestre, fecha_inicio, fecha_cierre, codigoDep)
                    VALUES (@nombre_proyecto, @nombre_portafolio, @descripcion, @tipo, @año, @trimestre, @fecha_inicio, @fecha_cierre, @codigoDep)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre_proyecto", Proyecto.nombre_proyecto);
                    command.Parameters.AddWithValue("@nombre_portafolio", Proyecto.nombre_portafolio);
                    command.Parameters.AddWithValue("@descripcion", Proyecto.descripcion);
                    command.Parameters.AddWithValue("@tipo", Proyecto.tipo);
                    command.Parameters.AddWithValue("@año", Proyecto.año);
                    command.Parameters.AddWithValue("@trimestre", Proyecto.trimestre);
                    command.Parameters.AddWithValue("@fecha_inicio", Proyecto.fecha_inicio);
                    command.Parameters.AddWithValue("@fecha_cierre", Proyecto.fecha_cierre);
                    command.Parameters.AddWithValue("@codigoDep", Proyecto.codigoDep);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}