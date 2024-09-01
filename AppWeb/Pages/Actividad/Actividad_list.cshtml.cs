using AppWeb.Pages.Proyectos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Actividad
{
    public class Actividad_listModel : PageModel
    {
        public List<ActividadInfo> listaActividades = new List<ActividadInfo>();

        public void OnGet()
        {
            string connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Actividad";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

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
