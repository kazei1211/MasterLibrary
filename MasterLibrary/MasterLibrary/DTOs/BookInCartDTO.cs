using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class BookInCartDTO
    {
        public int MaKH { get; set; }
        public int MaSach { get; set;}
        public string TenSach { get; set;}
        public string TacGia { get; set;}
        public string TheLoai { get; set;}
        public string SourceImg { get; set; }
        public int SoLuongHT { get; set;}
        public int SoLuongMax { get; set;}
        public decimal Gia { get; set;}

        public string GiaStr
        {
            get
            {
                return Helper.FormatVNMoney(Gia);
            }
        }
    }
}
