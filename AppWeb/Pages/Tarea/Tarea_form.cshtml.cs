using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Tarea
{
    public class Tarea_formModel : PageModel
    {
        [BindProperty]
        public TareaInfo Tarea { get; set; }

        private string connectionString;

        public Tarea_formModel()
        {
            connectionString = "Data Source=.\\mysqlserver;Initial Catalog=GestionProyectosTareas;Persist Security Info=True;User ID=sa;Password=***********;Encrypt=True;Trust Server Certificate=True";
        }

        public void OnPost()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Tarea (nombre_tarea, tipo, descripcion, cantidad_horas, nombre_proyecto)
                    VALUES (@nombre, @tipo, @descripcion, @horas, @nombre_proyecto)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nombre", Tarea.nombre);
                    command.Parameters.AddWithValue("@tipo", Tarea.tipo);
                    command.Parameters.AddWithValue("@descripcion", Tarea.descripcion);
                    command.Parameters.AddWithValue("@horas", Tarea.horas);
                    command.Parameters.AddWithValue("@nombre_proyecto", Tarea.nombre_proyecto);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public class TareaInfo
    {
        public string nombre { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public int horas { get; set; }
        public string nombre_proyecto { get; set; }
    }
}

