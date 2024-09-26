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
    internal class CTPhieuNhapDAO
    {
        KetNoi connectionData = new KetNoi();
        public static CTPhieuNhapDAO getInstance()
        {
            return new CTPhieuNhapDAO();
        }

        public void insert(CTPhieuNhap t)
        {


            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO CTPhieuNhap(SoPN, ISBN, NamXB, SoLuong, Gianhap) " +
                               "VALUES(@SoPN, @ISBN, @NamXB, @SoLuong, @Gianhap)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoPN", t.getSoPN());
                cmd.Parameters.AddWithValue("@ISBN", t.getISBN());
                cmd.Parameters.AddWithValue("@NamXB", t.getNamXB());
                cmd.Parameters.AddWithValue("@SoLuong", t.getSoLuong());
                cmd.Parameters.AddWithValue("@Gianhap", t.getGiaNhap());


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

    }
}
