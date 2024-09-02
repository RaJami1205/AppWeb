using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Departamento
{
    public class Depa_formModel : PageModel
    {
        [BindProperty]
        public DepartamentoInfo Departamento { get; set; } = new DepartamentoInfo();
        public List<string> listaCedulas = new List<string>();
        public string mensaje_error = "";
        public string mensaje_exito = "";

        public void OnPost()
        {
            Departamento.codigo = Request.Form["codigo"];
            Departamento.nombre = Request.Form["nombre"];
            Departamento.cedula_jefe = Request.Form["cedula_jefe"];
            string connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
            try
            {


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
                    Departamento.codigo = "";
                    Departamento.nombre = "";
                    Departamento.cedula_jefe = "";
                    mensaje_exito = "Departamento añadido exitosamente";
                }
            }
            catch (Exception ex)
            {
                mensaje_error=ex.Message;
                OnGet();
            }
        }
        public void OnGet()
        {
            string connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlProyectos = "SELECT cedula FROM Empleado";
                using (SqlCommand command = new SqlCommand(sqlProyectos, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaCedulas.Add(""+reader.GetInt32(0));
                        }
                    }
                }
            }
        }

        public class DepartamentoInfo
        {
            public string codigo { get; set; }
            public string nombre { get; set; }
            public string cedula_jefe { get; set; }
        }
    }
}

