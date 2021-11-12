using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class TaiKhoanController
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        public void CapnhatTaikhoan(TaiKhoan taikhoan)
        {
            db.Entry(taikhoan).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LuuTaikhoan(TaiKhoan taikhoan)
        {
            db.TaiKhoan.Add(taikhoan);
            db.SaveChanges();
        }

        public bool Dangnhap(string tentaikhoan, string matkhau)
        {
            var taiKhoan = db.TaiKhoan.Where(x => x.Tentaikhoan == tentaikhoan.Trim().ToLower() 
                        && x.Matkhau == matkhau.Trim().ToLower()).FirstOrDefault();
            if (taiKhoan.MaTK != 0)
                return true;
            return false;
        }

        public void Datlaimatkhau(TaiKhoan taikhoan)
        {
            taikhoan.Matkhau = "1";
            db.Entry(taikhoan).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Thaydoimatkhau(TaiKhoan taikhoan, string matkhaumoi)
        {
            taikhoan.Matkhau = matkhaumoi;
            CapnhatTaikhoan(taikhoan);
        }
    }
}
