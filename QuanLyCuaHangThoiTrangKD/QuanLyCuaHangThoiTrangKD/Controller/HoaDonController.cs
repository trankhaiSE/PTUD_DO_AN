using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class HoaDonController
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        public void CapnhatHoadon(HoaDon hoadon)
        {
            db.Entry(hoadon).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LuuSanpham(HoaDon hoadon)
        {
            db.HoaDon.Add(hoadon);
            db.SaveChanges();
        }

    }
}
