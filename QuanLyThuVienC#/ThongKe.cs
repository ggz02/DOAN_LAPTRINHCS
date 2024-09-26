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
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);
            lblLoc.ForeColor = Color.FromArgb(27, 70, 139);
            lblLoc.ForeColor = Color.FromArgb(27, 70, 139);

            tabSachDangMuon.BackColor = Color.FromArgb(27, 70, 139);
            tabSachChuaMuon.BackColor = Color.FromArgb(27, 70, 139);
            tabMuonQuaTai.BackColor = Color.FromArgb(27, 70, 139);
            tabDocGiaDangMuon.BackColor = Color.FromArgb(27, 70, 139);

            dateFromLuaChonKhac.Visible = false;
        }

        KetNoi connectionData = new KetNoi();
        public int selectedIndex;

        public void defChoice()
        {
            rdHomNay.Enabled =true;
            rdNamHT.Enabled =true;
            rdNamTrc.Enabled =true;
            rdThangHT.Enabled =true;
            rdLuaChonKhac.Enabled = true;
            dateFromLuaChonKhac.Visible = false;

        }

        public void cleanChoice()
        {
            rdHomNay.Checked = false;
            rdThangHT.Checked = false;
            rdNamHT.Checked = false;
            rdNamTrc.Checked = false;
            rdLuaChonKhac.Checked = false;

        }

        public void EnableChoice()
        {
            rdHomNay.Enabled = false;
            rdNamHT.Enabled = false;
            rdNamTrc.Enabled = false;
            rdThangHT.Enabled = false;
            rdLuaChonKhac.Enabled = false;
        }

        public void loadThongTinSachDangMuon()
        {
            dgvSachDangMuon.Columns.Clear();

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT pm.MaM,s.ISBN,s.MaSach,s.NamXB,Ngayhethan " +
                        "from Sach s join CTPhieuMuon ctpm on s.MaSach = ctpm.MaSach join PhieuMuon pm on pm.MaM = ctpm.MaM " +
                        "where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 " +
                        "order by ISBN", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvSachDangMuon.DataSource = data;


            dgvSachDangMuon.Columns[0].HeaderText = "Mã Mượn";
            dgvSachDangMuon.Columns[1].HeaderText = "ISBN";
            dgvSachDangMuon.Columns[2].HeaderText = "Mã Sách";
            dgvSachDangMuon.Columns[3].HeaderText = "Năm Xuất Bản";
            dgvSachDangMuon.Columns[4].HeaderText = "Ngày Hết Hạn";


            dgvSachDangMuon.Columns["Ngayhethan"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvSachDangMuon.Columns[0].Width = 130;
            dgvSachDangMuon.Columns[1].Width = 130;
            dgvSachDangMuon.Columns[2].Width = 130;
            dgvSachDangMuon.Columns[3].Width = 130;
            dgvSachDangMuon.Columns[4].Width = 190;

            dgvSachDangMuon.ClearSelection();
            dgvSachDangMuon.CurrentCell = null;
        }

        public void loadThongTinSachChuaMuon()
        {
            dgvSachChuaMuon.Columns.Clear();

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT ds.ISBN,s.MaSach,TenSach,Tacgia,NXB,NamXB,Thongtinsach\n" +
                "from Sach s join Dausach ds on s.ISBN = ds.ISBN \n" +
                "where MaSach not in (\n" +
                "	select MaSach\n" +
                "	from CTPhieuMuon ctpm join PhieuMuon pm on ctpm.MaM = pm.MaM\n" +
                "	where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0\n" +
                ")\n" +
                "order by ISBN", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvSachChuaMuon.DataSource = data;


            dgvSachChuaMuon.Columns[0].HeaderText = "ISBN";
            dgvSachChuaMuon.Columns[1].HeaderText = "Mã Sách";
            dgvSachChuaMuon.Columns[2].HeaderText = "Tên Sách";
            dgvSachChuaMuon.Columns[3].HeaderText = "Tác Giả";
            dgvSachChuaMuon.Columns[4].HeaderText = "Nhà Xuất Bản";
            dgvSachChuaMuon.Columns[5].HeaderText = "Năm Xuất Bản";
            dgvSachChuaMuon.Columns[6].HeaderText = "Thông Tin Sách";


            dgvSachChuaMuon.Columns[6].Width = 180;
            dgvSachChuaMuon.Columns[2].Width = 180;


            /*            dgvPhieuTra.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                        dgvPhieuTra.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;*/
            /*dgvPhieuTra.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;*/


            dgvSachChuaMuon.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSachChuaMuon.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSachChuaMuon.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSachChuaMuon.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvSachChuaMuon.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;



            // Thêm cột nút
            /*DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "Xem PT";
            btnColumn.Name = "btnXemPT";
            btnColumn.Text = "Xem";
            btnColumn.DefaultCellStyle.BackColor = Color.White;
            btnColumn.DefaultCellStyle.Padding = new Padding(3, 5, 3, 5);
            btnColumn.UseColumnTextForButtonValue = true;
            dgvPhieuTra.Columns.Add(btnColumn);*/

            dgvSachChuaMuon.ClearSelection();
            dgvSachChuaMuon.CurrentCell = null;
        }

        public void loadThongDocGiaDangMuon()
        {
            dgvDGDangMuon.Columns.Clear();

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select *\n" +
                    "from DocGia\n" +
                    "where MaDG in (\n" +
                    "	select pm.MaDG\n" +
                    "	from CTPhieuMuon ctpm join PhieuMuon pm on ctpm.MaM = pm.MaM\n" +
                    "	where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0\n" +
                    ")", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvDGDangMuon.DataSource = data;


            dgvDGDangMuon.Columns[0].HeaderText = "Mã ĐG";
            dgvDGDangMuon.Columns[1].HeaderText = "Họ Tên";
            dgvDGDangMuon.Columns[2].HeaderText = "Ngày Sinh";
            dgvDGDangMuon.Columns[3].HeaderText = "Giới Tính";
            dgvDGDangMuon.Columns[4].HeaderText = "Điện Thoại";
            dgvDGDangMuon.Columns[5].HeaderText = "Đia Chỉ";
            dgvDGDangMuon.Columns[6].HeaderText = "Đối Tượng";

            dgvDGDangMuon.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvDGDangMuon.Columns[0].Width = 80;
            dgvDGDangMuon.Columns[1].Width = 150;


            dgvDGDangMuon.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDGDangMuon.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDGDangMuon.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDGDangMuon.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            dgvDGDangMuon.ClearSelection();
            dgvDGDangMuon.CurrentCell = null;
        }

        public void loadThongTinMuonQuaHan()
        {
            dgvMuonQuaHan.Columns.Clear();

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select *,DATEDIFF(DAY,Ngayhethan,GETDATE()) as N'SoNgayTre'\n" +
                        "from PhieuMuon\n" +
                        "	where DATEDIFF(DAY,Ngayhethan,GETDATE()) > 0 and MaM not in (\n" +
                        "		select MaM\n" +
                        "		from PhieuTra\n" +
                        "	)", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvMuonQuaHan.DataSource = data;


            dgvMuonQuaHan.Columns[0].HeaderText = "Mã Mượn";
            dgvMuonQuaHan.Columns[1].HeaderText = "Mã DG";
            dgvMuonQuaHan.Columns[2].HeaderText = "Mã TT";
            dgvMuonQuaHan.Columns[3].HeaderText = "Ngày Mượn";
            dgvMuonQuaHan.Columns[4].HeaderText = "Ngày HH";
            dgvMuonQuaHan.Columns[5].HeaderText = "Số Lượng";
            dgvMuonQuaHan.Columns[6].HeaderText = "Ghi Chú";
            dgvMuonQuaHan.Columns[7].HeaderText = "Số Ngày Trễ";

            dgvMuonQuaHan.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvMuonQuaHan.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";



            dgvMuonQuaHan.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMuonQuaHan.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMuonQuaHan.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMuonQuaHan.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMuonQuaHan.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMuonQuaHan.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMuonQuaHan.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMuonQuaHan.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            dgvMuonQuaHan.ClearSelection();
            dgvMuonQuaHan.CurrentCell = null;
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            cleanChoice();

            MainTabControl.SelectedIndex = 0;
            selectedIndex = 0;
            if (selectedIndex == 0)
            {
                loadThongTinSachDangMuon();
            }

            dateFromLuaChonKhac.Format = DateTimePickerFormat.Custom;
            dateFromLuaChonKhac.CustomFormat = "dd-MM-yyyy";
            dateFromLuaChonKhac.Value = DateTime.Today;

        }

        private void MainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = MainTabControl.SelectedIndex;
            if (selectedIndex ==0)
            {
                loadThongTinSachDangMuon();
                defChoice();
            }else if(selectedIndex == 1)
            {
                loadThongTinSachChuaMuon();
                EnableChoice();

            }
            else if (selectedIndex == 2)
            {
                loadThongTinMuonQuaHan();
                defChoice();

            }
            else
            {
                loadThongDocGiaDangMuon();
                defChoice();

            }

        }

        private void rdHomNay_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;

            if (rdHomNay.Checked && selectedIndex == 0) 
            {
                DataTable table = (DataTable)dgvSachDangMuon.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<SachMuonTK> listSachMuon = SachMuonTKDAO.getInstance().SelectByCondition("",0);
                foreach (var sachMuon in listSachMuon)
                {
                    table.Rows.Add(new object[] { sachMuon.getMaM(), sachMuon.getISBN(), sachMuon.getMaSach(), sachMuon.getNamXB(), sachMuon.getNgayHetHan() });
                }

                dgvSachDangMuon.ClearSelection();
                dgvSachDangMuon.CurrentCell = null;


                object firstCellValue = dgvSachDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinSachDangMuon();
                    defChoice();
                    cleanChoice();
                }
            }
            if (rdHomNay.Checked && selectedIndex == 2)
            {
                DataTable table = (DataTable)dgvMuonQuaHan.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<PhieuMuonTK> listPhieuMuon = PhieuMuonTKDAO.getInstance().SelectByCondition("", 0);
                foreach (var phieuMuon in listPhieuMuon)
                {
                    table.Rows.Add(new object[] { phieuMuon.getMaM(), phieuMuon.getMaDG(), phieuMuon.getMaTT(), phieuMuon.getNgayMuon(), phieuMuon.getNgayHetHan(),phieuMuon.getSoLuongMuon(),phieuMuon.getGhiChu(),phieuMuon.getSoNgayTre() });
                }

                dgvMuonQuaHan.ClearSelection();
                dgvMuonQuaHan.CurrentCell = null;


                object firstCellValue = dgvMuonQuaHan.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinMuonQuaHan();
                    defChoice();
                    cleanChoice();
                }
            }

            if (rdHomNay.Checked && selectedIndex == 3)
            {
                DataTable table = (DataTable)dgvDGDangMuon.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<DocGia> listDG = DocGiaDAO.getInstance().SelectByCondition("", 0);
                foreach (var docGia in listDG)
                {
                    table.Rows.Add(new object[] { docGia.getMaDG(), docGia.getHoTenDG(), docGia.getNgaySinhDG(), docGia.getGioiTinhDG(), docGia.getDienThoaiDG(), docGia.getDiaChiDG(), docGia.getDoiTuong() });
                }

                dgvDGDangMuon.ClearSelection();
                dgvDGDangMuon.CurrentCell = null;


                object firstCellValue = dgvDGDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongDocGiaDangMuon();
                    defChoice();
                    cleanChoice();
                }
            }




        }

        private void rdThangHT_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (rdThangHT.Checked && selectedIndex == 0)
            {
                DataTable table = (DataTable)dgvSachDangMuon.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<SachMuonTK> listSachMuon = SachMuonTKDAO.getInstance().SelectByCondition("", 1);
                foreach (var sachMuon in listSachMuon)
                {
                    table.Rows.Add(new object[] { sachMuon.getMaM(), sachMuon.getISBN(), sachMuon.getMaSach(), sachMuon.getNamXB(), sachMuon.getNgayHetHan() });
                }

                dgvSachDangMuon.ClearSelection();
                dgvSachDangMuon.CurrentCell = null;


                object firstCellValue = dgvSachDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinSachDangMuon();
                    defChoice();
                    cleanChoice();
                }
            }

            if (rdThangHT.Checked && selectedIndex == 2)
            {
                DataTable table = (DataTable)dgvMuonQuaHan.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<PhieuMuonTK> listPhieuMuon = PhieuMuonTKDAO.getInstance().SelectByCondition("", 1);
                foreach (var phieuMuon in listPhieuMuon)
                {
                    table.Rows.Add(new object[] { phieuMuon.getMaM(), phieuMuon.getMaDG(), phieuMuon.getMaTT(), phieuMuon.getNgayMuon(), phieuMuon.getNgayHetHan(), phieuMuon.getSoLuongMuon(), phieuMuon.getGhiChu(), phieuMuon.getSoNgayTre() });
                }

                dgvMuonQuaHan.ClearSelection();
                dgvMuonQuaHan.CurrentCell = null;


                object firstCellValue = dgvMuonQuaHan.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinMuonQuaHan();
                    defChoice();
                    cleanChoice();
                }
            }

            if (rdThangHT.Checked && selectedIndex == 3)
            {
                DataTable table = (DataTable)dgvDGDangMuon.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<DocGia> listDG = DocGiaDAO.getInstance().SelectByCondition("", 1);
                foreach (var docGia in listDG)
                {
                    table.Rows.Add(new object[] { docGia.getMaDG(), docGia.getHoTenDG(), docGia.getNgaySinhDG(), docGia.getGioiTinhDG(), docGia.getDienThoaiDG(), docGia.getDiaChiDG(), docGia.getDoiTuong() });
                }

                dgvDGDangMuon.ClearSelection();
                dgvDGDangMuon.CurrentCell = null;


                object firstCellValue = dgvDGDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongDocGiaDangMuon();
                    defChoice();
                    cleanChoice();
                }
            }
        }

        private void rdNamHT_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (rdNamHT.Checked && selectedIndex == 0)
            {
                DataTable table = (DataTable)dgvSachDangMuon.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<SachMuonTK> listSachMuon = SachMuonTKDAO.getInstance().SelectByCondition("", 2);
                foreach (var sachMuon in listSachMuon)
                {
                    table.Rows.Add(new object[] { sachMuon.getMaM(), sachMuon.getISBN(), sachMuon.getMaSach(), sachMuon.getNamXB(), sachMuon.getNgayHetHan() });
                }

                dgvSachDangMuon.ClearSelection();
                dgvSachDangMuon.CurrentCell = null;


                object firstCellValue = dgvSachDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinSachDangMuon();
                    defChoice();
                    cleanChoice();
                }
            }

            if (rdNamHT.Checked && selectedIndex == 2)
            {
                DataTable table = (DataTable)dgvMuonQuaHan.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<PhieuMuonTK> listPhieuMuon = PhieuMuonTKDAO.getInstance().SelectByCondition("", 2);
                foreach (var phieuMuon in listPhieuMuon)
                {
                    table.Rows.Add(new object[] { phieuMuon.getMaM(), phieuMuon.getMaDG(), phieuMuon.getMaTT(), phieuMuon.getNgayMuon(), phieuMuon.getNgayHetHan(), phieuMuon.getSoLuongMuon(), phieuMuon.getGhiChu(), phieuMuon.getSoNgayTre() });
                }

                dgvMuonQuaHan.ClearSelection();
                dgvMuonQuaHan.CurrentCell = null;


                object firstCellValue = dgvMuonQuaHan.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinMuonQuaHan();
                    defChoice();
                    cleanChoice();
                }
            }

            if (rdNamHT.Checked && selectedIndex == 3)
            {
                DataTable table = (DataTable)dgvDGDangMuon.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<DocGia> listDG = DocGiaDAO.getInstance().SelectByCondition("", 2);
                foreach (var docGia in listDG)
                {
                    table.Rows.Add(new object[] { docGia.getMaDG(), docGia.getHoTenDG(), docGia.getNgaySinhDG(), docGia.getGioiTinhDG(), docGia.getDienThoaiDG(), docGia.getDiaChiDG(), docGia.getDoiTuong() });
                }

                dgvDGDangMuon.ClearSelection();
                dgvDGDangMuon.CurrentCell = null;


                object firstCellValue = dgvDGDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongDocGiaDangMuon();
                    defChoice();
                    cleanChoice();
                }
            }
        }

        private void rdNamTrc_CheckedChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.Visible = false;
            if (rdNamTrc.Checked && selectedIndex == 0)
            {
                DataTable table = (DataTable)dgvSachDangMuon.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<SachMuonTK> listSachMuon = SachMuonTKDAO.getInstance().SelectByCondition("", 3);
                foreach (var sachMuon in listSachMuon)
                {
                    table.Rows.Add(new object[] { sachMuon.getMaM(), sachMuon.getISBN(), sachMuon.getMaSach(), sachMuon.getNamXB(), sachMuon.getNgayHetHan() });
                }

                dgvSachDangMuon.ClearSelection();
                dgvSachDangMuon.CurrentCell = null;


                object firstCellValue = dgvSachDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinSachDangMuon();
                    defChoice();
                    cleanChoice();
                }
            }

            if (rdNamTrc.Checked && selectedIndex == 2)
            {
                DataTable table = (DataTable)dgvMuonQuaHan.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<PhieuMuonTK> listPhieuMuon = PhieuMuonTKDAO.getInstance().SelectByCondition("", 3);
                foreach (var phieuMuon in listPhieuMuon)
                {
                    table.Rows.Add(new object[] { phieuMuon.getMaM(), phieuMuon.getMaDG(), phieuMuon.getMaTT(), phieuMuon.getNgayMuon(), phieuMuon.getNgayHetHan(), phieuMuon.getSoLuongMuon(), phieuMuon.getGhiChu(), phieuMuon.getSoNgayTre() });
                }

                dgvMuonQuaHan.ClearSelection();
                dgvMuonQuaHan.CurrentCell = null;


                object firstCellValue = dgvMuonQuaHan.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinMuonQuaHan();
                    defChoice();
                    cleanChoice();
                }
            }

            if (rdNamTrc.Checked && selectedIndex == 3)
            {
                DataTable table = (DataTable)dgvDGDangMuon.DataSource;
                table.Clear();
                DateTime now = DateTime.Now;
                string date = now.ToString("yyyy/MM/dd");

                List<DocGia> listDG = DocGiaDAO.getInstance().SelectByCondition("", 3);
                foreach (var docGia in listDG)
                {
                    table.Rows.Add(new object[] { docGia.getMaDG(), docGia.getHoTenDG(), docGia.getNgaySinhDG(), docGia.getGioiTinhDG(), docGia.getDienThoaiDG(), docGia.getDiaChiDG(), docGia.getDoiTuong() });
                }

                dgvDGDangMuon.ClearSelection();
                dgvDGDangMuon.CurrentCell = null;


                object firstCellValue = dgvDGDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongDocGiaDangMuon();
                    defChoice();
                    cleanChoice();
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

        private void dateFromLuaChonKhac_ValueChanged(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.ValueChanged -= dateFromLuaChonKhac_ValueChanged;
            if (rdLuaChonKhac.Checked && selectedIndex == 0)
            {
                DataTable table = (DataTable)dgvSachDangMuon.DataSource;
                table.Clear();
                DateTime date = dateFromLuaChonKhac.Value;
                string NgayFilter = date.ToString();

                List<SachMuonTK> listSachMuon = SachMuonTKDAO.getInstance().SelectByCondition(NgayFilter, 4);
                foreach (var sachMuon in listSachMuon)
                {
                    table.Rows.Add(new object[] { sachMuon.getMaM(), sachMuon.getISBN(), sachMuon.getMaSach(), sachMuon.getNamXB(), sachMuon.getNgayHetHan() });
                }

                dgvSachDangMuon.ClearSelection();
                dgvSachDangMuon.CurrentCell = null;


                object firstCellValue = dgvSachDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinSachDangMuon();
                    defChoice();
                    cleanChoice();
                    dateFromLuaChonKhac.Value = DateTime.Today;
                }
            }

            if (rdLuaChonKhac.Checked && selectedIndex == 2)
            {
                DataTable table = (DataTable)dgvMuonQuaHan.DataSource;
                table.Clear();
                DateTime date = dateFromLuaChonKhac.Value;
                string NgayFilter = date.ToString();

                List<PhieuMuonTK> listPhieuMuon = PhieuMuonTKDAO.getInstance().SelectByCondition(NgayFilter, 4);
                foreach (var phieuMuon in listPhieuMuon)
                {
                    table.Rows.Add(new object[] { phieuMuon.getMaM(), phieuMuon.getMaDG(), phieuMuon.getMaTT(), phieuMuon.getNgayMuon(), phieuMuon.getNgayHetHan(), phieuMuon.getSoLuongMuon(), phieuMuon.getGhiChu(), phieuMuon.getSoNgayTre() });
                }

                dgvMuonQuaHan.ClearSelection();
                dgvMuonQuaHan.CurrentCell = null;


                object firstCellValue = dgvMuonQuaHan.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinMuonQuaHan();
                    defChoice();
                    cleanChoice();
                    dateFromLuaChonKhac.Value = DateTime.Today;
                }
            }

            if (rdLuaChonKhac.Checked && selectedIndex == 3)
            {
                DataTable table = (DataTable)dgvDGDangMuon.DataSource;
                table.Clear();
                DateTime date = dateFromLuaChonKhac.Value;
                string NgayFilter = date.ToString();

                List<DocGia> listDG = DocGiaDAO.getInstance().SelectByCondition(NgayFilter, 4);
                foreach (var docGia in listDG)
                {
                    table.Rows.Add(new object[] { docGia.getMaDG(), docGia.getHoTenDG(), docGia.getNgaySinhDG(), docGia.getGioiTinhDG(), docGia.getDienThoaiDG(), docGia.getDiaChiDG(), docGia.getDoiTuong() });
                }

                dgvDGDangMuon.ClearSelection();
                dgvDGDangMuon.CurrentCell = null;


                object firstCellValue = dgvDGDangMuon.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongDocGiaDangMuon();
                    defChoice();
                    cleanChoice();
                    dateFromLuaChonKhac.Value = DateTime.Today;
                }
            }
        }

        private void dateFromLuaChonKhac_CloseUp(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.ValueChanged += dateFromLuaChonKhac_ValueChanged;
            dateFromLuaChonKhac_ValueChanged(sender, e);
        }

        private void dateFromLuaChonKhac_DropDown(object sender, EventArgs e)
        {
            dateFromLuaChonKhac.ValueChanged -= dateFromLuaChonKhac_ValueChanged;
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void btnXuatBC_Click(object sender, EventArgs e)
        {
            if (selectedIndex == 0)
            {
                DataTable data2 = new DataTable();

                data2.Columns.Add("MaM", typeof(string));
                data2.Columns.Add("ISBN", typeof(string));
                data2.Columns.Add("MaSach", typeof(string));
                data2.Columns.Add("NamXB", typeof(string));
                data2.Columns.Add("Ngayhethan", typeof(string));

                foreach (DataGridViewRow row in dgvSachDangMuon.Rows)
                    if (!row.IsNewRow && !IsRowEmpty(row))
                    {
                        string ngayHethan = Convert.ToDateTime(row.Cells[4].Value).ToString("dd/MM/yyyy");
                        data2.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, ngayHethan);
                    }
                BaoCaoSachDangMuon report = new BaoCaoSachDangMuon();
                report.SetDataSource(data2);
                fBaoCao f = new fBaoCao();
                f.crystalReportViewer1.ReportSource = report;
                f.ShowDialog();
            }

            if (selectedIndex == 1)
            {
                DataTable data2 = new DataTable();

                data2.Columns.Add("ISBN", typeof(string)); 
                data2.Columns.Add("MaSach", typeof(string)); 
                data2.Columns.Add("TenSach", typeof(string)); 
                data2.Columns.Add("Tacgia", typeof(string)); 
                data2.Columns.Add("NXB", typeof(string)); 
                data2.Columns.Add("NamXB", typeof(string)); 
                data2.Columns.Add("Thongtinsach", typeof(string));

                foreach (DataGridViewRow row in dgvSachChuaMuon.Rows)
                    if (!row.IsNewRow && !IsRowEmpty(row))
                    {
                        data2.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value, row.Cells[5].Value, row.Cells[6].Value);
                    }
                BaoCaoSachChuaMuon report = new BaoCaoSachChuaMuon();
                report.SetDataSource(data2);
                fBaoCao f = new fBaoCao();
                f.crystalReportViewer1.ReportSource = report;
                f.ShowDialog();

                //Chạy được nỳ
                //SqlConnection conn = new SqlConnection("Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=QuanLyThuVienUFM;Integrated Security=True");
                //SqlCommand command = new SqlCommand();
                //SqlDataAdapter adapter = new SqlDataAdapter();
                //command.CommandType = CommandType.StoredProcedure;
                //command.CommandText = "sachchuamuon";
                //command.Parameters.Clear();
                //command.Connection = conn;
                //adapter.SelectCommand = command;
                //DataTable dt = new DataTable();
                //adapter.Fill(dt);

                //BC r = new BC();
                //r.SetDataSource(dt);
                //fBaoCao f = new fBaoCao();
                //f.crystalReportViewer1.ReportSource = r;
                //f.ShowDialog();

            }
            if (selectedIndex == 2)
            {
                DataTable data2 = new DataTable();

                data2.Columns.Add("MaM", typeof(string));
                data2.Columns.Add("MaDG", typeof(string));
                data2.Columns.Add("MaTT", typeof(string));
                data2.Columns.Add("Ngaymuon", typeof(string));
                
                data2.Columns.Add("Ngayhethan", typeof(string));
                
                data2.Columns.Add("SoLuongM", typeof(int));
                data2.Columns.Add("Ghichu", typeof(string));
                data2.Columns.Add("SoNgayTre", typeof(int));

                foreach (DataGridViewRow row in dgvMuonQuaHan.Rows)
                {
                    // Kiểm tra nếu dòng không phải là dòng cuối cùng và không rỗng
                    if (!row.IsNewRow && !IsRowEmpty(row))
                    {
                        string ngayMuon = Convert.ToDateTime(row.Cells[3].Value).ToString("dd/MM/yyyy");
                        string ngayHetHan = Convert.ToDateTime(row.Cells[4].Value).ToString("dd/MM/yyyy");

                        data2.Rows.Add(
                            row.Cells[0].Value, 
                            row.Cells[1].Value,  
                            row.Cells[2].Value, 
                            ngayMuon,         
                            ngayHetHan,       
                            row.Cells[5].Value, 
                            row.Cells[6].Value,  
                            row.Cells[7].Value   
                        );
                    }
                }

                BaoCaoSachMuonQuaHan report = new BaoCaoSachMuonQuaHan();
                report.SetDataSource(data2);
                fBaoCao f = new fBaoCao();
                f.crystalReportViewer1.ReportSource = report;
                f.ShowDialog();
            }
            if (selectedIndex == 3)
            {
                DataTable data3 = new DataTable();

                // Thêm các cột vào DataTable data3
                data3.Columns.Add("MaDG", typeof(string)); 
                data3.Columns.Add("HoTenDG", typeof(string));
                data3.Columns.Add("NgaySinhDG", typeof(string)); 
                data3.Columns.Add("GioiTinhDG", typeof(string)); 
                data3.Columns.Add("DthoaiDG", typeof(string)); 
                data3.Columns.Add("DiachiDG", typeof(string));
                data3.Columns.Add("DoiTuong", typeof(string));


                foreach (DataGridViewRow row in dgvDGDangMuon.Rows)
                {
                    // Kiểm tra nếu dòng không phải là dòng cuối cùng và không rỗng
                    if (!row.IsNewRow && !IsRowEmpty(row))
                    {
                        string ngaySinhDG = Convert.ToDateTime(row.Cells[2].Value).ToString("dd/MM/yyyy");

                        data3.Rows.Add(
                            row.Cells[0].Value,
                            row.Cells[1].Value,
                            ngaySinhDG,
                            row.Cells[3].Value,
                            row.Cells[4].Value,
                            row.Cells[5].Value,
                            row.Cells[6].Value
                        );
                    }
                }


                BaoCaoDocGiaDangMuon report = new BaoCaoDocGiaDangMuon();
                report.SetDataSource(data3);
                fBaoCao f = new fBaoCao();
                f.crystalReportViewer1.ReportSource = report;
                f.ShowDialog();
            }

        }
        private bool IsRowEmpty(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }

}
