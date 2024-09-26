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
    internal class TheThuVienDAO
    {
        public static TheThuVienDAO getInstance()
        {
            return new TheThuVienDAO();
        }

        KetNoi connectionData = new KetNoi();

        public List<TheThuVien> SelectNewMaTT()
        {
            List<TheThuVien> listThe = new List<TheThuVien>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "SELECT TOP 1 * FROM TheThuVien ORDER BY Mathe DESC";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TheThuVien ttv = new TheThuVien(
                        reader["Mathe"].ToString(),
                        reader["MaDG"].ToString(),
                        reader["Ngaytao"].ToString(),
                        reader["HanThe"].ToString());
                    listThe.Add(ttv);
                }
                connectionSQL.Close();
            }

            return listThe;
        }


        public void Insert(TheThuVien t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO TheThuVien(Mathe, MaDG, Ngaytao, HanThe) VALUES(@MaThe, @MaDG, @NgayTao, @HanThe)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaThe", t.MaTheProperty);
                cmd.Parameters.AddWithValue("@MaDG", t.MaDGProperty);
                cmd.Parameters.AddWithValue("@NgayTao", t.NgayTaoProperty);
                cmd.Parameters.AddWithValue("@HanThe", t.HanTheProperty);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Thêm thành công " + rowsAffected + " bản ghi.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
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
                    Console.WriteLine("Đóng kết nối.");
                }
            }
        }

        public void Update(TheThuVien t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "UPDATE TheThuVien SET Mathe = @MaThe, MaDG = @MaDG, Ngaytao = @NgayTao, HanThe = @HanThe WHERE Mathe = @MaThe";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaThe", t.MaTheProperty);
                cmd.Parameters.AddWithValue("@MaDG", t.MaDGProperty);
                cmd.Parameters.AddWithValue("@NgayTao", t.NgayTaoProperty);
                cmd.Parameters.AddWithValue("@HanThe", t.HanTheProperty);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Sửa thành công " + rowsAffected + " bản ghi.");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
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
                    Console.WriteLine("Đóng kết nối.");
                }
            }
        }

        public void Delete(TheThuVien t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "DELETE FROM TheThuVien WHERE Mathe = @MaThe AND MaDG = @MaDG AND Ngaytao = @NgayTao AND HanThe = @HanThe";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaThe", t.MaTheProperty);
                cmd.Parameters.AddWithValue("@MaDG", t.MaDGProperty);
                cmd.Parameters.AddWithValue("@NgayTao", t.NgayTaoProperty);
                cmd.Parameters.AddWithValue("@HanThe", t.HanTheProperty);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Xóa thành công " + rowsAffected + " bản ghi.");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
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
                    Console.WriteLine("Đóng kết nối.");
                }
            }
        }


        public List<TheThuVien> selectNewMaTTV()
        {
            List<TheThuVien> listNewTTV = new List<TheThuVien>();
            using(SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select top 1 * from TheThuVien order by Mathe desc";
                SqlCommand command = new SqlCommand(query, connectionSQL);
                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TheThuVien ttv = new TheThuVien();
                    ttv.MaTheProperty = reader["Mathe"].ToString();
                    ttv.MaDGProperty = reader["MaDG"].ToString();
                    DateTime ngayTaoDateTime = (DateTime)reader["Ngaytao"];
                    string ngayTaoString = ngayTaoDateTime.ToString("dd/MM/yyyy");
                    ttv.NgayTaoProperty = ngayTaoString;
                    DateTime hanTheDateTime = (DateTime)reader["HanThe"];
                    ttv.HanTheProperty = ngayTaoDateTime.ToString("dd/MM/yyyy");
                    listNewTTV.Add(ttv);
                }
                connectionSQL.Close();
            }
            return listNewTTV;
        }




    }
}
