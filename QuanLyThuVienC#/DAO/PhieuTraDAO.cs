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
    internal class PhieuTraDAO
    {
        public static PhieuTraDAO getInstance()
        {
            return new PhieuTraDAO();
        }

        KetNoi connectionData = new KetNoi();

        public void Insert(PhieuTra t)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "insert into PhieuTra(MaT,MaM,Ngaytra,SoLuongM,SoLuongT)"
                            + " VALUES(@MaT, @MaM, @Ngaytra, @SoLuongM, @SoLuongT)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaT", t.getMaT());
                cmd.Parameters.AddWithValue("@MaM", t.getMaM());
                cmd.Parameters.AddWithValue("@Ngaytra", t.getNgayTra());
                cmd.Parameters.AddWithValue("@SoLuongM", t.getSoLuongM());
                cmd.Parameters.AddWithValue("@SoLuongT", t.getSoLuongT());

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

        public List<PhieuTra> SelectByCondition(string Condition,string Condition2 ,int index)
        {
            List<PhieuTra> listPT = new List<PhieuTra>();
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
                    query = "select * from PhieuTra \n" +
                     "       where Ngaytra = '" + Condition + "'";
                }
                if (index == 2)
                {
                    query = "select * from PhieuTra \n" +
                    "where Month(Ngaytra) = '" + Condition + "' and Year(Ngaytra) = year(getdate())";
                }
                if (index == 3)
                {
                    query = "select * from PhieuTra \n" +
            "		where Year(Ngaytra) = year(getdate())";
                }
                if (index == 4)
                {
                    query = "select * from PhieuTra \n" +
            "		where Year(Ngaytra) = year(getdate()) -1";
                }
                if (index == 5)
                {
                    query = "select * from PhieuTra \n" +
            "		where MaT = '" + Condition + "'";
                }
                if (index == 6)
                {
                    query = "select * from PhieuTra \n" +
                        "where MaM = '" + Condition + "'";
                }
                if (index == 7)
                {
                    query = "select * from PhieuTra \n" +
                  "where MaM = '" + Condition + "' and MaT = '" + Condition2 + "'";
                }
               


                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string MaT = reader["MaT"].ToString();
                    string MaM = reader["MaM"].ToString();
                    string NgayTra = reader["Ngaytra"].ToString();
                    int SoLuongM = int.Parse(reader["SoLuongM"].ToString());
                    int SoLuongT = int.Parse(reader["SoLuongT"].ToString());


                    PhieuTra phieuTra = new PhieuTra(MaT, MaM, NgayTra, SoLuongM, SoLuongT);
                    listPT.Add(phieuTra);
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
            return listPT;
        }

        public List<PhieuTra> selectMaT()
        {
            List<PhieuTra> listMT = new List<PhieuTra>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select top 1 MaT\n" +
                    "from PhieuTra\n" +
                    "order by MaT desc";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PhieuTra ptr = new PhieuTra();
                    ptr.setMaT(reader["MaT"].ToString());


                    listMT.Add(ptr);
                }

                connectionSQL.Close();
            }
            return listMT;
        }


    }
}
