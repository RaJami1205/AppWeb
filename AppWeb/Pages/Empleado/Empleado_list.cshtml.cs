using Gestor_de_inventario_Super_Los_Patitos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Empleado
{
    public class Empleado_listModel : PageModel
    {
        public List<EmpleadoInfo> listaEmpleados = new List<EmpleadoInfo>();
        public Conexion conexionBD = new Conexion();

        public void OnGet()
        {
            try
            {
                conexionBD.abrir();
                string sql = "SELECT cedula, nombre, apellido1, apellido2, telefono FROM Empleado";
                SqlCommand command = conexionBD.obtenerComando(sql);
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

    /*Los empleados pueden estar en muchos proyectos pero en este caso se eligio que pusieramos persona
      por proyecto, que es como tener una foreign key de proyecto en empleado solo que el empleado puede
      ser parte de varios proyectos como lo indica en el documento*/
    public class EmpleadoProyecto
    {
        public string cedula_empleado { get; set; }
        public string nombre_proyecto { get; set; }
    }
}

