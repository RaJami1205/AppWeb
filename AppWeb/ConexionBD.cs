

using System.Data;
using System.Data.SqlClient;

namespace AppWeb;
public class ConexionBD{
    private string stringConnection;
    public List<>

    public ConexionBD()
    {
        stringConnection = "Data Source=.\\mysqlserver;Initial Catalog=GestionProyectosTareas;Persist Security Info=True;User ID=sa;Password=***********;Trust Server Certificate=True";
    }

    // Método para cargar datos de una tabla
    public void CargarDatos(string nombreTabla)
    {
        DataTable dataTable = new DataTable();
        using (SqlConnection connection = new SqlConnection(stringConnection)){
            string query = $"SELECT * FROM {nombreTabla}";
            using (SqlCommand command = new SqlCommand(query, connection)){//Ejecutamos el codigo SQL
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader()){
                    dataTable.Load(reader);
                }
            }
        }
    }
}
