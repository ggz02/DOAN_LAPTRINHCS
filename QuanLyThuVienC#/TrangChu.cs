using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class TrangChu : Form
    {

        private QuanLyDocGia formQuanLySV;

        public TrangChu()
        {
            InitializeComponent();
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            header1.ForeColor = Color.FromArgb(27, 70, 139);
            header2.ForeColor = Color.FromArgb(27, 70, 139);


            lblDate.Text = "";
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            timer1.Start();

           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            

        }



        private void label1_Click(object sender, EventArgs e)
        {
            formQuanLySV = new QuanLyDocGia();
            formQuanLySV.Show();
            this.Hide();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Click(object sender, EventArgs e)
        {
            formQuanLySV = new QuanLyDocGia();
            formQuanLySV.Show();
            this.Hide();
        }

        private void qlDG1_Click(object sender, EventArgs e)
        {
            formQuanLySV = new QuanLyDocGia();
            formQuanLySV.Show();
            this.Hide();
        }

        private void qlDG2_Click(object sender, EventArgs e)
        {
            formQuanLySV = new QuanLyDocGia();
            formQuanLySV.Show();
            this.Hide();
        }

        private void qlDG3_Click(object sender, EventArgs e)
        {
            formQuanLySV = new QuanLyDocGia();
            formQuanLySV.Show();
            this.Hide();
        }

        private void qlMuonTra2_Click(object sender, EventArgs e)
        {
            QuanLyMuon formQuanLyMuon = new QuanLyMuon();
            formQuanLyMuon.Show();
            this.Hide();
        }

        private void qlMuonTra1_Click(object sender, EventArgs e)
        {
            QuanLyMuon formQuanLyMuon = new QuanLyMuon();
            formQuanLyMuon.Show();
            this.Hide();
        }

        private void qlMuonTra3_Click(object sender, EventArgs e)
        {
            QuanLyMuon formQuanLyMuon = new QuanLyMuon();
            formQuanLyMuon.Show();
            this.Hide();
        }

        private void qlSach1_Click(object sender, EventArgs e)
        {
            QuanLyDauSach quanLyDauSach = new QuanLyDauSach();
            quanLyDauSach.Show();
            this.Hide();
        }

        private void qlSach2_Click(object sender, EventArgs e)
        {
            QuanLyDauSach quanLyDauSach = new QuanLyDauSach();
            quanLyDauSach.Show();
            this.Hide();
        }

        private void qlSach3_Click(object sender, EventArgs e)
        {
            QuanLyDauSach quanLyDauSach = new QuanLyDauSach();
            quanLyDauSach.Show();
            this.Hide();
        }

        private void tk1_Click(object sender, EventArgs e)
        {
            ThongKe qlThongKe = new ThongKe();
            qlThongKe.Show();
            this.Hide();
        }

        private void tk3_Click(object sender, EventArgs e)
        {
            ThongKe qlThongKe = new ThongKe();
            qlThongKe.Show();
            this.Hide();
        }

        private void tk2_Click(object sender, EventArgs e)
        {
            ThongKe qlThongKe = new ThongKe();
            qlThongKe.Show();
            this.Hide();
        }

        private void nhapSach1_Click(object sender, EventArgs e)
        {
            QuanLyPhieuNhap quanLyPhieuNhap = new QuanLyPhieuNhap();
            quanLyPhieuNhap.Show();
            this.Hide();
        }

        private void nhapSach2_Click(object sender, EventArgs e)
        {
            QuanLyPhieuNhap quanLyPhieuNhap = new QuanLyPhieuNhap();
            quanLyPhieuNhap.Show();
            this.Hide();
        }

        private void nhapSach3_Click(object sender, EventArgs e)
        {
            QuanLyPhieuNhap quanLyPhieuNhap = new QuanLyPhieuNhap();
            quanLyPhieuNhap.Show();
            this.Hide();
        }
    }
}
