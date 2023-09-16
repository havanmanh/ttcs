using QuanLiBanHang.Controllers;
using System.Data;

namespace QuanLiBanHang.Models
{
    public class M_AD_BillDetail
    {
        public string? tenMon;
        public int gia;
        public int soLuong;
        public int thanhTien;


        public List<M_AD_BillDetail> AD_GetBillDetail(int MaHD)
        {
            List<M_AD_BillDetail> Bill_view = new List<M_AD_BillDetail>();
            ConnectToSQL db = new ConnectToSQL();
            M_AD_BillDetail item;
            DataTable dt = db.ExecQuery("select CTHD.*,ThucDon.Gia, ThucDon.TenMon from CTHD,ThucDon where CTHD.MaMon = ThucDon.MaMon and MaHD =" + MaHD);
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                item = new M_AD_BillDetail();
                item.tenMon = dt.Rows[i]["TenMon"].ToString();
                item.gia = Convert.ToInt32(dt.Rows[i]["Gia"].ToString());
                item.soLuong = Convert.ToInt32(dt.Rows[i]["SoLuong"].ToString());
                item.thanhTien = Convert.ToInt32(dt.Rows[i]["ThanhGia"].ToString());
                Bill_view.Add(item);
            }
            return Bill_view;
        }
    }
}
