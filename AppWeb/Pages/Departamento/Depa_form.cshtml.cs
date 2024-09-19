using GestorProyectos;
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
        public Conexion conexionBD = new Conexion();

        public void OnPost()
        {
            Departamento.codigo = Request.Form["codigo"];
            Departamento.nombre = Request.Form["nombre"];
            Departamento.cedula_jefe = Request.Form["cedula_jefe"];
            try
            {
                string query = @"
                    INSERT INTO Departamento (codigo, nombre, cedula_jefe)
                    VALUES (@codigo, @nombre, @cedula_jefe)";
                SqlCommand command = conexionBD.obtenerComando(query);
                command.Parameters.AddWithValue("@codigo", Departamento.codigo);
                command.Parameters.AddWithValue("@nombre", Departamento.nombre);
                command.Parameters.AddWithValue("@cedula_jefe", Departamento.cedula_jefe);

                conexionBD.abrir();
                command.ExecuteNonQuery();

                Departamento.codigo = "";
                Departamento.nombre = "";
                Departamento.cedula_jefe = "";
                mensaje_exito = "Departamento añadido exitosamente";
            }
            catch (Exception ex)
            {
                mensaje_error = ex.Message;
                OnGet();
            }
        }
        public void OnGet()
        {
            conexionBD.abrir();
            string sqlProyectos = "SELECT cedula FROM Empleado";
            SqlCommand command = conexionBD.obtenerComando(sqlProyectos);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listaCedulas.Add(""+reader.GetInt32(0));
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

