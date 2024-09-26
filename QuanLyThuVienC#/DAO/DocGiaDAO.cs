using QuanLyThuVien.DAO;
using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    internal class DocGiaDAO
    {

        KetNoi connectionData = new KetNoi();


        public static DocGiaDAO getInstance()
        {
            return new DocGiaDAO();
        }


        public List<DocGia> selectAll()
        {
            List<DocGia> listNewDG = new List<DocGia>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select * from DocGia";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DocGia docGia = new DocGia();
                    docGia.setMaDG(reader["MaDG"].ToString());
                    docGia.setHoTenDG(reader["HoTenDG"].ToString());
                    DateTime ngaySinhDateTime = (DateTime)reader["NgaySinhDG"];
                    string ngaySinhString = ngaySinhDateTime.ToString("dd/MM/yyyy");
                    docGia.setNgaySinhDG(ngaySinhString);
                    docGia.setGioiTinhDG(reader["GioiTinhDG"].ToString());
                    docGia.setDienThoaiDG(reader["DthoaiDG"].ToString());
                    docGia.setDiaChiDG(reader["DiachiDG"].ToString());
                    docGia.setDoiTuong(reader["DoiTuong"].ToString());

                    listNewDG.Add(docGia);
                }

                connectionSQL.Close();
            }
            return listNewDG;
        }


        public List<DocGia> selectNewMaDG()
        {

            List<DocGia> listNewDG = new List<DocGia>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select top 1 * from DocGia order by MaDG desc";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DocGia docGia = new DocGia();
                    docGia.setMaDG(reader["MaDG"].ToString());
                    docGia.setHoTenDG(reader["HoTenDG"].ToString());
                    DateTime ngaySinhDateTime = (DateTime)reader["NgaySinhDG"];
                    string ngaySinhString = ngaySinhDateTime.ToString("dd/MM/yyyy");
                    docGia.setNgaySinhDG(ngaySinhString);
                    docGia.setGioiTinhDG(reader["GioiTinhDG"].ToString());
                    docGia.setDienThoaiDG(reader["DthoaiDG"].ToString());
                    docGia.setDiaChiDG(reader["DiachiDG"].ToString());
                    docGia.setDoiTuong(reader["DoiTuong"].ToString());

                    listNewDG.Add(docGia);
                }

                connectionSQL.Close();
            }

            return listNewDG;

        }

        public void insert(DocGia t)
        {


            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO DocGia(MaDG, HoTenDG, NgaySinhDG, GioiTinhDG, DthoaiDG, DiachiDG, DoiTuong) " +
                               "VALUES(@MaDG, @HoTenDG, @NgaySinhDG, @GioiTinhDG, @DthoaiDG, @DiachiDG, @DoiTuong)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDG", t.getMaDG());
                cmd.Parameters.AddWithValue("@HoTenDG", t.getHoTenDG());
                cmd.Parameters.AddWithValue("@NgaySinhDG", t.getNgaySinhDG());
                cmd.Parameters.AddWithValue("@GioiTinhDG", t.getGioiTinhDG());
                cmd.Parameters.AddWithValue("@DthoaiDG", t.getDienThoaiDG());
                cmd.Parameters.AddWithValue("@DiachiDG", t.getDiaChiDG());
                cmd.Parameters.AddWithValue("@DoiTuong", t.getDoiTuong());

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Thêm thành công " + rowsAffected + " dòng");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi SQL: " + ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    Console.WriteLine("Đóng kết nối");
                }
            }


        }

        public void Delete(DocGia t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                string query = "DELETE FROM DocGia" +
                               " WHERE " +
                               " MaDG = @MaDG";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDG", t.getMaDG());

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Xóa thành công " + rowsAffected);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi SQL: " + ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    Console.WriteLine("Đóng kết nối");
                }
            }
        }


        public void Update(DocGia t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "UPDATE DocGia" +
                               " SET" +
                               " MaDG = @MaDG," +
                               " HoTenDG = @HoTenDG," +
                               " NgaySinhDG = @NgaySinhDG," +
                               " GioiTinhDG = @GioiTinhDG," +
                               " DthoaiDG = @DthoaiDG," +
                               " DiachiDG = @DiachiDG," +
                               " DoiTuong = @DoiTuong" +
                               " WHERE MaDG = @OldMaDG";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaDG", t.getMaDG());
                cmd.Parameters.AddWithValue("@HoTenDG", t.getHoTenDG());
                cmd.Parameters.AddWithValue("@NgaySinhDG", t.getNgaySinhDG());
                cmd.Parameters.AddWithValue("@GioiTinhDG", t.getGioiTinhDG());
                cmd.Parameters.AddWithValue("@DthoaiDG", t.getDienThoaiDG());
                cmd.Parameters.AddWithValue("@DiachiDG", t.getDiaChiDG());
                cmd.Parameters.AddWithValue("@DoiTuong", t.getDoiTuong());
                cmd.Parameters.AddWithValue("@OldMaDG", t.getMaDG()); // Sử dụng MaDG cũ để làm điều kiện WHERE

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Sửa thành công " + rowsAffected);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi SQL: " + ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    Console.WriteLine("Đóng kết nối");
                }
            }
        }


        public List<DocGia> selectedById(String msDG)
        {
            List<DocGia> docGias = new List<DocGia>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionData.GetStrConn()))
                {
                    conn.Open();
                    string sql = "SELECT * FROM DocGia WHERE MaDG = @MaDG";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaDG", msDG);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DocGia docGia = new DocGia();
                                docGia.setMaDG(reader["MaDG"].ToString());
                                docGia.setHoTenDG(reader["HoTenDG"].ToString());
                                DateTime ngaySinhDateTime = (DateTime)reader["NgaySinhDG"];
                                string ngaySinhString = ngaySinhDateTime.ToString("dd/MM/yyyy");
                                docGia.setNgaySinhDG(ngaySinhString);
                                docGia.setGioiTinhDG(reader["GioiTinhDG"].ToString());
                                docGia.setDienThoaiDG(reader["DthoaiDG"].ToString());
                                docGia.setDiaChiDG(reader["DiachiDG"].ToString());
                                docGia.setDoiTuong(reader["DoiTuong"].ToString());
                                docGias.Add(docGia);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return docGias;
        }

        public List<DocGia> SelectByCondition(string Condition, int index)
        {
            List<DocGia> listDocGia = new List<DocGia>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                cmd = conn.CreateCommand();
                string querry = "";
                if (index == 0)
                {

                    querry = "select distinct dg.MaDG,dg.HoTenDG,dg.NgaySinhDG,dg.GioiTinhDG,dg.DthoaiDG,dg.DiachiDG,dg.DoiTuong\n" +
                         "from DocGia dg join PhieuMuon pm on dg.MaDG = pm.MaDG join CTPhieuMuon ctpm on ctpm.MaM = pm.MaM \n" +
                         "	where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and CONVERT(date, pm.Ngaymuon) = CONVERT(date, GETDATE())";
                }

                if (index == 1)
                {
                    querry = "select distinct dg.MaDG,dg.HoTenDG,dg.NgaySinhDG,dg.GioiTinhDG,dg.DthoaiDG,dg.DiachiDG,dg.DoiTuong\n" +
    "from DocGia dg join PhieuMuon pm on dg.MaDG = pm.MaDG join CTPhieuMuon ctpm on ctpm.MaM = pm.MaM \n" +
    "	where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and MONTH(Ngaymuon)= MONTH(GETDATE())";
                }
                if (index == 2)
                {
                    querry = "select distinct dg.MaDG,dg.HoTenDG,dg.NgaySinhDG,dg.GioiTinhDG,dg.DthoaiDG,dg.DiachiDG,dg.DoiTuong\n" +
     "from DocGia dg join PhieuMuon pm on dg.MaDG = pm.MaDG join CTPhieuMuon ctpm on ctpm.MaM = pm.MaM \n" +
     "	where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and Year(Ngaymuon)= Year(GETDATE())";
                }
                if (index == 3)
                {
                    querry = "select distinct dg.MaDG,dg.HoTenDG,dg.NgaySinhDG,dg.GioiTinhDG,dg.DthoaiDG,dg.DiachiDG,dg.DoiTuong\n" +
 "from DocGia dg join PhieuMuon pm on dg.MaDG = pm.MaDG join CTPhieuMuon ctpm on ctpm.MaM = pm.MaM \n" +
 "	where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and Year(Ngaymuon)= Year(GETDATE()) -1 ";
                }
                if (index == 4)
                {
                    querry = "select distinct dg.MaDG,dg.HoTenDG,dg.NgaySinhDG,dg.GioiTinhDG,dg.DthoaiDG,dg.DiachiDG,dg.DoiTuong\n" +
          "from DocGia dg join PhieuMuon pm on dg.MaDG = pm.MaDG join CTPhieuMuon ctpm on ctpm.MaM = pm.MaM \n" +
          "	where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and ('" + Condition + "' between NgayMuon and Ngayhethan)";
                }



                cmd.CommandText = querry;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string MaDG = reader["MaDG"].ToString();
                    string HoTenDG = reader["HoTenDG"].ToString();
                    string NgaySinh = reader["NgaySinhDG"].ToString();
                    string GioiTinh = reader["GioiTinhDG"].ToString();
                    string DienThoai = reader["DthoaiDG"].ToString();
                    string DiaChi = reader["DiachiDG"].ToString();
                    string DoiTuong = reader["DoiTuong"].ToString();


                    DocGia PhieuMuon = new DocGia(MaDG, HoTenDG, NgaySinh, GioiTinh, DienThoai, DiaChi, DoiTuong);
                    listDocGia.Add(PhieuMuon);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (conn != null && conn.State != ConnectionState.Closed) conn.Close();
            }
            return listDocGia;
        }



    }
}
