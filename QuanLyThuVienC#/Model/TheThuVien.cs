using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.Model
{
    internal class TheThuVien
    {
        private string MaThe;
        private string MaDG;
        private string NgayTao;
        private string HanThe;

        public TheThuVien() { }

        public TheThuVien(string MaThe, string MaDG, string NgayTao, string HanThe)
        {
            this.MaThe = MaThe;
            this.MaDG = MaDG;
            this.NgayTao = NgayTao;
            this.HanThe = HanThe;
        }

        public string MaTheProperty
        {
            get { return MaThe; }
            set { MaThe = value; }
        }

        public string MaDGProperty
        {
            get { return MaDG; }
            set { MaDG = value; }
        }

        public string NgayTaoProperty
        {
            get { return NgayTao; }
            set { NgayTao = value; }
        }

        public string HanTheProperty
        {
            get { return HanThe; }
            set { HanThe = value; }
        }
    }
}
