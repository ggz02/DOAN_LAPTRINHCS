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
    internal class PhieuMuonDAO
    {
        KetNoi connectionData = new KetNoi();

        public static PhieuMuonDAO getInstance()
        {
            return new PhieuMuonDAO();
        }

        public void insert(PhieuMuon t)
        {


            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "INSERT INTO PhieuMuon(MaM, MaDG, MaTT, Ngaymuon, Ngayhethan,SoLuongM,Ghichu) " +
                               "VALUES(@MaM, @MaDG, @MaTT, @Ngaymuon, @Ngayhethan,@SoLuongM,@Ghichu)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaM", t.getMaM());
                cmd.Parameters.AddWithValue("@MaDG", t.getMaDG());
                cmd.Parameters.AddWithValue("@MaTT", t.getMaTT());
                cmd.Parameters.AddWithValue("@Ngaymuon", t.getNgayMuon());
                cmd.Parameters.AddWithValue("@Ngayhethan", t.getNgayHetHan());
                cmd.Parameters.AddWithValue("@SoLuongM", t.getSoLuongMuon());
                cmd.Parameters.AddWithValue("@Ghichu", t.getGhiChu());


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

        public List<PhieuMuon> selectMaM()
        {
            List<PhieuMuon> listPM = new List<PhieuMuon>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select top 1 MaM " +
                    " from PhieuMuon " +
                    " order by MaM desc";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PhieuMuon pm = new PhieuMuon();
                    pm.setMaM(reader["MaM"].ToString());

                    listPM.Add(pm);
                }

                connectionSQL.Close();
            }
            return listPM;
        }

        public List<PhieuMuon> selectSoLuongMNotInPT(String Condition)
        {
            List<PhieuMuon> listPM = new List<PhieuMuon>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select SoLuongM\n" +
                "from PhieuMuon\n" +
                "where MaM = '" + Condition + "'";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PhieuMuon pm = new PhieuMuon();
                    pm.setSoLuongMuon(int.Parse(reader["SoLuongM"].ToString()));

                    listPM.Add(pm);
                }

                connectionSQL.Close();
            }
            return listPM;
        }





    }
}
