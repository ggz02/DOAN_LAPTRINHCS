using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class PhieuMuonTK : PhieuMuon
    {

        private int SoNgayTre;

        public PhieuMuonTK(String MaM, String MaDG, String MaTT, String NgayM, String NgayHH, int SL, String GhiChu, int SoNgayTre):base(MaM, MaDG, MaTT, NgayM, NgayHH, SL, GhiChu)
        {
           
            this.SoNgayTre = SoNgayTre;
        }

        public int getSoNgayTre()
        {
            return SoNgayTre;
        }

        public void setSoNgayTre(int SoNgayTre)
        {
            this.SoNgayTre = SoNgayTre;
        }
    }
}
