using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenQuangHuy.Models;

namespace KiemTra_NguyenQuangHuy.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        DataContextDataContext data = new DataContextDataContext();
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            SinhVien kh = data.SinhViens.SingleOrDefault(n => n.MaSV == tendangnhap);
            if (kh != null)
            {
                ViewBag.ThongBao = "Ban da dang nhap thanh cong";
                Session["TaiKhoan"] = kh;
            }
            else
            {
                ViewBag.ThongBao = "ten dang nhap hoac mat khau khong dung";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}