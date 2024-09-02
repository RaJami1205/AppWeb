using AppWeb.Pages.Proyectos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Actividad
{
    public class Actividad_formModel : PageModel
    {
        [BindProperty]
        public ActividadInfo Actividad { get; set; } = new ActividadInfo();
        public List<string> listaEmpleados = new List<string>();
        public string mensaje_error = "";
        public string mensaje_exito = "La actividad fue registrada exitosamente";

        private string connectionString;

        public Actividad_formModel()
        {
            connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
        }

        public void OnPost()
        {
            Actividad.empleado = Request.Form["empleado"];
            Actividad.fechaHora_inicio = Request.Form["fechaHora_inicio"];
            Actividad.fechaHora_final = Request.Form["fechaHora_final"];
            Actividad.hora = Request.Form["hora"];
            Actividad.tipo = Request.Form["tipo"];
            Actividad.etapa = Request.Form["etapa"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        INSERT INTO Actividad (empleado, fechaHora_inicio, fechaHora_final, hora, tipo, etapa)
                        VALUES (@empleado, @fechaHora_inicio, @fechaHora_final, @hora, @tipo, @etapa)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@empleado", Actividad.empleado);
                        command.Parameters.AddWithValue("@fechaHora_inicio", Actividad.fechaHora_inicio);
                        command.Parameters.AddWithValue("@fechaHora_final", Actividad.fechaHora_final);
                        command.Parameters.AddWithValue("@hora", Actividad.hora);
                        command.Parameters.AddWithValue("@tipo", Actividad.tipo);
                        command.Parameters.AddWithValue("@etapa", Actividad.etapa);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Limpieza del formulario
                    Actividad.empleado = "";
                    Actividad.fechaHora_inicio = "";
                    Actividad.fechaHora_final = "";
                    Actividad.hora = "";
                    Actividad.tipo = "";
                    Actividad.etapa = "Testing";  // Valor por defecto

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
            // Este método podría utilizarse para cargar la lista de empleados u otros datos que necesites
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlEmpleados = "SELECT * FROM Empleado"; // Ejemplo de consulta
                using (SqlCommand command = new SqlCommand(sqlEmpleados, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaEmpleados.Add(reader.GetString(0)); // Supongo que la primera columna es el ID del empleado
                        }
                    }
                }
            }
        }

        public class ActividadInfo
        {
            public string empleado { get; set; }
            public string fechaHora_inicio { get; set; }
            public string fechaHora_final { get; set; }
            public string hora { get; set; }
            public string tipo { get; set; }
            public string etapa { get; set; } = "Testing"; // Valor por defecto
        }
    }
}
