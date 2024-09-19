using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using GestorProyectos;

namespace AppWeb.Pages.Seguimiento
{
    public class follow_activityModel : PageModel
    {
        public List<String> listaActividades = new List<String>();
        public Conexion conexionBD = new Conexion();

        public void OnGet()
        {
            conexionBD.abrir();
            string sqlEmpleados = "SELECT fecha_hora_inicio FROM Actividad";
            SqlCommand command = conexionBD.obtenerComando(sqlEmpleados);
            
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Seguimiento actividad = new Seguimiento();
                    actividad.fecha_hora_inicio = "" + reader.GetDateTime(0).ToString();
                    listaActividades.Add(actividad.fecha_hora_inicio);
                }
            }
            
            
        }
        public class Seguimiento
        {
            public string fecha_hora_inicio { get; set; }
            public string nombre_tarea { get; set; }
            public string cedula_empleado { get; set; }
        }
    }
}
