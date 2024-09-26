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
    internal class PhieuMuonToHopDAO
    {
        KetNoi connectionData = new KetNoi();

        public static PhieuMuonToHopDAO getInstance()
        {
            return new PhieuMuonToHopDAO();
        }


        public List<PhieuMuonToHop> SelectByCondition(string Condition, int index)
        {
            List<PhieuMuonToHop> listPM = new List<PhieuMuonToHop>();
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
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu " +
                        " from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
                        "           where Ngaymuon = '" + Condition + "'";
                }
                if (index == 2)
                {
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu " +
                            " from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
                            " where Month(Ngaymuon) = '" + Condition + "' and Year(Ngaymuon) = year(getdate())";
                }
                if (index == 3)
                {
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu\n" +
                            " from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
                            " where Ngayhethan = '" + Condition + "'";
                }
                if (index == 4)
                {
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu\n" +
                             "from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
                               " where Month(Ngayhethan) = '" + Condition + "' and Year(Ngayhethan) = year(getdate())";
                }
                if (index == 5)
                {
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu\n" +
                    "from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
            "		where Year(Ngaymuon) = year(getdate())";
                }
                if (index == 6)
                {
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu\n" +
                       "from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
               "		where Year(Ngayhethan) = year(getdate())";
                }
                if (index == 7)
                {
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu\n" +
                      "from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
              "		where Year(Ngaymuon) = year(getdate()) -1";
                }
                if (index == 8)
                {
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu\n" +
              "from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
      "		where Year(Ngayhethan) = year(getdate()) -1";
                }
                if (index == 9)
                {
                    query = "select MaM,HoTenDG, Ngaymuon,Ngayhethan,SoLuongM, Ghichu\n" +
             "from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG " +
     "		where MaM = '" + Condition + "'";
                }


                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string MaM = reader["MaM"].ToString();
                    string HoTenDG = reader["HoTenDG"].ToString();
                    string NgayMuon = reader["Ngaymuon"].ToString();
                    string NgayHetHan = reader["Ngayhethan"].ToString();
                    int SoLuongM = int.Parse(reader["SoLuongM"].ToString());
                    string GhiChu = reader["Ghichu"].ToString();


                    PhieuMuonToHop phieuMuon = new PhieuMuonToHop(MaM,HoTenDG,NgayMuon,NgayHetHan,SoLuongM,GhiChu);
                    listPM.Add(phieuMuon);
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
            return listPM;
        }

        public List<PhieuMuonToHop> selectNameDGNotInPT()
        {
            List<PhieuMuonToHop> listPM = new List<PhieuMuonToHop>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "SELECT DISTINCT HoTenDG\n" +
                        "FROM (\n" +
                        "    SELECT HoTenDG\n" +
                        "    FROM DocGia\n" +
                        "    WHERE MaDG IN (\n" +
                        "        SELECT MaDG\n" +
                        "        FROM PhieuMuon\n" +
                        "        WHERE MaM NOT IN (\n" +
                        "            SELECT MaM\n" +
                        "            FROM PhieuTra\n" +
                        "        )\n" +
                        "    )\n" +
                        "    UNION ALL\n" +
                        "    SELECT HoTenDG\n" +
                        "    FROM DocGia dg\n" +
                        "    JOIN PhieuMuon pm ON dg.MaDG = pm.MaDG\n" +
                        "    JOIN CTPhieuMuon ctpm ON ctpm.MaM = pm.MaM\n" +
                        "    WHERE ctpm.MaSach IN (\n" +
                        "        SELECT MaSach\n" +
                        "        FROM CTPhieuMuon ctpm\n" +
                        "        WHERE MaSach NOT IN (\n" +
                        "            SELECT MaSach\n" +
                        "            FROM CTPhieuTra\n" +
                        "        )\n" +
                        "    )\n" +
                        ") AS subquery";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PhieuMuonToHop pm = new PhieuMuonToHop();
                    pm.setHoTen(reader["HoTenDG"].ToString());


                    listPM.Add(pm);
                }

                connectionSQL.Close();
            }

            return listPM;
        }


        


        public List<PhieuMuonToHop> selectMaMNotInPT(String Condition,String Condition2, int index)
        {
            List<PhieuMuonToHop> listPM = new List<PhieuMuonToHop>();
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
                    query = "select MaM\n" +
                "from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG\n" +
                "where HoTenDG = N'" + Condition + "' and MaM not in (select MaM from PhieuTra)";
                }
                if (index == 2)
                {
                    query = "select pm.MaM\n" +
"                from PhieuMuon pm join DocGia dg on pm.MaDG = dg.MaDG join CTPhieuMuon ctpm on ctpm.MaM = pm.MaM\n" +
"                where HoTenDG = N'" + Condition + "' and MaSach ='" + Condition2 + "' ";
                }


                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PhieuMuonToHop pm = new PhieuMuonToHop();
                    pm.setMaM(reader["MaM"].ToString());
                    listPM.Add(pm);

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
            return listPM;
        }
    }
}
