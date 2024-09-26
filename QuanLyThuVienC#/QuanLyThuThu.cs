using QuanLyThuVien.DAO;
using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QuanLyThuVien
{
    public partial class QuanLyThuThu : Form
    {

        KetNoi connectionData = new KetNoi();
        bool check = false;
        public QuanLyThuThu()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);
            txtNgaySinh.Format = DateTimePickerFormat.Custom;
            txtNgaySinh.CustomFormat = "dd-MM-yyyy";
            txtNgaySinh.Value = new DateTime(1900, 1, 1);
        }



        private void QuanLyThuThu_Load(object sender, EventArgs e)
        {
            loadThongTinThuThu();
            CleanUI();
        }

        public void CleanUI()
        {
            txtHoTen.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            txtNgaySinh.Value = new DateTime(1900,1,1);
            txtHoTen.Enabled = false;
            txtDienThoai.Enabled = false;
            txtDiaChi.Enabled = false;
            txtNgaySinh.Enabled = false;
            rbNam.Enabled = false;
            rbNu.Enabled = false;
            btnSave.Visible = false;
            btnAdd.Text = "Thêm";
            btnDel.Enabled = true;
            btnUpdate.Enabled = true;
            btnAdd.Enabled = true;
        }

        public void DefUI()
        {
            txtHoTen.Text = "";
            txtNgaySinh.Text = "";
            txtDiaChi.Text = "";
            rbNam.Checked = false;
            rbNu.Checked = false;
            txtNgaySinh.Value = new DateTime(1900, 1, 1);
            txtHoTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtDienThoai.Enabled = true;
            txtNgaySinh.Enabled = true;
            rbNam.Enabled = true;
            rbNu.Enabled = true;
            txtHoTen.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String textBtn = this.btnUpdate.Text.Trim();

            if (textBtn.Equals("Sửa"))
            {
                if (dgvThuThu.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn sửa");
                }
                else
                {
                    btnUpdate.Text = "Hủy";
                    btnAdd.Enabled = false;
                    btnDel.Enabled = false;
                    btnSave.Enabled = true;
                    txtHoTen.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtDienThoai.Enabled = true;
                    txtNgaySinh.Enabled = true;
                    rbNam.Enabled = true;
                    rbNu.Enabled = true;
                    btnSave.Visible = true;
                    check = false;
                }
            }
            else
            {
                CleanUI();
                btnUpdate.Text = "Sửa";
                MessageBox.Show("Hủy sửa thu thư thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void loadThongTinThuThu()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from ThuThu", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvThuThu.DataSource = data;

            dgvThuThu.Columns[0].HeaderText = "Mã TT";
            dgvThuThu.Columns[1].HeaderText = "Họ Tên";
            dgvThuThu.Columns[2].HeaderText = "Ngày Sinh";
            dgvThuThu.Columns[3].HeaderText = "Giới Tính";
            dgvThuThu.Columns[4].HeaderText = "Điện Thoại";
            dgvThuThu.Columns[5].HeaderText = "Địa Chỉ";

            dgvThuThu.Columns["NgaySinhTT"].DefaultCellStyle.Format = "dd/MM/yyyy";



 
            dgvThuThu.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvThuThu.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvThuThu.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            dgvThuThu.Columns[0].Width =80;
            dgvThuThu.Columns[1].Width = 300;
            dgvThuThu.Columns[5].Width =300;

            dgvThuThu.ClearSelection();
            dgvThuThu.CurrentCell = null;

        }

        private void dgvThuThu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvThuThu.ReadOnly = true;
        }

        private void trangChuBtn_Click(object sender, EventArgs e)
        {
            TrangChu mainTrangChu = new TrangChu();
            mainTrangChu.Show();
            this.Hide();
        }

        private void dgvThuThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            // Lấy chỉ số của cột được chọn
            int columnIndex = e.ColumnIndex;

            // Lấy chỉ số của hàng được chọn
            int rowIndex = e.RowIndex;

            // Chọn toàn bộ dòng được chọn
            dgvThuThu.Rows[rowIndex].Selected = true;
            if (dgvThuThu.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dgvThuThu.SelectedRows[0];
                txtHoTen.Text = selectedRow.Cells[1].Value.ToString();
                txtDiaChi.Text = selectedRow.Cells[5].Value.ToString();
                txtDienThoai.Text = selectedRow.Cells[4].Value.ToString();
                txtNgaySinh.Text = selectedRow.Cells[2].Value.ToString();
                string tblGioiTinhDG = selectedRow.Cells[3].Value.ToString();

                if (tblGioiTinhDG.Equals("Nam", StringComparison.OrdinalIgnoreCase))
                {
                    rbNam.Checked = true;
                }
                else
                {
                    rbNu.Checked = true;
                }
            }

        }

        private void thoatBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                this.Close();
            }
            else if (result == DialogResult.Cancel)
            {
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvThuThu.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int selectedRowIndex = dgvThuThu.SelectedRows[0].Index;
                string MaTT = dgvThuThu.Rows[selectedRowIndex].Cells["MaTT"].Value.ToString();
                connectionData.ExcuteNonQuery("Delete from ThuThu where MaTT ='" + MaTT + "'");
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadThongTinThuThu();
                CleanUI();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String textBtn = this.btnAdd.Text;
            if (textBtn.Equals("Thêm"))
            {
                DefUI();
                btnAdd.Text = "Hủy";
                btnDel.Enabled = false;
                btnUpdate.Enabled = false;
                btnSave.Visible = true;
                check = true;
            }
            else
            {
                CleanUI();
                btnDel.Enabled = true;
                btnUpdate.Enabled = true;
                MessageBox.Show("Đã hủy thêm thu thư thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (check)
            {
                String msTT = "";
                String splitMaTT = "";
                int newMaTTInt;
                String newMaTTString = "";
                List<ThuThu> listTT = new List<ThuThu>();
                listTT = ThuThuDAO.getInstance().selectNewMaDG();
                foreach (ThuThu tt in listTT)
                {
                    msTT = tt.getMaTT();
                }
                splitMaTT = msTT.Substring(2);
                newMaTTInt = int.Parse(splitMaTT) + 1;
                if (newMaTTInt > 10)
                {
                    newMaTTString = "TT0" + newMaTTInt.ToString();
                }
                else if (newMaTTInt > 100)
                {
                    newMaTTString = "TT" + newMaTTInt.ToString();
                }
                String HoTen = txtHoTen.Text;


                DateTime date = txtNgaySinh.Value;
                String NgaySinh = date.ToString();
                String GioiTinh = rbNam.Checked ? "Nam" : "Nữ";
                if (rbNam.Checked)
                {
                    GioiTinh = "Nam";
                }
                else
                {
                    GioiTinh = "Nữ";
                }
                String DienThoai = txtDienThoai.Text;
                String DiaChi = txtDiaChi.Text;
                ThuThu TT1 = new ThuThu(newMaTTString, HoTen, NgaySinh, GioiTinh, DienThoai, DiaChi);

                try
                {
                    ThuThuDAO.getInstance().insert(TT1);

                    btnAdd.Text = "Thêm";
                    CleanUI();
                    MessageBox.Show("Thêm thu thư thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinThuThu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            else
            {
                int selectedRowIndex = dgvThuThu.SelectedCells[0].RowIndex;
                string msTT = dgvThuThu.Rows[selectedRowIndex].Cells["MaTT"].Value.ToString();
                String HoTen = txtHoTen.Text;
                DateTime date = txtNgaySinh.Value;
                String NgaySinh = date.ToString();
                String GioiTinh = "";
                if (rbNam.Checked)
                {
                    GioiTinh = "Nam";
                }
                else
                {
                    GioiTinh = "Nữ";
                }
                String DienThoai = txtDienThoai.Text;
                String DiaChi = txtDiaChi.Text;
                ThuThu thuThu = new ThuThu(msTT, HoTen, NgaySinh, GioiTinh, DienThoai, DiaChi);

                try
                {
                    ThuThuDAO.getInstance().Update(thuThu);
                    MessageBox.Show("Sửa thu thư thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnUpdate.Text = "Sửa";
                    CleanUI();
                    loadThongTinThuThu();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }

        private void trangChuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrangChu trangChu = new TrangChu();
            trangChu.Show();
            this.Hide();
        }

        private void danhSachDGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyDocGia formDocGia = new QuanLyDocGia();
            formDocGia.Show();
            this.Hide();
        }

        private void danhSachTheThuVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyThe formQuanLyThe = new QuanLyThe();
            formQuanLyThe.Show();
            this.Hide();
        }

        private void danhSachThuThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyThuThu formQuanLyThuThu = new QuanLyThuThu();
            formQuanLyThuThu.Show();
            this.Hide();
        }

        private void danhMucDauSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyDauSach quanLyDauSach = new QuanLyDauSach();
            quanLyDauSach.Show();
            this.Hide();
        }

        private void danhSachPhieuMuonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyMuon quanLyMuon = new QuanLyMuon();
            quanLyMuon.Show();
            this.Hide();
        }

        private void themPhieuMuonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemPM themPM = new ThemPM();
            themPM.Show();
            this.Hide();
        }

        private void danhSachTraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyTra quanLyTra = new QuanLyTra();
            quanLyTra.Show();
            this.Hide();
        }

        private void themPhieuTraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemPT themPT = new ThemPT();
            themPT.Show();
            this.Hide();
        }

        private void danhMucNhapSachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyPhieuNhap quanLyPhieuNhap = new QuanLyPhieuNhap();
            quanLyPhieuNhap.Show();
            this.Hide();
        }

        private void themPhieuNhapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemPN themPN = new ThemPN();
            themPN.Show();
            this.Hide();
        }

        private void danhMucNhaCungCapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuanLyNCC quanLyNCC = new QuanLyNCC();
            quanLyNCC.Show();
            this.Hide();
        }

        private void thongKeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ThongKe thongKe = new ThongKe();
            thongKe.Show();
            this.Hide();
        }

        private void qlSachBtn_Click(object sender, EventArgs e)
        {
            QuanLyDauSach quanLyDauSach = new QuanLyDauSach();
            this.Hide();
            quanLyDauSach.Show();
        }
    }
}
