using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppWeb.Pages.Proyectos
{
    public class Proyect_listModel : PageModel
    {
        public List<ProyectoInfo> listaProyectos = new List<ProyectoInfo>();
        public void OnGet()
        {

            string connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT nombre_proyecto, nombre_portafolio, descripcion, tipo, año, trimestre, fecha_inicio, fecha_cierre, codigoDep FROM Proyecto";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                             
                                ProyectoInfo Proyectoinfo = new ProyectoInfo();
                                Proyectoinfo.nombre_proyecto = reader.GetString(0);
                                Proyectoinfo.nombre_portafolio = reader.GetString(1);
                                Proyectoinfo.descripcion = reader.GetString(2);
                                Proyectoinfo.tipo = reader.GetString(3);
                                Proyectoinfo.año = "" + reader.GetInt32(4);
                                Proyectoinfo.trimestre = "" + reader.GetInt32(5);
                                Proyectoinfo.fecha_inicio = "" + reader.GetDateTime(6).ToString();
                                Proyectoinfo.fecha_cierre = "" + reader.GetDateTime(7).ToString();
                                Proyectoinfo.codigoDep = "" + reader.GetInt32(8);

                                listaProyectos.Add(Proyectoinfo);
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

    public class ProyectoInfo
    {
        public string nombre_proyecto { get; set; }
        public string nombre_portafolio { get; set; }
        public string descripcion { get; set; }
        public string tipo { get; set; }
        public string año { get; set; }
        public string trimestre { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_cierre { get; set; }
        public string codigoDep { get; set; }
    }
}