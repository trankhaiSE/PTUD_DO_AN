using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class KhachHangController
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        public void CapnhatKhachhang(KhachHang khachhang)
        {
            db.Entry(khachhang).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LuuKhachhang(KhachHang khachhang)
        {
            db.KhachHang.Add(khachhang);
            db.SaveChanges();
        }

        public List<KhachHang> Timkiem(string thongtinKH)
        {
            var dsTK = db.KhachHang
                .Where(x => x.HovaTen.Contains(thongtinKH.Trim().ToUpper()) 
                || x.MaKH.Contains(thongtinKH.Trim().ToUpper())).ToList();

            return dsTK;
        }

    }
}
