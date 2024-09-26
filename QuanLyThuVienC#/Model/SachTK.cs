using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class SachTK : DauSach
    {
        private String MaSach;
        private int NamXB;

        public SachTK()
        {

        }

        public SachTK(String ISBN,String MaSach,String TenSach,String TacGia,String NhaXB,int NamXB, String ThongTin) : base(ISBN,TenSach,TacGia,NhaXB, ThongTin) 
        {
            this.MaSach = MaSach;
            this.NamXB = NamXB;
        }

        public void setMaSach(String MaSach)
        {
            this.MaSach = MaSach;
        }

        public String getMaSach()
        {
            return this.MaSach;
        }


        public void setNamXB(int NamXB)
        {
            this.NamXB = NamXB;
        }

        public int getNamXB()
        {
            return this.NamXB;
        }

    }
}
