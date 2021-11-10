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
        Random ran = new Random();

        public FormLapPhieuNhap()
        {
            InitializeComponent();

            LoadComboboxSP();
            var dsSPHT = db.SanPham
                .Join(db.ChiTietPhieuNhap, sp => sp.MaSP, ctpn => ctpn.SanPham.MaSP
                , (sp, ctpn) => new
                {
                    IDCTPN = ctpn.MaCTPN,
                    IDSP = sp.MaSP,
                    SLSPPN = ctpn.Soluong
                })
                .Join(db.ChiTietHoaDon, sppn => sppn.IDSP, cthd => cthd.MaSP
                , (sppn, cthd) => new
                {
                    idCTPN = sppn.IDCTPN,
                    idCTHD = cthd.MaCTHD,
                    idSP = sppn.IDSP,
                    slSPPN = sppn.SLSPPN,
                    slSPHD = cthd.Soluong
                })
                .GroupBy(x => x.idSP)
                .Select(z => new { MASP = z.Key, TongSLSPPN = z.Sum(y => y.slSPPN), TongSLSPHD = z.Sum(g => g.slSPHD) })
                .OrderByDescending(z => z.TongSLSPPN);

            string mess = "";
            foreach(var i in dsSPHT)
            {
                mess += i.MASP + " - " + i.TongSLSPHD + " - " + i.TongSLSPPN + " | "; 
            }

            //MessageBox.Show(mess);

            //foreach (var item in dsSPHT)
            //{
            //    SanPham spht = db.SanPham.Where(x => x.MaSP == item.CTPN).FirstOrDefault();
            //    DataGridViewRow row = (DataGridViewRow)dgvThongtinSPHT.Rows[0].Clone();
            //    row.Cells[0].Value = spht.MaSP.ToString();
            //    row.Cells[1].Value = spht.TenSP.ToString();
            //    row.Cells[2].Value = 
            //    row.Cells[3].Value = spht.ToString();

            //    dgvThongtinSPHT.Rows.Add(row);
            //}
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
            ctpn.MaCTPN = ran.Next(99999, 1000000); //100000 - 999999
            ctpn.SanPham = sp;
            ctpn.Soluong = slNhap;

            if(dsCTPN.Count == 0)
            {
                ChiTietPhieuNhap ctpnInit = new ChiTietPhieuNhap();
                ctpnInit.MaCTPN = ran.Next(99999, 1000000);
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
            if(dsCTPN.Count == 0)
            {
                MessageBox.Show("Chưa có thông tin sản phẩm cần nhập ! Vui lòng quay lại");
            }
            else
            {
                NhaCungCap ncc = db.NhaCungCap.Where(x => x.TenNCC == cbTenNCC.SelectedItem.ToString().Trim()).FirstOrDefault();

                phieu.MaPN = ran.Next(9999, 100000);
                phieu.NgayLap = DateTime.Today;
                phieu.Tinhtrang = "Đã thanh toán";
                phieu.Tongtien = tongtien;
                phieu.ChiTietPhieuNhap = dsCTPN;
                phieu.NhaCungCap = ncc;
                phieu.Calamviec = Helpers.KiemtraCalamviecHientai();

                try
                {
                    var sps = db.SanPham.ToList();
                    foreach (var item in dsSPPNSess)
                    {
                        if (!sps.Contains(item))
                        {
                            db.SanPham.Add(item);
                        }
                    }
                    db.PhieuNhap.Add(phieu);
                    db.SaveChanges();
                    MessageBox.Show("Phiếu nhập lưu thành công");
                    dgvDanhsachSPPN.Rows.Clear();
                    XoarongThongtinSP();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thông tin lỗi: " + e.ToString());
                }
               
            }    
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

            cbMausac.Items.Add("Hồng");
            cbMausac.Items.Add("Cam");
            cbMausac.Items.Add("Trắng");
            cbMausac.Items.Add("Đen");
            cbMausac.Items.Add("Vàng");
            cbMausac.Items.Add("Trắng Xám");
            cbMausac.Items.Add("Xanh xám");
            cbMausac.Items.Add("Xanh dương");
        }

        void XoarongThongtinSP()
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

        private void btnXoarongTTSP_Click(object sender, EventArgs e)
        {
            XoarongThongtinSP();
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            FormThemNCC frm = new FormThemNCC();
            frm.Show();
        }

        private void btnXoakhoiPN_Click(object sender, EventArgs e)
        {
            int index = 0;
            if (dsCTPN.Count > 0)
            {
                string masp = dgvDanhsachSPPN.CurrentRow.Cells[0].Value.ToString();
                string strThanhtien = dgvDanhsachSPPN.CurrentRow.Cells[4].Value.ToString();
                double thanhtien = double.Parse(strThanhtien);
                foreach (var cthd in dsCTPN)
                {
                    if (cthd.SanPham.MaSP == masp)
                    {
                        index = dsCTPN.IndexOf(cthd);
                    }
                }
                dsCTPN.RemoveAt(index);
                dgvDanhsachSPPN.Rows.Remove(dgvDanhsachSPPN.CurrentRow);
                tongtien -= thanhtien;
                lbTongtienPN.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongtien);
            }
            else
            {
                MessageBox.Show("Danh sách sản phẩm của hóa đơn đã rỗng");
            }
        }
    }
}
