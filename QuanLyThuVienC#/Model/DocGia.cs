using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class DocGia
    {
        private String MaDG;
        private String HoTenDG;
        private String NgaySinhDG;
        private String GioiTinhDG;
        private String DienThoaiDG;
        private String DiaChiDG;
        private String DoiTuong;

        public DocGia() { }

        public DocGia(String Ma, String Hoten, String NgaySinh, String GioiTinh, String DT, String DiaChi, String DoiTuong)
        {
            this.MaDG = Ma;
            this.HoTenDG = Hoten;
            this.NgaySinhDG = NgaySinh;
            this.GioiTinhDG = GioiTinh;
            this.DienThoaiDG = DT;
            this.DiaChiDG = DiaChi;
            this.DoiTuong = DoiTuong;
        }

        public String getMaDG()
        {
            return this.MaDG;
        }

        public void setMaDG(String Ma)
        {
            this.MaDG = Ma;
        }

        public String getHoTenDG()
        {
            return this.HoTenDG;
        }

        public void setHoTenDG(String Hoten)
        {
            this.HoTenDG = Hoten;
        }

        public String getNgaySinhDG()
        {
            return this.NgaySinhDG;
        }

        public void setNgaySinhDG(String NgaySinhDG)
        {
            this.NgaySinhDG = NgaySinhDG;
        }

        public String getGioiTinhDG()
        {
            return this.GioiTinhDG;
        }

        public void setGioiTinhDG(String GioiTinhDG)
        {
            this.GioiTinhDG = GioiTinhDG;
        }

        public String getDienThoaiDG()
        {
            return this.DienThoaiDG;
        }

        public void setDienThoaiDG(String DienThoaiDG)
        {
            this.DienThoaiDG = DienThoaiDG;
        }

        public String getDiaChiDG()
        {
            return this.DiaChiDG;
        }

        public void setDiaChiDG(String DiaChiDG)
        {
            this.DiaChiDG = DiaChiDG;
        }

        public String getDoiTuong()
        {
            return this.DoiTuong;
        }

        public void setDoiTuong(String DoiTuong)
        {
            this.DoiTuong = DoiTuong;
        }
    }
}
