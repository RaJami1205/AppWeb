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
                    command.Parameters.AddWithValue("@nombre", Tarea.nombre_tarea);
                    command.Parameters.AddWithValue("@tipo", Tarea.tipo);
                    command.Parameters.AddWithValue("@descripcion", Tarea.descripcion);
                    command.Parameters.AddWithValue("@horas", Tarea.cantidad_horas);
                    command.Parameters.AddWithValue("@nombre_proyecto", Tarea.nombre_proyecto);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}