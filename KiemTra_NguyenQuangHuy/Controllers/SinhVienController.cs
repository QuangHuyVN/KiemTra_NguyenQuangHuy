using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenQuangHuy.Models;

namespace KiemTra_NguyenQuangHuy.Controllers
{
    public class SinhVienController : Controller
    {
        // GET: SinhVien
         DataContextDataContext data = new DataContextDataContext();

        public ActionResult Index()
        {
            var all_sinhvien = from ss in data.SinhViens select ss;
            return View(all_sinhvien);
        }
        public ActionResult Details(string id)
        {
            var D_sinhvien = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(D_sinhvien);
        }
        //[HttpPost]
        public ActionResult Create()
        {
            ViewBag.MaNganh = new SelectList(data.NganhHocs.ToList().OrderBy(n => n.TenNganh), "MaNganh", "TenNganh");
            return View();
        }
        [HttpPost]
        public ActionResult Create(SinhVien sv)
        {
            data.SinhViens.InsertOnSubmit(sv);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Create(FormCollection collection, SinhVien sv)
        //{
        //    var E_masv = collection["MaSV"];
        //    var E_hoten = collection["HoTen"];
        //    var E_gioitinh = collection["GioiTinh"];
        //    var E_ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
        //    var E_hinh = collection["Hinh"];
        //    var E_manganh = collection["MaNganh"];

        //    var E_soluongton = Convert.ToInt32(collection["soluongton"]);
        //    if (string.IsNullOrEmpty(E_masv))
        //    {
        //        ViewData["Error"] = "Don't empty!";
        //    }
        //    else
        //    {
        //        sv.MaSV = E_masv.ToString();
        //        sv.HoTen = E_hoten.ToString();
        //        sv.GioiTinh = E_gioitinh.ToString();
        //        sv.NgaySinh = E_ngaysinh;
        //        sv.Hinh = E_hinh.ToString();
        //        sv.MaNganh = E_manganh.ToString();
        //        data.SinhViens.InsertOnSubmit(sv);
        //        data.SubmitChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return this.Create();
        //}
        public ActionResult Edit(string id)
        {
            var E_sach = data.SinhViens.First(m => m.MaSV == id);
            return View(E_sach);
        }
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            var E_sinhvien = data.SinhViens.First(m => m.MaSV == id);
            var E_hoten = collection["HoTen"];
            var E_gioitinh = collection["GioiTinh"];
            var E_ngaysinh = Convert.ToDateTime(collection["NgaySinh"]);
            var E_hinh = collection["Hinh"];
            var E_manganh = collection["MaNganh"];
            E_sinhvien.MaSV = id;
            if (string.IsNullOrEmpty(E_hoten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                E_sinhvien.HoTen = E_hoten.ToString();
                E_sinhvien.GioiTinh = E_gioitinh.ToString();
                E_sinhvien.NgaySinh = E_ngaysinh;
                E_sinhvien.Hinh = E_hinh.ToString();
                E_sinhvien.MaNganh = E_manganh.ToString();
                UpdateModel(E_sinhvien);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }

            return this.Edit(id);
        }
        ////-----------------------------------------
        public ActionResult Delete(string id)
        {
            var D_sinhvien = data.SinhViens.First(m => m.MaSV == id);
            return View(D_sinhvien);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var D_sinhvien = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(D_sinhvien);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }

    }
}