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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string tkText = txtTK.Text.Trim();
            string mkText = txtMK.Text.Trim();
 
            
            if (tkText.Equals("admin") & mkText.Equals("123456"))
            {
                MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                TrangChu trangChu = new TrangChu();
                this.Hide();
                trangChu.Show();
            }else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác \n Vui lòng nhập lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTK.Text = "";
                txtMK.Text = "";
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            txtMK.PasswordChar = '*';
        }
    }
}
