using Glimpse.Core.ClientScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using QuanLiBanHang.Models;
using System.Collections.Generic;
using System.Data;

namespace QuanLiBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


		public IActionResult SignIn()
        {
            return View("V_Login");
        }

		public IActionResult Login( string username, string password)
        {
            M_Login log = new M_Login();
            //return log.login(username, password);


			if (log.login(username, password) == "employe")
            {
                return Json(new { url = Url.Action("Employe", "Home") });
            }
            else if (log.login(username, password) == "ADMIN")
            {
                return Json(new { url = Url.Action("ADMIN", "Home") });
			}
			return Json(new { url = Url.Action("SignIn", "home") }); ;
		}

		public IActionResult Employe()
		{
			ViewBag.Url = Url;
			return View("V_Employe_Layout");
		}

        public IActionResult ADMIN()
        {
            ViewBag.Url = Url;
            return View("V_ADMIN_Layout");
        }

        public IActionResult Table()
		{
			return View("V_Tables");
		}

		public IActionResult PartTables()
        {
            return PartialView("V_Tables");
        }

        public IActionResult V_Menu()
        {
            return PartialView("V_Menu");
        }

        public IActionResult PartMenu(string option)
        {
            
            List<M_Menu> list = new List<M_Menu>();
            Menu m = new Menu();
            list = m.GetMenu(option);
            return PartialView("V_Show_item", list);
        }

        [HttpGet]
        public IActionResult billDetail(int id)
        {
            List<Ban_HoaDon> Bills = new List<Ban_HoaDon>();
            BillList BL = new BillList();
            Bills = BL.GetBan_HoaDons(id);
            if(Bills.Count > 0) { 
                return PartialView("V_BillDetail", Bills);
            }
            return PartialView("empty", id);
        }

        [HttpPost]
        public int UpdateQuantity(int tableId, int itemId, int qty)
        {
            ConnectToSQL conn = new ConnectToSQL();
            conn.ExecNonQuery("update Ban_HoaDon set SoLuong = "+qty+" where MaBan = "+tableId+" and MaMon = "+itemId);
            return getTotalItem(tableId);
        }
        
        public int getTotalItem(int tableId)
        {
            ConnectToSQL conn = new ConnectToSQL();
            DataTable dt = conn.ExecQuery("select SUM(Ban_HoaDon.SoLuong*ThucDon.Gia) as 'tong' from Ban_HoaDon, ThucDon where Ban_HoaDon.MaMon = ThucDon.MaMon and Ban_HoaDon.MaBan = " + tableId);
            if(!dt.Rows[0].IsNull("tong")) { 
                return Convert.ToInt32(dt.Rows[0]["tong"].ToString());
            }
            return 0;
        }
        public void Del_item(int tableId, int itemId)
        {
            string sql = "delete Ban_HoaDon where MaBan = '" + tableId + "' and MaMon = '" + itemId + "'";
            ConnectToSQL conn = new ConnectToSQL();
             conn.ExecNonQuery(sql);
           
        }

        public IActionResult Insert_item(int tableId, int itemId)
        {
            string sql = "insert into Ban_HoaDon values('"+tableId+"','"+itemId+"',1)";
            ConnectToSQL conn = new ConnectToSQL();
            int ex = conn.ExecNonQuery(sql);
            
            if (ex == 2627) 
            {
                conn.ExecNonQuery("update Ban_Hoadon set SoLuong = SoLuong+1 where MaBan = '"+tableId+"' and MaMon = '"+itemId+"'");
            }
            return billDetail(tableId);
        }

        public IActionResult Pay(int tableId, int itemId, int cash)
        {
            string sql = "insert into HoaDon(MaBan, TongTien, ThoiGian) values( "+tableId+" , "+cash+", GETDATE())";
            ConnectToSQL conn = new ConnectToSQL();
            conn.ExecNonQuery(sql);
            sql = "insert into CTHD select(select MAX(MaHD) from HoaDon), Ban_HoaDon.MaMon, Ban_HoaDon.SoLuong, "+cash+" from Ban_HoaDon";
            conn.ExecNonQuery(sql);
            conn.ExecNonQuery("UPDATE CTHD SET ThanhGia = SoLuong * Gia FROM CTHD INNER JOIN ThucDon ON CTHD.MaMon = ThucDon.MaMon WHERE CTHD.MaHD = (select max(MaHD) from HoaDon)");
            conn.ExecNonQuery("delete Ban_HoaDon where MaBan ="+tableId );
            return billDetail(tableId);
        }


        public IActionResult AD_QLThongKe()
        {
            ThongKe conn = new ThongKe();
            List<M_AD_ThongKe> thongKe = conn.GetThongKe();
            return PartialView("V_AD_ThongKe",thongKe);
        }

        public IActionResult AD_QLBill()
        {
            var conn = new Bill();
            List<M_AD_QLBill> bills = conn.GetBills();
            return PartialView("V_AD_QLBill", bills);
        }

        public IActionResult AD_View_Bill_byDate(string date)
        {
            var conn = new Bill();
            List<M_AD_QLBill> bills = conn.GetBills_byDate(date);
            return PartialView("V_AD_QLBill", bills);
        }

        public IActionResult AD_View_BillDetail(int MaHD)
        {
            var conn = new M_AD_BillDetail();
            List<M_AD_BillDetail> bills = conn.AD_GetBillDetail(MaHD);
            return PartialView("V_AD_BillDetail", bills);
        }

        

        public void AD_deleteBill(int MaHD)
        {
            var conn = new ConnectToSQL();
            conn.ExecNonQuery("delete CTHD where MaHD = " + MaHD);
            conn.ExecNonQuery("delete HoaDon where MaHD = " + MaHD);
        }

        
        public IActionResult AD_QLMenu()
        {
            var conn = new M_AD_QLMenu();
            List<M_AD_QLMenu> menu = conn.GetMenu();
            return PartialView("V_AD_QLMenu", menu);
        }

        public IActionResult AD_getMenu_byName(string name)
        {
            var conn = new M_AD_QLMenu();
            List<M_AD_QLMenu> menu = conn.GetMenu_byName(name);
            return PartialView("V_AD_QLMenu", menu);
        }
        public void AD_deleteMenu(int MaMon)
        {
            var conn = new ConnectToSQL();
            conn.ExecNonQuery("delete ThucDon where MaMon = " + MaMon);
            
        }

        public IActionResult AD_updateMenu(int MaMon, string TenMon, int Gia, string Loai)
        {
            var conn = new M_AD_QLMenu();
            conn.updateMenu(MaMon, TenMon, Gia, Loai);
            return AD_QLMenu();
        }

        public IActionResult AD_showFormExchangeMenu(int MaMon)
        {
            var conn = new M_AD_QLMenu();
            return PartialView("V_Form_exchangeMenu", conn.GetMenu_byId(MaMon));
        }

        public IActionResult AD_insertMenu()
        {
            return PartialView("V_AD_insertMenu");
        }


        public IActionResult AD_Action_insertMenu(string TenMon, int Gia, string Loai)
        {
            M_AD_QLMenu conn = new M_AD_QLMenu();
            conn.inserMenu(TenMon, Gia, Loai);
            return AD_QLMenu();
        }
    }
}