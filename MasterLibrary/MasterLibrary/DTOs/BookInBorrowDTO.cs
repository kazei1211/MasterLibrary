using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class BookInBorrowDTO
    {
        public int MaPhieuMuon { get; set; }
        public string TenKH { get; set; }
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string img { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongMax { get; set; }
        public int Gia { get; set; }
        public DateTime NgayHetHan { get; set; }
        public DateTime NgayMuon { get; set; }
    }

    public class BookInCollectDTO
    {
        public int MaPhieuMuon { get; set; }
        public string TenKH { get; set; }
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongMax { get; set; }
        public int SoLuongHong { get; set; }

        public int TienHong { get; set; }
        public int TongTienHong { get; set; }
        public string TongTienHongStr
        {
            get
            {
                return Helper.FormatVNMoney(TongTienHong);
            }
        }

        public int TienTre { get; set; }
        public string TienTreStr
        {
            get
            {
                return Helper.FormatVNMoney(TienTre);
            }
        }

        public int TongTienTreMotCuon { get; set; }

        public int TongTienTra { get; set; }
        public string TongTienTraStr
        {
            get
            {
                return Helper.FormatVNMoney(TongTienTra);
            }
        }

        public DateTime NgayHetHan { get; set; }
        public DateTime NgayTra { get; set; }
    }
}
