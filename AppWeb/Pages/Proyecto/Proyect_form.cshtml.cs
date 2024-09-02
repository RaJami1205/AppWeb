using AppWeb.Pages.Departamento;
using Gestor_de_inventario_Super_Los_Patitos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppWeb.Pages.Proyectos
{
    public class Proyect_formModel : PageModel
    {
        [BindProperty]
        public ProyectoInfo Proyecto { get; set; }
        public List<string> listaCodigosDep = new List<string>();
        public ProyectoInfo proyeForm = new ProyectoInfo();
        public string mensaje_error = "";
        public string mensaje_exito = "El proyecto fue añadido exitosamente";
        public Conexion conexionBD= new Conexion();

        public void OnPost()
        {
            proyeForm.nombre_proyecto = Request.Form["nombre_proyecto"];
            proyeForm.nombre_portafolio = Request.Form["nombre_portafolio"];
            proyeForm.año = Request.Form["año"];
            proyeForm.trimestre = Request.Form["trimestre"];
            proyeForm.descripcion = Request.Form["descripcion"];
            proyeForm.tipo = Request.Form["tipo"];
            proyeForm.fecha_inicio = Request.Form["fecha_inicio"];
            proyeForm.fecha_cierre = Request.Form["fecha_cierre"];
            proyeForm.codigoDep = Request.Form["codigoDep"];

            try
            {
                string query = @"
                    INSERT INTO Proyecto (nombre_proyecto, nombre_portafolio, descripcion, tipo, año, trimestre, fecha_inicio, fecha_cierre, codigoDep)
                    VALUES (@nombre_proyecto, @nombre_portafolio, @descripcion, @tipo, @año, @trimestre, @fecha_inicio, @fecha_cierre, @codigoDep)";

                SqlCommand command=conexionBD.obtenerComando(query);

                command.Parameters.AddWithValue("@nombre_proyecto", proyeForm.nombre_proyecto);
                command.Parameters.AddWithValue("@nombre_portafolio", proyeForm.nombre_portafolio);
                command.Parameters.AddWithValue("@descripcion", proyeForm.descripcion);
                command.Parameters.AddWithValue("@tipo", proyeForm.tipo);
                command.Parameters.AddWithValue("@año", proyeForm.año);
                command.Parameters.AddWithValue("@trimestre", proyeForm.trimestre);
                command.Parameters.AddWithValue("@fecha_inicio", proyeForm.fecha_inicio);
                command.Parameters.AddWithValue("@fecha_cierre", proyeForm.fecha_cierre);
                command.Parameters.AddWithValue("@codigoDep", proyeForm.codigoDep);

                conexionBD.abrir();
                command.ExecuteNonQuery();
            
                proyeForm.nombre_proyecto = "";
                proyeForm.nombre_portafolio = "";
                proyeForm.descripcion = "";
                proyeForm.tipo = "";
                proyeForm.año = "";
                proyeForm.trimestre = "";
                proyeForm.fecha_inicio = "";
                proyeForm.fecha_cierre = "";
                proyeForm.codigoDep = "";
                mensaje_exito = "Proyecto añadido exitosamente";
            }
            catch (Exception ex)
            {
                mensaje_error = ex.Message;
                OnGet();
            }
        }

        public void OnGet()
        {
            conexionBD.abrir();
            string sqlDepartamentos = "SELECT codigoDep FROM Departamento";
            SqlCommand command= conexionBD.obtenerComando(sqlDepartamentos);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    DepartamentoInfo departamento = new DepartamentoInfo();
                    listaCodigosDep.Add("" + reader.GetInt32(0));
                }
            }
        }

        public class DepartamentoInfo
        {
            public string codigo { get; set; }
        }
    }
}