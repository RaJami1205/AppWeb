using Gestor_de_inventario_Super_Los_Patitos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AppWeb.Pages.Departamento
{
    public class Depa_listModel : PageModel
    {
        public List<DepartamentoInfo> listaDepartamentos = new List<DepartamentoInfo>();
        public Conexion conexionBD = new Conexion();

        public void OnGet()
        {
            try
            {
                conexionBD.Open();
                String sql = "SELECT codigo, nombre, cedula_jefe FROM Departamento";
                SqlCommand command = conexionBD.obtenerComando(sql);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DepartamentoInfo departamento = new DepartamentoInfo();
                        departamento.codigo = ""+reader.GetInt32(0);
                        departamento.nombre = reader.GetString(1);
                        departamento.cedula_jefe = "" + reader.GetInt32(2);

                        listaDepartamentos.Add(departamento);
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

    public class DepartamentoInfo
    {
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string cedula_jefe { get; set; }
    }
}

