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
    internal class CTPhieuTraDAO
    {
        public static CTPhieuTraDAO getInstance()
        {
            return new CTPhieuTraDAO();
        }

        KetNoi connectionData = new KetNoi();

        public void insert(CTPhieuTra t)
        {


            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                conn.Open();
                string query = "insert into CTPhieuTra(MaT,MaSach,MaVP,Phat) " +
                               "VALUES(@MaT, @MaSach, @MaVP, @Phat)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaT", t.getMaT());
                cmd.Parameters.AddWithValue("@MaSach", t.getMaSach());
                cmd.Parameters.AddWithValue("@MaVP", t.getMaVP());
                cmd.Parameters.AddWithValue("@Phat", t.getPhat());



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


        public List<CTPhieuTra> selectMaSachNotInPT(String Condition,String Condition2, int index)
        {
            List<CTPhieuTra> listMaSach = new List<CTPhieuTra>();
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
                    query = "select s.MaSach\n" +
                        "from DocGia dg join PhieuMuon pm on dg.MaDG = pm.MaDG join CTPhieuMuon ctpm on pm.MaM = ctpm.MaM\n" +
                        "join Sach s on s.MaSach = ctpm.MaSach join Dausach ds on ds.ISBN = s.ISBN\n" +
                        "where dg.HoTenDG = N'" + Condition + "' and TenSach =N'" + Condition2 + "' and pm.MaM not in (\n" +
                        "	select MaM\n" +
                        "	from PhieuTra\n" +
                        ")";
                }
                if (index == 2)
                {
                    query = "select s.MaSach\n" +
                        "from DocGia dg join PhieuMuon pm on dg.MaDG = pm.MaDG join CTPhieuMuon ctpm on pm.MaM = ctpm.MaM\n" +
                        "join Sach s on s.MaSach = ctpm.MaSach join Dausach ds on ds.ISBN = s.ISBN\n" +
                        "where dg.HoTenDG = N'" + Condition + "' and TenSach =N'" + Condition2 + "'";


                   
                }
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CTPhieuTra pt = new CTPhieuTra();
                    string MaSach = reader["MaSach"].ToString();
                    pt.setMaSach(MaSach);




                    listMaSach.Add(pt);
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
            return listMaSach;
        }
    }
}
