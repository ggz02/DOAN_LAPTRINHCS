using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class CTPhieuTra
    {
        private String MaT;
        private String MaSach;
        private String MaVP;
        private float Phat;

        public CTPhieuTra() { }

        public CTPhieuTra(String MaT, String MaM, String MaSach, String MaVP, float Phat)
        {
            this.MaT = MaT;
            this.MaSach = MaSach;
            this.MaVP = MaVP;
            this.Phat = Phat;
        }

        public void setMaT(String MaT)
        {
            this.MaT = MaT;
        }

        public String getMaT()
        {
            return this.MaT;
        }


        public void setMaSach(String MaSach)
        {
            this.MaSach = MaSach;
        }

        public String getMaSach()
        {
            return this.MaSach;
        }

        public void setMaVP(String MaVP)
        {
            this.MaVP = MaVP;
        }

        public String getMaVP()
        {
            return this.MaVP;
        }

        public void setPhat(float Phat)
        {
            this.Phat = Phat;
        }

        public float getPhat()
        {
            return this.Phat;
        }
    }
}
