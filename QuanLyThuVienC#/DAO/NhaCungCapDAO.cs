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
    internal class NhaCungCapDAO
    {
        KetNoi connectionData = new KetNoi();
        public static NhaCungCapDAO getInstance()
        {
            return new NhaCungCapDAO();
        }

        public void insert(NhaCungCap t)
        {


            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO Nhacungcap(MaNCC, TenNCC, Diachi, Dienthoai, Website) " +
                               "VALUES(@MaNCC, @TenNCC, @Diachi, @Dienthoai, @Website)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNCC", t.getMaNCC());
                cmd.Parameters.AddWithValue("@TenNCC", t.getTenNCC());
                cmd.Parameters.AddWithValue("@Diachi", t.getDiaChi());
                cmd.Parameters.AddWithValue("@Dienthoai", t.getDienThoai());
                cmd.Parameters.AddWithValue("@Website", t.getWebsite());

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

        public void Update(NhaCungCap t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "UPDATE Nhacungcap" +
                               " SET" +
                               " MaNCC = @MaNCC," +
                               " TenNCC = @TenNCC," +
                               " Diachi = @Diachi," +
                               " Dienthoai = @Dienthoai," +
                               " Website = @Website" +
                               " WHERE MaNCC = @OldMaNCC";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNCC", t.getMaNCC());
                cmd.Parameters.AddWithValue("@TenNCC", t.getTenNCC());
                cmd.Parameters.AddWithValue("@Diachi", t.getDiaChi());
                cmd.Parameters.AddWithValue("@Dienthoai", t.getDienThoai());
                cmd.Parameters.AddWithValue("@Website", t.getWebsite());
                cmd.Parameters.AddWithValue("@OldMaNCC", t.getMaNCC());

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

        public List<NhaCungCap> selectNewMaNCC()
        {

            List<NhaCungCap> listNewNCC = new List<NhaCungCap>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select top 1 * from Nhacungcap order by MaNCC desc";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    NhaCungCap ncc = new NhaCungCap();
                    ncc.setMaNCC(reader["MaNCC"].ToString());
                    ncc.setTenNCC(reader["TenNCC"].ToString());
                    ncc.setDienThoai(reader["Dienthoai"].ToString());
                    ncc.setDiaChi(reader["Diachi"].ToString());
                    ncc.setWebsite(reader["Website"].ToString());

                    listNewNCC.Add(ncc);
                }

                connectionSQL.Close();
            }

            return listNewNCC;

        }
    }
}
