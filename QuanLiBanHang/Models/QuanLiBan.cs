using global::QuanLiBanHang.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace QuanLiBanHang.Models
{
    public class QuanLiBan
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên bàn")]
        [Display(Name = "TenBan")]
        public string? TableName { get; set; }
    }

    class TableList
    {
        ConnectToSQL db;
        public TableList()
        {
            db = new ConnectToSQL();
        }

        public List<QuanLiBan> GetTables()
        {
            List<QuanLiBan> tables = new List<QuanLiBan>();
            string sql = "SELECT * FROM Ban ";
            DataTable dt = db.ExecQuery(sql);
            QuanLiBan table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                table = new QuanLiBan();
                table.ID = Convert.ToInt32(dt.Rows[i]["MaBan"].ToString());
                table.TableName = dt.Rows[i]["TenBan"].ToString();
                tables.Add(table);
            }

            return tables;
        }
    }
}


