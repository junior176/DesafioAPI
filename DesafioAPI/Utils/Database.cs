using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Data.SqlClient;

namespace DesafioAPI.Utils
{
    public static class DbDesafio
    {
        public static string getConnectionString()
        {
            string ipServidor = "127.0.0.1";
            string banco = "DesafioLuizaLabs";
            string usuario = "api";
            string senha = "19pi(#Ab283";
            string conexao = @"Data Source=" + ipServidor + ";Persist Security Info=True;Password=" + senha + ";User ID="+ usuario + ";Initial Catalog=" + banco + ";TrustServerCertificate=True";

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
