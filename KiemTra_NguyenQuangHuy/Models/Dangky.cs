using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KiemTra_NguyenQuangHuy.Models
{
    public class Dangky
    {
        DataContextDataContext data = new DataContextDataContext();

        public string Mahocphan { set; get; }
        [Display(Name = "Tên học phần")]
        public string Tenhocphan { set; get; }
        [Display(Name = "Số tín chỉ")]
        public int Sotinchi { set; get; }

        public Dangky(string MaHP)
        {
            Mahocphan = MaHP;
            HocPhan hocphan = data.HocPhans.Single(n => n.MaHP == Mahocphan);
            Tenhocphan = hocphan.TenHP;
            Sotinchi = (int)hocphan.SoTinChi;
        }
    }
}