using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormThongTinSanPham : Form
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();

        public FormThongTinSanPham()
        {
            InitializeComponent();

            LoadComboboxSP();
            DisableTTSP();
            btnLuuTTSP.Enabled = false;
            btnXoarongTTSP.Enabled = false;
            LoadDanhsachTTSP();

        }

        private void btnTimkiemSP_Click(object sender, EventArgs e)
        {
            if (tbThongtinTKSP.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin tìm kiếm !");
            }
            else
            {
                var dsSanpham = db.SanPham.Where(x => x.TenSP.Trim().ToUpper().Contains(tbThongtinTKSP.Text) || x.MaSP.Trim().ToUpper().Contains(tbThongtinTKSP.Text)).ToList();

                dgvDanhsachTTSP.Rows.Clear();
                foreach (var sanpham in dsSanpham)
                {
                    dgvDanhsachTTSP.Rows.Add(ConvertSanPhamtoGridViewRow(sanpham));
                }
            }
        }

        private void dgvDanhsachTTSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDanhsachTTSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var vitri = new List<int>();
            for (int i = 0; i < dgvDanhsachTTSP.CurrentRow.Cells.Count; i++)
            {
                if (dgvDanhsachTTSP.CurrentRow.Cells[i].Value.ToString() != "")
                {
                    switch (i)
                    {
                        case 1:
                            tbTenSP.Text = dgvDanhsachTTSP.CurrentRow.Cells[1].Value.ToString();
                            break;
                        case 2:
                            tbChatlieuSP.Text = dgvDanhsachTTSP.CurrentRow.Cells[2].Value.ToString();
                            break;
                        case 3:
                            cbMausacSP.Text = dgvDanhsachTTSP.CurrentRow.Cells[3].Value.ToString();
                            break;
                        case 4:
                            cbKichthuocSP.Text = dgvDanhsachTTSP.CurrentRow.Cells[4].Value.ToString();
                            break;
                        case 5:
                            cbLoaiSP.Text = dgvDanhsachTTSP.CurrentRow.Cells[5].Value.ToString();
                            break;
                        case 6:
                            cbDonviSP.Text = dgvDanhsachTTSP.CurrentRow.Cells[6].Value.ToString();
                            break;
                        case 7:
                            nudDongianhapSP.Value = decimal.Parse(dgvDanhsachTTSP.CurrentRow.Cells[7].Value.ToString());
                            break;
                        case 8:
                            nudDongiaSP.Value = decimal.Parse(dgvDanhsachTTSP.CurrentRow.Cells[8].Value.ToString());
                            break;
                        case 9:
                            cbTinhtrangSP.Text = dgvDanhsachTTSP.CurrentRow.Cells[9].Value.ToString();
                            break;
                    }
                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            tbTenSP.Text = "";
                            break;
                        case 2:
                            tbChatlieuSP.Text = "";
                            break;
                        case 3:
                            cbMausacSP.ResetText();
                            break;
                        case 4:
                            cbKichthuocSP.ResetText();
                            break;
                        case 5:
                            cbLoaiSP.ResetText();
                            break;
                        case 6:
                            cbDonviSP.ResetText();
                            break;
                        case 7:
                            nudDongianhapSP.ResetText();
                            break;
                        case 8:
                            nudDongiaSP.ResetText();
                            break;
                        case 9:
                            cbTinhtrangSP.ResetText();
                            break;
                    }
                }
            }
        }

        private void btnCapnhatTTSP_Click(object sender, EventArgs e)
        {
            if (btnCapnhatTTSP.Text == "Cập nhật")
            {
                EnableTTSP();
                btnCapnhatTTSP.Text = "Hủy";
                btnLuuTTSP.Enabled = true;
                btnXoarongTTSP.Enabled = true;
            }
            else
            {
                DisableTTSP();
                btnCapnhatTTSP.Text = "Cập nhật";
                btnLuuTTSP.Enabled = false;
                btnXoarongTTSP.Enabled = false;
            }
        }

        private void btnLuuTTSP_Click(object sender, EventArgs e)
        {
            string masp = dgvDanhsachTTSP.CurrentRow.Cells[0].Value.ToString();
            SanPham sp = db.SanPham.Where(x => x.MaSP == masp).FirstOrDefault();
            sp.TenSP = tbTenSP.Text;
            sp.Chatlieu = tbChatlieuSP.Text;
            sp.Mausac = cbMausacSP.Text;
            sp.Kichthuoc = cbKichthuocSP.Text;
            sp.LoaiSP = cbLoaiSP.Text;
            sp.Donvi = cbDonviSP.Text;
            sp.Dongianhap = double.Parse(nudDongianhapSP.Value.ToString());
            sp.Dongia = double.Parse(nudDongiaSP.Value.ToString());
            sp.Tinhtrang = cbTinhtrangSP.Text;

            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
            LoadDanhsachTTSP();
            
            if (MessageBox.Show("Cập nhật thông tin sản phẩm thành công ! Tiếp tục cập nhật ?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.No){
                DisableTTSP();
                btnCapnhatTTSP.Text = "Cập nhật";
                btnLuuTTSP.Enabled = false;
            }            
        }

        private void btnXoarongTTSP_Click(object sender, EventArgs e)
        {
            tbTenSP.Text = "";
            tbChatlieuSP.Text = "";
            cbMausacSP.ResetText();
            cbKichthuocSP.ResetText();
            cbLoaiSP.ResetText();
            cbDonviSP.ResetText();
            nudDongianhapSP.ResetText();
            nudDongiaSP.ResetText();
            cbTinhtrangSP.ResetText();
        }

        private void bunifuGroupBox1_Enter(object sender, EventArgs e)
        {

        }

        DataGridViewRow ConvertSanPhamtoGridViewRow(SanPham sp)
        {
            DataGridViewRow row = (DataGridViewRow)dgvDanhsachTTSP.Rows[0].Clone();
            row.Cells[0].Value = sp.MaSP.ToString();
            row.Cells[1].Value = sp.TenSP.ToString();
            row.Cells[2].Value = sp.Chatlieu.ToString();
            row.Cells[3].Value = sp.Mausac.ToString();
            row.Cells[4].Value = sp.Kichthuoc.ToString();
            row.Cells[5].Value = sp.LoaiSP.ToString();
            row.Cells[6].Value = sp.Donvi.ToString();
            row.Cells[7].Value = sp.Dongianhap.ToString();
            row.Cells[8].Value = sp.Dongia.ToString();
            row.Cells[9].Value = sp.Tinhtrang.ToString();

            return row;
        }

        void LoadComboboxSP()
        {
            cbTinhtrangSP.Items.Add("Còn hàng");
            cbTinhtrangSP.Items.Add("Hết hàng");
            cbTinhtrangSP.Items.Add("Ngừng kinh doanh");

            cbKichthuocSP.Items.Add("S");
            cbKichthuocSP.Items.Add("M");
            cbKichthuocSP.Items.Add("L");
            cbKichthuocSP.Items.Add("XL");
            cbKichthuocSP.Items.Add("XXL");

            cbDonviSP.Items.Add("Đôi");
            cbDonviSP.Items.Add("Chiếc");
            cbDonviSP.Items.Add("Bộ");

            cbLoaiSP.Items.Add("Quần");
            cbLoaiSP.Items.Add("Áo");
            cbLoaiSP.Items.Add("Áo khoát");
            cbLoaiSP.Items.Add("Đặc biệt");
            cbLoaiSP.Items.Add("Phụ kiện");

            cbMausacSP.Items.Add("Hồng");
            cbMausacSP.Items.Add("Cam");
            cbMausacSP.Items.Add("Trắng");
            cbMausacSP.Items.Add("Đen");
            cbMausacSP.Items.Add("Vàng");
            cbMausacSP.Items.Add("Trắng Xám");
            cbMausacSP.Items.Add("Xanh xám");
            cbMausacSP.Items.Add("Xanh dương");
        }

        void DisableTTSP()
        {
            tbTenSP.Enabled = false;
            tbChatlieuSP.Enabled = false;
            cbMausacSP.Enabled = false;
            cbKichthuocSP.Enabled = false;
            cbLoaiSP.Enabled = false;
            cbDonviSP.Enabled = false;
            nudDongianhapSP.Enabled = false;
            nudDongiaSP.Enabled = false;
            cbTinhtrangSP.Enabled = false;
            btnChonanhSP.Enabled = false;
        }

        void EnableTTSP()
        {
            tbTenSP.Enabled = true;
            tbChatlieuSP.Enabled = true;
            cbMausacSP.Enabled = true;
            cbKichthuocSP.Enabled = true;
            cbLoaiSP.Enabled = true;
            cbDonviSP.Enabled = true;
            nudDongianhapSP.Enabled = true;
            nudDongiaSP.Enabled = true;
            cbTinhtrangSP.Enabled = true;
            btnChonanhSP.Enabled = true;
        }

        void LoadDanhsachTTSP()
        {
            dgvDanhsachTTSP.Rows.Clear();
            var dsSPHT = db.SanPham.ToList();
            foreach (var sp in dsSPHT)
            {
                dgvDanhsachTTSP.Rows.Add(ConvertSanPhamtoGridViewRow(sp));
            }
        }
    }
}
