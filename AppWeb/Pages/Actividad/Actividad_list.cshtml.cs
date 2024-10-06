using AppWeb.Pages.Proyectos;
using GestorProyectos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Tarea
{
    public class Actividad_listModel : PageModel
    {
        public List<ActividadInfo> listaActividades = new List<ActividadInfo>();
        public Conexion conexionBD = new Conexion();

        public void OnGet()
        {
            try
            {
                conexionBD.abrir();
                String sql = "SELECT fecha_hora_inicio, fecha_hora_final, horas, tipo, etapa, nombre_tarea, cedula_empleado FROM Actividad";
                SqlCommand command = conexionBD.obtenerComando(sql);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        ActividadInfo actividad = new ActividadInfo();
                        actividad.fecha_hora_inicio = "" + reader.GetDateTime(0).ToString();
                        actividad.fecha_hora_final = "" + reader.GetDateTime(1).ToString();
                        actividad.horas = "" + reader.GetInt32(2);
                        actividad.tipo = reader.GetString(3);
                        actividad.etapa = reader.GetString(4);
                        actividad.nombre_tarea = reader.GetString(5);
                        actividad.cedulaEmpleado = ""+reader.GetInt32(6);

                        listaActividades.Add(actividad);
                    }
                    conexionBD.cerrar();
                }
            }
            catch (Exception ex)
            {
                // Aquí se maneja el error
                Console.WriteLine("Error: " + ex.Message);
                conexionBD.cerrar();
            }
        }
    }

    public class ActividadInfo
    {
        public string fecha_hora_inicio { get; set; }
        public string fecha_hora_final { get; set; }
        public string horas { get; set; }
        public string tipo { get; set; }
        public string etapa { get; set; }
        public string nombre_tarea { get; set; }
        public string cedulaEmpleado { get; set; }
    }
}
