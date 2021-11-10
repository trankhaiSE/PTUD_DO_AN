using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThemNCC : Form
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public FormThemNCC()
        {
            InitializeComponent();

            LoadDanhsachNCC();
            btnXoarongTTNCC.Enabled = false;
            DisableTTNCC();
        }

        private void btnXỏaongTTNCC_Click(object sender, EventArgs e)
        {
            tbTenNCC.Text = "";
            tbSDTNCC.Text = "";
            tbDiachiNCC.Text = "";
            tbEmailNCC.Text = "";
        }

        private void btnThemTTNCC_Click(object sender, EventArgs e)
        {
            if (btnThemTTNCC.Text == "Thêm")
            {
                btnCapnhatNCC.Text = "Lưu";
                btnThemTTNCC.Text = "Hủy";
                btnXoarongTTNCC.Enabled = true;

                EnableTTNCC();
            }
            else if(btnThemTTNCC.Text == "Hủy")
            {
                btnCapnhatNCC.Text = "Cập nhật";
                btnThemTTNCC.Text = "Thêm";
                btnXoarongTTNCC.Enabled = false;

                DisableTTNCC();
            }
            else if(btnThemTTNCC.Text == "Lưu")
            {
                string mancc = dgvDanhsachTTNCC.CurrentRow.Cells[0].Value.ToString();
                NhaCungCap ncc = db.NhaCungCap.Where(x => x.MaNCC == mancc).FirstOrDefault();
                ncc.TenNCC = tbTenNCC.Text;
                ncc.SDT = tbSDTNCC.Text;
                ncc.Diachi = tbDiachiNCC.Text;
                ncc.Email = tbEmailNCC.Text;

                db.Entry(ncc).State = EntityState.Modified;
                db.SaveChanges();

                if (MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công ! Tiếp tục cập nhật ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    DisableTTNCC();
                    btnCapnhatNCC.Text = "Cập nhật";
                    btnThemTTNCC.Text = "Thêm";
                    btnXoarongTTNCC.Enabled = false;
                    LoadDanhsachNCC();
                }
            }
        }

        private void btnCapnhatNCC_Click(object sender, EventArgs e)
        {
            if(btnCapnhatNCC.Text == "Cập nhật")
            {
                btnCapnhatNCC.Text = "Hủy";
                btnThemTTNCC.Text = "Lưu";
                btnXoarongTTNCC.Enabled = true;

                EnableTTNCC();
            }
            else if(btnCapnhatNCC.Text == "Hủy")
            {
                btnCapnhatNCC.Text = "Cập nhật";
                btnThemTTNCC.Text = "Thêm";
                btnXoarongTTNCC.Enabled = false;

                DisableTTNCC();
            }
            else if(btnCapnhatNCC.Text == "Lưu")
            {
                NhaCungCap ncc = new NhaCungCap();
                ncc.TenNCC = tbTenNCC.Text;
                ncc.SDT = tbSDTNCC.Text;
                ncc.Diachi = tbDiachiNCC.Text;
                ncc.Email = tbEmailNCC.Text;

                db.NhaCungCap.Add(ncc);
                db.SaveChanges();

                MessageBox.Show("Thêm thông tin nhà cung cấp thành công !", "Thông báo");
            }
        }

        void LoadDanhsachNCC()
        {
            dgvDanhsachTTNCC.Rows.Clear();
            var dsncc = db.NhaCungCap.ToList();
            foreach(var ncc in dsncc)
            {
                DataGridViewRow row = (DataGridViewRow)dgvDanhsachTTNCC.Rows[0].Clone();
                row.Cells[0].Value = ncc.MaNCC.ToString();
                row.Cells[1].Value = ncc.TenNCC.ToString();
                row.Cells[2].Value = ncc.SDT.ToString();
                row.Cells[3].Value = ncc.Diachi.ToString();
                row.Cells[4].Value = ncc.Email.ToString();

                dgvDanhsachTTNCC.Rows.Add(row);
            }
        }

        private void dgvDanhsachTTNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dgvDanhsachTTNCC.CurrentRow.Cells.Count; i++)
            {
                if (dgvDanhsachTTNCC.CurrentRow.Cells[i].Value.ToString() != "")
                {
                    switch (i)
                    {
                        case 1:
                            tbTenNCC.Text = dgvDanhsachTTNCC.CurrentRow.Cells[1].Value.ToString();
                            break;
                        case 2:
                            tbSDTNCC.Text = dgvDanhsachTTNCC.CurrentRow.Cells[2].Value.ToString();
                            break;
                        case 3:
                            tbDiachiNCC.Text = dgvDanhsachTTNCC.CurrentRow.Cells[3].Value.ToString();
                            break;
                        case 4:
                            tbEmailNCC.Text = dgvDanhsachTTNCC.CurrentRow.Cells[4].Value.ToString();
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            tbTenNCC.Text = "";
                            break;
                        case 2:
                            tbSDTNCC.Text = "";
                            break;
                        case 3:
                            tbDiachiNCC.ResetText();
                            break;
                        case 4:
                            tbEmailNCC.ResetText();
                            break;
                    }
                }
            }
        }

        //private void btnLuuTTNCC_Click(object sender, EventArgs e)
        //{
        //    string mancc = dgvDanhsachTTNCC.CurrentRow.Cells[0].Value.ToString();
        //    NhaCungCap ncc = db.NhaCungCap.Where(x => x.MaNCC == mancc).FirstOrDefault();
        //    ncc.TenNCC = tbTenNCC.Text;
        //    ncc.SDT = tbSDTNCC.Text;
        //    ncc.Diachi = tbDiachiNCC.Text;
        //    ncc.Email = tbEmailNCC.Text;

        //    db.Entry(ncc).State = EntityState.Modified;
        //    db.SaveChanges();

        //    if (MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công ! Tiếp tục cập nhật ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No)
        //    {
        //        DisableTTNCC();
        //        btnCapnhatNCC.Text = "Cập nhật";
        //        btnLuuTTNCC.Enabled = false;
        //    }
        //}

        void EnableTTNCC()
        {
            tbTenNCC.Enabled = true;
            tbSDTNCC.Enabled = true;
            tbDiachiNCC.Enabled = true;
            tbEmailNCC.Enabled = true;
        }

        void DisableTTNCC()
        {
            tbTenNCC.Enabled = false;
            tbSDTNCC.Enabled = false;
            tbDiachiNCC.Enabled = false;
            tbEmailNCC.Enabled = false;
        }


    }
}
