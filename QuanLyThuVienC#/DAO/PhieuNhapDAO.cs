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
    internal class PhieuNhapDAO
    {
        KetNoi connectionData = new KetNoi();
        public static PhieuNhapDAO getInstance()
        {
            return new PhieuNhapDAO();
        }

        public void insert(PhieuNhap t)
        {


            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO PhieuNhap(SoPN, MaNCC, MaTT, Ngaytao, Ngaynhap) " +
                               "VALUES(@SoPN, @MaNCC, @MaTT, @Ngaytao, @Ngaynhap)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoPN", t.getSoPN());
                cmd.Parameters.AddWithValue("@MaNCC", t.getMaNCC());
                cmd.Parameters.AddWithValue("@MaTT", t.getMaTT());
                cmd.Parameters.AddWithValue("@Ngaytao", t.getNgayTao());
                cmd.Parameters.AddWithValue("@Ngaynhap", t.getNgayNhap());


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



        public List<PhieuNhap> selectSoPN()
        {
            List<PhieuNhap> listPN = new List<PhieuNhap>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select top 1 SoPN " +
                    " from PhieuNhap " +
                    " order by SoPN desc";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PhieuNhap pn = new PhieuNhap();
                    pn.setSoPN(reader["SoPN"].ToString());

                    listPN.Add(pn);
                }

                connectionSQL.Close();
            }
            return listPN;
        }

    }
}
