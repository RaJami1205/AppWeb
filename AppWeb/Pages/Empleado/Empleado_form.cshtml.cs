using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppWeb.Pages.Empleado
{
    public class Empleado_formModel : PageModel
    {
        public EmpleadoInfo Empleado { get; set; } = new EmpleadoInfo();
        public EmpleadoProyecto empleadoProyecto { get; set; } = new EmpleadoProyecto();
        public List<string> listaNombresProyectos { get; set; } = new List<string>();
        public string mensaje_error = "";
        public string mensaje_exito = "El empleado fue añadido exitosamente";

        private string connectionString;

        public Empleado_formModel()
        {
            connectionString = "Data source=" + Environment.MachineName + "; Initial Catalog=GestionProyectosTareas; Integrated Security=True";
        }

        public void OnPost()
        {
            // Capturar datos del formulario
            Empleado.cedula = Request.Form["cedula"];
            Empleado.nombre = Request.Form["nombre"];
            Empleado.apellido1 = Request.Form["apellido1"];
            Empleado.apellido2 = Request.Form["apellido2"];
            Empleado.telefono = Request.Form["telefono"];
            empleadoProyecto.nombre_proyecto = Request.Form["nombre_proyecto"];

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Insertar en la tabla Empleado
                    string query1 = @"
                        INSERT INTO Empleado (cedula, nombre, apellido1, apellido2, telefono)
                        VALUES (@cedula, @nombre, @apellido1, @apellido2, @telefono)";

                    using (SqlCommand command1 = new SqlCommand(query1, connection))
                    {
                        command1.Parameters.AddWithValue("@cedula", Empleado.cedula);
                        command1.Parameters.AddWithValue("@nombre", Empleado.nombre);
                        command1.Parameters.AddWithValue("@apellido1", Empleado.apellido1);
                        command1.Parameters.AddWithValue("@apellido2", Empleado.apellido2);
                        command1.Parameters.AddWithValue("@telefono", Empleado.telefono);

                        connection.Open();
                        command1.ExecuteNonQuery();
                    }

                    // Insertar en la tabla EmpleadoProyecto
                    string query2 = @"
                        INSERT INTO EmpleadoProyecto (cedula_empleado, nombre_proyecto)
                        VALUES (@cedula_empleado, @nombre_proyecto)";

                    using (SqlCommand command2 = new SqlCommand(query2, connection))
                    {
                        command2.Parameters.AddWithValue("@cedula_empleado", Empleado.cedula);
                        command2.Parameters.AddWithValue("@nombre_proyecto", empleadoProyecto.nombre_proyecto);

                        command2.ExecuteNonQuery();
                    }

                    // Resetear los datos del formulario
                    Empleado.cedula = "";
                    Empleado.nombre = "";
                    Empleado.apellido1 = "";
                    Empleado.apellido2 = "";
                    Empleado.telefono = "";
                    empleadoProyecto.nombre_proyecto = "";
                    mensaje_exito = "Empleado añadido exitosamente";
                }
            }
            catch (Exception ex)
            {
                mensaje_error = ex.Message;
                OnGet(); // Recargar datos necesarios
            }
        }

        public void OnGet()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlProyectos = "SELECT nombre_proyecto FROM Proyecto";
                using (SqlCommand command = new SqlCommand(sqlProyectos, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaNombresProyectos.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }
    }

    public class ProyectoInfo
    {
        public string nombre_proyecto { get; set; }
    }
}

