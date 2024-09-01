using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Departamento
{
    public class Depa_formModel : PageModel
    {
        [BindProperty]
        public DepartamentoInfo Departamento { get; set; }

        public void OnPost()
        {
            string connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";

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
        }
    }
}

