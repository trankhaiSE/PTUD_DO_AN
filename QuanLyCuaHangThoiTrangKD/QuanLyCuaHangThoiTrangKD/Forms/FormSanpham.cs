using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Forms.Function;

namespace QuanLyCuaHangThoiTrangKD.Forms
{
    public partial class FormSanpham : Form
    {
        public FormSanpham()
        {
            InitializeComponent();
        }

        private void btnLapPhieuNhap_Click(object sender, EventArgs e)
        {
            FormLapPhieuNhap frm = new FormLapPhieuNhap();
            frm.Show();
        }

        private void btnThongtinSanPham_Click(object sender, EventArgs e)
        {
            FormThongTinSanPham frm = new FormThongTinSanPham();
            frm.Show();
        }

        private void btnCapnhatPN_Click(object sender, EventArgs e)
        {
            FormThongTinPhieuNhap frm = new FormThongTinPhieuNhap();
            frm.Show();
        }
    }
}
