using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using Web_Hoa.Models;

namespace Web_Hoa.Controllers
{
    public class HomeController : Controller
    {
        WEB_HOA1Entities context = new WEB_HOA1Entities();
        public ActionResult Index()
        {
            if (Session["IDNguoiDung"] != null)
            {
                var model = context.Hoas.Where(x => x.TenHoa != null).ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        [ChildActionOnly]
        public ActionResult DanhMuc_Drop()
        {
            var m = context.DanhMucSanPhams.ToList();
            return PartialView("DanhMuc_Drop", m);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult GetHoaByDM(int id)
        {
            if (Session["IDNguoiDung"] != null)
            {
                var gethoabydm = context.Hoas.Where(x => x.IdDanhMucSanPham == id).ToList();
                ViewBag.TenDM = context.DanhMucSanPhams.Where(x => x.IdDanhMucSanPham == id).Select(x => x.TenDanhMucSanPham).ToString();
                return View("Index", gethoabydm);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(NguoiDung userModel)
        {
            using (WEB_HOA1Entities dBHoaTuoi = new WEB_HOA1Entities())
            {
                var data = dBHoaTuoi.NguoiDungs.Where(x => x.Email == userModel.Email).FirstOrDefault();
                if (data == null)
                {
                    ViewBag.LoginErrorMessage = "Mật khẩu hoặc Email không đúng.";
                    return View("Login");
                }
                else
                {
                    // gán session và chuyển hướng về trang chủ
                    Session["Email"] = data.Email;
                    Session["TenDangNhap"] = data.TenDangNhap;
                    Session["IDNguoiDung"] = data.IDNguoiDung;
                    return RedirectToAction("Index");
                }
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult SignUp(int id = 0)
        {
            NguoiDung userModel = new NguoiDung();
            return View(userModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SignUp(NguoiDung userModel)
        {
            using (WEB_HOA1Entities dBHoaTuoi = new WEB_HOA1Entities())
            {
                if (dBHoaTuoi.NguoiDungs.Any(x => x.Email == userModel.Email))
                {
                    ViewBag.DuplicateMessage = "Email đã tồn tại.";
                    return View("SignUp", userModel);
                }
                dBHoaTuoi.NguoiDungs.Add(userModel);
                dBHoaTuoi.Configuration.ValidateOnSaveEnabled = false;
                dBHoaTuoi.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Đăng Ký Thành Công";
            return RedirectToAction("Login");
        }
    }
}