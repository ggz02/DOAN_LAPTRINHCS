using QuanLyThuVien.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DAO
{
    internal class LoaiVPDAO
    {
        KetNoi connectionData = new KetNoi();

        public static LoaiVPDAO getInstance()
        {
            return new LoaiVPDAO();
        }

        public List<LoaiVP> selectTenViPham()
        {
            List<LoaiVP> listVP = new List<LoaiVP>();
            using (SqlConnection connectionSQL = new SqlConnection(connectionData.GetStrConn()))
            {
                string query = "select TenLoaiVP\n" +
                        " from LoaiVP";
                SqlCommand command = new SqlCommand(query, connectionSQL);

                connectionSQL.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    LoaiVP viPham = new LoaiVP();
                    viPham.setTenLoaiVP(reader["TenLoaiVP"].ToString());


                    listVP.Add(viPham);
                }

                connectionSQL.Close();
            }
            return listVP;
        }


    }
}
