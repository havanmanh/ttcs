using System.Data;
using System.Data.SqlClient;


namespace QuanLiBanHang.Controllers
{
    public class ConnectToSQL
    {
        string StrSQl = @"Data Source=DESKTOP-2NQMFP0\MSSQLSERVER01;Initial Catalog=QuanLiBanHang;Integrated Security=True";

        public DataTable ExecQuery(string sql)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(StrSQl);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public int ExecNonQuery(string sql)
        {
            SqlConnection conn = new SqlConnection(StrSQl);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
                // Code thêm bản ghi vào CSDL
            }
            catch (SqlException ex)
            {
                return ex.Number;
            }
            conn.Close();
            return 0;
        }
    }
}
