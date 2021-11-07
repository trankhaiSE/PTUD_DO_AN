using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangThoiTrangKD.Common
{
    class Helpers
    {
        public static string RandomID (string key)
        {
            Random num = new Random();
            string ID = key + num.Next(999, 10000);
            return ID;
        }

        public static int KiemtraCalamviecHientai()
        {
            int calamviec = 0;
            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour < 16)
                calamviec = 1;
            else if (DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 22)
                calamviec = 2;
            return calamviec;
        }
    }
}
