using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormDangnhap : Form
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        public FormDangnhap()
        {
            InitializeComponent();
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                TaiKhoan taiKhoan = db.TaiKhoan.Where(x => x.Tentaikhoan == tbTenTK.Text.Trim().ToLower() && x.Matkhau == tbMatkhau.Text.Trim().ToLower()).FirstOrDefault();
                if (taiKhoan.MaTK != 0)
                {
                    FormMain frm = new FormMain(taiKhoan);
                    this.Hide();
                    frm.Show();
                }
                else
                {

                }
            }
        }

        private void tbTenTK_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbTenTK.Text))
            {
                e.Cancel = true;
                tbTenTK.Focus();
                errorTentaikhoan.SetError(tbTenTK, "Vui lòng nhập tên tài khoản");
            }
            else
            {
                e.Cancel = false;
                errorTentaikhoan.SetError(tbTenTK, null);
            }
        }

        private void tbMatkhau_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbMatkhau.Text))
            {
                e.Cancel = true;
                tbMatkhau.Focus();
                errorMatkhau.SetError(tbMatkhau, "Vui lòng nhập mật khẩu");
            }
            else
            {
                e.Cancel = false;
                errorMatkhau.SetError(tbMatkhau, null);
            }
        }
    }
}
