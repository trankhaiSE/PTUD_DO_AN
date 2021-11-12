using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class PhieuNhapController
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        public void CapnhatPhieunhap(PhieuNhap phieunhap)
        {
            db.Entry(phieunhap).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LuuPhieunhap(PhieuNhap phieunhap)
        {
            db.PhieuNhap.Add(phieunhap);
            db.SaveChanges();
        }


    }
}
