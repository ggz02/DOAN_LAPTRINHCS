using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class TheLoai
    {
            private string MaLoai;
            private string TenLoai;

            public TheLoai() { }

            public TheLoai(string MaLoai, string TenLoai)
            {
                this.MaLoai = MaLoai;
                this.TenLoai = TenLoai;
            }

            public void setMaLoai(String MaLoai)
            {
                this.MaLoai = MaLoai;
            }

            public String getMaLoai()
            {
                return this.MaLoai;
            }

            public void setTenLoai(String TenLoai)
            {
                this.TenLoai = TenLoai;
            }

            public String getTenLoai()
            {
                return this.TenLoai;
            }
        
    }
}
