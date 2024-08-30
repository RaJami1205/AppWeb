using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Proyectos
{
    public class Proyect_listModel : PageModel
    {
        public List<ProyectoInfo> listaProyectos = new List<ProyectoInfo>();

        public void OnGet()
        {
           
            string connectionString = "Data Source=.\\mysqlserver;Initial Catalog=GestionProyectosTareas;Persist Security Info=True;User ID=sa;Password=***********;Trust Server Certificate=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Proyecto";
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
                                Proyectoinfo.año = reader.GetInt32(4);
                                Proyectoinfo.trimestre = reader.GetInt32(5);
                                Proyectoinfo.fecha_inicio = reader.GetString(6);
                                Proyectoinfo.fecha_cierre = reader.GetString(7);
                                Proyectoinfo.codigoDep = reader.GetInt32(8);

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

}



