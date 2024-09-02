using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Tarea
{
    public class Tarea_formModel : PageModel
    {
        [BindProperty]
        public TareaInfo Tarea { get; set; } = new TareaInfo();
        public List<string> listaProyectos = new List<string>();
        public string mensaje_error = "";
        public string mensaje_exito = "";

        private string connectionString;

        public Tarea_formModel()
        {
            connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
        }

        public void OnPost()
        {

            Tarea.nombre_tarea = Request.Form["nombre_tarea"];
            Tarea.tipo = Request.Form["tipo"];
            Tarea.descripcion = Request.Form["descripcion"];
            Tarea.cantidad_horas = Request.Form["cantidad_horas"];
            Tarea.nombre_proyecto = Request.Form["nombre_proyecto"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                        INSERT INTO Tarea (nombre_tarea, tipo, descripcion, cantidad_horas, nombre_proyecto)
                        VALUES (@nombre_tarea, @tipo, @descripcion, @cantidad_horas, @nombre_proyecto)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre_tarea", Tarea.nombre_tarea);
                        command.Parameters.AddWithValue("@tipo", Tarea.tipo);
                        command.Parameters.AddWithValue("@descripcion", Tarea.descripcion);
                        command.Parameters.AddWithValue("@cantidad_horas", Tarea.cantidad_horas);
                        command.Parameters.AddWithValue("@nombre_proyecto", Tarea.nombre_proyecto);
                        
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Limpieza del formulario
                    Tarea.nombre_proyecto = "";
                    Tarea.tipo = "";
                    Tarea.descripcion = "";
                    Tarea.cantidad_horas = "";
                    Tarea.nombre_proyecto = "";

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
                string sqlEmpleados = "SELECT * FROM Proyecto";
                using (SqlCommand command = new SqlCommand(sqlEmpleados, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaProyectos.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }

        public class TareaInfo
        {
            public string nombre_tarea { get; set; }
            public string tipo { get; set; }
            public string descripcion { get; set; }
            public string cantidad_horas { get; set; }
            public string nombre_proyecto { get; set; }
        }
    }
}