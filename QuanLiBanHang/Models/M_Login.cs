using QuanLiBanHang.Controllers;
using System.Data;

namespace QuanLiBanHang.Models
{
    public class M_Login
    {
        string _username;
        string _password;
        public M_Login() { 
            _username = string.Empty;
            _password = string.Empty;
        }

        public string login(string username, string password)
        {
            _username = username;
            _password = password;
            ConnectToSQL connect = new ConnectToSQL();
            DataTable dt = connect.ExecQuery("SELECT * FROM Login Where UserName = '" + username + "' and Pass = '" + password +"'");
            if(dt.Rows.Count == 0) { 
			    return "";
            }
            if (dt.Rows[0]["UserName"].ToString().Trim() == _username && dt.Rows[0]["Pass"].ToString().Trim() == _password)
            {
                return dt.Rows[0]["Role"].ToString().Trim();
            }
            return "NULL";
        }
    }
}
