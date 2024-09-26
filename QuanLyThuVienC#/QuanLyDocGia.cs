using QuanLyThuVien.DAO;
using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class QuanLyDocGia : Form
    {

        KetNoi connectionData = new KetNoi();
        bool check = false;




        public void loadThongDocGia()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from DocGia", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvDocGia.DataSource = data;

            dgvDocGia.Columns[0].HeaderText = "Mã Độc Giả";
            dgvDocGia.Columns[1].HeaderText = "Họ Tên";
            dgvDocGia.Columns[2].HeaderText = "Ngày Sinh";
            dgvDocGia.Columns[3].HeaderText = "Giới Tính";
            dgvDocGia.Columns[4].HeaderText = "Điện Thoại";
            dgvDocGia.Columns[5].HeaderText = "Địa Chỉ";
            dgvDocGia.Columns[6].HeaderText = "Đối Tượng";

            dgvDocGia.Columns["NgaySinhDG"].DefaultCellStyle.Format = "dd/MM/yyyy";


            /*dgvDocGia.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;*/
            dgvDocGia.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
/*            dgvDocGia.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;*/

            dgvDocGia.Columns[0].Width = 100;
            dgvDocGia.Columns[6].Width = 200;

            dgvDocGia.ClearSelection();
            dgvDocGia.CurrentCell = null;


        }
        public QuanLyDocGia()
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

            listDT.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void QuanLySinhVien_Load(object sender, EventArgs e)
        {
            loadThongDocGia();
            cleanUI();
        }

        public void cleanUI()
        {
            txtHoTen.Text ="";
            txtDienThoai.Text = "";
            txtNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtNgaySinh.Value = new DateTime(1900,1,1);
            txtHoTen.Enabled = false;
            txtDiaChi.Enabled = false;
            txtDienThoai.Enabled = false;
            txtNgaySinh.Enabled = false;
            
            rbNam.Enabled = false;
            rbNu.Enabled = false;
            listDT.Enabled = false;
            btnSave.Visible = false;
            addBtn.Text= "Thêm";
            btnDel.Enabled = true;
            btnUpdate.Enabled  =true;
            addBtn.Enabled = true;

        }

        public void defUI()
        {
           
            txtHoTen.Text = "";
            txtNgaySinh.Text  ="";
            txtDiaChi.Text = "";
            rbNam.Checked = false;
            rbNu.Checked = false;
            txtNgaySinh.Value = new DateTime(1900, 1, 1);
            txtHoTen.Enabled = true;
            txtDiaChi.Enabled = true;
            txtDienThoai.Enabled = true;
            txtNgaySinh.Enabled=true;
            rbNam.Enabled = true;
            rbNu.Enabled=true;
            listDT.Enabled = true;
            txtHoTen.Focus();
            listDT.SelectedIndex = 0;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            String textBtn = this.addBtn.Text;
            if (textBtn.Equals("Thêm"))
            {
                defUI();
                addBtn.Text = "Hủy";
                btnDel.Enabled = false;
                btnUpdate.Enabled = false;
                btnSave.Visible = true;
                check = true;
            }else
            {
                cleanUI();
                btnDel.Enabled = true;
                btnUpdate.Enabled = true;
                MessageBox.Show("Đã hủy thêm độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (check)
            {
                //                inputMa.setEnabled(true);
                String msDG = "";
                String splitMaDG = "";
                int newMaDGInt;
                String newMaDGString = "";
                List<DocGia> listDG = new List<DocGia>();
                listDG = DocGiaDAO.getInstance().selectNewMaDG();
                foreach (DocGia docGia in listDG)
                {
                    msDG = docGia.getMaDG();
                }
                splitMaDG = msDG.Substring(2);
                newMaDGInt = int.Parse(splitMaDG) + 1;
                if (newMaDGInt > 10)
                {
                    newMaDGString = "DG0" + newMaDGInt.ToString();
                }
                else if (newMaDGInt > 100)
                {
                    newMaDGString = "DG" + newMaDGInt.ToString();
                }
                String HoTen = txtHoTen.Text;
                

                DateTime date = txtNgaySinh.Value;
                String NgaySinh =date.ToString();
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
                String DoiTuong = "";
                string selected = this.listDT.GetItemText(this.listDT.SelectedItem);
                if (selected.Equals("Sinh Viên"))
                {
                    DoiTuong = "Sinh viên";
                }
                else if (selected.Equals("Giảng Viên"))
                {
                    DoiTuong = "Giảng viên";
                }
                DocGia DG1 = new DocGia(newMaDGString, HoTen, NgaySinh, GioiTinh, DienThoai, DiaChi, DoiTuong);

                try
                {
                    DocGiaDAO.getInstance().insert(DG1);
                    loadThongDocGia();
                    addBtn.Text="Thêm";
                    cleanUI();
                    MessageBox.Show("Thêm độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }

            else
            {
                int selectedRowIndex = dgvDocGia.SelectedCells[0].RowIndex;
                string msDG = dgvDocGia.Rows[selectedRowIndex].Cells["MaDG"].Value.ToString();
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
                String DoiTuong = "";
                string selected = this.listDT.GetItemText(this.listDT.SelectedItem);
                if (selected.Equals("Sinh Viên"))
                {
                    DoiTuong = "Sinh viên";
                }
                else if (selected.Equals("Giảng Viên"))
                {
                    DoiTuong = "Giảng viên";
                }
                DocGia DG1 = new DocGia(msDG, HoTen, NgaySinh, GioiTinh, DienThoai, DiaChi, DoiTuong);

                try
                {
                    DocGiaDAO.getInstance().Update(DG1);
                    MessageBox.Show("Sửa độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongDocGia();
                    btnUpdate.Text="Sửa";
                    cleanUI();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int selectedRowIndex = dgvDocGia.SelectedRows[0].Index;

                // Lấy dữ liệu từ các ô của dòng được chọn
                string msDG = dgvDocGia.Rows[selectedRowIndex].Cells["MaDG"].Value.ToString();
                string HoTen = dgvDocGia.Rows[selectedRowIndex].Cells["HoTenDG"].Value.ToString();
                string NgaySinh = dgvDocGia.Rows[selectedRowIndex].Cells["NgaySinhDG"].Value.ToString();
                string GioiTinh = dgvDocGia.Rows[selectedRowIndex].Cells["GioiTinhDG"].Value.ToString();
                string DienThoai = dgvDocGia.Rows[selectedRowIndex].Cells["DthoaiDG"].Value.ToString();
                string DiaChi = dgvDocGia.Rows[selectedRowIndex].Cells["DiachiDG"].Value.ToString();
                string DoiTuong = dgvDocGia.Rows[selectedRowIndex].Cells["DoiTuong"].Value.ToString();

                // Tạo đối tượng DocGia từ dữ liệu đã lấy
                DocGia DG1 = new DocGia(msDG, HoTen, NgaySinh, GioiTinh, DienThoai, DiaChi, DoiTuong);

                try
                {
                    DocGiaDAO.getInstance().Delete(DG1);
                    MessageBox.Show("Xóa Độc Giả Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongDocGia();
                    cleanUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String textBtn = this.btnUpdate.Text.Trim();

            if (textBtn.Equals("Sửa"))
            {
                if (dgvDocGia.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    btnUpdate.Text = "Hủy";
                    addBtn.Enabled = false;
                    btnDel.Enabled = false;
                    btnSave.Enabled = true;
                    txtHoTen.Enabled = true;
                    txtDiaChi.Enabled = true;
                    txtDienThoai.Enabled = true;
                    txtNgaySinh.Enabled = true;
                    rbNam.Enabled = true;
                    rbNu.Enabled = true;
                    listDT.Enabled = true;
                    btnSave.Visible = true;
                    check = false;
                }
            }
            else
            {
                cleanUI();
                btnUpdate.Text = "Sửa";
                MessageBox.Show("Hủy sửa Độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
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
            dgvDocGia.Rows[rowIndex].Selected = true;

            if (dgvDocGia.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dgvDocGia.SelectedRows[0];
                string tblMaDG = selectedRow.Cells[0].Value.ToString();
                string tblHoTenDG = selectedRow.Cells[1].Value.ToString();
                string tblNgaySinhDG = selectedRow.Cells[2].Value.ToString();
                string tblGioiTinhDG = selectedRow.Cells[3].Value.ToString();
                string tblDiaChiDG = selectedRow.Cells[5].Value.ToString();
                string tblDienThoaiDG = selectedRow.Cells[4].Value.ToString();
                string tblDoiTuongDG = selectedRow.Cells[6].Value.ToString();

                txtHoTen.Text = tblHoTenDG;
                txtNgaySinh.Value = DateTime.Parse(tblNgaySinhDG);



                if (tblGioiTinhDG.Equals("Nam", StringComparison.OrdinalIgnoreCase))
                {
                    rbNam.Checked = true;
                }
                else
                {
                    rbNu.Checked = true;
                }

                txtDienThoai.Text = tblDienThoaiDG;
                txtDiaChi.Text = tblDiaChiDG;

                if (tblDoiTuongDG.Equals("Sinh viên", StringComparison.OrdinalIgnoreCase))
                {
                    listDT.SelectedIndex = 1;
                }
                else
                {
                    listDT.SelectedIndex = 2;
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

        private void trangChuBtn_Click(object sender, EventArgs e)
        {
            TrangChu main = new TrangChu();
            main.Show();
            this.Hide();
        }

        private void dgvDocGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDocGia.ReadOnly = true;
        }

        private void qlTheBtn_Click(object sender, EventArgs e)
        {
            QuanLyThe ttv = new QuanLyThe();
            ttv.Show();
            this.Hide();

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
