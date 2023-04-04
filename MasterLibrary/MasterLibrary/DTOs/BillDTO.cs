using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.DTOs
{
    public class BillDTO
    {
        public BillDTO()
        {

        }

        public int MAKH { get; set; }
        private int _cusId;
        public int cusId
        {
            get
            {
                if (_cusId.ToString() is null)
                {
                    return -1;
                }
                return _cusId; 
            }
            set { _cusId = value; }
        }
        private string _cusName;
        public string cusName
        {
            set { _cusName = value; }
            get 
            {
                if (_cusName is null)
                {
                    return "Khách hàng mới";
                }
                return _cusName; 
            }
        }
        public int MAHD { get; set; }
        public decimal TRIGIA { get; set; }
        public string TRIGIAStr
        {
            get
            {
                return Helper.FormatVNMoney(TRIGIA);
            }
        } 
        public DateTime NGHD { get; set; }


        //sử dụng để gán thông tin cho hoá đơn bán hàng
        public string cusAdd { get; set; }
        public string bookName { get; set; }
    }
}
