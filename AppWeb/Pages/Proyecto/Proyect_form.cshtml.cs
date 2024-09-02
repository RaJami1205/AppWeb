using AppWeb.Pages.Departamento;
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
        public List<string> listaCodigosDep = new List<string>();

        private string connectionString;

        public Proyect_formModel()
        {
            connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
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
                string sqlDepartamentos = "SELECT * FROM Departamento";
                using (SqlCommand command = new SqlCommand(sqlDepartamentos, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DepartamentoInfo departamento = new DepartamentoInfo();
                            listaCodigosDep.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }
    }
    public class DepartamentoInfo
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string cedula_jefe { get; set; }
    }
}