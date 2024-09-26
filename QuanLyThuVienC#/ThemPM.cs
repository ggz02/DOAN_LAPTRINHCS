using QuanLyThuVien.DAO;
using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class ThemPM : Form
    {
        public ThemPM()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);

            txtNgayHH.Format = DateTimePickerFormat.Custom;
            txtNgayHH.CustomFormat = "dd-MM-yyyy";
            txtNgayHH.Value = DateTime.Today;

            txtNgayMuon.Format = DateTimePickerFormat.Custom;
            txtNgayMuon.CustomFormat = "dd-MM-yyyy";
            txtNgayMuon.Value = DateTime.Today;

            saveBtn.Visible = false;

            cbChonDauSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cbChonNamXB.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGhiChu.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        KetNoi connectionData = new KetNoi();
        bool check = false;
        public String tenDS = "";
        public String ISBN = "";
        public String MaSach = "";
        public int NamXB = 0;
        public int SoLuong = 0;
        public String GhiChu = "";

        public void AddListDauSach()
        {
            cbChonDauSach.Items.Clear();
            cbChonDauSach.Items.Add("");
            List<DauSach> listPN = DauSachDAO.getInstance().listTenSach();
            foreach (DauSach ds in listPN)
            {
                cbChonDauSach.Items.Add(ds.TenSach);
            }
        }

        public void AddListNamXB(string tenDS)
        {
            cbChonNamXB.Items.Clear();
            cbChonNamXB.Items.Add("");
            cbChonNamXB.SelectedIndex = 0;
            List<Sach> listPN = SachDAO.getInstance().selectByConditonNamXB(tenDS);
            foreach (Sach ncc in listPN)
            {
                cbChonNamXB.Items.Add(ncc.getNamXB().ToString());
            }
        }

        public void defUI()
        {
            cbChonDauSach.SelectedIndex = 0;
            cbChonNamXB.SelectedIndex = 0;
            valueSP.Text = "0";
            cbGhiChu.SelectedIndex = 0;
        }

        public void defUI2()
        {
            txtMaDG.Text = "";
            txtMaTT.Text = "";
            txtNgayHH.Value = txtNgayMuon.Value.AddDays(10);
            txtNgayMuon.Value = DateTime.Today;
        }

        public void clearSelectDataGridView()
        {
            dgvListAddMuonSach.ClearSelection();
            dgvListAddMuonSach.CurrentCell = null;
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

        private void qlQuanLyMuon_Click(object sender, EventArgs e)
        {
            QuanLyMuon qlPhieuMuon = new QuanLyMuon();
            qlPhieuMuon.Show();
            this.Hide();
        }

        private void trangChuBtn_Click(object sender, EventArgs e)
        {
            TrangChu main = new TrangChu();
            main.Show();
            this.Hide();
        }



        private void plusBtn_Click(object sender, EventArgs e)
        {
            int plusValue = int.Parse(valueSP.Text);
            plusValue += 1;
            valueSP.Text = plusValue.ToString();
        }

        private void minusBtn_Click(object sender, EventArgs e)
        {
            int minusValue = int.Parse(valueSP.Text);
            if (minusValue > 0)
            {
                minusValue -= 1;
                valueSP.Text = minusValue.ToString();
            }
        }

        private void ThemPM_Load(object sender, EventArgs e)
        {
            AddListDauSach();
        }

        private void cbChonDauSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            tenDS = cbChonDauSach.GetItemText(cbChonDauSach.SelectedItem);
            cbChonNamXB.Items.Clear();
            AddListNamXB(tenDS);
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (dgvListAddMuonSach.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DataGridViewSelectedRowCollection selectedRows = dgvListAddMuonSach.SelectedRows;
                if (selectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        dgvListAddMuonSach.Rows.Remove(row);
                        dgvListAddMuonSach.Refresh();
                        clearSelectDataGridView();
                    }
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            List<DauSach> listPM = DauSachDAO.getInstance().seletByConditonSoPN(tenDS);
            foreach (var ds in listPM)
            {
                ISBN = ds.GetISBN();
            }
            SoLuong = int.Parse(valueSP.Text);
       
            List<Sach> listGia = SachDAO.getInstance().selectByConditionMaSach(ISBN,NamXB.ToString());
            foreach (var sach in listGia)
            {
                MaSach = sach.getMaSach();
            }
            
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dgvListAddMuonSach, ISBN, tenDS,MaSach, NamXB, SoLuong);
            dgvListAddMuonSach.Rows.Add(newRow);
            clearSelectDataGridView();


            defUI();
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            if (upBtn.Text.Equals("Sửa", StringComparison.OrdinalIgnoreCase))
            {
                if (dgvListAddMuonSach.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    saveBtn.Visible = true;
                    upBtn.Text = "Hủy";
                    addBtn.Enabled = false;
                    delBtn.Enabled = false;

                    DataGridViewRow selectedRow = dgvListAddMuonSach.SelectedRows[0];
                    string chooseISBN = selectedRow.Cells[0].Value.ToString();
                    string chooseTenDS = selectedRow.Cells[1].Value.ToString();
                    string chooseMaSach = selectedRow.Cells[2].Value.ToString();
                    string chooseNamXB = selectedRow.Cells[3].Value.ToString();
                    string chooseSoLuong = selectedRow.Cells[4].Value.ToString();

                    valueSP.Text = chooseSoLuong;

                    for (int i = 0; i < cbChonDauSach.Items.Count; i++)
                    {
                        string item = cbChonDauSach.GetItemText(cbChonDauSach.Items[i]);
                        if (item.Equals(chooseTenDS, StringComparison.OrdinalIgnoreCase))
                        {
                            cbChonDauSach.SelectedIndex = i;
                            break;
                        }
                    }

                    // Chọn năm xuất bản trong ComboBox
                    for (int i = 0; i < cbChonNamXB.Items.Count; i++)
                    {
                        string item = cbChonNamXB.GetItemText(cbChonNamXB.Items[i]);
                        if (item.Equals(chooseNamXB, StringComparison.OrdinalIgnoreCase))
                        {
                            cbChonNamXB.SelectedIndex = i;
                            break;
                        }
                    }

                    dgvListAddMuonSach.Rows.Remove(selectedRow);
                    clearSelectDataGridView();
                    dgvListAddMuonSach.Refresh();

                }
            }
            else
            {
                saveBtn.Visible = false;
                upBtn.Text = "Sửa";
                addBtn.Enabled = true;
                delBtn.Enabled = true;
                MessageBox.Show("Hủy sửa mượn sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                addBtn_Click(sender, e);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            List<DauSach> listPN = DauSachDAO.getInstance().seletByConditonSoPN(tenDS);
            foreach (var ds in listPN)
            {
                ISBN = ds.GetISBN();
            }
            SoLuong = int.Parse(valueSP.Text);
            
            List<Sach> listMaSach = SachDAO.getInstance().selectByConditionMaSach(ISBN,NamXB.ToString());
            foreach (Sach s in listMaSach)
            {
                MaSach = s.getMaSach();
            }

            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dgvListAddMuonSach, ISBN, tenDS,MaSach, NamXB, SoLuong);
            dgvListAddMuonSach.Rows.Add(newRow);
            clearSelectDataGridView();
            defUI();

            upBtn.Text = "Sửa";
            saveBtn.Visible = false;
            addBtn.Enabled = true;
            delBtn.Enabled = true;
        }

        private void valueSP_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(valueSP.Text) <= 0)
            {
                minusBtn.Enabled = false;
            }else
            {
                minusBtn.Enabled = true;
            }

            if (int.Parse(valueSP.Text) >= 1)
            {
                plusBtn.Enabled = false;
            }else
            {
                plusBtn.Enabled = true;
            }
        }

        private void cbChonNamXB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChonNamXB.SelectedIndex != 0)
            {
                NamXB = int.Parse(cbChonNamXB.GetItemText(cbChonNamXB.SelectedItem));
            }
        }

        private void dgvListAddMuonSach_CellClick(object sender, DataGridViewCellEventArgs e)
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
            dgvListAddMuonSach.Rows[rowIndex].Selected = true;
        }

        private void dgvListAddMuonSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvListAddMuonSach.ReadOnly = true;
        }

        private void saveBtnPN_Click(object sender, EventArgs e)
        {
            String msPM = "";
            String splitMaPM = "";
            int newMaPMInt;
            String newMaMString = "";
            List<PhieuMuon> listNewSoPN = PhieuMuonDAO.getInstance().selectMaM();
            foreach (var phieuMuon in listNewSoPN)
            {
                msPM = phieuMuon.getMaM();
            }
            splitMaPM = msPM.Substring(2);
            newMaPMInt = int.Parse(splitMaPM) + 1;
            if (newMaPMInt < 10)
            {
                newMaMString = "PM00" + newMaPMInt.ToString();
            }
            else if (newMaPMInt > 10)
            {
                newMaMString = "PM0" + newMaPMInt.ToString();
            }


            //
            //
            String MaDG = txtMaDG.Text;
            String MaTT = txtMaTT.Text;

            DateTime date = txtNgayMuon.Value;
            String NgayMuon = date.ToString();

            DateTime date2 = txtNgayHH.Value;
            String NgayHH = date2.ToString();

            int SoLuongM = 0;

            for (int i = 0; i < dgvListAddMuonSach.RowCount; i++)
            {
                DataGridViewRow row = dgvListAddMuonSach.Rows[i];

                // Kiểm tra nếu hàng là null thì dừng lại
                if (row != null && !row.IsNewRow)
                {
                    SoLuongM += 1;
                }
            }

                string selected = this.cbGhiChu.GetItemText(this.cbGhiChu.SelectedItem);

            if (selected.Equals("Mượn về nhà"))
            {
                GhiChu = "Mượn về nhà";
            }
            else
            {
                GhiChu = "Mượn đọc tại chỗ";
            }

            PhieuMuon pm = new PhieuMuon(newMaMString, MaDG, MaTT, NgayMuon, NgayHH, SoLuongM, GhiChu);
            try
            {
                PhieuMuonDAO.getInstance().insert(pm);
                MessageBox.Show("Thêm Phiếu Mượn Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                defUI2();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            //
            int rowCount = dgvListAddMuonSach.RowCount;
            int columnCount = dgvListAddMuonSach.ColumnCount;
            String MaSach = "";

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                DataGridViewRow row = dgvListAddMuonSach.Rows[rowIndex];

                // Kiểm tra nếu hàng là null thì dừng lại
                if (row == null || row.IsNewRow)
                {
                    dgvListAddMuonSach.Rows.Clear();
                    return;
                }

                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    object value = dgvListAddMuonSach.Rows[rowIndex].Cells[columnIndex].Value;
                    if (columnIndex == 2)
                    {
                        MaSach = (String)value;
                    }

                }

                CTPhieuMuon ctpn = new CTPhieuMuon(newMaMString, MaSach);
                try
                {
                    CTPhieuMuonDAO.getInstance().insert(ctpn);
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
