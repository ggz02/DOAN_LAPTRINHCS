using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    internal class SachDAO
    {
        KetNoi connectionData = new KetNoi();
        public static SachDAO getInstance()
        {
            return new SachDAO();
        }

        public void Insert(Sach t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO Sach(MaSach, ISBN, Gia, NamXB, Ghichu)"
                            + " VALUES(@MaSach, @ISBN, @Gia, @NamXB, @Ghichu)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaSach", t.getMaSach());
                cmd.Parameters.AddWithValue("@ISBN", t.getISBN());
                cmd.Parameters.AddWithValue("@Gia", t.getGia());
                cmd.Parameters.AddWithValue("@NamXB", t.getNamXB());
                cmd.Parameters.AddWithValue("@Ghichu", t.getGhiChu());

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

        public void Update(Sach t)
        {
            using (SqlConnection conn = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "UPDATE Sach SET " +
                                "ISBN = @ISBN, " +
                                "Gia = @Gia, " +
                                "NamXB = @NamXB, " +
                                "Ghichu = @Ghichu, " +
                                "WHERE MaSach = @MaSach";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ISBN", t.getISBN());
                    cmd.Parameters.AddWithValue("@Gia", t.getGia());
                    cmd.Parameters.AddWithValue("@NamXB", t.getNamXB());
                    cmd.Parameters.AddWithValue("@Ghichu", t.getGhiChu());
                    cmd.Parameters.AddWithValue("@MaSach", t.getMaSach());

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


        public List<Sach> selectNewMaSach()
        {

            List<Sach> listNewSach = new List<Sach>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select top 1 * from Sach order by MaSach desc";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sach sach = new Sach();
                    sach.setMaSach(reader["MaSach"].ToString());
                    sach.setISBN(reader["ISBN"].ToString());
                    sach.setGia(int.Parse(reader["Gia"].ToString()));
                    sach.setNamXB(int.Parse(reader["NamXB"].ToString()));
                    sach.setGhiChu(reader["Ghichu"].ToString());


                    listNewSach.Add(sach);
                }

                connectionSQL.Close();
            }

            return listNewSach;

        }

        public List<Sach> selectByConditonNamXB(String conditon)
        {

            List<Sach> listSachConditon = new List<Sach>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select NamXB from Sach s join Dausach ds on s.ISBN = ds.ISBN " +
                    "where TenSach = N'"+conditon+"'";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sach sach = new Sach();
                    sach.setNamXB(int.Parse(reader["NamXB"].ToString()));


                    listSachConditon.Add(sach);
                }

                connectionSQL.Close();
            }

            return listSachConditon;

        }

        public List<Sach> selectByConditionMaSach(String conditon, String condition2)
        {

            List<Sach> listSachConditon = new List<Sach>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select MaSach from Sach s join Dausach ds on s.ISBN = ds.ISBN " +
                    "where s.ISBN = '" + conditon + "' and s.NamXB = '"+condition2+"'";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sach sach = new Sach();
                    sach.setMaSach(reader["MaSach"].ToString());


                    listSachConditon.Add(sach);
                }

                connectionSQL.Close();
            }

            return listSachConditon;

        }

        public List<Sach> selectByConditonGia(String conditon, String condition2, int index)
        {

            List<Sach> listSachConditon = new List<Sach>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "";
                if (index == 1) 
                {
                    query += "select Gia from Sach " +
                    "where NamXB = '" + conditon + "' and ISBN = '" + condition2 + "'";
                }
                if (index == 2)
                {
                    query += "select Gia from Sach " +
                    "where MaSach = '" +conditon+"'";
                }

                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Sach sach = new Sach();
                    sach.setGia(int.Parse(reader["Gia"].ToString()));


                    listSachConditon.Add(sach);
                }

                connectionSQL.Close();
            }

            return listSachConditon;

        }



    }
}
