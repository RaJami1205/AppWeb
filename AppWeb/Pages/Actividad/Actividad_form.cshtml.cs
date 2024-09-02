using AppWeb.Pages.Proyectos;
using Gestor_de_inventario_Super_Los_Patitos;
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
        public string mensaje_exito = "La actividad fue registrada exitosamente";
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
                string query = @"
                    INSERT INTO Actividad (empleado, fechaHora_inicio, fechaHora_final, hora, tipo, etapa)
                    VALUES (@empleado, @fechaHora_inicio, @fechaHora_final, @hora, @tipo, @etapa)";
                SqlCommand command= new SqlCommand(query);
                command.Parameters.AddWithValue("@empleado", Actividad.empleado);
                command.Parameters.AddWithValue("@fechaHora_inicio", Actividad.fechaHora_inicio);
                command.Parameters.AddWithValue("@fechaHora_final", Actividad.fechaHora_final);
                command.Parameters.AddWithValue("@hora", Actividad.hora);
                command.Parameters.AddWithValue("@tipo", Actividad.tipo);
                command.Parameters.AddWithValue("@etapa", Actividad.etapa);

                conexionBD.abrir();
                command.ExecuteNonQuery();


                // Limpieza del formulario
                Actividad.cedula_empleado = "";
                Actividad.fecha_hora_inicio = "";
                Actividad.fecha_hora_final = "";
                Actividad.horas = "";
                Actividad.tipo = "";
                Actividad.etapa = "Testing";  // Valor por defecto

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
            string sqlEmpleados = "SELECT cedula FROM Empleado"; // Ejemplo de consulta
            SqlCommand command = conexionBD.obtenerComando(sqlEmpleados);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listaEmpleados.Add(reader.GetString(0)); // Supongo que la primera columna es el ID del empleado
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