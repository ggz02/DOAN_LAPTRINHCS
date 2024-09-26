using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien.DAO
{
    internal class PhieuNhapToHopDAO
    {
        KetNoi connectionData = new KetNoi();
        public static PhieuNhapToHopDAO getInstance()
        {
            return new PhieuNhapToHopDAO();
        }

        public List<PhieuNhapToHop> SelectByCondition(string Condition, int index)
        {
            List<PhieuNhapToHop> listPN = new List<PhieuNhapToHop>();
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
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum(Gianhap) as 'TongThanhTien'\n" +
                        "FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
                        "join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
                        "group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
                        "having Ngaytao = @Condition";
                }
                if (index == 2)
                {
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum(Gianhap) as 'TongThanhTien'\n" +
                        "FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
                        "join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
                        "group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
                        "having Month(Ngaytao) = @Condition and Year(Ngaytao) = year(getdate())";
                }
                if (index == 3)
                {
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum( Gianhap) as N'TongThanhTien'\n" +
                "           FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
                "            join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
                "            group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
                "			having Ngaynhap = '" + Condition + "'";
                }
                if (index == 4)
                {
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum( Gianhap) as N'TongThanhTien'\n" +
            "           FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
            "            join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
            "            group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
            "		having Month(Ngaynhap) = '" + Condition + "' and Year(Ngaynhap) = year(getdate())";
                }
                if (index == 5)
                {
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum(Gianhap) as N'TongThanhTien'\n" +
                "           FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
                "            join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
                "            group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
                "		having Year(Ngaytao) = year(getdate())";
                }
                if (index == 6)
                {
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum(Gianhap) as N'TongThanhTien'\n" +
            "           FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
            "            join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
            "            group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
            "		having Year(Ngaynhap) = year(getdate())";
                }
                if (index == 7)
                {
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum(Gianhap) as N'TongThanhTien'\n" +
                "           FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
                "            join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
                "            group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
                "		having Year(Ngaytao) = year(getdate()) -1";
                }
                if (index == 8)
                {
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum(Gianhap) as N'TongThanhTien'\n" +
            "           FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
            "            join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
            "            group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
            "		having Year(Ngaynhap) = year(getdate()) -1";
                }
                if (index == 9)
                {
                    query = "select pn.SoPN,TenNCC,Ngaytao,Ngaynhap,sum(Gianhap) as N'TongThanhTien'\n" +
            "           FROM PhieuNhap pn join CTPhieuNhap ctpn on pn.SoPN = ctpn.SoPN\n" +
            "            join Nhacungcap ncc on pn.MaNCC = ncc.MaNCC\n" +
            "            group by pn.SoPN,TenNCC,Ngaytao,Ngaynhap\n" +
            "		having pn.SoPn = '" + Condition + "'";
                }


                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Condition", Condition);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string soPN = reader["SoPN"].ToString();
                    string TenNCC = reader["TenNCC"].ToString();
                    string NgayTao = reader["Ngaytao"].ToString();
                    string NgayNhap = reader["Ngaynhap"].ToString();
                    long TongThanhTien = long.Parse(reader["TongThanhTien"].ToString());
                    

                    PhieuNhapToHop pn = new PhieuNhapToHop(soPN, TenNCC, NgayTao, NgayNhap, TongThanhTien);
                    listPN.Add(pn);
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
            return listPN;
        }


    }
}
