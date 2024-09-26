using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class ThuThu
    {
        private String MaTT;
        private String HoTenTT;
        private String NgaySinhTT;
        private String GioiTinhTT;
        private String DienThoaiTT;
        private String DiaChiTT;

        public ThuThu() { }

        public ThuThu(String Ma, String Hoten, String NgaySinh, String GioiTinh, String DT, String DiaChi)
        {
            this.MaTT = Ma;
            this.HoTenTT = Hoten;
            this.NgaySinhTT = NgaySinh;
            this.GioiTinhTT = GioiTinh;
            this.DienThoaiTT = DT;
            this.DiaChiTT = DiaChi;
        }

        public String getMaTT()
        {
            return this.MaTT;
        }

        public void setMaTT(String Ma)
        {
            this.MaTT = Ma;
        }

        public String getHoTenTT()
        {
            return this.HoTenTT;
        }

        public void setHoTenTT(String Hoten)
        {
            this.HoTenTT = Hoten;
        }

        public String getNgaySinhTT()
        {
            return this.NgaySinhTT;
        }

        public void setNgaySinhTT(String NgaySinh)
        {
            this.NgaySinhTT = NgaySinh;
        }

        public String getGioiTinhTT()
        {
            return this.GioiTinhTT;
        }

        public void setGioiTinhTT(String GioiTinh)
        {
            this.GioiTinhTT = GioiTinh;
        }

        public String getDienThoaiTT()
        {
            return this.DienThoaiTT;
        }

        public void setDienThoaiTT(String DienThoai)
        {
            this.DienThoaiTT = DienThoai;
        }

        public String getDiaChiTT()
        {
            return this.DiaChiTT;
        }

        public void setDiaChiTT(String DiaChi)
        {
            this.DiaChiTT = DiaChi;
        }
    }
}
