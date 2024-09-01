using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Empleado
{
    public class Empleado_formModel : PageModel
    {
        [BindProperty]
        public EmpleadoInfo Empleado { get; set; }

        public void OnPost()
        {
            string connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Empleado (cedula, nombre, apellido1, apellido2, telefono)
                    VALUES (@cedula, @nombre, @apellido1, @apellido2, @telefono)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cedula", Empleado.cedula);
                    command.Parameters.AddWithValue("@nombre", Empleado.nombre);
                    command.Parameters.AddWithValue("@apellido1", Empleado.apellido1);
                    command.Parameters.AddWithValue("@apellido2", Empleado.apellido2);
                    command.Parameters.AddWithValue("@telefono", Empleado.telefono);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

