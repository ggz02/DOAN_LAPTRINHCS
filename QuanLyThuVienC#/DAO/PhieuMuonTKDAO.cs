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
    internal class PhieuMuonTKDAO
    {

        KetNoi connectionData = new KetNoi();

        public static PhieuMuonTKDAO getInstance()
        {
            return new PhieuMuonTKDAO();
        }

        public List<PhieuMuonTK> SelectByCondition(string Condition, int index)
        {
            List<PhieuMuonTK> listPMTKDAO = new List<PhieuMuonTK>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                cmd = conn.CreateCommand();
                string querry = "";
                if (index == 0)
                {

                    querry = "select *,DATEDIFF(DAY,Ngayhethan,GETDATE()) as N'SoNgayTre'\n" +
                            "from PhieuMuon\n" +
                            "	where DATEDIFF(DAY,Ngayhethan,GETDATE()) > 0 and CONVERT(date, pm.Ngayhethan) = CONVERT(date, GETDATE()) and MaM not in (\n" +
                            "		select MaM\n" +
                            "		from PhieuTra\n" +
                            "	)";
                }

                if (index == 1)
                {
                    querry = "select *,DATEDIFF(DAY,Ngayhethan,GETDATE()) as N'SoNgayTre'\n" +
                            "from PhieuMuon\n" +
                            "	where DATEDIFF(DAY,Ngayhethan,GETDATE()) > 0 and MONTH(Ngayhethan) = MONTH(GETDATE()) and MaM not in (\n" +
                            "		select MaM\n" +
                            "		from PhieuTra\n" +
                            "	)";
                }
                if (index == 2)
                {
                    querry = "select *,DATEDIFF(DAY,Ngayhethan,GETDATE()) as N'SoNgayTre'\n" +
                        "from PhieuMuon\n" +
                        "	where DATEDIFF(DAY,Ngayhethan,GETDATE()) > 0 and Year(Ngayhethan) = year(GETDATE()) and MaM not in (\n" +
                        "		select MaM\n" +
                        "		from PhieuTra\n" +
                        "	)";
                }
                if (index == 3)
                {
                    querry = "select *,DATEDIFF(DAY,Ngayhethan,GETDATE()) as N'SoNgayTre'\n" +
                        "from PhieuMuon\n" +
                        "	where DATEDIFF(DAY,Ngayhethan,GETDATE()) > 0 and Year(Ngayhethan) = year(GETDATE()) - 1 and MaM not in (\n" +
                        "		select MaM\n" +
                        "		from PhieuTra\n" +
                        "	)";
                }
                if (index == 4)
                {
                    querry = "select *,DATEDIFF(DAY,Ngayhethan,GETDATE()) as N'SoNgayTre'\n" +
                        "from PhieuMuon\n" +
                        "	where DATEDIFF(DAY,Ngayhethan,GETDATE()) > 0 and  '" + Condition + "' = Ngayhethan and MaM not in (\n" +
                        "		select MaM\n" +
                        "		from PhieuTra\n" +
                        "	)";
                }



                cmd.CommandText = querry;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string MaM = reader["MaM"].ToString();
                    string MaDG = reader["MaDG"].ToString();
                    string MaTT = reader["MaTT"].ToString();
                    string NgayMuon = reader["Ngaymuon"].ToString();
                    string NgayHetHan = reader["Ngayhethan"].ToString();
                    int SoLuongM = int.Parse( reader["SoLuongM"].ToString());
                    String GhiChu = reader["GhiChu"].ToString();
                    int SoNgayTre = int.Parse(reader["SoNgayTre"].ToString());


                    PhieuMuonTK PhieuMuon = new PhieuMuonTK(MaM,MaDG,MaTT,NgayMuon,NgayHetHan,SoLuongM, GhiChu, SoNgayTre);
                    listPMTKDAO.Add(PhieuMuon);
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
            return listPMTKDAO;
        }


    }
}
