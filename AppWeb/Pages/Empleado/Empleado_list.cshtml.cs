using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Empleado
{
    public class Empleado_listModel : PageModel
    {
        public List<EmpleadoInfo> listaEmpleados = new List<EmpleadoInfo>();

        public void OnGet()
        {
            string connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Empleado";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmpleadoInfo empleado = new EmpleadoInfo();
                                empleado.cedula = "" + reader.GetInt32(0);
                                empleado.nombre = reader.GetString(1);
                                empleado.apellido1 = reader.GetString(2);
                                empleado.apellido2 = reader.GetString(3);
                                empleado.telefono = "" + reader.GetInt32(4);

                                listaEmpleados.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí se maneja el error
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    public class EmpleadoInfo
    {
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string telefono { get; set; }
    }
}

