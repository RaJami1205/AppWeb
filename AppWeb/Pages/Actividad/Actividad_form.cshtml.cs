using AppWeb.Pages.Proyectos;
using GestorProyectos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace AppWeb.Pages.Tarea
{
    public class Actividad_formModel : PageModel
    {
        [BindProperty]
        public ActividadInfo Actividad { get; set; } = new ActividadInfo();
        public List<string> listaEmpleados = new List<string>();
        public List<string> listaTareas = new List<string>();
        public string mensaje_error = "";
        public string mensaje_exito = "";
        public Conexion conexionBD = new Conexion();

        public void OnPost()
        {

            Actividad.fecha_hora_inicio = Request.Form["fecha_hora_inicio"];
            Actividad.fecha_hora_final = Request.Form["fecha_hora_final"];
            Actividad.horas = Request.Form["horas"];
            Actividad.tipo = Request.Form["tipo"];
            Actividad.etapa = Request.Form["etapa"];
            Actividad.nombre_tarea = Request.Form["nombre_tarea"];
            Actividad.cedula_empleado = Request.Form["cedula_empleado"];

            try
            {
                conexionBD.abrir();
                string query = @"
                    INSERT INTO Actividad (fecha_hora_inicio, fecha_hora_final, horas, tipo, etapa, nombre_tarea, cedula_empleado)
                    VALUES (@fecha_hora_inicio, @fecha_hora_final, @horas, @tipo, @etapa, @nombre_tarea, @cedula_empleado)";
                SqlCommand command = conexionBD.obtenerComando(query);
                command.Parameters.AddWithValue("@fecha_hora_inicio", Actividad.fecha_hora_inicio);
                command.Parameters.AddWithValue("@fecha_hora_final", Actividad.fecha_hora_final);
                command.Parameters.AddWithValue("@horas", Actividad.horas);
                command.Parameters.AddWithValue("@tipo", Actividad.tipo);
                command.Parameters.AddWithValue("@etapa", Actividad.etapa);
                command.Parameters.AddWithValue("@nombre_tarea", Actividad.nombre_tarea);
                command.Parameters.AddWithValue("@cedula_empleado", Actividad.cedula_empleado);

                command.ExecuteNonQuery();


                // Limpieza del formulario
                Actividad.cedula_empleado = "";
                Actividad.fecha_hora_inicio = "";
                Actividad.fecha_hora_final = "";
                Actividad.horas = "";
                Actividad.tipo = "";
                Actividad.etapa = "Testing";
                Actividad.nombre_tarea = "";
                Actividad.cedula_empleado = "";

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
            // Este método podría utilizarse para cargar la lista de empleados u otros datos que necesites
            conexionBD.abrir();
            string sqlTareas = "SELECT nombre_tarea FROM Tarea";
            SqlCommand command2 = conexionBD.obtenerComando(sqlTareas);
            using (SqlDataReader reader = command2.ExecuteReader())
            {
                while (reader.Read())
                {
                    listaTareas.Add(reader.GetString(0)); // Supongo que la primera columna es el ID del empleado
                }
            }

            // Este método podría utilizarse para cargar la lista de empleados u otros datos que necesites
            conexionBD.abrir();
            string sqlEmpleados = "SELECT cedula FROM Empleado"; // Ejemplo de consulta
            SqlCommand command1 = conexionBD.obtenerComando(sqlEmpleados);
            using (SqlDataReader reader = command1.ExecuteReader())
            {
                while (reader.Read())
                {
                    listaEmpleados.Add("" + reader.GetInt32(0)); // Supongo que la primera columna es el ID del empleado
                }
            }
        }

        public class ActividadInfo
        {
            public string fecha_hora_inicio { get; set; }
            public string fecha_hora_final { get; set; }
            public string horas { get; set; }
            public string tipo { get; set; }
            public string etapa { get; set; } = "Testing"; // Valor por defecto
            public string nombre_tarea { get; set; }
            public string cedula_empleado { get; set; }
        }
    }
}