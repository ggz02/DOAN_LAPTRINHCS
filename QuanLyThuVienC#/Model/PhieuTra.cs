using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class PhieuTra
    {
        private String MaT;
        private String MaM;
        private String NgayTra;
        private int SoLuongM;
        private int SoLuongT;


        public PhieuTra() { }

        public PhieuTra(String MaT, String MaM, String NgayTra, int SLM, int SLT)
        {
            this.MaT = MaT;
            this.MaM = MaM;
            this.NgayTra = NgayTra;
            this.SoLuongM = SLM;
            this.SoLuongT = SLT;
        }

        public void setMaT(String MaT)
        {
            this.MaT = MaT;
        }

        public String getMaT()
        {
            return this.MaT;
        }

        public void setMaM(String MaM)
        {
            this.MaM = MaM;
        }

        public String getMaM()
        {
            return this.MaM;
        }

        public void setNgayTra(String NgayTra)
        {
            this.NgayTra = NgayTra;
        }

        public String getNgayTra()
        {
            return this.NgayTra;
        }

        public void setSoLuongM(int SoLuongM)
        {
            this.SoLuongM = SoLuongM;
        }

        public int getSoLuongM()
        {
            return this.SoLuongM;
        }

        public void setSoLuongT(int SoLuongT)
        {
            this.SoLuongT = SoLuongT;
        }

        public int getSoLuongT()
        {
            return this.SoLuongT;
        }
    }
}
