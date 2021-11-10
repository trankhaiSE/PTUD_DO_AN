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
        private List<SanPham> dsSPHDSess = new List<SanPham>();
        double? tongtien = 0;
        Random random = new Random();

        public FormLapHoaDon()
        {
            InitializeComponent();
            txtSoluongSP.Text = "1";
            if(dgvThongtinSP.Rows.Count == 1)
            {
                btnThemvaoHoadon.Enabled = false;
                btnLuuhoadon.Enabled = false;
                btnXoakhoiHD.Enabled = false;
            }
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
                btnThemvaoHoadon.Enabled = true;
                btnLuuhoadon.Enabled = true;
            }
            else
            {
                danhsachSP = db.SanPham.Where(x => x.TenSP == txtThongtinSPTK.Text || x.MaSP == txtThongtinSPTK.Text).ToList();   
                if(danhsachSP.Count != 0)
                {
                    btnThemvaoHoadon.Enabled = true;
                    btnLuuhoadon.Enabled = true;
                }
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
            string masp = dgvThongtinSP.CurrentRow.Cells[0].Value.ToString();
            SanPham sanPham = db.SanPham.Where(x => x.MaSP == masp).FirstOrDefault();
            int soluong = int.Parse(txtSoluongSP.Text.ToString());

            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
            chiTietHoaDon.MaCTHD = random.Next(99999, 1000000);
            chiTietHoaDon.SanPham = sanPham;
            chiTietHoaDon.Soluong = soluong;
            if(dsSPHD.Count == 0)
            {
                ChiTietHoaDon cthdInit = new ChiTietHoaDon();
                cthdInit.SanPham = sanPham;
                cthdInit.Soluong = 0;
                dsSPHD.Add(cthdInit);
                dsSPHDSess.Add(sanPham);
            }

            if (dsSPHDSess.Contains(sanPham))
            {
                if(dsSPHD.Count != 1)
                {
                    foreach (var cthd in dsSPHD)
                    {
                        if (cthd.SanPham.MaSP == masp)
                        {
                            cthd.Soluong += soluong;
                        }
                    }
                }
                else
                {
                    dsSPHD[0].Soluong += soluong;
                }
            }
            else
            {
                dsSPHD.Add(chiTietHoaDon);
                dsSPHDSess.Add(sanPham);
            }
            
            if(dsSPHD.Count != 0)
            {
                btnXoakhoiHD.Enabled = true;
            }

            //Clear gridview
            dgvDanhsachSPHD.Rows.Clear();
            tongtien = 0;

            foreach (var cthd in dsSPHD)
            {
                DataGridViewRow row = (DataGridViewRow)dgvDanhsachSPHD.Rows[0].Clone();
                row.Cells[0].Value = cthd.SanPham.TenSP.ToString();
                row.Cells[1].Value = cthd.SanPham.Donvi.ToString();
                row.Cells[2].Value = cthd.Soluong.ToString();
                row.Cells[3].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", cthd.SanPham.Dongia);
                row.Cells[4].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", cthd.SanPham.Dongia * cthd.Soluong);
                tongtien += (cthd.SanPham.Dongia * cthd.Soluong);
                dgvDanhsachSPHD.Rows.Add(row);
            }
            lbTongtienHD.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);

        }

        private void btnXoakhoiHD_Click(object sender, EventArgs e)
        {
            int index = 0;
            if (dsSPHD.Count > 0)
            {
                string masp = dgvDanhsachSPHD.CurrentRow.Cells[0].Value.ToString();
                string strThanhtien = dgvDanhsachSPHD.CurrentRow.Cells[4].Value.ToString();
                double thanhtien = double.Parse(strThanhtien);
                foreach (var cthd in dsSPHD)
                {
                    if(cthd.SanPham.MaSP == masp)
                    {
                        index = dsSPHD.IndexOf(cthd);
                    }
                }
                dsSPHD.RemoveAt(index);
                dgvDanhsachSPHD.Rows.Remove(dgvDanhsachSPHD.CurrentRow);
                tongtien -= thanhtien;
                lbTongtienHD.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}",tongtien);
            }
            else
            {
                MessageBox.Show("Danh sách sản phẩm của hóa đơn đã rỗng");
            }
        }

        private void btnLuuhoadon_Click(object sender, EventArgs e)
        {
            if(dsSPHD.Count == 0)
            {

            }
            else
            {
                hoaDon.MaHD = random.Next(9999, 100000);
                hoaDon.Ngaylap = DateTime.Today;
                //hoaDon.KhachHang = 
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
