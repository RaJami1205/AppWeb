using AppWeb.Pages.Proyectos;
using GestorProyectos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Tarea
{
    public class Tarea_listModel : PageModel
    {
        public List<TareaInfo> listaTareas = new List<TareaInfo>();
        public Conexion conexionBD = new Conexion();
        public void OnGet()
        {
            try
            {
                conexionBD.abrir();
                string sql = "SELECT * FROM Tarea";
                SqlCommand command = conexionBD.obtenerComando(sql);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        TareaInfo Tarea = new TareaInfo();
                        Tarea.nombre_tarea = reader.GetString(0);
                        Tarea.tipo = reader.GetString(1);
                        Tarea.descripcion = reader.GetString(2);
                        Tarea.cantidad_horas = "" + reader.GetInt32(3);
                        Tarea.nombre_proyecto = reader.GetString(4);

                        listaTareas.Add(Tarea);
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
    public class TareaInfo
    {
        public string nombre_tarea { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public string cantidad_horas { get; set; }
        public string nombre_proyecto { get; set; }
    }
}
