using QuanLiBanHang.Controllers;
using System.Data;

namespace QuanLiBanHang.Models
{
    public class Ban_HoaDon
    {
        public int MaBan { get; set; }
        public string? TenBan { get; set; }
        public int MaMon { get; set; }
        public string? TenMon { get; set; }
        public int SoLuong { get; set;}

        public int Gia { get; set; }

        public Ban_HoaDon()
        {
            MaBan = 0;
            TenBan = string.Empty;
            MaMon = 0;
            TenMon = string.Empty;
            SoLuong = 0;
            Gia = 0;
        }
    }

    class BillList
    {
        ConnectToSQL db;
        int cash;
        public BillList()
        {
            cash = 0;
            db = new ConnectToSQL();
        }

        public List<Ban_HoaDon> GetBan_HoaDons(int id)
        {
            List<Ban_HoaDon> bills = new List<Ban_HoaDon>();
            string sql = "SELECT Ban.MaBan, Ban.TenBan, ThucDon.MaMon, ThucDon.TenMon, Ban_HoaDon.SoLuong , ThucDon.gia FROM Ban,ThucDon,Ban_HoaDon where Ban.MaBan = Ban_HoaDon.MaBan and ThucDon.MaMon = Ban_HoaDon.MaMon and Ban_HoaDon.MaBan =" + id;
            DataTable dt = db.ExecQuery(sql);
            Ban_HoaDon bill;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bill = new Ban_HoaDon();
                bill.MaBan = Convert.ToInt32(dt.Rows[i]["MaBan"].ToString());
                bill.TenBan = dt.Rows[i]["TenBan"].ToString();
                bill.MaMon = Convert.ToInt32(dt.Rows[i]["MaMon"].ToString());
                bill.TenMon = dt.Rows[i]["TenMon"].ToString();
                bill.SoLuong = Convert.ToInt32(dt.Rows[i]["SoLuong"].ToString());
                bill.Gia = Convert.ToInt32(dt.Rows[i]["Gia"].ToString());
                cash += bill.SoLuong * bill.Gia;
                bills.Add(bill);
            }

            return bills;
        }
    }
}
