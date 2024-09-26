using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyThuVien.Model;

namespace QuanLyThuVien.DAO
{
    internal class DauSachDAO
    {
        KetNoi connectionData = new KetNoi();

        public static DauSachDAO getInstance()
        {
            return new DauSachDAO();
        }

        public void Insert(DauSach t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO DauSach(ISBN, MaLoai, TenSach, Tacgia, NXB, SoLuong, Thongtinsach)"
                            + " VALUES(@ISBN, @MaLoai, @TenSach, @TacGia, @NXB, @SoLuong, @ThongTinSach)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ISBN", t.ISBN);
                cmd.Parameters.AddWithValue("@MaLoai", t.MaLoai);
                cmd.Parameters.AddWithValue("@TenSach", t.TenSach);
                cmd.Parameters.AddWithValue("@TacGia", t.TacGia);
                cmd.Parameters.AddWithValue("@NXB", t.NXB);
                cmd.Parameters.AddWithValue("@SoLuong", t.SoLuong);
                cmd.Parameters.AddWithValue("@ThongTinSach", t.ThongTinSach);

                int rs = cmd.ExecuteNonQuery();
                if (rs > 0)
                {
                    Console.WriteLine("Thêm thành công " + rs);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                    Console.WriteLine("Đóng kết nối");
                }

                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    Console.WriteLine("Đóng kết nối");
                }
            }
        }


        public void Update(DauSach t)
        {
            using (SqlConnection conn = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "UPDATE DauSach SET " +
                                "ISBN = @ISBN, " +
                                "MaLoai = @MaLoai, " +
                                "TenSach = @TenSach, " +
                                "Tacgia = @Tacgia, " +
                                "NXB = @NXB, " +
                                "SoLuong = @SoLuong, " +
                                "ThongTinSach = @ThongTinSach " +
                                "WHERE ISBN = @OriginalISBN";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ISBN", t.ISBN);
                    cmd.Parameters.AddWithValue("@MaLoai", t.MaLoai);
                    cmd.Parameters.AddWithValue("@TenSach", t.TenSach);
                    cmd.Parameters.AddWithValue("@Tacgia", t.TacGia);
                    cmd.Parameters.AddWithValue("@NXB", t.NXB);
                    cmd.Parameters.AddWithValue("@SoLuong", t.SoLuong);
                    cmd.Parameters.AddWithValue("@ThongTinSach", t.ThongTinSach);
                    cmd.Parameters.AddWithValue("@OriginalISBN", t.ISBN);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Sửa thành công " + rowsAffected + " dòng được cập nhật.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Lỗi khi thực hiện câu lệnh SQL: " + ex.Message);
                    }
                    finally
                    {
                        if (cmd != null)
                        {
                            cmd.Dispose();
                            Console.WriteLine("Đóng kết nối");
                        }

                        if (conn != null && conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                            Console.WriteLine("Đóng kết nối");
                        }
                    }
                }
            }

        }

        public void Delete(DauSach t)
        {
            using (SqlConnection conn = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "DELETE FROM DauSach " +
                                "WHERE " +
                                "ISBN = @ISBN " +
                                "AND MaLoai = @MaLoai " +
                                "AND TenSach = @TenSach " +
                                "AND Tacgia = @Tacgia " +
                                "AND NXB = @NXB " +
                                "AND SoLuong = @SoLuong " +
                                "AND ThongTinSach = @ThongTinSach";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ISBN", t.ISBN);
                    cmd.Parameters.AddWithValue("@MaLoai", t.MaLoai);
                    cmd.Parameters.AddWithValue("@TenSach", t.TenSach);
                    cmd.Parameters.AddWithValue("@Tacgia", t.TacGia);
                    cmd.Parameters.AddWithValue("@NXB", t.NXB);
                    cmd.Parameters.AddWithValue("@SoLuong", t.SoLuong);
                    cmd.Parameters.AddWithValue("@ThongTinSach", t.ThongTinSach);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Xóa thành công " + rowsAffected + " dòng được xóa.");
                        }
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Lỗi khi thực hiện câu lệnh SQL: " + ex.Message);
                    }
                    finally
                    {
                        if (cmd != null)
                        {
                            cmd.Dispose();
                            Console.WriteLine("Đóng kết nối");
                        }

                        if (conn != null && conn.State == ConnectionState.Open)
                        {
                            conn.Close();
                            Console.WriteLine("Đóng kết nối");
                        }
                    }
                }
            }
        }

        public List<DauSach> listTenSach()
        {
            List<DauSach> listDauSach = new List<DauSach>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select TenSach from Dausach";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DauSach dauSach = new DauSach();
                    dauSach.SetTenSach(reader["TenSach"].ToString());


                    listDauSach.Add(dauSach);
                }

                connectionSQL.Close();
            }
            return listDauSach;
        }

        public List<DauSach> seletByConditonSoPN(String Condition)
        {
            List<DauSach> listDauSach = new List<DauSach>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select ISBN from Dausach " +
                    "where TenSach = N'"+Condition +"'";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DauSach dauSach = new DauSach();
                    dauSach.SetISBN(reader["ISBN"].ToString());


                    listDauSach.Add(dauSach);
                }

                connectionSQL.Close();
            }
            return listDauSach;
        }


        public List<DauSach> selectTenSachNotInPT(String Condition, int index)
        {
            List<DauSach> listSach = new List<DauSach>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                cmd = conn.CreateCommand();
                string query = "";
                if (index == 1)
                {
                    query = "select TenSach " +
                    "from DocGia dg join PhieuMuon pm on dg.MaDG = pm.MaDG join CTPhieuMuon ctpm on pm.MaM = ctpm.MaM " +
                    "join Sach s on s.MaSach = ctpm.MaSach join Dausach ds on ds.ISBN = s.ISBN " +
                    "where dg.HoTenDG = N'" + Condition + "' and pm.MaM not in (select MaM from PhieuTra)";
                }
                if (index == 2)
                {
                    query = "select TenSach\n" +
"					from CTPhieuMuon ctpm join Sach s on ctpm.MaSach = s.MaSach join Dausach ds on ds.ISBN = s.ISBN\n" +
"					where s.MaSach not in (\n" +
"						select MaSach\n" +
"						from CTPhieuTra\n" +
"					)";
                }
               

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string TenSach = reader["TenSach"].ToString();
                    DauSach ds = new DauSach();
                    ds.SetTenSach(TenSach);




                    listSach.Add(ds);
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
            return listSach;
        }


    }
}
