using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class CTPhieuMuon
    {
        private String MaM;
        private String MaSach;

        public CTPhieuMuon() { }

        public CTPhieuMuon(String MaM, String MaSach)
        {
            this.MaM = MaM;
            this.MaSach = MaSach;
        }

        public void setMaM(String MaM)
        {
            this.MaM = MaM;
        }

        public String getMaM()
        {
            return this.MaM;
        }

        public void setMaSach(String MaSach)
        {
            this.MaSach = MaSach;
        }

        public String getMaSach()
        {
            return this.MaSach;
        }
    }
}
