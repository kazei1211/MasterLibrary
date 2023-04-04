using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class BillDetailDTO
    {
        public int MaHD { get; set; }
        public int MaSach { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaMoiCai { get; set; }
    }
}
