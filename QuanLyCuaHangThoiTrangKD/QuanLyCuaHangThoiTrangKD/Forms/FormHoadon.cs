using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Forms;
using QuanLyCuaHangThoiTrangKD.Forms.Function;

namespace QuanLyCuaHangThoiTrangKD.Forms
{
    public partial class FormHoadon : Form
    {
        public FormHoadon()
        {
            InitializeComponent();
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            FormLapHoaDon frmLHD = new FormLapHoaDon();
            frmLHD.ShowDialog();
        }
    }
}
