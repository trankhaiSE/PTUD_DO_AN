using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCuaHangThoiTrangKD.Common;

using System.Globalization;

namespace QuanLyCuaHangThoiTrangKD.Forms.Function
{
    public partial class FormLapPhieuNhap : Form
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        PhieuNhap phieu = new PhieuNhap();
        private List<SanPham> dsSPPNSess = new List<SanPham>();
        private List<ChiTietPhieuNhap> dsCTPN = new List<ChiTietPhieuNhap>();
        double? tongtien = 0;

        public FormLapPhieuNhap()
        {
            InitializeComponent();

            LoadComboboxSP();
            var dsSPHT = db.ChiTietPhieuNhap
                .GroupBy(x => x.SanPham.MaSP)
                .Select(z => new { CTPN = z.Key, TongSL = z.Sum(y => y.Soluong) })
                .OrderByDescending(z => z.TongSL);

            string mess = "";
            foreach(var i in dsSPHT)
            {
                mess += "San pham " + i.CTPN.ToString() + " sl: " + i.TongSL + " | "; 
            }
        }

        private void btnThemvaoPN_Click(object sender, EventArgs e)
        {
            SanPham sp = db.SanPham.Where(x => x.TenSP == tbTenSP.Text).FirstOrDefault();
            int slNhap = int.Parse(nudSoluongnhap.Value.ToString());

            if (sp == null)
            {
                sp = new SanPham();
                sp.MaSP = Helpers.RandomID("SP");
                sp.TenSP = tbTenSP.Text;
                sp.Chatlieu = tbChatlieu.Text;
                sp.Dongianhap = double.Parse(nudDongianhap.Value.ToString());
                sp.Donvi = cbDonvi.SelectedItem.ToString();
                sp.Kichthuoc = cbKichthuoc.SelectedItem.ToString();
                sp.LoaiSP = cbLoaiSP.SelectedItem.ToString();
                sp.Mausac = cbMausac.SelectedItem.ToString();
                sp.Tinhtrang = "Còn hàng";

                foreach (var sanpham in dsSPPNSess)
                {
                    if(sanpham.TenSP == tbTenSP.Text)
                    {
                        sp = sanpham;
                    }
                }

            }

            ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap();
            ctpn.SanPham = sp;
            ctpn.Soluong = slNhap;

            if(dsCTPN.Count == 0)
            {
                ChiTietPhieuNhap ctpnInit = new ChiTietPhieuNhap();
                ctpnInit.SanPham = sp;
                ctpnInit.Soluong = 0;
                dsSPPNSess.Add(sp);
                dsCTPN.Add(ctpnInit);
            }

            if (dsSPPNSess.Contains(sp))
            {
                if (dsCTPN.Count != 1)
                {
                    foreach (var cthd in dsCTPN)
                    {
                        if (cthd.SanPham.MaSP == sp.MaSP)
                        {
                            cthd.Soluong += slNhap;
                        }
                    }
                }
                else
                {
                    dsCTPN[0].Soluong += slNhap;
                }
            }
            else
            {
                dsCTPN.Add(ctpn);
                dsSPPNSess.Add(sp);
            }

            //string mess = "";
            //foreach(var i in dsSPPNSess)
            //{
            //    mess += i.TenSP + " - ";
            //}
            //MessageBox.Show("SP: " + mess);

            if (dsCTPN.Count != 0)
            {
                btnXoakhoiPN.Enabled = true;
            }

            dgvDanhsachSPPN.Rows.Clear();
            tongtien = 0;

            foreach (var ct in dsCTPN)
            {
                DataGridViewRow row = (DataGridViewRow)dgvDanhsachSPPN.Rows[0].Clone();
                row.Cells[0].Value = ct.SanPham.TenSP.ToString();
                row.Cells[1].Value = ct.SanPham.Donvi.ToString();
                row.Cells[2].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ct.SanPham.Dongianhap);
                row.Cells[3].Value = ct.Soluong.ToString();
                row.Cells[4].Value = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", ct.SanPham.Dongianhap * ct.Soluong);
                tongtien += (ct.SanPham.Dongianhap * ct.Soluong);
                dgvDanhsachSPPN.Rows.Add(row);
            }
            lbTongtienPN.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);
        }

        private void btnLuuPN_Click(object sender, EventArgs e)
        {
            NhaCungCap ncc = db.NhaCungCap.Where(x => x.TenNCC == cbTenNCC.SelectedItem.ToString().Trim()).FirstOrDefault();
            Random ran = new Random();

            phieu.MaPN = ran.Next(9999, 100000);
            phieu.NgayLap = DateTime.Today;
            phieu.Tinhtrang = "Đã thanh toán";
            phieu.Tongtien = tongtien;
            phieu.ChiTietPhieuNhap = dsCTPN;
            phieu.NhaCungCap = ncc;
            phieu.Calamviec = Helpers.KiemtraCalamviecHientai();

            //db.PhieuNhap.Add(phieu);
            //MessageBox.Show("PN: " + phieu.MaPN + " - " + phieu.NgayLap + " - " + phieu.Tongtien + " - " + phieu.ChiTietPhieuNhap.Count + " - " + phieu.NhaCungCap.TenNCC + " - " + phieu.Calamviec + " - ");
            try {
                foreach(var item in dsSPPNSess)
                {
                    //if()
                    //db.SanPham.Add()
                }

                MessageBox.Show("Phiếu nhập lưu thành công");
            }
            catch(Exception)
            {

            }

            dgvDanhsachSPPN.Rows.Clear();
        }

        void LoadComboboxSP()
        {
            var dsNCC = db.NhaCungCap.ToList();
            foreach (var ncc in dsNCC)
            {
                cbTenNCC.Items.Add(ncc.TenNCC);
            }

            cbKichthuoc.Items.Add("S");
            cbKichthuoc.Items.Add("M");
            cbKichthuoc.Items.Add("L");
            cbKichthuoc.Items.Add("XL");
            cbKichthuoc.Items.Add("XXL");

            cbDonvi.Items.Add("Đôi");
            cbDonvi.Items.Add("Chiếc");
            cbDonvi.Items.Add("Bộ");

            cbLoaiSP.Items.Add("Quần");
            cbLoaiSP.Items.Add("Áo");
            cbLoaiSP.Items.Add("Áo khoát");
            cbLoaiSP.Items.Add("Đặc biệt");
            cbLoaiSP.Items.Add("Phụ kiện");

            cbMausac.Items.Add("Cam");
            cbMausac.Items.Add("Trắng");
            cbMausac.Items.Add("Đen");
            cbMausac.Items.Add("Vàng");
            cbMausac.Items.Add("Trắng Xám");
            cbMausac.Items.Add("Xanh xám");
            cbMausac.Items.Add("Xanh dương");
        }

        private void btnXoarongTTSP_Click(object sender, EventArgs e)
        {
            tbTenSP.Text = "";
            tbChatlieu.Text = "";
            cbMausac.SelectedItem = "";
            cbKichthuoc.SelectedItem = "";
            cbDonvi.SelectedItem = "";
            cbLoaiSP.SelectedItem = "";
            nudDongianhap.Value = 100000;
            nudSoluongnhap.Value = 1;
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            
        }
    }
}
