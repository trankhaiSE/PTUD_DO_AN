using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThoiTrangKD.Controller
{
    class NhanVienController
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        public void CapnhatNhanvien(NhanVien nhanvien)
        {
            db.Entry(nhanvien).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LuuNhanvien(NhanVien nhanvien)
        {
            db.NhanVien.Add(nhanvien);
            db.SaveChanges();
        }

        public List<NhanVien> Timkiem(string thongtinNV)
        {
            var dsTK = db.NhanVien
                .Where(x => x.HovaTen.Contains(thongtinNV.Trim().ToUpper())
                || x.MaNV.Contains(thongtinNV.Trim().ToUpper())).ToList();

            return dsTK;
        }
    }
}
