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
    public partial class QuanLyDauSach : Form
    {
        KetNoi connectData = new KetNoi();
        bool check = false;
        String choice = "";

        public QuanLyDauSach()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);

        }

        public void loadThongTinDauSach()
        {
           
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select  ds.ISBN,tl.TenLoai,TenSach,Tacgia,NXB, SoLuong,Thongtinsach from DauSach ds  join TheLoai tl on tl.MaLoai = ds.MaLoai", connectData.GetStrConn());
            adapter.Fill(data);
            dgvDauSach.DataSource = data;

            dgvDauSach.Columns[0].HeaderText = "ISBN";
            dgvDauSach.Columns[1].HeaderText = "Tên Loại Sách";
            dgvDauSach.Columns[2].HeaderText = "Tên Sách";
            dgvDauSach.Columns[3].HeaderText = "Tác Giả";
            dgvDauSach.Columns[4].HeaderText = "NXB";
            dgvDauSach.Columns[5].HeaderText = "Số Lượng";
            dgvDauSach.Columns[6].HeaderText = "Thông Tin Sách";



            dgvDauSach.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDauSach.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDauSach.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDauSach.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDauSach.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDauSach.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvDauSach.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            /*
                        dgvDauSach.Columns[0].Width = 70;
                        dgvDauSach.Columns[6].Width = 200;*/

            dgvDauSach.ClearSelection();
            dgvDauSach.CurrentCell = null;
        }

        public void ShowListChoiceTenLoai()
        {
            choiceTenLoai.Items.Add("");
            List<TheLoai> listT1 = TheLoaiDAO.getInstance().SelectTenLoaiSach("", 1);
            foreach (TheLoai theLoai in listT1)
            {
                choiceTenLoai.Items.Add(theLoai.getTenLoai());
            }
        }

        public void CleanUI()
        {
            txtSearch.Text = "";
            txtISBN.Text = "";
            txtTenSach.Text = "";
            txtTenTacGia.Text = "";
            txtNhaXB.Text = "";
            txtSoLuong.Text = "";
            txtThongTinSach.Text = "";
            txtThongTinSach.Enabled = false;
            txtISBN.Enabled = false;
            txtTenSach.Enabled = false;
            txtTenTacGia.Enabled = false;
            txtNhaXB.Enabled = false;
            txtSoLuong.Enabled = false;
            choiceTenLoai.Enabled = false;

            rdISBN.Checked = false;
            rdTenLoaiSach.Checked = false;
            rdTenSach.Checked = false;
            rdTenTacGia.Checked = false;

            saveBtn.Visible = false;
            addBtn.Text = "Thêm";
            delBtn.Enabled = true;
            upBtn.Enabled = true;
            addBtn.Enabled = true;

            choiceTenLoai.SelectedIndex = 0;
        }


        public void DefUI()
        {
            txtSearch.Text = "";
            txtISBN.Text = "";
            txtTenSach.Text = "";
            txtTenTacGia.Text = "";
            txtNhaXB.Text = "";
            txtSoLuong.Text = "";
            txtThongTinSach.Text = "";
            txtThongTinSach.Enabled = true;
            txtISBN.Enabled = true;
            choiceTenLoai.Enabled = true;
            txtTenSach.Enabled = true;
            txtTenTacGia.Enabled = true;
            txtNhaXB.Enabled = true;
            txtSoLuong.Enabled = true;

            rdISBN.Checked = false;
            rdTenLoaiSach.Checked = false;
            rdTenSach.Checked = false;
            rdTenTacGia.Checked = false;
        }

        private void trangChuBtn_Click(object sender, EventArgs e)
        {
            TrangChu trangChu = new TrangChu();
            trangChu.Show();
            this.Hide();
        }

        private void thoatBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            DefUI();
            String textBtn = addBtn.Text.Trim();
            if (textBtn.Equals("Thêm"))
            {
                DefUI();
                addBtn.Text = "Hủy";
                delBtn.Enabled = false;
                upBtn.Enabled = false;
                saveBtn.Visible = true;
                check = true;
            }else
            {
                CleanUI();
                addBtn.Text = "Thêm";
                delBtn.Enabled = true;
                upBtn.Enabled = true;
                MessageBox.Show("Đã hủy thêm độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            String textString = upBtn.Text.Trim();
            if (textString.Equals("Sửa"))
            {
                if(dgvDauSach.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn Sửa" ,"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    upBtn.Text = "Hủy";
                    addBtn.Enabled = false;
                    delBtn.Enabled = false;
                    saveBtn.Enabled = true;
                    txtISBN.Enabled = false;
                    txtTenSach.Enabled = true;
                    txtTenTacGia.Enabled = true;
                    txtSoLuong.Enabled = true;
                    txtThongTinSach.Enabled = true;
                    txtNhaXB.Enabled = true;
                    choiceTenLoai.Enabled = true;
                    saveBtn.Visible = true;
                    check = false;
                }
            }
            else
            {
                CleanUI();
                upBtn.Text = "Sửa";
                MessageBox.Show("Đã hủy sửa độc giả thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                /*choiceTenLoai.SelectedIndex = 0;*/
                loadThongTinDauSach();
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (dgvDauSach.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo");
            }
            else
            {
                string ISBN = txtISBN.Text;
                string MaLoai = "";
                List<TheLoai> listTl = TheLoaiDAO.getInstance().SelectTenLoaiSach(choice,2);
                foreach (TheLoai theLoai in listTl)
                {
                    MaLoai = theLoai.getMaLoai();
                }
                string TenTG = txtTenTacGia.Text;
                string TenSach = txtTenSach.Text;
                string NXB = txtNhaXB.Text;
                int SoLuong = int.Parse(txtSoLuong.Text);
                string ThongTin = txtThongTinSach.Text;
                DauSach ds = new DauSach(ISBN, MaLoai, TenSach, TenTG, NXB, SoLuong, ThongTin);
                try
                {
                    DauSachDAO.getInstance().Delete(ds);
                    MessageBox.Show("Xóa Đầu sách thành công", "Thông báo");
                    loadThongTinDauSach();
                    CleanUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa Đầu sách: " + ex.Message, "Lỗi");
                }
            }
        }

        private void choiceTenLoai_SelectedValueChanged(object sender, EventArgs e)
        {
            choice = choiceTenLoai.SelectedItem.ToString();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {

/*            if(!rdISBN.Checked || !rdTenLoaiSach.Checked || !rdTenSach.Checked || !rdTenTacGia.Checked || txtSearch.Text == "")
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm và chọn mục muốn tìm theo", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }*/

            if (rdISBN.Checked && txtSearch.Text != "")
            {
                string search = txtSearch.Text;
                 DataTable data = new DataTable();
                 SqlDataAdapter adapter = new SqlDataAdapter("select  ds.ISBN,tl.TenLoai,TenSach,Tacgia,NXB, SoLuong,Thongtinsach from DauSach ds  join TheLoai tl on tl.MaLoai = ds.MaLoai where ISBN = '" + search+"'", connectData.GetStrConn());
                 adapter.Fill(data);
                 dgvDauSach.DataSource = data;
                object firstCellValue = dgvDauSach.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinDauSach();
                }

            }
            if (rdTenSach.Checked && txtSearch.Text != "")
            {
                string search = txtSearch.Text;
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select  ds.ISBN,tl.TenLoai,TenSach,Tacgia,NXB, SoLuong,Thongtinsach from DauSach ds  join TheLoai tl on tl.MaLoai = ds.MaLoai WHERE TenSach LIKE N'%" + search + "%'", connectData.GetStrConn());
                adapter.Fill(data);
                dgvDauSach.DataSource = data;
                object firstCellValue = dgvDauSach.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinDauSach();
                }
            }

            if (rdTenTacGia.Checked && txtSearch.Text != "")
            {
                string search = txtSearch.Text;
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select  ds.ISBN,tl.TenLoai,TenSach,Tacgia,NXB, SoLuong,Thongtinsach from DauSach ds  join TheLoai tl on tl.MaLoai = ds.MaLoai WHERE Tacgia LIKE N'%" + search + "%'", connectData.GetStrConn());
                adapter.Fill(data);
                dgvDauSach.DataSource = data;
                object firstCellValue = dgvDauSach.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinDauSach();
                }
            }

            if (rdTenLoaiSach.Checked && txtSearch.Text != "")
            {
                string search = txtSearch.Text;
                DataTable data = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select  ds.ISBN,tl.TenLoai,TenSach,Tacgia,NXB, SoLuong,Thongtinsach\n" +
"                        from DauSach ds  join TheLoai tl on tl.MaLoai = ds.MaLoai\n" +
"			  WHERE Tacgia LIKE N'%" + search + "%'", connectData.GetStrConn());
                adapter.Fill(data);
                dgvDauSach.DataSource = data;
                object firstCellValue = dgvDauSach.Rows[0].Cells[0].Value;
                if (firstCellValue == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinDauSach();
                }

            }

            rdISBN.Checked = false;
            rdTenLoaiSach.Checked = false;
            rdTenSach.Checked = false;
            rdTenTacGia.Checked = false;
            txtSearch.Text = "";
        }

        private void dgvDauSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Lấy chỉ số của cột được chọn
            int columnIndex = e.ColumnIndex;

            // Lấy chỉ số của hàng được chọn
            int rowIndex = e.RowIndex;

            // Chọn toàn bộ dòng được chọn
            dgvDauSach.Rows[rowIndex].Selected = true;

            if (dgvDauSach.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dgvDauSach.SelectedRows[0];
                txtISBN.Text = selectedRow.Cells[0].Value.ToString();
                txtTenTacGia.Text = selectedRow.Cells[3].Value.ToString();
                txtTenSach.Text = selectedRow.Cells[2].Value.ToString();
                txtNhaXB.Text = selectedRow.Cells[4].Value.ToString();
                txtSoLuong.Text = selectedRow.Cells[5].Value.ToString();
                txtThongTinSach.Text = selectedRow.Cells[6].Value.ToString();
                String getTenLoaiSach = selectedRow.Cells[1].Value.ToString();

                for (int i = 0; i < choiceTenLoai.Items.Count; i++)
                {
                    if (getTenLoaiSach.Equals(choiceTenLoai.Items[i].ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        choiceTenLoai.SelectedIndex = i;
                        break;
                    }
                }
                ;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (check)
            {
/*                inputMa.Enabled = true;*/
                string ISBN = txtISBN.Text;
                string MaLoai = "";
                List<TheLoai> listTl = TheLoaiDAO.getInstance().SelectTenLoaiSach(choice, 2);
                foreach (TheLoai theLoai in listTl)
                {
                    MaLoai = theLoai.getMaLoai();
                }
                string TenTG = txtTenTacGia.Text;
                string TenSach = txtTenSach.Text;
                string NXB = txtNhaXB.Text;
                int SoLuong = int.Parse(txtSoLuong.Text);
                string ThongTin = txtThongTinSach.Text;
                DauSach ds = new DauSach(ISBN, MaLoai, TenSach, TenTG, NXB, SoLuong, ThongTin);

                try
                {
                    DauSachDAO.getInstance().Insert(ds);
                    MessageBox.Show("Thêm Đầu Sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinDauSach();
                    addBtn.Text = "Thêm";
                    CleanUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                string ISBN = txtISBN.Text;
                string MaLoai = "";
                List<TheLoai> listTl = TheLoaiDAO.getInstance().SelectTenLoaiSach(choice, 2);
                foreach (TheLoai theLoai in listTl)
                {
                    MaLoai = theLoai.getMaLoai();
                }
                string TenTG = txtTenTacGia.Text;
                string TenSach = txtTenSach.Text;
                string NXB = txtNhaXB.Text;
                int SoLuong = int.Parse(txtSoLuong.Text);
                string ThongTin = txtThongTinSach.Text;
                DauSach ds = new DauSach(ISBN, MaLoai, TenSach, TenTG, NXB, SoLuong, ThongTin);

                try
                {
                    DauSachDAO.getInstance().Update(ds);
                    MessageBox.Show("Sửa Đầu Sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadThongTinDauSach();
                    upBtn.Text = "Sửa";
                    CleanUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dssBtn_Click(object sender, EventArgs e)
        {
            if (dgvDauSach.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn đầu sách muốn xem", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                String StringISBN = txtISBN.Text;
                QuanLySach mainLySach = new QuanLySach(StringISBN);

                this.Hide();
                mainLySach.Show();

            }
        }

        private void QuanLyDauSach_Load(object sender, EventArgs e)
        {

            loadThongTinDauSach();
            ShowListChoiceTenLoai();
            CleanUI();
        }

        private void dgvDauSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDauSach.ReadOnly = true;
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

        private void btnThuThu_Click(object sender, EventArgs e)
        {
            QuanLyThuThu quanLyThuThu = new QuanLyThuThu();
            this.Hide();
            quanLyThuThu.Show();
        }
    }
}
