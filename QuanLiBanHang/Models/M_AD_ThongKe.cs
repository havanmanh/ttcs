using Microsoft.AspNetCore.Mvc;
using QuanLiBanHang.Controllers;
using System.Data;

namespace QuanLiBanHang.Models
{
    public class M_AD_ThongKe
    {
        public string day;
        public int money;

        public M_AD_ThongKe()
        {
            day = string.Empty;
            money = 0;
        }

    }

    class ThongKe
    {
        ConnectToSQL db;

        public ThongKe () { db = new ConnectToSQL(); }

        public List<M_AD_ThongKe> GetThongKe()
        {
            ConnectToSQL db = new ConnectToSQL();
            List<M_AD_ThongKe> TK = new List<M_AD_ThongKe>();
            DataTable dt = db.ExecQuery("SELECT sum(TongTien) as 'total', CONVERT(date, ThoiGian) as 'time' FROM HoaDon WHERE ThoiGian >= DATEADD(day, -5, GETDATE())group by CONVERT(date, ThoiGian)");

            M_AD_ThongKe item;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new M_AD_ThongKe();
                item.money = Convert.ToInt32(dt.Rows[i]["total"].ToString());
                item.day = dt.Rows[i]["time"].ToString();
                TK.Add(item);
            }
            return TK;
        }
    }
}
