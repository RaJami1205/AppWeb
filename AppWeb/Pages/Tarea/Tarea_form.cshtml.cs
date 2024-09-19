using GestorProyectos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Tarea
{
    public class Tarea_formModel : PageModel
    {
        [BindProperty]
        public TareaInfo Tarea { get; set; } = new TareaInfo();
        public List<string> listaProyectos = new List<string>();
        public string mensaje_error = "";
        public string mensaje_exito = "";
        public Conexion conexionBD = new Conexion();

        public void OnPost()
        {

            Tarea.nombre_tarea = Request.Form["nombre_tarea"];
            Tarea.tipo = Request.Form["tipo"];
            Tarea.descripcion = Request.Form["descripcion"];
            Tarea.cantidad_horas = Request.Form["cantidad_horas"];
            Tarea.nombre_proyecto = Request.Form["nombre_proyecto"];

            try
            {
                string query = @"
                    INSERT INTO Tarea (nombre_tarea, tipo, descripcion, cantidad_horas, nombre_proyecto)
                    VALUES (@nombre_tarea, @tipo, @descripcion, @cantidad_horas, @nombre_proyecto)";

                SqlCommand command = conexionBD.obtenerComando(query);
                command.Parameters.AddWithValue("@nombre_tarea", Tarea.nombre_tarea);
                command.Parameters.AddWithValue("@tipo", Tarea.tipo);
                command.Parameters.AddWithValue("@descripcion", Tarea.descripcion);
                command.Parameters.AddWithValue("@cantidad_horas", Tarea.cantidad_horas);
                command.Parameters.AddWithValue("@nombre_proyecto", Tarea.nombre_proyecto);

                conexionBD.abrir();
                command.ExecuteNonQuery();
                    

                // Limpieza del formulario
                Tarea.nombre_proyecto = "";
                Tarea.tipo = "";
                Tarea.descripcion = "";
                Tarea.cantidad_horas = "";
                Tarea.nombre_proyecto = "";

                mensaje_exito = "Actividad registrada exitosamente";
            }
            catch (Exception ex)
            {
                mensaje_error = ex.Message;
                OnGet(); // Recargar datos si hay error
            }
        }
        public void OnGet()
        {
            conexionBD.abrir();
            string sqlEmpleados = "SELECT * FROM Proyecto";
            SqlCommand command= conexionBD.obtenerComando(sqlEmpleados);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listaProyectos.Add(reader.GetString(0));
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
}
