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
        public ProyectoInfo proyeForm=new ProyectoInfo();
        public string mensaje_error="";
        public string mensaje_exito = "El proyecto fue añadido exitosamente";

        private string connectionString;

        public Proyect_formModel()
        {
            connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
        }

        public void OnPost()
        {
            proyeForm.nombre_proyecto = Request.Form["nombre_proyecto"];
            proyeForm.nombre_portafolio = Request.Form["nombre_portafolio"];
            proyeForm.descripcion = Request.Form["descripcion"];
            proyeForm.tipo = Request.Form["año"];
            proyeForm.trimestre = Request.Form["trimestre"];
            proyeForm.fecha_inicio = Request.Form["fecha_inicio"];
            proyeForm.fecha_cierre = Request.Form["fecha_cierre"];
            proyeForm.codigoDep = Request.Form["codigoDep"];

            if (proyeForm.nombre_proyecto.Length == 0 || proyeForm.nombre_portafolio.Length == 0 || proyeForm.descripcion.Length == 0 || proyeForm.año.Length == 0 || proyeForm.trimestre.Length == 0 || proyeForm.fecha_inicio.Length == 0 || proyeForm.fecha_cierre.Length == 0)
            {
                mensaje_error = "Todos los campos son requeridos";
                return;
            }

            try
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
                    proyeForm.nombre_proyecto = "";
                    proyeForm.nombre_portafolio = "";
                    proyeForm.descripcion = "";
                    proyeForm.tipo = "";
                    proyeForm.año = "";
                    proyeForm.trimestre = "";
                    proyeForm.fecha_inicio = "";
                    proyeForm.fecha_cierre = "";
                    proyeForm.codigoDep = "";
                    mensaje_exito = "Proyecto añadido exitosamente";
                }
            }
            catch (Exception ex)
            {
                mensaje_error=ex.Message;
                return;
            }
    }
    public class DepartamentoInfo
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string cedula_jefe { get; set; }
    }
}