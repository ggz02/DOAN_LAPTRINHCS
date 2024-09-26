using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class NhaCungCap
    {
        private String MaNCC;
        private String TenNCC;
        private String DiaChi;
        private String DienThoai;
        private String Website;

        public NhaCungCap()
        {
        }

        public NhaCungCap(String MaNCC, String TenNCC, String DC, String DT, String Website)
        {
            this.MaNCC = MaNCC;
            this.TenNCC = TenNCC;
            this.DiaChi = DC;
            this.DienThoai = DT;
            this.Website = Website;
        }

        public void setMaNCC(String MaNCC)
        {
            this.MaNCC = MaNCC;
        }

        public String getMaNCC()
        {
            return this.MaNCC;
        }

        public void setTenNCC(String TenNCC)
        {
            this.TenNCC = TenNCC;
        }

        public String getTenNCC()
        {
            return this.TenNCC;
        }

        public void setDiaChi(String DiaChi)
        {
            this.DiaChi = DiaChi;
        }

        public String getDiaChi()
        {
            return this.DiaChi;
        }

        public void setDienThoai(String DienThoai)
        {
            this.DienThoai = DienThoai;
        }

        public String getDienThoai()
        {
            return this.DienThoai;
        }

        public void setWebsite(String Website)
        {
            this.Website = Website;
        }

        public String getWebsite()
        {
            return this.Website;
        }

    }
}
