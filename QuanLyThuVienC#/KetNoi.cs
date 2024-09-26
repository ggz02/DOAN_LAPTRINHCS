using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien
{
    internal class KetNoi
    {
        public static String strConn = "Data Source=ADMIN-PC\\SQLEXPRESS; Initial Catalog=QuanLyThuVienUFM; Integrated Security=True";
        public SqlConnection sqlconn = new SqlConnection(strConn);

        public String GetStrConn()
        {
            return strConn;
        }

        public SqlConnection ConnectionDataBase()
        {
            sqlconn.Open();
            return sqlconn;
        }

        public SqlConnection CloseDataBase()
        {
            sqlconn.Close();
            return sqlconn;
        }

        public int ExcuteNonQuery(string query)
        {
            int data = 0;
            using (SqlConnection ketnoi = new SqlConnection(strConn))
            {
                ketnoi.Open();
                SqlCommand thucthi = new SqlCommand(query, ketnoi);
                data = thucthi.ExecuteNonQuery();
                ketnoi.Close();
            }
            return data;
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            using (SqlConnection ketnoi = new SqlConnection(strConn))
            {
                ketnoi.Open();
                SqlCommand thucthi = new SqlCommand(query, ketnoi);
                SqlDataAdapter laydulieu = new SqlDataAdapter(thucthi);
                laydulieu.Fill(dt);
                ketnoi.Close();
            }
            return dt;
        }



    }
}
