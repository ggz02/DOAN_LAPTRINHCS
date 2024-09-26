using QuanLyThuVien.DAO;
using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class QuanLyNCC : Form
    {

        KetNoi connectionData = new KetNoi();
        bool check = false;
        public QuanLyNCC()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);
        }

        public void CleanUI()
        {
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtWebsite.Text = "";

            txtTenNCC.Enabled = false;
            txtDiaChi.Enabled = false;
            txtDienThoai.Enabled = false;
            txtWebsite.Enabled = false;

            btnSave.Visible = false;
            btnAdd.Text = "Thêm";
            btnDel.Enabled = true;
            btnUpdate.Enabled = true;
            btnAdd.Enabled = true;
        }

        public void DefUI()
        {
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtWebsite.Text = "";

            txtTenNCC.Enabled = true;
            txtDienThoai.Enabled = true;
            txtDiaChi.Enabled = true;
            txtWebsite.Enabled = true;
            txtTenNCC.Focus();
        }





        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvNCC.ReadOnly = true;
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
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
            dgvNCC.Rows[rowIndex].Selected = true;


            if (dgvNCC.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dgvNCC.SelectedRows[0];
                txtTenNCC.Text = selectedRow.Cells[1].Value.ToString();
                txtDiaChi.Text = selectedRow.Cells[2].Value.ToString();
                txtDienThoai.Text = selectedRow.Cells[3].Value.ToString();
                String urlWebsite = selectedRow.Cells[4].Value.ToString();
                txtWebsite.Text = urlWebsite;


                if (urlWebsite != null || urlWebsite.Equals( ""))
                {

                    DialogResult result = MessageBox.Show("Bạn có muốn truy cập vào trang web không ", "Yêu cầu truy cập", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.OK)
                    {
                        ProcessStartInfo sInfo = new ProcessStartInfo(urlWebsite);
                        Process.Start(sInfo);
                    }
                    else if (result == DialogResult.Cancel)
                    {
                    }
                    
                }
                
            }
        }

        private void QuanLyNCC_Load(object sender, EventArgs e)
        {
            showData();
            CleanUI();
        }

        public void showData()
        {
            string str = "select * \n" +
                    "from Nhacungcap  ";
            SqlDataAdapter da = new SqlDataAdapter(str, connectionData.GetStrConn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvNCC.DataSource = dt;

            dgvNCC.Columns[0].HeaderText = "Mã NCC";
            dgvNCC.Columns[1].HeaderText = "Tên NCC";
            dgvNCC.Columns[2].HeaderText = "Địa Chỉ";
            dgvNCC.Columns[3].HeaderText = "Số ĐT";
            dgvNCC.Columns[4].HeaderText = "Website";


            dgvNCC.Columns[0].Width = 80;
            dgvNCC.Columns[1].Width = 300;
            dgvNCC.Columns[2].Width = 200;
            dgvNCC.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvNCC.Columns[4].Width = 170;


            dgvNCC.ClearSelection();
            dgvNCC.CurrentCell = null;
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

        private void trangChuBtn_Click(object sender, EventArgs e)
        {
            TrangChu main = new TrangChu();
            main.Show();
            this.Hide();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvNCC.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int selectedRowIndex = dgvNCC.SelectedRows[0].Index;
                string MaNCC = dgvNCC.Rows[selectedRowIndex].Cells["MaNCC"].Value.ToString();
                connectionData.ExcuteNonQuery("Delete from Nhacungcap where MaNCC ='" + MaNCC + "'");
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                showData();
                CleanUI();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String textBtn = this.btnUpdate.Text.Trim();

            if (textBtn.Equals("Sửa"))
            {
                if (dgvNCC.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn sửa");
                }
                else
                {
                    btnUpdate.Text = "Hủy";
                    btnAdd.Enabled = false;
                    btnDel.Enabled = false;
                    btnSave.Enabled = true;
                    txtTenNCC.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtDienThoai.Enabled = true;
                    txtWebsite.Enabled = true;

                    btnSave.Visible = true;
                    check = false;
                }
            }
            else
            {
                CleanUI();
                btnUpdate.Text = "Sửa";
                MessageBox.Show("Hủy sửa nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Đã hủy thêm nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (check)
            {
                String msNCC = "";
                String splitMaNCC = "";
                int newMaNCCInt;
                String newMaNCCString = "";
                List<NhaCungCap> listNCC = new List<NhaCungCap>();
                listNCC = NhaCungCapDAO.getInstance().selectNewMaNCC();
                foreach (NhaCungCap ncc in listNCC)
                {
                    msNCC = ncc.getMaNCC();
                }
                splitMaNCC = msNCC.Substring(3);
                newMaNCCInt = int.Parse(splitMaNCC) + 1;
                if (newMaNCCInt > 10)
                {
                    newMaNCCString = "NCC0" + newMaNCCInt.ToString();
                }
                else if (newMaNCCInt < 10)
                {
                    newMaNCCString = "NCC" + newMaNCCInt.ToString();
                }
                String tenNCC = txtTenNCC.Text;
                String DienThoai = txtDienThoai.Text;
                String DiaChi = txtDiaChi.Text;
                String Website = txtWebsite.Text;
                NhaCungCap ncc1 = new NhaCungCap(newMaNCCString, tenNCC, DiaChi, DienThoai, Website);

                try
                {
                    NhaCungCapDAO.getInstance().insert(ncc1);

                    btnAdd.Text = "Thêm";
                    CleanUI();
                    MessageBox.Show("Thêm nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            else
            {
                int selectedRowIndex = dgvNCC.SelectedCells[0].RowIndex;
                string msNCC = dgvNCC.Rows[selectedRowIndex].Cells["MaNCC"].Value.ToString();
                String TenNCC = txtTenNCC.Text;
                String DienThoai = txtDienThoai.Text;
                String DiaChi = txtDiaChi.Text;
                String Website = txtWebsite.Text;
                NhaCungCap ncc = new NhaCungCap(msNCC, TenNCC, DiaChi, DienThoai, Website);

                try
                {
                    NhaCungCapDAO.getInstance().Update(ncc);
                    MessageBox.Show("Sửa nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnUpdate.Text = "Sửa";
                    CleanUI();
                    showData();

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
    }
}
