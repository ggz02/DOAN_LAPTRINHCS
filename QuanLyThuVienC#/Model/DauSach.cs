using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class DauSach
    {
        public string ISBN;
        public string MaLoai;
        public string TenSach;
        public string TacGia;
        public string NXB;
        public int SoLuong;
        public string ThongTinSach;

        public DauSach() { }

        public DauSach(string ISBN, string MaLoai, string TenSach, string TacGia, string NXB, int SoLuong, string ThongTin)
        {
            this.ISBN = ISBN;
            this.MaLoai = MaLoai;
            this.TenSach = TenSach;
            this.TacGia = TacGia;
            this.NXB = NXB;
            this.SoLuong = SoLuong;
            this.ThongTinSach = ThongTin;
        }

        public DauSach(string ISBN, string TenSach, string TacGia, string NXB, string ThongTin)
        {
            this.ISBN = ISBN;
            this.TenSach = TenSach;
            this.TacGia = TacGia;
            this.NXB = NXB;
            this.ThongTinSach = ThongTin;
        }

        public DauSach(string TenSach)
        {
            this.TenSach = TenSach;
        }

        public void SetISBN(string ISBN)
        {
            this.ISBN = ISBN;
        }

        public string GetISBN()
        {
            return this.ISBN;
        }

        public void SetMaLoai(string MaLoai)
        {
            this.MaLoai = MaLoai;
        }

        public string GetMaLoai()
        {
            return this.MaLoai;
        }

        public void SetTenSach(string TenSach)
        {
            this.TenSach = TenSach;
        }

        public string GetTenSach()
        {
            return this.TenSach;
        }

        public void SetTacGia(string TacGia)
        {
            this.TacGia = TacGia;
        }

        public string GetTacGia()
        {
            return this.TacGia;
        }

        public void SetNXB(string NXB)
        {
            this.NXB = NXB;
        }

        public string GetNXB()
        {
            return this.NXB;
        }

        public void SetSoLuong(int SoLuong)
        {
            this.SoLuong = SoLuong;
        }

        public int GetSoLuong()
        {
            return this.SoLuong;
        }


        public void SetThongTinSach(string ThongTinSach)
        {
            this.ThongTinSach = ThongTinSach;
        }

        public string GetThongTinSach()
        {
            return this.ThongTinSach;
        }
    }
}
