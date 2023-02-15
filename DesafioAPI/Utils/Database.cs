using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Data.SqlClient;

namespace DesafioAPI.Utils
{
    public static class Database
    {
        public static string getConnectionString()
        {
            string ipServidor = "127.0.0.1";
            string banco = "DesafioLuizaLabs";
            string usuario = "user";
            string senha = "user123";
            string conexao = @"Server=" + ipServidor + ";Database=" + banco + ";Password=" + senha + ";User ID="+ usuario;

            return conexao;
        }

        public static DataTable getReader(string sql)
        {
            try
            {
                SqlConnection conn = new(getConnectionString());
                conn.Open();
                SqlCommand cmd = new(sql, conn);
                cmd.CommandTimeout = 6000;
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                conn.Close();
                return dataTable;
            }
            catch (Exception e)
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("Error", typeof(string));
                dataTable.Rows.Add(e.Message);
                dataTable.Rows.Add(e.InnerException);
                return dataTable;
            }

        }

    }
}
