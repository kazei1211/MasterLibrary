using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class InputBookDTO
    {
        public InputBookDTO() { }

        public int IDInput { get; set; }
        public int IDBook { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public string NhaXuatBan { get; set; }
        public int GiaNhap { get; set; }
        public string GiaNhapStr
        {
            get
            {
                return Helper.FormatVNMoney(GiaNhap);
            }
        }
        public int GiaBan { get; set; }
        public int SoLuong { get; set; }
        public DateTime NgNhap { get; set; }
    }
}
