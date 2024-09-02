using Gestor_de_inventario_Super_Los_Patitos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Tarea
{
    public class Tarea_formModel : PageModel
    {
        [BindProperty]
        public TareaInfo Tarea { get; set; }

        public Conexion conexionBD = new Conexion();

        public void OnPost()
        {
            string query = @"
                INSERT INTO Tarea (nombre_tarea, tipo, descripcion, cantidad_horas, nombre_proyecto)
                VALUES (@nombre, @tipo, @descripcion, @horas, @nombre_proyecto)";

            SqlCommand command=conexionBD.obtenerComando(query);
            command.Parameters.AddWithValue("@nombre", Tarea.nombre_tarea);
            command.Parameters.AddWithValue("@tipo", Tarea.tipo);
            command.Parameters.AddWithValue("@descripcion", Tarea.descripcion);
            command.Parameters.AddWithValue("@horas", Tarea.cantidad_horas);
            command.Parameters.AddWithValue("@nombre_proyecto", Tarea.nombre_proyecto);

            conexionBD.abrir();
            command.ExecuteNonQuery();
        }
    }
}