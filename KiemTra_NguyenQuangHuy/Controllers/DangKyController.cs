using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenQuangHuy.Models;

namespace KiemTra_NguyenQuangHuy.Controllers
{
    public class DangKyController : Controller
    {
        // GET: DangKy
        DataContextDataContext data = new DataContextDataContext();
        public List<Dangky> LayDangky()
        {
            List<Dangky> lstDangky = Session["Dangky"] as List<Dangky>;
            if (lstDangky == null)
            {
                lstDangky = new List<Dangky>();
                Session["Dangky"] = lstDangky;
            }
            return lstDangky;
        }
        public ActionResult Themdangky(string id, string strURL)
        {
            List<Dangky> lstDangky = LayDangky();
            Dangky sanpham = lstDangky.Find(n => n.Mahocphan == id);
            if (sanpham == null)
            {
                sanpham = new Dangky(id);
                lstDangky.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                return Redirect(strURL);
            }
        }
        private int TongSoLuongHocPhan()
        {
            int tsl = 0;
            List<Dangky> lstDangky = Session["Dangky"] as List<Dangky>;
            if (lstDangky != null)
            {
                tsl = lstDangky.Count;
            }
            return tsl;
        }

        public ActionResult Dangky()
        {
            List<Dangky> lstDangky = LayDangky();
            ViewBag.Tongsoluongsanpham = TongSoLuongHocPhan();
            return View(lstDangky);
        }

        public ActionResult DangkyPartial()
        {
            ViewBag.Tongsoluongsanpham = TongSoLuongHocPhan();
            return PartialView();
        }
        public ActionResult XoaDangky(string id)
        {
            List<Dangky> lstDangky = LayDangky();
            Dangky sanpham = lstDangky.SingleOrDefault(n => n.Mahocphan == id);
            if (sanpham != null)
            {
                lstDangky.RemoveAll(n => n.Mahocphan == id);
                return RedirectToAction("Dangky");
            }
            return RedirectToAction("Dangky");
        }
        public ActionResult XoaTatCaDangky()
        {
            List<Dangky> lstDangky = LayDangky();
            lstDangky.Clear();
            return RedirectToAction("Dangky");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["Dangky"] == null)
            {
                return RedirectToAction("Index", "Sach");
            }
            List<Dangky> lstDangky = LayDangky();
            ViewBag.Tongsoluongsanpham = TongSoLuongHocPhan();
            return View(lstDangky);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            DangKy dh = new DangKy();
            SinhVien kh = (SinhVien)Session["TaiKhoan"];
            List<Dangky> gh = LayDangky();
            dh.MaSV = kh.MaSV;
            dh.NgayDK = DateTime.Now;
            data.DangKies.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                ChiTietDangKy ctdh = new ChiTietDangKy();
                ctdh.MaDK = dh.MaDK;
                ctdh.MaHP = item.Mahocphan;
                data.ChiTietDangKies.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();

            Session["Dangky"] = null;
            return RedirectToAction("XacNhanDonHang", "Dangky");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
        public ActionResult Index()
        {
            XoaTatCaDangky();
            return View();
        }
    }
}