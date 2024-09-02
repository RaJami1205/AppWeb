using Gestor_de_inventario_Super_Los_Patitos;
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
        public Conexion conexionBD = new Conexion();

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
                // Insertar en la tabla Empleado
                string query1 = @"
                    INSERT INTO Empleado (cedula, nombre, apellido1, apellido2, telefono)
                    VALUES (@cedula, @nombre, @apellido1, @apellido2, @telefono)";

                SqlCommand command1= conexionBD.obtenerComando(query1);
                command1.Parameters.AddWithValue("@cedula", Empleado.cedula);
                command1.Parameters.AddWithValue("@nombre", Empleado.nombre);
                command1.Parameters.AddWithValue("@apellido1", Empleado.apellido1);
                command1.Parameters.AddWithValue("@apellido2", Empleado.apellido2);
                command1.Parameters.AddWithValue("@telefono", Empleado.telefono);

                conexionBD.abrir();
                command1.ExecuteNonQuery();

                // Insertar en la tabla EmpleadoProyecto
                string query2 = @"
                    INSERT INTO EmpleadoProyecto (cedula_empleado, nombre_proyecto)
                    VALUES (@cedula_empleado, @nombre_proyecto)";
                SqlCommand command2 = conexionBD.obtenerComando(query2);
                command2.Parameters.AddWithValue("@cedula_empleado", Empleado.cedula);
                command2.Parameters.AddWithValue("@nombre_proyecto", empleadoProyecto.nombre_proyecto);

                command2.ExecuteNonQuery();

                // Resetear los datos del formulario
                Empleado.cedula = "";
                Empleado.nombre = "";
                Empleado.apellido1 = "";
                Empleado.apellido2 = "";
                Empleado.telefono = "";
                empleadoProyecto.nombre_proyecto = "";
                mensaje_exito = "Empleado añadido exitosamente";
                
            }
            catch (Exception ex)
            {
                mensaje_error = ex.Message;
                OnGet(); // Recargar datos necesarios
            }
        }

        public void OnGet()
        {
            conexionBD.abrir();
            string sqlProyectos = "SELECT nombre_proyecto FROM Proyecto";
            SqlCommand command = conexionBD.obtenerComando(sqlProyectos);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listaNombresProyectos.Add(reader.GetString(0));
                }
            }
        }
    }

    public class ProyectoInfo
    {
        public string nombre_proyecto { get; set; }
    }
}

