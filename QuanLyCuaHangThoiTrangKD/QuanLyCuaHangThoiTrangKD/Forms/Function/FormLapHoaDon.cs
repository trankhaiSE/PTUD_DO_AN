using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormLapHoaDon : Form
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        private HoaDon hoaDon = new HoaDon();
        private List<ChiTietHoaDon> dsSPHD = new List<ChiTietHoaDon>();
        private List<ChiTietHoaDon> dsSPHDSess = new List<ChiTietHoaDon>();
        public FormLapHoaDon()
        {
            InitializeComponent();
            txtSoluongSP.Text = "1";

        }

        private void grThongtinSanpham_Enter(object sender, EventArgs e)
        {

        }

        private void btnLuuKH_Click(object sender, EventArgs e)
        {
            KhachHang kh = db.KhachHang.Where(x => x.HovaTen == txtTenKH.Text && x.SDT == txtSDTKH.Text).FirstOrDefault();
            if(kh != null)
            {
                MessageBox.Show("Khách hàng đã tồn tại");
            }
            else
            {
                MessageBox.Show("Lưu thông tin khách hàng thành công !!");
            }
        }

        private void btnXoarong_Click(object sender, EventArgs e)
        {

        }

        private void btnTimkiemTTSP_Click(object sender, EventArgs e)
        {
            dgvThongtinSP.Rows.Clear();
            List<SanPham> danhsachSP = new List<SanPham>();
            if (txtThongtinSPTK.Text == "")
            {
                danhsachSP = db.SanPham.ToList();
            }
            else
            {
                danhsachSP = db.SanPham.Where(x => x.TenSP == txtThongtinSPTK.Text || x.MaSP == txtThongtinSPTK.Text).ToList();              
            }

            foreach (var sp in danhsachSP)
            {
                DataGridViewRow dgvr = (DataGridViewRow)dgvThongtinSP.Rows[0].Clone();
                dgvr.Cells[0].Value = sp.MaSP.ToString();
                dgvr.Cells[1].Value = sp.TenSP.ToString();
                dgvr.Cells[2].Value = sp.Mausac.ToString();
                dgvr.Cells[3].Value = sp.Chatlieu.ToString();
                dgvr.Cells[4].Value = sp.Kichthuoc.ToString();
                dgvr.Cells[6].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sp.Dongia);
                dgvThongtinSP.Rows.Add(dgvr);
            }
        }

        private void btnThemvaoHoadon_Click(object sender, EventArgs e)
        {
            String masp = dgvThongtinSP.CurrentRow.Cells[0].Value.ToString();
            SanPham sanPham = db.SanPham.Where(x => x.MaSP == masp).FirstOrDefault();

            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
            chiTietHoaDon.SanPham = sanPham;
            chiTietHoaDon.MaSP = masp;
            chiTietHoaDon.Soluong = int.Parse(txtSoluongSP.Text.ToString());
            dsSPHD.Add(chiTietHoaDon);
            


            DataGridViewRow row = (DataGridViewRow)dgvDanhsachSPHD.Rows[0].Clone();
            row.Cells[0].Value = sanPham.TenSP.ToString();
            row.Cells[1].Value = sanPham.Donvi.ToString();
            row.Cells[2].Value = txtSoluongSP.Text.ToString();
            row.Cells[3].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sanPham.Dongia);
            row.Cells[4].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", sanPham.Dongia * int.Parse(txtSoluongSP.Text.ToString()));

            dgvDanhsachSPHD.Rows.Add(row);

        }

        private void btnXoakhoiHD_Click(object sender, EventArgs e)
        {
            if(dgvDanhsachSPHD.Rows.Count != 0)
            {
                dgvDanhsachSPHD.Rows.Remove(dgvDanhsachSPHD.CurrentRow);
            }
            else
            {
                MessageBox.Show("Danh sách sản phẩm của hóa đơn đã rỗng");
            }
        }

        private void dgvDanhsachSPHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvThongtinSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grThongtinKhachhang_Enter(object sender, EventArgs e)
        {

        }

    }
}
