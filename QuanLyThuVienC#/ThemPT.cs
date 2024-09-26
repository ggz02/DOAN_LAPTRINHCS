using QuanLyThuVien.DAO;
using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class ThemPT : Form
    {
        public ThemPT()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            panelHead2.BackColor = Color.FromArgb(27, 70, 139);
            panelHead1.BackColor = Color.FromArgb(27, 70, 139);
            labelHeader1.ForeColor = Color.FromArgb(27, 70, 139);
            labelHeader2.ForeColor = Color.FromArgb(27, 70, 139);
            header.ForeColor = Color.FromArgb(27, 70, 139);

            txtNgayTra.Format = DateTimePickerFormat.Custom;
            txtNgayTra.CustomFormat = "dd-MM-yyyy";
            txtNgayTra.Value = new DateTime(1900, 1, 1);


            saveBtn.Visible = false;

            cbChonNguoiMuon.DropDownStyle = ComboBoxStyle.DropDownList;
            cbChonSachMuonTra.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLoaiViPham.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        KetNoi connectionData = new KetNoi();
        bool check = false;
        public String choiceTenDG = "";
        public String choiceVP = "";
        public String tenDS = "";
        public String MaSach = "";
        public long Phat = 0;

        public string lastViPham = "";


        public void AddListNguoiMuon()
        {
            cbChonNguoiMuon.Items.Clear();
            cbChonNguoiMuon.Items.Add("");
            List<PhieuMuonToHop> listPM = PhieuMuonToHopDAO.getInstance().selectNameDGNotInPT();
            foreach (PhieuMuonToHop pm in listPM)
            {
                cbChonNguoiMuon.Items.Add(pm.getHoTen());
            }
        }

        public void AddListSach()
        {
            cbChonSachMuonTra.Items.Clear();

            cbChonSachMuonTra.Items.Add("");

            List<DauSach> listS = DauSachDAO.getInstance().selectTenSachNotInPT(choiceTenDG, 1);
            foreach (DauSach dauSach in listS)
            {
                cbChonSachMuonTra.Items.Add(dauSach.TenSach);
            }

            if (cbChonSachMuonTra.Items.Count == 1)
            {
                List<DauSach> listS1 = DauSachDAO.getInstance().selectTenSachNotInPT(choiceTenDG, 2);
                foreach (DauSach dauSach in listS1)
                {
                    cbChonSachMuonTra.Items.Add(dauSach.TenSach);
                }
            }
        }

        public void AddListVP()
        {
            cbLoaiViPham.Items.Clear();
            cbLoaiViPham.Items.Add("");
            List<LoaiVP> listVp = LoaiVPDAO.getInstance().selectTenViPham();
            foreach (var loaiVP in listVp)
            {
                cbLoaiViPham.Items.Add(loaiVP.getTenLoaiVP());
            }
        }

        public void defUI()
        {
            cbLoaiViPham.SelectedIndex = 0;
            cbChonSachMuonTra.SelectedIndex = 0;
            valueTH.Text = "0";
            txtTreHan.Visible = false;
            plusBtn.Visible = false;
            minusBtn.Visible = false;
            valueTH.Visible = false;
        }

        public void clearSelectDataGridView()
        {
            dgvListAddTraSach.ClearSelection();
            dgvListAddTraSach.CurrentCell = null;
        }



        private void trangChuBtn_Click(object sender, EventArgs e)
        {
            TrangChu main = new TrangChu();
            main.Show();
            this.Hide();
        }

        private void qlQuanLyMuon_Click(object sender, EventArgs e)
        {
            QuanLyTra qlPhieuTra = new QuanLyTra();
            qlPhieuTra.Show();
            this.Hide();
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

        private void cbChonNguoiMuon_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbChonNguoiMuon.SelectedIndex != 0)
            {

                choiceTenDG = cbChonNguoiMuon.GetItemText(cbChonNguoiMuon.SelectedItem);
                cbChonSachMuonTra.Items.Clear();
                AddListSach();
                cbChonNguoiMuon.Enabled = false;
                btnChangeDT.Visible = true;
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            if (dgvListAddTraSach.SelectedRows.Count != 1)
            {
                MessageBox.Show("Vui lòng chọn hàng muốn Xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DataGridViewSelectedRowCollection selectedRows = dgvListAddTraSach.SelectedRows;
                DataGridViewRow selectedRow = dgvListAddTraSach.SelectedRows[0];
                string TenSach = selectedRow.Cells[1].Value.ToString();
                cbChonSachMuonTra.Items.Add(TenSach);
                if (selectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in selectedRows)
                    {
                        dgvListAddTraSach.Rows.Remove(row);
                        dgvListAddTraSach.Refresh();
                        clearSelectDataGridView();
                    }
                }
               
            }
        }

        private void plusBtn_Click(object sender, EventArgs e)
        {
            int plusValue = int.Parse(valueTH.Text);
            plusValue += 1;
            valueTH.Text = plusValue.ToString();

        }

        private void minusBtn_Click(object sender, EventArgs e)
        {
            int minusValue = int.Parse(valueTH.Text);
            if (minusValue > 0)
            {
                minusValue -= 1;
                valueTH.Text = minusValue.ToString();
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            MaSach = "";
            List<CTPhieuTra> listPT = CTPhieuTraDAO.getInstance().selectMaSachNotInPT(choiceTenDG, tenDS, 1);
            foreach (var pt in listPT)
            {
                MaSach = pt.getMaSach();
            }

            if (MaSach.Equals(""))
            {
                List<CTPhieuTra> listPT2 = CTPhieuTraDAO.getInstance().selectMaSachNotInPT(choiceTenDG, tenDS, 2);
                foreach (var pt in listPT2)
                {
                    MaSach = pt.getMaSach();
                }
            }

            int Gia = 0;
            List<Sach> listSach = SachDAO.getInstance().selectByConditonGia(MaSach, "", 2);
            foreach (Sach sach in listSach)
            {
                Gia = sach.getGia();
            }
            //        System.out.println(Gia);

            if (choiceVP.Equals("Không vi phạm"))
            {
                Phat = 0;
            }
            else if (choiceVP.Equals("Mất tài liệu"))
            {
                Phat = Gia * 3;
            }
            else if (choiceVP.Equals("Tài liệu bị xé hoặc rách 1/3 trở lên"))
            {
                Phat = Gia * 2;
            }
            else if (choiceVP.Equals("Tài liệu bị bẩn, nhàu nát (ghi chép, gạch xóa, tô đậm)"))
            {
                Phat = Gia;
            }
            else if (choiceVP.Equals("Trả tài liệu trễ hạn (tài liệu mượn về nhà)"))
            {
                Phat = 1000 * int.Parse(valueTH.Text);
            }
            else
            {
                Phat = 1000 * int.Parse(valueTH.Text);
            }

            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dgvListAddTraSach, MaSach, tenDS,choiceVP, Phat);
            dgvListAddTraSach.Rows.Add(newRow);
            clearSelectDataGridView();
            cbChonSachMuonTra.Items.Remove(tenDS);
            defUI();
            


        }

        private void upBtn_Click(object sender, EventArgs e)
        {
            if (upBtn.Text.Equals("Sửa", StringComparison.OrdinalIgnoreCase))
            {
                if (dgvListAddTraSach.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Vui lòng chọn hàng muốn sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    saveBtn.Visible = true;
                    upBtn.Text = "Hủy";
                    addBtn.Enabled = false;
                    delBtn.Enabled = false;

                    DataGridViewRow selectedRow = dgvListAddTraSach.SelectedRows[0];
         
                    string chooseTenDS = selectedRow.Cells[1].Value.ToString();
                    string chooseMaSach = selectedRow.Cells[0].Value.ToString();
                    string chooseVP = selectedRow.Cells[2].Value.ToString();
                    string choosePhat = selectedRow.Cells[3].Value.ToString();

                    if (choiceVP.Equals("Trả tài liệu trễ hạn (tài liệu mượn về nhà)"))
                    {
                        txtTreHan.Visible =true;
                        minusBtn.Visible =true;
                        valueTH.Visible =true;
                        plusBtn.Visible =(true);
                        valueTH.Text = (int.Parse(choosePhat) / 1000).ToString();
                    }

                    if (choiceVP.Equals("Tài liệu mượn đọc tại chỗ nhưng hội viên cố tình đem về nhà"))
                    {
                        txtTreHan.Visible = true;
                        minusBtn.Visible = true;
                        valueTH.Visible = true;
                        plusBtn.Visible = (true);
                        valueTH.Text = (int.Parse(choosePhat) / 1000).ToString();
                    }

                    for (int i = 0; i < cbChonNguoiMuon.Items.Count; i++)
                    {
                        string item = cbChonNguoiMuon.GetItemText(cbChonNguoiMuon.Items[i]);
                        if (item.Equals(choiceTenDG, StringComparison.OrdinalIgnoreCase))
                        {
                            cbChonNguoiMuon.SelectedIndex = i;
                            break;
                        }
                    }

                    AddListSach();


                    for (int i = 0; i < cbChonSachMuonTra.Items.Count; i++)
                    {
                        string item = cbChonSachMuonTra.GetItemText(cbChonSachMuonTra.Items[i]);
                        if (item.Equals(chooseTenDS, StringComparison.OrdinalIgnoreCase))
                        {
                            cbChonSachMuonTra.SelectedIndex = i;
                            break;
                        }
                    }

                    // Chọn năm xuất bản trong ComboBox
                    for (int i = 0; i < cbLoaiViPham.Items.Count; i++)
                    {
                        string item = cbLoaiViPham.GetItemText(cbLoaiViPham.Items[i]);
                        if (item.Equals(lastViPham, StringComparison.OrdinalIgnoreCase))
                        {
                            cbLoaiViPham.SelectedIndex = i;
                            break;
                        }
                    }

                    dgvListAddTraSach.Rows.Remove(selectedRow);
                    clearSelectDataGridView();
                    dgvListAddTraSach.Refresh();

                }
            }
            else
            {
                saveBtn.Visible = false;
                upBtn.Text = "Sửa";
                addBtn.Enabled = true;
                delBtn.Enabled = true;
                MessageBox.Show("Hủy sửa trả sách thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                addBtn_Click(sender, e);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            
            List<CTPhieuTra> listPT = CTPhieuTraDAO.getInstance().selectMaSachNotInPT(choiceTenDG, tenDS, 1);
            foreach (var pt in listPT)
            {
                MaSach = pt.getMaSach();
            }
            if (MaSach.Equals(""))
            {
                List<CTPhieuTra> listPT1 = CTPhieuTraDAO.getInstance().selectMaSachNotInPT(choiceTenDG, tenDS, 2);
                foreach (var pt in listPT1)
                {
                    MaSach = pt.getMaSach();
                }
            }
            

/*            tenDS = cbChonSachMuonTra.GetItemText(cbChonSachMuonTra.SelectedIndex);

            choiceVP = cbLoaiViPham.GetItemText(cbLoaiViPham.SelectedIndex);*/

            int Gia = 0;
            List<Sach> listSach = SachDAO.getInstance().selectByConditonGia(MaSach, "", 2);
            foreach (Sach sach in listSach)
            {
                Gia = sach.getGia();
            }

            if (choiceVP.Equals("Không vi phạm"))
            {
                Phat = 0;
            }
            else if (choiceVP.Equals("Mất tài liệu"))
            {
                Phat = Gia * 3;
            }
            else if (choiceVP.Equals("Tài liệu bị xé hoặc rách 1/3 trở lên"))
            {
                Phat = Gia * 2;
            }
            else if (choiceVP.Equals("Tài liệu bị bẩn, nhàu nát (ghi chép, gạch xóa, tô đậm)"))
            {
                Phat = Gia;
            }
            else if (choiceVP.Equals("Trả tài liệu trễ hạn (tài liệu mượn về nhà)"))
            {
                Phat = 1000 * int.Parse(valueTH.Text);
            }
            else
            {
                Phat = 1000 * int.Parse(valueTH.Text);
            }


            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(dgvListAddTraSach, MaSach, tenDS, choiceVP, Phat);
            dgvListAddTraSach.Rows.Add(newRow);
            clearSelectDataGridView();
            cbChonSachMuonTra.Items.Remove(tenDS);

            defUI();

            upBtn.Text = "Sửa";
            saveBtn.Visible = false;
            addBtn.Enabled = true;
            delBtn.Enabled = true;
        }

        private void valueTH_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(valueTH.Text) <= 0)
            {
                minusBtn.Enabled = false;
            }
            else
            {
                minusBtn.Enabled = true;
            }
        }

        private void cbChonSachMuonTra_SelectedIndexChanged(object sender, EventArgs e)
        {
            tenDS = cbChonSachMuonTra.GetItemText(cbChonSachMuonTra.SelectedItem);

        }

        private void ThemPT_Load(object sender, EventArgs e)
        {
            defUI();
            AddListNguoiMuon();
            AddListVP();
            btnChangeDT.Visible = false;
        }

        private void cbLoaiViPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            choiceVP = cbLoaiViPham.GetItemText(cbLoaiViPham.SelectedItem);
            if (!choiceVP.Equals(""))
            {
                if (choiceVP.Equals("Trả tài liệu trễ hạn (tài liệu mượn về nhà)"))
                {
                    txtTreHan.Visible = true;
                    plusBtn.Visible = true;
                    minusBtn.Visible = true;
                    valueTH.Visible = true;
                }

                else if (choiceVP.Equals("Tài liệu mượn đọc tại chỗ nhưng hội viên cố tình đem về nhà"))
                {
                    txtTreHan.Visible = true;
                    plusBtn.Visible = true;
                    minusBtn.Visible = true;
                    valueTH.Visible = true;
                }
                else
                {
                    txtTreHan.Visible = false;
                    plusBtn.Visible = false;
                    minusBtn.Visible = false;
                    valueTH.Visible = false;
                }
                lastViPham = choiceVP;
            }
            
        }

        private void dgvListAddTraSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            dgvListAddTraSach.Rows[rowIndex].Selected = true;
        }

        private void dgvListAddTraSach_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvListAddTraSach.ReadOnly = true;
        }

        private void btnChangeDT_Click(object sender, EventArgs e)
        {



            DialogResult result = MessageBox.Show("Nếu đổi người toàn bộ sẽ xóa các thông tin ở trong bảng tạo phiếu trả\n" +
                "Bạn có chắc chắn muốn đổi người không?", "Đổi người trả", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                btnChangeDT.Visible = false;
                cbChonNguoiMuon.Enabled = true;
                AddListNguoiMuon();
                dgvListAddTraSach.Rows.Clear();
            }
            else if (result == DialogResult.Cancel)
            {
            }

        }

        private void saveBtnPT_Click(object sender, EventArgs e)
        {
            String msPT = "";
            String splitMT = "";
            int newMaPTInt;
            String newMaTString = "";
            List<PhieuTra> listNewPT = PhieuTraDAO.getInstance().selectMaT();
            foreach (var phieuTra in listNewPT)
            {
                msPT = phieuTra.getMaT();
            }
            splitMT = msPT.Substring(2);
            newMaPTInt = int.Parse(splitMT) + 1;
            if (newMaPTInt < 10)
            {
                newMaTString = "PT00" +newMaPTInt.ToString();
            }
            else if (newMaPTInt > 10)
            {
                newMaTString = "PT0" + newMaPTInt.ToString();
            }


            String MaM = "";
            List<PhieuMuonToHop> listMaM = PhieuMuonToHopDAO.getInstance().selectMaMNotInPT(choiceTenDG, MaSach,1);
            foreach (var pm in listMaM)
            {
                MaM = pm.getMaM();
            }
            if (MaM.Equals(""))
            {
                List<PhieuMuonToHop> listMaM2 = PhieuMuonToHopDAO.getInstance().selectMaMNotInPT(choiceTenDG, MaSach, 2);
                foreach (var pm in listMaM2)
                {
                    MaM = pm.getMaM();
                }
            }

            DateTime date = txtNgayTra.Value;
            String NgayTra = date.ToString();


            int SoLuong = 0;
            List<PhieuMuon> listPM = PhieuMuonDAO.getInstance().selectSoLuongMNotInPT(MaM);
            foreach (var phieuMuon in listPM)
            {
                SoLuong = phieuMuon.getSoLuongMuon();
            }


            int SoLuongT = dgvListAddTraSach.RowCount - 1;


            PhieuTra pt = new PhieuTra(newMaTString, MaM, NgayTra, SoLuong, SoLuongT);
            try
            {
                PhieuTraDAO.getInstance().Insert(pt);
                MessageBox.Show("Thêm Phiếu Trả Thành Công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNgayTra.Value = new DateTime(1900, 1, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);

            }
            SoLuong = SoLuong - 1;
            ////        //
            int rowCount = dgvListAddTraSach.RowCount;
            int columnCount = dgvListAddTraSach.ColumnCount;
            MaSach = "";
            String MaVP = "";
            int Phat = 0;

            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                DataGridViewRow row = dgvListAddTraSach.Rows[rowIndex];
                if (row == null || row.IsNewRow)
                {
                    dgvListAddTraSach.Rows.Clear();
                    cbChonNguoiMuon.SelectedIndex = 0;
                    cbChonSachMuonTra.SelectedIndex = 0;
                    cbChonNguoiMuon.Enabled = true;
                    cbChonSachMuonTra.Items.Clear();
                    btnChangeDT.Visible = false;
                    AddListNguoiMuon();
                    return;
                }


                for (int columnIndex = 0; columnIndex < columnCount; columnIndex++)
                {
                    object value = dgvListAddTraSach.Rows[rowIndex].Cells[columnIndex].Value;
                    if (columnIndex == 0)
                    {
                        MaSach = (string)value;
                    }
                    if (columnIndex == 2)
                    {
                        string VP = (string)value;
                        if (VP.Equals("Không vi phạm"))
                        {
                            MaVP = "VP000";
                        }
                        else if (VP.Equals("Mất tài liệu"))
                        {
                            MaVP = "VP001";
                        }
                        else if (VP.Equals("Tài liệu bị xé hoặc rách 1/3 trở lên"))
                        {
                            MaVP = "VP002";
                        }
                        else if (VP.Equals("Tài liệu bị bẩn, nhàu nát (ghi chép, gạch xóa, tô đậm)"))
                        {
                            MaVP = "VP004";
                        }
                        else
                        {
                            MaVP = "VP005";
                        }
                    }
                    if (columnIndex == 3)
                    {
                        if (value is int)
                        {
                            Phat = (int)value;
                        }
                        else if (value is string)
                        {
                            Phat = int.Parse((string)value);
                        }
                    }
                }

                CTPhieuTra ctpn = new CTPhieuTra(newMaTString, MaM, MaSach, MaVP, Phat);
                try
                {
                    CTPhieuTraDAO.getInstance().insert(ctpn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }

            cbChonNguoiMuon.SelectedIndex = 0;
            cbChonSachMuonTra.SelectedIndex = 0;
            cbChonNguoiMuon.Enabled = true;
            cbChonSachMuonTra.Items.Clear();
            AddListNguoiMuon();

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
