using QuanLyThuVien.DAO;
using QuanLyThuVien.Model;
using System;
using System.Collections;
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
    public partial class QuanLyThe : Form
    {
        public QuanLyThe()
        {
            InitializeComponent();
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);

            


        }

        KetNoi connectionData = new KetNoi();

        public void loadThongTinTheThuVien()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from TheThuVien", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvTheThuVien.DataSource = data;

            dgvTheThuVien.Columns[0].HeaderText = "Mã Thẻ";
            dgvTheThuVien.Columns[1].HeaderText = "Mã Độc Giả";
            dgvTheThuVien.Columns[2].HeaderText = "Ngày Tạo Thẻ";
            dgvTheThuVien.Columns[3].HeaderText = "Hạn Thẻ";

            dgvTheThuVien.Columns["HanThe"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvTheThuVien.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvTheThuVien.Columns[0].Width = 70;
            dgvTheThuVien.Columns[1].Width = 70;
            dgvTheThuVien.Columns[2].Width = 95;
            dgvTheThuVien.Columns[3].Width = 95;

            dgvTheThuVien.ClearSelection();
            dgvTheThuVien.CurrentCell = null;


        }

        public void loadThongTinDocGiaNew()
        {
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from DocGia where MaDG not in ( select MaDG from TheThuVien)",connectionData.GetStrConn());
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


            dgvDocGia.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDocGia.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDocGia.ClearSelection();
            dgvDocGia.CurrentCell = null;
/*            dgvDocGia.Columns[0].Width = 80;
            dgvDocGia.Columns[6].Width = 100;*/
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

        private void btnQuanLyDG_Click(object sender, EventArgs e)
        {
            QuanLyDocGia qlDocGia = new QuanLyDocGia();
            qlDocGia.Show();
            this.Hide();
        }

        public void cleanUI()
        {
            txtMaThe.Text = "";
            txtHanThe.Text = "";
            txtNgayTao.Format = DateTimePickerFormat.Custom;
            txtHanThe.Format = DateTimePickerFormat.Custom;
            txtNgayTao.CustomFormat = "dd-MM-yyyy";
            txtHanThe.CustomFormat = "dd-MM-yyyy";
            txtNgayTao.Value = new DateTime(1900, 1, 1);
            txtHanThe.Value = new DateTime(1900, 1, 1);
            txtMaThe.Enabled = false;
            txtMaDocGia.Enabled = false;
            txtNgayTao.Enabled = false;
            txtHanThe.Enabled = false;

            btnDel.Enabled = true;
            btnUp.Enabled = true;
            btnSave.Visible = false;

            rsMaDG.Text = "";
            rsHoTen.Text = "";
            rsNgaySinh.Text = "";
            rsGioiTinh.Text = "";
            rsDoiTuong.Text = "";
            rsDiaChi.Text = "";
            rsDienThoai.Text = "";

            btnUp.Text = "Sửa";



           
        }

        public void defUI()
        {
            txtMaThe.Text = "";
            txtMaDocGia.Text = "";
            txtNgayTao.Value = new DateTime(1900, 1, 1);
            txtHanThe.Value = new DateTime(1900, 1, 1);
            txtMaThe.Enabled = true;
            txtMaDocGia.Enabled = true;
            txtNgayTao.Enabled = true;
            txtHanThe.Enabled = true;
        }

        private void QuanLyThe_Load(object sender, EventArgs e)
        {
            loadThongTinTheThuVien();
            loadThongTinDocGiaNew();
            cleanUI();
        }

        private void dgvTheThuVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvTheThuVien.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dgvTheThuVien.SelectedRows[0];
                string chooseMaThe = selectedRow.Cells[0].Value.ToString();
                string chooseMaDG = selectedRow.Cells[1].Value.ToString();
                string chooseNgayTao = selectedRow.Cells[2].Value.ToString();
                string chooseHanThe = selectedRow.Cells[3].Value.ToString();

                txtMaDocGia.Text = chooseMaDG;
                txtMaThe.Text = chooseMaThe;

                txtNgayTao.Value = DateTime.Parse(chooseNgayTao);
                txtHanThe.Value = DateTime.Parse(chooseHanThe);

                List<DocGia> listDG = DocGiaDAO.getInstance().selectedById(chooseMaDG);
                foreach (DocGia docGia in listDG)
                {
                    rsMaDG.Text = docGia.getMaDG();
                    rsHoTen.Text = docGia.getHoTenDG();
                    rsNgaySinh.Text = docGia.getNgaySinhDG();
                    rsGioiTinh.Text = docGia.getGioiTinhDG();
                    rsDienThoai.Text = docGia.getDienThoaiDG();
                    rsDiaChi.Text = docGia.getDiaChiDG();
                    rsDoiTuong.Text = docGia.getDoiTuong();
                }

            }
        }

        private void dgvTheThuVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvTheThuVien.ReadOnly = true;
        }

        private void dgvDocGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDocGia.ReadOnly = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Tạo", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDocGia.ClearSelection();
                dgvDocGia.CurrentCell = null;
            }
            else
            {
                String maThe = "";
                String splitMaThe = "";
                int newMaTheInt;
                String newMaTheString = "";
                List<TheThuVien> listNewMaTTV = TheThuVienDAO.getInstance().selectNewMaTTV();
                foreach (TheThuVien theTV in listNewMaTTV)
                {
                    maThe = theTV.MaDGProperty;
                }
                splitMaThe = maThe.Substring(2);
                newMaTheInt = int.Parse(splitMaThe) + 1;
                if (newMaTheInt > 10)
                {
                    newMaTheString = "TTV" + newMaTheInt.ToString();
                }
                else if (newMaTheInt < 10)
                {
                    newMaTheString = "TTV0" + newMaTheInt.ToString();
                }

                String chooseMaDGNew = dgvDocGia.Rows[dgvDocGia.CurrentRow.Index].Cells[0].Value?.ToString();
                string doiTuongChoose = dgvDocGia.Rows[dgvDocGia.CurrentRow.Index].Cells[6].Value?.ToString();


                DateTime ngayTaoDateTime = DateTime.Now;
                string ngayTaoString = ngayTaoDateTime.ToString("yyyy/MM/dd");

                String NgayTao = ngayTaoString;
                DateTime myDate = new DateTime();

                if (doiTuongChoose.Equals("Sinh viên"))
                {
                    int newYear = DateTime.Now.Year + 4;
                    myDate = new DateTime(newYear, 12, 30);
                }
                if (doiTuongChoose.Equals("Giảng viên"))
                {
                    int newYear = DateTime.Now.Year + 5;
                    myDate = new DateTime(newYear, 12, 30);
                }


                
                string HanThe = myDate.ToString("yyyy-MM-dd");

                TheThuVien ttv = new TheThuVien(newMaTheString, chooseMaDGNew, NgayTao, HanThe);
                try
                {
                    TheThuVienDAO.getInstance().Insert(ttv);
                    MessageBox.Show("Tạo thẻ thư viện thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinTheThuVien();
                    loadThongTinDocGiaNew();
                    cleanUI();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            String textBtn = this.btnUp.Text.Trim();
            if (textBtn.Equals("Sửa")){
                if (dgvTheThuVien.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDocGia.ClearSelection();
                    dgvDocGia.CurrentCell = null;
                }
                else
                {
                    btnUp.Text = "Hủy";
                    btnDel.Enabled = false;
                    btnSave.Enabled = true;
                    txtMaThe.Enabled = false;
                    txtMaDocGia.Enabled = false;
                    txtNgayTao.Enabled = true;
                    txtHanThe.Enabled = true;
                    btnSave.Visible = true;
                }
            } else
            {
                cleanUI();
                MessageBox.Show("Hủy sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
                
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string maMaThe = txtMaThe.Text;
            string maMaDG = txtMaDocGia.Text;
            DateTime dateNT = txtNgayTao.Value;
            string NgayTao = dateNT.ToString();
            DateTime date = txtHanThe.Value;
            string HanThe = date.ToString();

            TheThuVien ttv1 = new TheThuVien(maMaThe, maMaDG, NgayTao, HanThe);
            try
            {
                TheThuVienDAO.getInstance().Update(ttv1);
                MessageBox.Show("Sửa Thẻ Thư Viện thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadThongTinTheThuVien();
                btnUp.Text = "Sửa";
                cleanUI();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvTheThuVien.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvDocGia.ClearSelection();
                dgvDocGia.CurrentCell = null;
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có muốn xóa cả thông tin độc giả tương ứng không \n", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string maThe = txtMaThe.Text;
                    string maDG = txtMaDocGia.Text;

                    DateTime dateNT = txtNgayTao.Value;
                    string NgayTao = dateNT.ToString();

                    DateTime date = txtHanThe.Value;
                    string hanThe = date.ToString();

                    TheThuVien ttv = new TheThuVien(maThe, maDG, NgayTao, hanThe);

                    foreach (DocGia docGia in DocGiaDAO.getInstance().selectAll())
                    {
                        if (maDG.Equals(docGia.getMaDG(), StringComparison.OrdinalIgnoreCase))
                        {
                            string HoTen = rsHoTen.Text;
                            string NgaySinh = rsNgaySinh.Text;
                            string GioiTinh = rsGioiTinh.Text;
                            string DienThoai = rsDienThoai.Text;
                            string DiaChi = rsDiaChi.Text;
                            string DoiTuong = rsDoiTuong.Text;
                            DocGia DG1 = new DocGia(maDG, HoTen, NgaySinh, GioiTinh, DienThoai, DiaChi, DoiTuong);

                            try
                            {
                                TheThuVienDAO.getInstance().Delete(ttv);
                                MessageBox.Show("Xóa Thẻ Thư viện và Độc Giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadThongTinTheThuVien();
                                cleanUI();
                                DocGiaDAO.getInstance().Delete(DG1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
                else if (result == DialogResult.No)
                {
                    string maThe = txtMaThe.Text;
                    string maDG = txtMaDocGia.Text;

                    DateTime dateNT = txtNgayTao.Value;
                    string NgayTao = dateNT.ToString();

                    DateTime date = txtHanThe.Value;
                    string hanThe = date.ToString();

                    TheThuVien ttv = new TheThuVien(maThe, maDG, NgayTao, hanThe);
                    try
                    {
                        TheThuVienDAO.getInstance().Delete(ttv);
                        MessageBox.Show("Xóa Thẻ Thư viện thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadThongTinTheThuVien();
                        loadThongTinDocGiaNew();
                        cleanUI();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
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
