using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Departamento
{
    public class Depa_formModel : PageModel
    {
        [BindProperty]
        public DepartamentoInfo Departamento { get; set; }

        private string connectionString;

        public Depa_formModel()
        {
            connectionString = "Data Source=.\\mysqlserver;Initial Catalog=GestionProyectosTareas;Persist Security Info=True;User ID=sa;Password=***********;Trust Server Certificate=True";
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es v�lido, retorna a la misma p�gina.
                return Page();
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Departamento (codigo, nombre, cedula_jefe)
                    VALUES (@codigo, @nombre, @cedula_jefe)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@codigo", Departamento.codigo);
                    command.Parameters.AddWithValue("@nombre", Departamento.nombre);
                    command.Parameters.AddWithValue("@cedula_jefe", Departamento.cedula_jefe);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Redirige a otra p�gina despu�s de la inserci�n si es necesario.
            return RedirectToPage("Success"); // Cambia "Success" por la p�gina deseada.
        }
    }

    public class DepartamentoInfo
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public int cedula_jefe { get; set; }
    }
}

