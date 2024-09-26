using QuanLyThuVien.DAO;
using QuanLyThuVien.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class QuanLySach : Form
    {
        KetNoi connectionData = new KetNoi();
        private String ISBNChoose;
        bool check = false;

        public QuanLySach(String ISBN)
        {


            this.ISBNChoose = ISBN;

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);

        }

        public void showData()
        {
            string str = "select * \n" +
                    "from Sach  \n" +
"			 where ISBN = '" + ISBNChoose + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, connectionData.GetStrConn());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvDanhSach.DataSource = dt;

            dgvDanhSach.Columns[0].HeaderText = "Mã Sách";
            dgvDanhSach.Columns[1].HeaderText = "ISBN";
            dgvDanhSach.Columns[2].HeaderText = "Giá";
            dgvDanhSach.Columns[3].HeaderText = "Năm XB";
            dgvDanhSach.Columns[4].HeaderText = "Ghi Chú";


            dgvDanhSach.Columns[0].Width =100;
            dgvDanhSach.Columns[1].Width = 200;
            dgvDanhSach.Columns[2].Width = 200;
            dgvDanhSach.Columns[3].Width = 200;
            dgvDanhSach.Columns[4].Width = 170;


            dgvDanhSach.ClearSelection();
            dgvDanhSach.CurrentCell = null;
        }

            public void CleanUI()
        {
            txtISBN.Text = "";
            txtMaSach.Text = "";
            txtGiaSach.Text = "";
            txtNamXB.Text = "";
            txtGhiChu.Text = "";
            txtISBN.Enabled = false;
            txtMaSach.Enabled = false;
            txtGiaSach.Enabled = false;
            txtNamXB.Enabled = false;
            txtGhiChu.Enabled = false;

            saveBtn.Visible = false;
            addBtn.Text = "Thêm";
            delBtn.Enabled = true;
            upBtn.Enabled = true;
            addBtn.Enabled = true;



            rdMaSach.Checked = false;
            rdNamXB.Checked = false;

         

        }


        public void DefUI()
        {
            txtISBN.Text = ISBNChoose;
            txtMaSach.Text = "";
            txtGiaSach.Text = "";
            txtNamXB.Text = "";
            txtGhiChu.Text = "";
            txtISBN.Enabled = true;
            txtMaSach.Enabled = true;
            txtGiaSach.Enabled = true;
            txtNamXB.Enabled = true;
            txtGhiChu.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
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

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            TrangChu main = new TrangChu();
            main.Show();
            this.Hide();
        }

        private void dgvDanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDanhSach.ReadOnly = true;
        }

        private void btnDauSach_Click(object sender, EventArgs e)
        {
            QuanLyDauSach mainDauSach = new QuanLyDauSach();
            mainDauSach.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (rdMaSach.Checked)
            {
                string search = txtSearch.Text;
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Sach where MaSach = '" + search + "'", connectionData.GetStrConn());
                adapter.Fill(data);
                dgvDanhSach.DataSource = data;
                object firstCellValue = dgvDanhSach.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showData();
                }
            }
            else if (rdNamXB.Checked)
            {
                string search = txtSearch.Text;
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Sach where NamXB = '" + search + "'", connectionData.GetStrConn());
                adapter.Fill(data);
                dgvDanhSach.DataSource = data;
                object firstCellValue = dgvDanhSach.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Hình Thức Bạn Muốn Tìm Theo","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                showData();
            }


            rdMaSach.Checked= false;
            rdNamXB.Checked = false;
            txtSearch.Text = "";
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string MaSach = txtMaSach.Text;
                connectionData.ExcuteNonQuery("Delete from Sach where MaSach ='" + MaSach +"'");
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                showData();
                CleanUI();
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (addBtn.Text.Equals("Thêm", StringComparison.OrdinalIgnoreCase))
            {
                DefUI();
                txtISBN.Enabled = false;
                txtMaSach.Enabled = false;
                addBtn.Text = "Hủy";
                delBtn.Enabled = false;
                upBtn.Enabled = false;
                saveBtn.Visible = true;
                check = true;
            }
            else
            {
                CleanUI();
                addBtn.Text = "Thêm";
                delBtn.Enabled = true;
                upBtn.Enabled = true;
                MessageBox.Show("Hủy thêm Sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                showData();
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (check)
            {
                string msSach = "";
                string splitMaSach = "";
                int newMaSachInt;
                string newMaSachString = "";
                List<Sach> listNewMaSach = SachDAO.getInstance().selectNewMaSach();
                foreach (Sach sach in listNewMaSach)
                {
                    msSach = sach.getMaSach();
                }
                splitMaSach = msSach.Substring(1);
                newMaSachInt = int.Parse(splitMaSach) + 1;
                if (newMaSachInt < 10)
                {
                    newMaSachString = "S000" + newMaSachInt.ToString();
                }
                else if (newMaSachInt > 10)
                {
                    newMaSachString = "S00" + newMaSachInt.ToString();
                }
                Console.WriteLine(newMaSachString);

                int NamXB = int.Parse(txtNamXB.Text);
                string Gia = txtGiaSach.Text;
                string GhiChu = txtGhiChu.Text;
                Sach s = new Sach(newMaSachString, ISBNChoose, int.Parse(Gia), NamXB, GhiChu);

                try
                {
                    SachDAO.getInstance().Insert(s);
                    MessageBox.Show("Thêm Sách thành công" , "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showData();
                    addBtn.Text = "Thêm";
                    CleanUI();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            else
            {
                string MaSach = txtMaSach.Text;
                string NamXB = txtNamXB.Text;
                string Gia = txtGiaSach.Text;
                string GhiChu = txtGhiChu.Text;
                Sach s = new Sach(MaSach, ISBNChoose, int.Parse(Gia), int.Parse(NamXB), GhiChu);

                try
                {
                    SachDAO.getInstance().Update(s);
                    MessageBox.Show("Sửa Sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    showData();
                    upBtn.Text = "Sửa";
                    CleanUI();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            if (upBtn.Text.Equals("Sửa", StringComparison.OrdinalIgnoreCase))
            {
                if (dgvDanhSach.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn sửa");
                }
                else
                {
                    txtISBN.Enabled = false;
                    txtMaSach.Enabled = false;
                    upBtn.Text = "Hủy";
                    addBtn.Enabled = false;
                    delBtn.Enabled = false;
                    saveBtn.Enabled = true;
                    
                    txtGiaSach.Enabled = true;
                    txtNamXB.Enabled = true;
                    txtGhiChu.Enabled = true;
                    saveBtn.Visible = true;
                    check = false;
                }
            }
            else
            {
                CleanUI();
                upBtn.Text = "Sửa";
                MessageBox.Show("Hủy sửa Sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                showData();
            }
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
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
            dgvDanhSach.Rows[rowIndex].Selected = true;

            if (dgvDanhSach.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dgvDanhSach.SelectedRows[0];
                txtISBN.Text = selectedRow.Cells[1].Value.ToString();
                txtMaSach.Text = selectedRow.Cells[0].Value.ToString();
                txtGiaSach.Text = selectedRow.Cells[2].Value.ToString();
                txtNamXB.Text = selectedRow.Cells[3].Value.ToString();
                txtGhiChu.Text = selectedRow.Cells[4].Value.ToString();
            }
        }

        private void QuanLySach_Load(object sender, EventArgs e)
        {
            showData();
            CleanUI();
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
