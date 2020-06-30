using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Hoa.Models;

namespace Web_Hoa.Controllers
{
    public class ShoppingCartController : Controller
    {
        WEB_HOA1Entities db = new WEB_HOA1Entities();
        private const string GioHang = "GioHang";
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = Session[GioHang];
            List<Item> list = new List<Item>();
            if (cart != null)
            {
                list = (List<Item>)cart;
            }
            return View(list);
        }
        [ChildActionOnly]
        public ActionResult DanhMuc_Drop()
        {
            var m = db.DanhMucSanPhams.ToList();
            return PartialView("DanhMuc_Drop", m);
        }

        public ActionResult Remove(int id)
        {
            var model = (List<Item>)Session["GioHang"];
            if (model != null)
            {
                model.RemoveAll(o => o.Hoa.IDHoa == id);
                Session["GioHang"] = model;
            }
            return RedirectToAction("Index");
        }
        public ActionResult OrderNow(int id, int soluonghoa = 1)
        {
            var hoa = db.Hoas.Find(id);
            var cart = Session[GioHang];
            if (cart != null)
            {
                List<Item> list = (List<Item>)Session[GioHang];
                if (list.Exists(x => x.Hoa.IDHoa == id))
                {
                    foreach (var item in list)
                    {
                        if (item.Hoa.IDHoa == id)
                        {
                            item.SoLuongHoa++;
                            item.ThanhTien = item.SoLuongHoa * item.Hoa.GiaTien;
                        }
                    }
                }
                else
                {
                    var item = new Item();
                    item.Hoa = hoa;
                    item.SoLuongHoa = soluonghoa;
                    item.ThanhTien = item.Hoa.GiaTien * soluonghoa;
                    list.Add(item);
                }
                Session[GioHang] = list;
            }
            else
            {
                var item = new Item();
                item.Hoa = hoa;
                item.SoLuongHoa = soluonghoa;
                item.ThanhTien = item.Hoa.GiaTien * soluonghoa;
                List<Item> list = new List<Item>();
                list.Add(item);
                Session[GioHang] = list;

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var li = (List<Item>)Session[GioHang];
            var ttnd = new ThanhToanNguoiDung();
            ttnd.SoLuongHoa = li.Sum(x => x.SoLuongHoa); ;
            ttnd.SoTien = li.Sum(x => x.ThanhTien);
            var id = Session["IDNguoiDung"];
            ttnd.TenNguoiDung = db.NguoiDungs.Find(id).TenNguoiDung;
            ttnd.Email = Session["Email"].ToString();
            ttnd.DienThoai = db.NguoiDungs.Find(id).DienThoai.ToString();
            ttnd.DiaChi = db.NguoiDungs.Find(id).DiaChi;
            return View(ttnd);
        }
        public ActionResult PVComplete()
        {
            return PartialView("Complete");
        }
    }
}