using MasterLibrary.Utils;
using System;

namespace MasterLibrary.DTOs
{
    public class TroubleDTO
    {
        public int MaSC { get; set; }
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public string Img { get; set; }
        public decimal ChiPhi { get; set; }
        public int MaLSC { get; set; }
        public string TenLoaiSuCo { get; set; }
        public string MaTTSC { get; set; }
        public string TenTrangThaiSuCo { get; set; }
        public DateTime NgayBaoCao { get; set; }

        public string ChiPhistr
        {
            get
            {
                return Helper.FormatVNMoney(ChiPhi);
            }
        }
    }

    public class StatusTroubleDTO
    {
        public string MaTTSC { get; set;}
        public string TenTrangThaiSuCo { get; set;}
    }

    public class TypeTroubleDTO
    {
        public int MaLSC { get; set; }
        public string TenLoaiSuCo { get; set; }
    }
}
