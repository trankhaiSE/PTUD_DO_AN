using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class SanPhamControllercs
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public void CapnhatSanpham(SanPham sanpham)
        {
            db.Entry(sanpham).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LuuSanpham(SanPham sanpham)
        {
            db.SanPham.Add(sanpham);
            db.SaveChanges();
        }

        public List<SanPham> Timkiem(string thongtintimkiem)
        {
            List<SanPham> dsSanpham = db.SanPham
                    .Where(x => x.TenSP.Trim().ToUpper().Contains(thongtintimkiem)
                    || x.MaSP.Trim().ToUpper().Contains(thongtintimkiem)).ToList();

            return dsSanpham;
        }
    }
}
