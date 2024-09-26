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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace QuanLyThuVien
{
    public partial class QuanLyPhieuNhap : Form
    {
        public QuanLyPhieuNhap()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);
            lblLoc.ForeColor = Color.FromArgb(27, 70, 139);
            lblTimKiem.ForeColor = Color.FromArgb(27, 70, 139);

            cbChonNgay.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        KetNoi connectionData = new KetNoi();
        public static string choose = "";
        List<PhieuNhapToHop> listPN = new List<PhieuNhapToHop>();


        public void LoadThongTinPhieuNhap()
        {

            dgvPhieuNhap.Columns.Clear();

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select pn.SoPN, TenNCC, Ngaytao, Ngaynhap, sum(Gianhap) as TongThanhTien " +
                "from PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC " +
                "group by pn.SoPN, TenNCC, Ngaytao, Ngaynhap ", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvPhieuNhap.DataSource = data;


            dgvPhieuNhap.Columns[0].HeaderText = "Số PN";
            dgvPhieuNhap.Columns[1].HeaderText = "Tên NCC";
            dgvPhieuNhap.Columns[2].HeaderText = "Ngày Tạo";
            dgvPhieuNhap.Columns[3].HeaderText = "Ngày Nhập";
            dgvPhieuNhap.Columns[4].HeaderText = "Tổng Thành Tiền";

            dgvPhieuNhap.Columns["Ngaytao"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhieuNhap.Columns["Ngaynhap"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvPhieuNhap.Columns[0].Width = 50;
            dgvPhieuNhap.Columns[1].Width = 220;
            dgvPhieuNhap.Columns[2].Width = 200;

            dgvPhieuNhap.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPhieuNhap.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvPhieuNhap.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



            // Thêm cột nút
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Xem PN";
            btnColumn.Name = "btnXemPN";
            btnColumn.Text = "Xem";
            btnColumn.DefaultCellStyle.BackColor = Color.White;
            btnColumn.DefaultCellStyle.Padding = new Padding(3, 5, 3, 5);
            btnColumn.UseColumnTextForButtonValue = true;
            dgvPhieuNhap.Columns.Add(btnColumn);

            dgvPhieuNhap.ClearSelection();
            dgvPhieuNhap.CurrentCell = null;


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

        private void QuanLyPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadThongTinPhieuNhap();
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
            string inputSoPn = txtSearch.Text;
            if (string.IsNullOrEmpty(inputSoPn))
            {
                MessageBox.Show("Vui lòng Nhập Số Phiếu Nhập cần tìm");
                LoadThongTinPhieuNhap();
            }
            else
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();
                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition(inputSoPn, 9);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                txtSearch.Text = "";

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
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

        private void rdHomNay_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if(choose.Equals("Ngày tạo"))
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition(date, 1);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
                    DefUI();
                }


            }
            else
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition(date, 3);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
                    DefUI();
                }
            }

        }

        private void rdThangHT_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (choose.Equals("Ngày tạo"))
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();
                DateTime today = DateTime.Today;
                string month = today.Month.ToString();

                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition(month, 2);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
                    DefUI();
                }


            }
            else
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();
                DateTime today = DateTime.Today;
                string month = today.Month.ToString();

                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition(month, 4);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
                    DefUI();
                }
            }
        }

        private void rdNamTrc_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (choose.Equals("Ngày tạo"))
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();

                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition("", 7);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
                    DefUI();
                }


            }
            else
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();


                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition("", 8);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
                    DefUI();
                }
            }
        }

        private void rdNamHT_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (choose.Equals("Ngày tạo"))
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();

                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition("", 5);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
                    DefUI();
                }


            }
            else
            {
                DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                table.Clear();


                listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition("", 6);
                foreach (var pn in listPN)
                {
                    table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                }

                dgvPhieuNhap.ClearSelection();
                dgvPhieuNhap.CurrentCell = null;
                DefUI();

                object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThongTinPhieuNhap();
                    txtSearch.Text = "";
                    DefUI();
                }
            }
        }

        private void dateFromLuaChonKhac_ValueChanged(object sender, EventArgs e)
        {
            if((!choose.Equals("") || choose == null) && rdLuaChonKhac.Checked)
            {

                    if (choose.Equals("Ngày tạo"))
                    {
                        DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                        table.Clear();
                        DateTime date = dateFromLuaChonKhac.Value;
                        string NgayFilter = date.ToString();

                        listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition(NgayFilter, 1);
                        foreach (var pn in listPN)
                        {
                            table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                        }

                        dgvPhieuNhap.ClearSelection();
                        dgvPhieuNhap.CurrentCell = null;
                        DefUI();

                        object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                        if (firstCellValue == null)
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadThongTinPhieuNhap();
                            txtSearch.Text = "";
                            DefUI();
                        }


                    }
                    else
                    {
                        DataTable table = (DataTable)dgvPhieuNhap.DataSource;
                        table.Clear();
                        DateTime date = dateFromLuaChonKhac.Value;
                        string NgayFilter = date.ToString();

                        listPN = PhieuNhapToHopDAO.getInstance().SelectByCondition(NgayFilter, 3);
                        foreach (var pn in listPN)
                        {
                            table.Rows.Add(new object[] { pn.getSoPN(), pn.getTenNCC(), pn.getNgayTao(), pn.getNgayNhap(), pn.getTongThanhTien() });
                        }

                        dgvPhieuNhap.ClearSelection();
                        dgvPhieuNhap.CurrentCell = null;
                        DefUI();

                        object firstCellValue = dgvPhieuNhap.Rows[0].Cells[0].Value;
                        if (firstCellValue == null)
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadThongTinPhieuNhap();
                            txtSearch.Text = "";
                            DefUI();
                        }
                    }
                
            }
            else
            {
                return;
            }


        }

        private void dateFromLuaChonKhac_CloseUp(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.ValueChanged += dateFromLuaChonKhac_ValueChanged;
            dateFromLuaChonKhac_ValueChanged(sender,e);
        }

        private void dateFromLuaChonKhac_DropDown(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.ValueChanged -= dateFromLuaChonKhac_ValueChanged;
        }

        private void dgvPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvPhieuNhap.ReadOnly = true;
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                string soPN = dgvPhieuNhap.Rows[e.RowIndex].Cells[0].Value.ToString();
                XemPN xemPN = new XemPN(soPN);
                this.Hide();
                xemPN.Show();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThemPN themPNBtn = new ThemPN();
            themPNBtn.Show();
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

        private void qlNCC_Click(object sender, EventArgs e)
        {
            QuanLyNCC quanLyNCC = new QuanLyNCC();
            this.Hide();
            quanLyNCC.Show();
        }
    }
}
