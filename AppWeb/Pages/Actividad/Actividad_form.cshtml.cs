using AppWeb.Pages.Proyectos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Tarea
{
    public class Actividad_formModel : PageModel
    {
        [BindProperty]
        public ActividadInfo Actividad { get; set; } = new ActividadInfo();
        public List<string> listaEmpleados = new List<string>();
        public List<string> listaTareas = new List<string>();
        public string mensaje_error = "";
        public string mensaje_exito = "";

        private string connectionString;

        public Actividad_formModel()
        {
            connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
        }

        public void OnPost()
        {

            Actividad.fecha_hora_inicio = Request.Form["fecha_hora_inicio"];
            Actividad.fecha_hora_final = Request.Form["fecha_hora_final"];
            Actividad.horas = Request.Form["horas"];
            Actividad.tipo = Request.Form["tipo"];
            Actividad.etapa = Request.Form["etapa"];
            Actividad.nombre_tarea = Request.Form["nombre_tarea"];
            Actividad.cedula_empleado = Request.Form["cedula_empleado"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        INSERT INTO Actividad (fecha_hora_inicio, fecha_hora_final, horas, tipo, etapa, nombre_tarea, cedula_empleado)
                        VALUES (@fecha_hora_inicio, @fecha_hora_final, @horas, @tipo, @etapa, @nombre_tarea, @cedula_empleado)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@fecha_hora_inicio", Actividad.fecha_hora_inicio);
                        command.Parameters.AddWithValue("@fecha_hora_final", Actividad.fecha_hora_final);
                        command.Parameters.AddWithValue("@horas", Actividad.horas);
                        command.Parameters.AddWithValue("@tipo", Actividad.tipo);
                        command.Parameters.AddWithValue("@etapa", Actividad.etapa);
                        command.Parameters.AddWithValue("@nombre_tarea", Actividad.nombre_tarea);
                        command.Parameters.AddWithValue("@cedula_empleado", Actividad.cedula_empleado);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Limpieza del formulario
                    Actividad.fecha_hora_inicio = "";
                    Actividad.fecha_hora_final = "";
                    Actividad.horas = "";
                    Actividad.tipo = "";
                    Actividad.etapa = "Testing";  // Valor por defecto
                    Actividad.cedula_empleado = "";
                    Actividad.nombre_tarea = "";

                    mensaje_exito = "Actividad registrada exitosamente";
                }
            }
            catch (Exception ex)
            {
                mensaje_error = ex.Message;
                OnGet(); // Recargar datos si hay error
            }
        }

        public void OnGet()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlEmpleados = "SELECT * FROM Empleado";
                using (SqlCommand command = new SqlCommand(sqlEmpleados, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaEmpleados.Add("" + reader.GetInt32(0));
                        }
                    }
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlEmpleados = "SELECT * FROM Tarea";
                using (SqlCommand command = new SqlCommand(sqlEmpleados, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaTareas.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }

        public class ActividadInfo
        {
            public string fecha_hora_inicio { get; set; }
            public string fecha_hora_final { get; set; }
            public string horas { get; set; }
            public string tipo { get; set; }
            public string etapa { get; set; } = "Testing"; // Valor por defecto
            public string nombre_tarea { get; set; }
            public string cedula_empleado { get; set; }
        }
    }
}