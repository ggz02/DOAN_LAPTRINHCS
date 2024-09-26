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
    internal class TheLoaiDAO
    {

        KetNoi connectionData = new KetNoi();

        public static TheLoaiDAO getInstance()
        {
            return new TheLoaiDAO();
        }

        public List<TheLoai> SelectTenLoaiSach(string condition, int index)
        {
            List<TheLoai> listTL = new List<TheLoai>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;

            try
            {
                conn = new SqlConnection(connectionData.GetStrConn());
                string query = "";

                if (index == 1)
                {
                    query = "SELECT MaLoai, TenLoai FROM TheLoai";
                }
                else if (index == 2)
                {
                    query = "SELECT MaLoai, TenLoai FROM TheLoai WHERE TenLoai = N'" + condition + "'";
                }
                conn.Open();
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TheLoai tl = new TheLoai();
                    tl.setMaLoai(reader["MaLoai"].ToString());
                    tl.setTenLoai(reader["TenLoai"].ToString());
                    listTL.Add(tl);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (cmd != null)
                {
                    cmd.Dispose();
                }

                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return listTL;
        }
    }
}
