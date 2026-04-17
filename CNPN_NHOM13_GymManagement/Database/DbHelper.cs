using System;
using System.Data;
using System.Data.SqlClient;

namespace CNPN_NHOM13_GymManagement.Database
{
    internal class DbHelper
    {
        private string connectionString = "Server=Quang\\SQLEXPRESS;Database=GymManagement;Trusted_Connection=True;";

        // Lấy connection
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Mở kết nối
        public void OpenConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        // Đóng kết nối
        public void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // SELECT
        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = GetConnection();
            OpenConnection(conn);

            SqlCommand cmd = new SqlCommand(query, conn);

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            CloseConnection(conn);
            return dt;
        }

        // INSERT, UPDATE, DELETE
        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = GetConnection();
            OpenConnection(conn);

            SqlCommand cmd = new SqlCommand(query, conn);

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            int result = cmd.ExecuteNonQuery();

            CloseConnection(conn);
            return result;
        }

        // COUNT, SUM...
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = GetConnection();
            OpenConnection(conn);

            SqlCommand cmd = new SqlCommand(query, conn);

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            object result = cmd.ExecuteScalar();

            CloseConnection(conn);
            return result;
        }
    }
}
