using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenQuangHuy.Models;

namespace KiemTra_NguyenQuangHuy.Controllers
{
    public class HocPhanController : Controller
    {
        // GET: HocPhan
        DataContextDataContext data = new DataContextDataContext();
        public ActionResult Index()
        {
            var all_hocphan = from ss in data.HocPhans select ss;
            return View(all_hocphan);
        }
    }
}