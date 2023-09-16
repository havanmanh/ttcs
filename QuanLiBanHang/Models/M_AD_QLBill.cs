using QuanLiBanHang.Controllers;
using System.Data;

namespace QuanLiBanHang.Models
{
    public class M_AD_QLBill
    {
        public int MaHD;
        public string? TenBan;
        public string? Gia;
        public string? ThoiGian;
        
    }

    class Bill {
        ConnectToSQL db;

        public Bill()
        {
            db = new ConnectToSQL();
        }

        public List<M_AD_QLBill> GetBills ()
        {
            List<M_AD_QLBill> Bills = new List<M_AD_QLBill>();
            string sql = "select HoaDon.*, Ban.TenBan from HoaDon, Ban where Ban.MaBan = HoaDon.MaBan and DAY(ThoiGian) = DAY(getdate()) and MONTH(ThoiGian) = MONTH(getdate()) and YEAR(ThoiGian) = YEAR(getdate())";
            DataTable dt = new DataTable();
            dt = db.ExecQuery(sql);
            M_AD_QLBill item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new M_AD_QLBill();
                item.MaHD = Convert.ToInt32(dt.Rows[i]["MaHD"].ToString());
                item.TenBan = dt.Rows[i]["TenBan"].ToString();
                item.Gia = dt.Rows[i]["TongTien"].ToString();
                item.ThoiGian = dt.Rows[i]["ThoiGian"].ToString() ;
                Bills.Add(item);
            }
            return Bills;
        }

        public List<M_AD_QLBill> GetBills_byDate(string date)
        {
            List<M_AD_QLBill> Bills = new List<M_AD_QLBill>();
            string sql = "SELECT HoaDon.*, Ban.TenBan FROM HoaDon, Ban WHERE HoaDon.MaBan = Ban.MaBan and CONVERT(varchar(10),ThoiGian,23) = '" + date+"'";
            DataTable dt = new DataTable();
            dt = db.ExecQuery(sql);
            M_AD_QLBill item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new M_AD_QLBill();
                item.MaHD = Convert.ToInt32(dt.Rows[i]["MaHD"].ToString());
                item.TenBan = dt.Rows[i]["TenBan"].ToString();
                item.Gia = dt.Rows[i]["TongTien"].ToString();
                item.ThoiGian = dt.Rows[i]["ThoiGian"].ToString();
                Bills.Add(item);
            }
            return Bills;
        }

    }

}
