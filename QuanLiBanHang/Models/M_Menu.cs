using QuanLiBanHang.Controllers;
using System.Data;

namespace QuanLiBanHang.Models
{
    public class M_Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public M_Menu() { 
            Id = 0;
            Name = string.Empty;
        }
    }

    class Menu
    {
        ConnectToSQL db;

        public Menu()
        {
            db = new ConnectToSQL();
        }

        public List<M_Menu> GetMenu (string option){
            List<M_Menu> Menu = new List<M_Menu>();
            string sql = "SELECT * FROM ThucDon where loai like N'%"+option+"%'";
            DataTable dt = db.ExecQuery(sql);
            M_Menu item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new M_Menu();
                item.Id = Convert.ToInt32(dt.Rows[i]["MaMon"].ToString());
                item.Name = dt.Rows[i]["TenMon"].ToString();
                Menu.Add(item);
            }

            return Menu;
        }
    }
}
