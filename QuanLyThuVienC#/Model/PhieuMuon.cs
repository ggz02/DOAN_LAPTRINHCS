using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class PhieuMuon
    {
        private String MaM;
        private String MaDG;
        private String MaTT;
        private String NgayMuon;
        private String NgayHetHan;
        private int SoLuongMuon;
        private String GhiChu;

        public PhieuMuon() { }

        public PhieuMuon(String MaM, String MaDG, String MaTT, String NgayM, String NgayHH, int SL, String GhiChu)
        {
            this.MaM = MaM;
            this.MaDG = MaDG;
            this.MaTT = MaTT;
            this.NgayMuon = NgayM;
            this.NgayHetHan = NgayHH;
            this.SoLuongMuon = SL;
            this.GhiChu = GhiChu;
        }

        public void setMaM(String MaM)
        {
            this.MaM = MaM;
        }

        public String getMaM()
        {
            return this.MaM;
        }

        public void setMaDG(String MaDG)
        {
            this.MaDG = MaDG;
        }

        public String getMaDG()
        {
            return this.MaDG;
        }

        public void setMaTT(String MaTT)
        {
            this.MaTT = MaTT;
        }

        public String getMaTT()
        {
            return this.MaTT;
        }

        public void setNgayMuon(String NgayMuon)
        {
            this.NgayMuon = NgayMuon;
        }

        public String getNgayMuon()
        {
            return this.NgayMuon;
        }

        public void setNgayHetHan(String NgayHetHan)
        {
            this.NgayHetHan = NgayHetHan;
        }

        public String getNgayHetHan()
        {
            return this.NgayHetHan;
        }

        public void setSoLuongMuon(int SoLuongMuon)
        {
            this.SoLuongMuon = SoLuongMuon;
        }

        public int getSoLuongMuon()
        {
            return this.SoLuongMuon;
        }

        public void setGhiChu(String GhiChu)
        {
            this.GhiChu = GhiChu;
        }

        public String getGhiChu()
        {
            return this.GhiChu;
        }
    }
}
