using QuanLiBanHang.Controllers;
using System.Collections.Generic;
using System.Data;

namespace QuanLiBanHang.Models
{
    public class M_AD_QLMenu
    {
        public int id;
        public string? TenMon;
        public int Gia;
        public string? Loai;
        
        public List<M_AD_QLMenu> GetMenu()
        {
            string sql = "select * from ThucDon";
            List < M_AD_QLMenu >  Menu = new List<M_AD_QLMenu>();
            ConnectToSQL db = new ConnectToSQL();
            DataTable dt = new DataTable();
            dt = db.ExecQuery(sql);
            M_AD_QLMenu item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new M_AD_QLMenu();
                item.TenMon = dt.Rows[i]["TenMon"].ToString();
                item.Gia = Convert.ToInt32(dt.Rows[i]["Gia"].ToString());
                item.Loai = dt.Rows[i]["loai"].ToString();
                item.id = Convert.ToInt32(dt.Rows[i]["MaMon"].ToString());
                Menu.Add(item);
            }
            return Menu;
        }

        public List<M_AD_QLMenu> GetMenu_byName(string name)
        {
            string sql = "select * from ThucDon where TenMon like N'%"+name+"%'";
            List<M_AD_QLMenu> Menu = new List<M_AD_QLMenu>();
            ConnectToSQL db = new ConnectToSQL();
            DataTable dt = new DataTable();
            dt = db.ExecQuery(sql);
            M_AD_QLMenu item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new M_AD_QLMenu();
                item.TenMon = dt.Rows[i]["TenMon"].ToString();
                item.Gia = Convert.ToInt32(dt.Rows[i]["Gia"].ToString());
                item.Loai = dt.Rows[i]["loai"].ToString();
                item.id = Convert.ToInt32(dt.Rows[i]["MaMon"].ToString());
                Menu.Add(item);
            }
            return Menu;
        }

        public void updateMenu(int MaMon, string TenMon, int Gia, string Loai)
        {
            ConnectToSQL db = new ConnectToSQL();
            db.ExecNonQuery("update ThucDon set TenMon = N'"+TenMon+"', Gia = "+Gia+", Loai = '"+Loai+"' where MaMon = " + MaMon);
        }

        public M_AD_QLMenu GetMenu_byId(int id)
        {
            string sql = "select * from ThucDon where MaMon =" + id;
            ConnectToSQL db = new ConnectToSQL();
            DataTable dt = new DataTable();
            dt = db.ExecQuery(sql);
            M_AD_QLMenu item = new M_AD_QLMenu();
            if(dt.Rows.Count > 0)
            {
                item.TenMon = dt.Rows[0]["TenMon"].ToString();
                item.Gia = Convert.ToInt32(dt.Rows[0]["Gia"].ToString());
                item.Loai = dt.Rows[0]["loai"].ToString();
                item.id = Convert.ToInt32(dt.Rows[0]["MaMon"].ToString());
            }
            return item;
        }

        public void inserMenu(string TenMon, int Gia, string Loai)
        {
            ConnectToSQL db = new ConnectToSQL();
            db.ExecNonQuery("insert into ThucDon(TenMon, Gia, loai) values( N'" + TenMon + "', " + Gia + ", '" + Loai + "')");
        }

    }
}
