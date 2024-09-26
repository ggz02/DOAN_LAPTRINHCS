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
    public partial class ThemPN : Form
    {
        public ThemPN()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);

            txtNgayTao.Format = DateTimePickerFormat.Custom;
            txtNgayTao.CustomFormat = "dd-MM-yyyy";
            txtNgayTao.Value = new DateTime(1900, 1, 1);

            txtNgayNhap.Format = DateTimePickerFormat.Custom;
            txtNgayNhap.CustomFormat = "dd-MM-yyyy";
            txtNgayNhap.Value = new DateTime(1900, 1, 1);

            saveBtn.Visible = false;

            cbChonDauSach.DropDownStyle = ComboBoxStyle.DropDownList;
            cbChonNamXB.DropDownStyle = ComboBoxStyle.DropDownList;




        }

        KetNoi connectionData = new KetNoi();

        public String tenDS = "";
        public String ISBN = "";
        public int NamXB = 0;
        public int GiaNhap = 0;
        public int SoLuong = 0;

        bool check = false;

        public void defUI()
        {
            cbChonDauSach.SelectedIndex = 0;
            cbChonNamXB.SelectedIndex = 0;
            valueSP.Text = "0";
        }

        public void defUI2()
        {
            txtMaNCC.Text = "";
            txtMaTT.Text = "";
            txtNgayTao.Value = new DateTime(1900, 1, 1); 
            txtNgayNhap.Value = new DateTime(1900, 1, 1); 
        }

        public void clearSelectDataGridView()
        {
            dgvListAddDauSach.ClearSelection();
            dgvListAddDauSach.CurrentCell = null;
        }

        /*public void showData()
        {
            dgvListAddDauSach.Columns.Clear();

            DataTable data = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter("select pn.SoPN, TenNCC, Ngaytao, Ngaynhap, sum(Gianhap) as TongThanhTien " +
                "from PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC " +
                "group by pn.SoPN, TenNCC, Ngaytao, Ngaynhap ", connectionData.GetStrConn());
            adapter.Fill(data);
            dgvPhieuNhap.DataSource = data;

        }*/

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

        private void qlQuanLyNhap_Click(object sender, EventArgs e)
        {
            QuanLyPhieuNhap qlPhieuNhap = new QuanLyPhieuNhap();
            qlPhieuNhap.Show();
            this.Hide();
        }

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

        private void ThemPN_Load(object sender, EventArgs e)
        {
            AddListDauSach();
        }

        private void cbChonDauSach_SelectedIndexChanged(object sender, EventArgs e)
        {

            tenDS = cbChonDauSach.GetItemText(cbChonDauSach.SelectedItem);
            cbChonNamXB.Items.Clear();
            AddListNamXB(tenDS);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            List<DauSach> listPN = DauSachDAO.getInstance().seletByConditonSoPN(tenDS);
            foreach(var ds in listPN)
            {
                ISBN = ds.GetISBN();
            }
            SoLuong = int.Parse(valueSP.Text);
            int Gia = 0;
            List<Sach> listGia = SachDAO.getInstance().selectByConditonGia(NamXB.ToString(), ISBN, 1);
            foreach(var sach in listGia) {
                Gia = sach.getGia();
            }
            GiaNhap = Gia * SoLuong;
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dgvListAddDauSach, ISBN, tenDS, NamXB, SoLuong, GiaNhap);
            dgvListAddDauSach.Rows.Add(newRow);
            clearSelectDataGridView();


            defUI();

        }

        private void dgvListAddDauSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvListAddDauSach.ReadOnly = true;
        }

        private void cbChonNamXB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChonNamXB.SelectedIndex != 0)
            {
                NamXB = int.Parse(cbChonNamXB.GetItemText(cbChonNamXB.SelectedItem));
            }
            
        }

        private void dgvListAddDauSach_CellClick(object sender, DataGridViewCellEventArgs e)
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
            dgvListAddDauSach.Rows[rowIndex].Selected = true;
           
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (dgvListAddDauSach.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DataGridViewSelectedRowCollection selectedRows = dgvListAddDauSach.SelectedRows;
                if (selectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        dgvListAddDauSach.Rows.Remove(row);
                        dgvListAddDauSach.Refresh();
                        clearSelectDataGridView();
                    }
                }
            }
        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            if (upBtn.Text.Equals("Sửa", StringComparison.OrdinalIgnoreCase))
            {
                if (dgvListAddDauSach.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    saveBtn.Visible = true;
                    upBtn.Text = "Hủy";
                    addBtn.Enabled = false;
                    delBtn.Enabled = false;

                    DataGridViewRow selectedRow = dgvListAddDauSach.SelectedRows[0];
                    string chooseISBN = selectedRow.Cells[0].Value.ToString();
                    string chooseTenDS = selectedRow.Cells[1].Value.ToString();
                    string chooseNamXB = selectedRow.Cells[2].Value.ToString();
                    string chooseSoLuong = selectedRow.Cells[3].Value.ToString();

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

                    dgvListAddDauSach.Rows.Remove(selectedRow);
                    clearSelectDataGridView();
                    dgvListAddDauSach.Refresh();

                }
            }
            else
            {
                saveBtn.Visible = false;
                upBtn.Text = "Sửa";
                addBtn.Enabled = true;
                delBtn.Enabled = true;
                MessageBox.Show("Hủy sửa sách nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                addBtn_Click(sender, e);
            }
        }

        private void saveBtnPN_Click(object sender, EventArgs e)
        {
            String msPN = "";
            String splitSoPN = "";
            int newSoPNInt;
            String newSoPNString = "";
            List<PhieuNhap> listNewSoPN = PhieuNhapDAO.getInstance().selectSoPN();
            foreach (var phieuNhap in listNewSoPN)
            {
                msPN = phieuNhap.getSoPN();
            }
            splitSoPN = msPN.Substring(2);
            newSoPNInt = int.Parse(splitSoPN) + 1;
            if (newSoPNInt < 10)
            {
                newSoPNString = "PN00" + newSoPNInt.ToString();
            }
            else if (newSoPNInt >= 10)
            {
                newSoPNString = "PN0" + newSoPNInt.ToString();
            }

            String MaNCC = txtMaNCC.Text;
            String MaTT = txtMaTT.Text;

            DateTime date = txtNgayTao.Value;
            String NgayTao = date.ToString();

            DateTime date2 = txtNgayNhap.Value;
            String NgayNhap = date2.ToString();

            PhieuNhap pn = new PhieuNhap(newSoPNString, MaNCC, MaTT, NgayTao, NgayNhap);
            try
            {
                PhieuNhapDAO.getInstance().insert(pn);
                MessageBox.Show("Thêm Phiếu Nhập Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                defUI2();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            int rowCount = dgvListAddDauSach.RowCount;
            int columnCount = dgvListAddDauSach.ColumnCount;
            string ISBN = "";
            int NamXB = 0;
            int SoLuong = 0;
            int GiaNhap = 0;


            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {

                DataGridViewRow row = dgvListAddDauSach.Rows[rowIndex];

                // Kiểm tra nếu hàng là null thì dừng lại
                if (row == null || row.IsNewRow)
                {
                    dgvListAddDauSach.Rows.Clear();
                    return;
                }

                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    object value = dgvListAddDauSach.Rows[rowIndex].Cells[columnIndex].Value;

                    if (columnIndex == 0)
                    {
                       
                        ISBN = value.ToString();
                    }
                    else if (columnIndex == 2 || columnIndex == 3 || columnIndex == 4)
                    {
                        if (value != null)
                        {
                            int intValue;
                            if (int.TryParse(value.ToString(), out intValue))
                            {
                                if (columnIndex == 2)
                                {
                                    NamXB = intValue;
                                }
                                else if (columnIndex == 3)
                                {
                                    SoLuong = intValue;
                                }
                                else if (columnIndex == 4)
                                {
                                    GiaNhap = intValue;
                                }
                            }
                        }
                    }
                }

                CTPhieuNhap ctpn = new CTPhieuNhap(newSoPNString, ISBN, NamXB, SoLuong, GiaNhap);
                try
                {
                    CTPhieuNhapDAO.getInstance().insert(ctpn);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

           


        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            List<DauSach> listPN = DauSachDAO.getInstance().seletByConditonSoPN(tenDS);
            foreach(var ds in listPN)
            {
                ISBN = ds.GetISBN();
            }
            SoLuong = int.Parse(valueSP.Text);
            int Gia = 0;
            List<Sach> listGia = SachDAO.getInstance().selectByConditonGia(NamXB.ToString(), ISBN, 1);
            foreach(Sach s in listGia)
            {
                Gia = s.getGia();
            }
            GiaNhap = Gia * SoLuong;
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dgvListAddDauSach, ISBN, tenDS, NamXB, SoLuong, GiaNhap);
            dgvListAddDauSach.Rows.Add(newRow);
            clearSelectDataGridView();
            defUI();

            upBtn.Text = "Sửa";
            saveBtn.Visible = false;
            addBtn.Enabled = true;
            delBtn.Enabled = true ;
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
