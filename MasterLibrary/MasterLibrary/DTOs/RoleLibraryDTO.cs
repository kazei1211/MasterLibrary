using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class RoleLibraryDTO
    {
        public int Songaymuon { get; set; }
        public decimal TienTraTreMotNgay { get; set; }
        public string TienTraTreMotNgayStr
        {
            get
            {
                return Utils.Helper.FormatVNMoney(TienTraTreMotNgay);
            }
        }
    }
}
