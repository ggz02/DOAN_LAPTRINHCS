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
    internal class CTPhieuMuonDAO
    {
        KetNoi connectionData = new KetNoi();

        public static CTPhieuMuonDAO getInstance()
        {
            return new CTPhieuMuonDAO();
        }

        public void insert(CTPhieuMuon t)
        {


            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO CTPhieuMuon(MaM, MaSach) " +
                               "VALUES(@MaM, @MaSach)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaM", t.getMaM());
                cmd.Parameters.AddWithValue("@MaSach", t.getMaSach());



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
