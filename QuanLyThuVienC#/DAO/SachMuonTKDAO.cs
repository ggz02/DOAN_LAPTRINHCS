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
    internal class SachMuonTKDAO
    {
        KetNoi connectionData = new KetNoi();

        public static SachMuonTKDAO getInstance()
        {
            return new SachMuonTKDAO();
        }

        public List<SachMuonTK> SelectByCondition(string Condition, int index)
        {
            List<SachMuonTK> listSM = new List<SachMuonTK>();
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
                    querry = "SELECT pm.MaM,s.ISBN,s.MaSach,s.NamXB,Ngayhethan\n" +
                    "from Sach s join CTPhieuMuon ctpm on s.MaSach = ctpm.MaSach join PhieuMuon pm on pm.MaM = ctpm.MaM\n" +
                    "where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and CONVERT(date, pm.Ngaymuon) = CONVERT(date, GETDATE())" +
                    "order by ISBN";
                }

                if (index == 1)
                {
                    querry = "SELECT pm.MaM,s.ISBN,s.MaSach,s.NamXB,Ngayhethan\n" +
                    "from Sach s join CTPhieuMuon ctpm on s.MaSach = ctpm.MaSach join PhieuMuon pm on pm.MaM = ctpm.MaM\n" +
                    "where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and MONTH(Ngaymuon) = MONTH(GETDATE())\n" +
                    "order by ISBN";
                }
                if (index == 2)
                {
                    querry = "SELECT pm.MaM,s.ISBN,s.MaSach,s.NamXB,Ngayhethan\n" +
                    "from Sach s join CTPhieuMuon ctpm on s.MaSach = ctpm.MaSach join PhieuMuon pm on pm.MaM = ctpm.MaM\n" +
                    "where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and Year(Ngaymuon) = Year(GETDATE())\n" +
                    "order by ISBN";
                }
                if (index == 3)
                {
                    querry = "SELECT pm.MaM,s.ISBN,s.MaSach,s.NamXB,Ngayhethan\n" +
                "from Sach s join CTPhieuMuon ctpm on s.MaSach = ctpm.MaSach join PhieuMuon pm on pm.MaM = ctpm.MaM\n" +
                "where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and Year(Ngaymuon) = year(GETDATE()) -1\n" +
                "order by ISBN";
                }
                if (index == 4)
                {
                    querry = "SELECT pm.MaM,s.ISBN,s.MaSach,s.NamXB,Ngayhethan\n" +
                        "from Sach s join CTPhieuMuon ctpm on s.MaSach = ctpm.MaSach join PhieuMuon pm on pm.MaM = ctpm.MaM\n" +
                        "where DatedIff (day, GETDATE(), pm.Ngayhethan) > 0 and ('" + Condition + "' between Ngaymuon and Ngayhethan)\n" +
                        "order by ISBN";
                }
                


                cmd.CommandText = querry;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string MaM = reader["MaM"].ToString();
                    string ISBN = reader["ISBN"].ToString();
                    string MaSach = reader["MaSach"].ToString();
                    int NamXB = int.Parse(reader["NamXB"].ToString());
                    string NgayHetHan = reader["Ngayhethan"].ToString();


                    SachMuonTK sachMuon = new SachMuonTK(MaM, ISBN, MaSach, NamXB, NgayHetHan);
                    listSM.Add(sachMuon);
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
            return listSM;
        }



    }
}
