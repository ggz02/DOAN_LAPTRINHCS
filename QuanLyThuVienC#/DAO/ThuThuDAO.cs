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
    internal class ThuThuDAO
    {

        KetNoi connectionData = new KetNoi();


        public static ThuThuDAO getInstance()
        {
            return new ThuThuDAO();
        }

        public void insert(ThuThu t)
        {


            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO ThuThu(MaTT, HoTenTT, NgaySinhTT, GioiTinhTT, DthoaiTT, DiachiTT) " +
                               "VALUES(@MaTT, @HoTenTT, @NgaySinhTT, @GioiTinhTT, @DthoaiTT, @DiachiTT)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTT", t.getMaTT());
                cmd.Parameters.AddWithValue("@HoTenTT", t.getHoTenTT());
                cmd.Parameters.AddWithValue("@NgaySinhTT", t.getNgaySinhTT());
                cmd.Parameters.AddWithValue("@GioiTinhTT", t.getGioiTinhTT());
                cmd.Parameters.AddWithValue("@DthoaiTT", t.getDienThoaiTT());
                cmd.Parameters.AddWithValue("@DiachiTT", t.getDiaChiTT());

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

        public void Update(ThuThu t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "UPDATE ThuThu" +
                               " SET" +
                               " MaTT = @MaTT," +
                               " HoTenTT = @HoTenTT," +
                               " NgaySinhTT = @NgaySinhTT," +
                               " GioiTinhTT = @GioiTinhTT," +
                               " DthoaiTT = @DthoaiTT," +
                               " DiachiTT = @DiachiTT" +
                               " WHERE MaTT = @OldMaTT";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaTT", t.getMaTT());
                cmd.Parameters.AddWithValue("@HoTenTT", t.getHoTenTT());
                cmd.Parameters.AddWithValue("@NgaySinhTT", t.getNgaySinhTT());
                cmd.Parameters.AddWithValue("@GioiTinhTT", t.getGioiTinhTT());
                cmd.Parameters.AddWithValue("@DthoaiTT", t.getDienThoaiTT());
                cmd.Parameters.AddWithValue("@DiachiTT", t.getDiaChiTT());
                cmd.Parameters.AddWithValue("@OldMaTT", t.getMaTT()); 

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



        public List<ThuThu> selectNewMaDG()
        {

            List<ThuThu> listNewTT = new List<ThuThu>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select top 1 * from ThuThu order by MaTT desc";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ThuThu thuthu = new ThuThu();
                    thuthu.setMaTT(reader["MaTT"].ToString());
                    thuthu.setHoTenTT(reader["HoTenTT"].ToString());
                    DateTime ngaySinhDateTime = (DateTime)reader["NgaySinhTT"];
                    string ngaySinhString = ngaySinhDateTime.ToString("dd/MM/yyyy");
                    thuthu.setNgaySinhTT(ngaySinhString);
                    thuthu.setGioiTinhTT(reader["GioiTinhTT"].ToString());
                    thuthu.setDienThoaiTT(reader["DthoaiTT"].ToString());
                    thuthu.setDiaChiTT(reader["DiachiTT"].ToString());

                    listNewTT.Add(thuthu);
                }

                connectionSQL.Close();
            }

            return listNewTT;

        }

    }
}
