using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangThoiTrangKD
{
    public partial class FormMain : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public FormMain()
        {
            InitializeComponent();
            random = new Random();
            btnCloseChildform.Visible = false;
        }
        private Color SelectColors()
        {
            int index = random.Next(Colors.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(Colors.ColorList.Count);
            }
            tempIndex = index;
            string color = Colors.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectColors();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;

                    currentButton.ForeColor = Color.White;

                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitlebar.BackColor = color;
                    panelLogo.BackColor = Colors.ChangeColorBrightness(color, -0.3);
                    Colors.Primarycolor = color;
                    Colors.SecondaryColor = Colors.ChangeColorBrightness(color, -0.3);
                    btnCloseChildform.Visible = true;
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelTrangchu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }

            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }
        private void btnSanpham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormSanpham(), sender);
        }

        private void btnKhachhang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormKhachhang(), sender);
        }

        private void btnNhanvien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormNhanvien(), sender);
        }

        private void btnHoadon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormHoadon(), sender);
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormThongke(), sender);
        }

        private void btnTaikhoan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormTaikhoan(), sender);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void btnCloseChildform_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }
            private void Reset()
            {
                DisableButton();
                lblTitle.Text = "TRANG CHỦ";
                panelTitlebar.BackColor = Color.FromArgb(0, 150, 136);
                panelLogo.BackColor = Color.FromArgb(39,39,58);
                currentButton = null;
                btnCloseChildform.Visible = false;
        }

      

        //private void FormMain_Load(object sender, EventArgs e)
        // {

        //}
    }
}
