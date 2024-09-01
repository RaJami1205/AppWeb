using AppWeb.Pages.Proyectos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Actividad
{
    public class Actividad_formModel : PageModel
    {
        public ActividadInfo actividad { get; set; }
    

        public void OnPost()
        {
            string connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO Actividad (fecha_hora_inicio, fecha_hora_final, horas, tipo, etapa, nombre_tarea, cedula_empleado)
                    VALUES (@fecha_hora_inicio, @fecha_hora_final, @horas, @tipo, @etapa, @nombre_tarea, @cedula_empleado)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fecha_hora_inicio",actividad.fecha_hora_inicio);
                    command.Parameters.AddWithValue("@fecha_hora_final", actividad.fecha_hora_final);
                    command.Parameters.AddWithValue("@horas", actividad.horas);
                    command.Parameters.AddWithValue("@tipo", actividad.tipo);
                    command.Parameters.AddWithValue("@etapa", actividad.etapa);
                    command.Parameters.AddWithValue("@nombre_tarea", actividad.nombre_tarea);
                    command.Parameters.AddWithValue("@cedula_empleado", actividad.cedulaEmpleado);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
