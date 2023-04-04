using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BillDetailServices
    {
        private static BillDetailServices _ins;
        public static BillDetailServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillDetailServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        
    }
}
