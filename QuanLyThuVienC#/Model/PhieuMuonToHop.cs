using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class PhieuMuonToHop
    {
        private String MaM;
        private String HoTen;
        private String NgayMuon;
        private String NgayHetHan;
        private int SoLuong;
        private String GhiChu;

        public PhieuMuonToHop()
        {

        }

        public PhieuMuonToHop(String MaM, String HoTen, String NgayMuon, String NgayHetHan, int SoLuong, String GhiChu)
        {
            this.MaM = MaM;
            this.HoTen = HoTen;
            this.NgayMuon = NgayMuon;
            this.NgayHetHan = NgayHetHan;
            this.SoLuong = SoLuong;
            this.GhiChu = GhiChu;
        }

        public String getMaM()
        {
            return this.MaM;
        }

        public void setMaM(String MaM)
        {
            this.MaM = MaM;
        }

        public String getHoTen()
        {
            return this.HoTen;
        }

        public void setHoTen(String HoTen)
        {
            this.HoTen = HoTen;
        }

        public String getNgayMuon()
        {
            return this.NgayMuon;
        }

        public void setNgayMuon(String NgayMuon)
        {
            this.NgayMuon = NgayMuon;
        }

        public String getNgayHetHan()
        {
            return this.NgayHetHan;
        }

        public void setNgayHetHan(String NgayHetHan)
        {
            this.NgayHetHan = NgayHetHan;
        }

        public int getSoLuong()
        {
            return this.SoLuong;
        }

        public void setSoLuong(int SoLuong)
        {
            this.SoLuong = SoLuong;
        }

        public String getGhiChu()
        {
            return this.GhiChu;
        }

        public void setGhiChu(String GhiChu)
        {
            this.GhiChu = GhiChu;
        }
    }
}
