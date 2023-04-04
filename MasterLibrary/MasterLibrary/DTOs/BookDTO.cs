using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterLibrary.Utils;

namespace MasterLibrary.DTOs
{
    public class BookDTO
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public int NamXB { get; set; }
        public string MoTa { get; set; }
        public int MaTang { get; set; }
        public string TenTang { get; set; }
        public int MaDay { get; set; }
        public string TenDay { get; set; }
        public string ImageSource { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string TheLoai { get; set; }
        public string NXB { get; set; }
        public bool IsIncomplete { get; set; }
        public string GiaStr
        {
            get
            {
                return Helper.FormatVNMoney(Gia);
            }
        }
    }
}