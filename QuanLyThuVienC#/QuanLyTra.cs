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

namespace QuanLyThuVien
{
    public partial class QuanLyTra : Form
    {
        public QuanLyTra()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);
            lblTimKiem.ForeColor = Color.FromArgb(27, 70, 139);
            lblTimKiem.ForeColor = Color.FromArgb(27, 70, 139);

            cbChonNgay.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        KetNoi connectionData = new KetNoi();
        public static string choose = "";
        List<PhieuTra> listPT = new List<PhieuTra>();


        private void mainBtn_Click(object sender, EventArgs e)
        {
            TrangChu main = new TrangChu();
            main.Show();
            this.Hide();
        }

        private void exitBtn_Click(object sender, EventArgs e)
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

        public void LoadThongTinPhieuTra()
        {
            dgvPhieuTra.Columns.Clear();

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select * from PhieuTra", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvPhieuTra.DataSource = data;


            dgvPhieuTra.Columns[0].HeaderText = "Mã Trả";
            dgvPhieuTra.Columns[1].HeaderText = "Mã Mượn";
            dgvPhieuTra.Columns[2].HeaderText = "Ngày Trả";
            dgvPhieuTra.Columns[3].HeaderText = "Số Lượng Mượn";
            dgvPhieuTra.Columns[4].HeaderText = "Số Lượng Trả";


            dgvPhieuTra.Columns["Ngaytra"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvPhieuTra.Columns[0].Width = 90;
            dgvPhieuTra.Columns[1].Width = 90;
            dgvPhieuTra.Columns[2].Width = 180;
            /*            dgvPhieuTra.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dgvPhieuTra.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;*/
            /*dgvPhieuTra.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;*/
            dgvPhieuTra.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPhieuTra.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



            // Thêm cột nút
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Xem PT";
            btnColumn.Name = "btnXemPT";
            btnColumn.Text = "Xem";
            btnColumn.DefaultCellStyle.BackColor = Color.White;
            btnColumn.DefaultCellStyle.Padding = new Padding(3, 5, 3, 5);
            btnColumn.UseColumnTextForButtonValue = true;
            dgvPhieuTra.Columns.Add(btnColumn);

            dgvPhieuTra.ClearSelection();
            dgvPhieuTra.CurrentCell = null;
        }

        public void DefUI()
        {
            cbChonNgay.SelectedIndex = 0;
            rdHomNay.Checked = false;
            rdNamHT.Checked = false;
            rdNamTrc.Checked = false;
            rdThangHT.Checked = false;
            rdLuaChonKhac.Checked = false;
            dateFromLuaChonKhac.Format = DateTimePickerFormat.Custom;
            dateFromLuaChonKhac.CustomFormat = "dd-MM-yyyy";
            dateFromLuaChonKhac.Value = new DateTime(1900, 1, 1);
            dateFromLuaChonKhac.Visible = false;

        }



        private void QuanLyTra_Load(object sender, EventArgs e)
        {
            LoadThongTinPhieuTra();
            dateFromLuaChonKhac.Visible = false;
            dateFromLuaChonKhac.Format = DateTimePickerFormat.Custom;
            dateFromLuaChonKhac.CustomFormat = "dd-MM-yyyy";
            dateFromLuaChonKhac.Value = new DateTime(1900, 1, 1);

            rdHomNay.Enabled = false;
            rdThangHT.Enabled = false;
            rdNamHT.Enabled = false;
            rdNamTrc.Enabled = false;
            rdLuaChonKhac.Enabled = false;
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            string inputMaM = txtMaMuon.Text;
            string inputMaT = txtMaTra.Text;
            if (string.IsNullOrEmpty(inputMaM) && string.IsNullOrEmpty(inputMaT))
            {
                MessageBox.Show("Vui lòng nhập mã mượn hoặc mã trả cần tìm");
                LoadThongTinPhieuTra();
            }
            else if (string.IsNullOrEmpty(inputMaM))
            {
                DataTable table = (DataTable)dgvPhieuTra.DataSource;
                table.Clear();
                listPT = PhieuTraDAO.getInstance().SelectByCondition(inputMaT,"", 5);
                foreach (var pt in listPT)
                {
                    table.Rows.Add(new object[] { pt.getMaT(), pt.getMaM(), pt.getNgayTra(), pt.getSoLuongM(), pt.getSoLuongT() });
                }

                dgvPhieuTra.ClearSelection();
                dgvPhieuTra.CurrentCell = null;
                txtMaMuon.Text = "";
                txtMaTra.Text = "";


                object firstCellValue = dgvPhieuTra.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuTra();
                    txtMaMuon.Text = "";
                    txtMaTra.Text = "";
                }
            }
            else if (string.IsNullOrEmpty(inputMaT))
            {
                DataTable table = (DataTable)dgvPhieuTra.DataSource;
                table.Clear();
                listPT = PhieuTraDAO.getInstance().SelectByCondition(inputMaM, "", 6);
                foreach (var pt in listPT)
                {
                    table.Rows.Add(new object[] { pt.getMaT(), pt.getMaM(), pt.getNgayTra(), pt.getSoLuongM(), pt.getSoLuongT() });
                }

                dgvPhieuTra.ClearSelection();
                dgvPhieuTra.CurrentCell = null;
                txtMaMuon.Text = "";
                txtMaTra.Text = "";


                object firstCellValue = dgvPhieuTra.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuTra();
                    txtMaMuon.Text = "";
                    txtMaTra.Text = "";
                }
            }
            else
            {
                DataTable table = (DataTable)dgvPhieuTra.DataSource;
                table.Clear();
                listPT = PhieuTraDAO.getInstance().SelectByCondition(inputMaM, inputMaT, 6);
                foreach (var pt in listPT)
                {
                    table.Rows.Add(new object[] { pt.getMaT(), pt.getMaM(), pt.getNgayTra(), pt.getSoLuongM(), pt.getSoLuongT() });
                }

                dgvPhieuTra.ClearSelection();
                dgvPhieuTra.CurrentCell = null;
                txtMaMuon.Text = "";
                txtMaTra.Text = "";


                object firstCellValue = dgvPhieuTra.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuTra();
                    txtMaMuon.Text = "";
                    txtMaTra.Text = "";
                }
            }
        }

        private void rdLuaChonKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (rdLuaChonKhac.Checked)
            {
                dateFromLuaChonKhac.Visible = true;
                rdHomNay.Enabled = false;
                rdThangHT.Enabled = false;
                rdNamHT.Enabled = false;
                rdNamTrc.Enabled = false;

            }
        }

        private void rdHomNay_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (choose.Equals("Ngày trả"))
            {
                DataTable table = (DataTable)dgvPhieuTra.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                listPT = PhieuTraDAO.getInstance().SelectByCondition(date,"", 1);
                foreach (var pt in listPT)
                {
                    table.Rows.Add(new object[] { pt.getMaT(), pt.getMaM(), pt.getNgayTra(), pt.getSoLuongM(), pt.getSoLuongT() });
                }

                dgvPhieuTra.ClearSelection();
                dgvPhieuTra.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuTra.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuTra();
                    txtMaMuon.Text = "";
                    txtMaTra.Text = "";
                    DefUI();
                }


            }
            
        }

        private void rdThangHT_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (choose.Equals("Ngày trả"))
            {
                DataTable table = (DataTable)dgvPhieuTra.DataSource;
                table.Clear();
                DateTime today = DateTime.Today;
                string month = today.Month.ToString();

                listPT = PhieuTraDAO.getInstance().SelectByCondition(month,"", 2);
                foreach (var pt in listPT)
                {
                    table.Rows.Add(new object[] { pt.getMaT(), pt.getMaM(), pt.getNgayTra(), pt.getSoLuongM(), pt.getSoLuongT() });
                }

                dgvPhieuTra.ClearSelection();
                dgvPhieuTra.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuTra.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuTra();
                    txtMaMuon.Text = "";
                    txtMaTra.Text = "";
                    DefUI();
                }


            }
        }

        private void rdNamHT_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (choose.Equals("Ngày trả"))
            {
                DataTable table = (DataTable)dgvPhieuTra.DataSource;
                table.Clear();

                listPT = PhieuTraDAO.getInstance().SelectByCondition("", "",3);
                foreach (var pt in listPT)
                {
                    table.Rows.Add(new object[] { pt.getMaT(), pt.getMaM(), pt.getNgayTra(), pt.getSoLuongM(), pt.getSoLuongT() });
                }

                dgvPhieuTra.ClearSelection();
                dgvPhieuTra.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuTra.Rows[0].Cells[0].Value;
                if (firstCellValue == null || firstCellValue.ToString().Equals(""))
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuTra();
                    txtMaMuon.Text = "";
                    txtMaTra.Text = "";
                    DefUI();
                }


            }
        }

        private void rdNamTrc_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (choose.Equals("Ngày trả"))
            {
                DataTable table = (DataTable)dgvPhieuTra.DataSource;
                table.Clear();

                listPT = PhieuTraDAO.getInstance().SelectByCondition("","", 4);
                foreach (var pt in listPT)
                {
                    table.Rows.Add(new object[] { pt.getMaT(), pt.getMaM(), pt.getNgayTra(), pt.getSoLuongM(), pt.getSoLuongT() });

                }

                dgvPhieuTra.ClearSelection();
                dgvPhieuTra.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuTra.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuTra();
                    txtMaMuon.Text = "";
                    txtMaTra.Text = "";
                    DefUI();
                }


            }
        }

        private void dateFromLuaChonKhac_ValueChanged(object sender, EventArgs e)
        {
            if ((!choose.Equals("") || choose == null) && rdLuaChonKhac.Checked)
            {

                if (choose.Equals("Ngày trả"))
                {
                    DataTable table = (DataTable)dgvPhieuTra.DataSource;
                    table.Clear();
                    DateTime date = dateFromLuaChonKhac.Value;
                    string NgayFilter = date.ToString();

                    listPT = PhieuTraDAO.getInstance().SelectByCondition(NgayFilter,"", 1);
                    foreach (var pt in listPT)
                    {
                    table.Rows.Add(new object[] { pt.getMaT(), pt.getMaM(), pt.getNgayTra(), pt.getSoLuongM(), pt.getSoLuongT() });

                    }

                    dgvPhieuTra.ClearSelection();
                    dgvPhieuTra.CurrentCell = null;
                    dgvPhieuTra.Columns[1].Width = 200;
                    DefUI();

                    object firstCellValue = dgvPhieuTra.Rows[0].Cells[0].Value;
                    if (firstCellValue == null)
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadThongTinPhieuTra();
                        txtMaMuon.Text = "";
                        txtMaTra.Text = "";
                        DefUI();
                    }


                }
            }
            
          }

        private void dateFromLuaChonKhac_DropDown(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.ValueChanged -= dateFromLuaChonKhac_ValueChanged;
        }

        private void dateFromLuaChonKhac_CloseUp(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.ValueChanged += dateFromLuaChonKhac_ValueChanged;
            dateFromLuaChonKhac_ValueChanged(sender, e);
        }

        private void cbChonNgay_SelectedIndexChanged(object sender, EventArgs e)
        {
            choose = cbChonNgay.SelectedItem.ToString();
            if (choose.Equals("") || choose == null)
            {
                rdHomNay.Enabled = false;
                rdThangHT.Enabled = false;
                rdNamHT.Enabled = false;
                rdNamTrc.Enabled = false;
                rdLuaChonKhac.Enabled = false;
            }
            else
            {
                rdHomNay.Enabled = true;
                rdThangHT.Enabled = true;
                rdNamHT.Enabled = true;
                rdNamTrc.Enabled = true;
                rdLuaChonKhac.Enabled = true;
            }
        }

        private void dgvPhieuTra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPhieuTra.ReadOnly = true;
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                String MaT = dgvPhieuTra.Rows[e.RowIndex].Cells[0].Value.ToString();
                XemPT xemPT = new XemPT(MaT);
                this.Hide();
                xemPT.Show();
            }
        }

        private void themPT_Click(object sender, EventArgs e)
        {
            ThemPT themPT = new ThemPT();
            themPT.Show();
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
