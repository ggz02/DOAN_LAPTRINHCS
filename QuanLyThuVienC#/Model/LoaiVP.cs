using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class LoaiVP
    {
        private String MaVP;
        private String TenLoaiVP;
        private String CachXuLy;

        public LoaiVP() { }

        public LoaiVP(String MaVP, String TenLoaiVP, String CachXuLy)
        {
            this.MaVP = MaVP;
            this.TenLoaiVP = TenLoaiVP;
            this.CachXuLy = CachXuLy;
        }

        public void setMaVP(String MaVP)
        {
            this.MaVP = MaVP;
        }

        public String getMaVP()
        {
            return this.MaVP;
        }

        public void setTenLoaiVP(String TenLoaiVP)
        {
            this.TenLoaiVP = TenLoaiVP;
        }

        public String getTenLoaiVP()
        {
            return this.TenLoaiVP;
        }

        public void setCachXuLy(String CachXuLy)
        {
            this.CachXuLy = CachXuLy;
        }

        public String getCachXuLy()
        {
            return this.CachXuLy;
        }
    }
}
