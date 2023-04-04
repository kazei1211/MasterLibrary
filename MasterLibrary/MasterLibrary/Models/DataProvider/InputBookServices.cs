using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace MasterLibrary.Models.DataProvider
{
    public class InputBookServices
    {
        private static InputBookServices _ins;
        public static InputBookServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new InputBookServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        //get receipt
        public async Task<List<InputBookDTO>> GetBookInput()
        {
            List<InputBookDTO> BookInput;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    BookInput = await (from ct in context.CHITIET_NHAP
                                       select new InputBookDTO
                                       {
                                           IDInput = ct.SOHD,
                                           TenSach = ct.TENSACH,
                                           GiaNhap = (int)ct.NHAPKHO.TRIGIA,
                                           NgNhap = ct.NHAPKHO.NGNHAP,
                                           SoLuong = (int)ct.SL,
                                       }).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return BookInput;
        }


        //get receipt by month
        public async Task<List<InputBookDTO>> GetBookInput(int month, int year)
        {
            List<InputBookDTO> BookInput;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    BookInput = await (from ct in context.CHITIET_NHAP
                                       where ct.NHAPKHO.NGNHAP.Year == year && ct.NHAPKHO.NGNHAP.Month == month
                                       select new InputBookDTO
                                       {
                                           IDInput = ct.SOHD,
                                           TenSach = ct.TENSACH,
                                           GiaNhap = (int)ct.NHAPKHO.TRIGIA,
                                           NgNhap = ct.NHAPKHO.NGNHAP,
                                           SoLuong = (int)ct.SL,
                                       }).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return BookInput;
        }

        public async Task<decimal> GetInputMoneyList(List<BookInCollectDTO> feeBook)
        {
            decimal inputMoney = 0;
            List<CHITIET_NHAP> input = null;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    for (int i = 0; i < feeBook.Count ; i++)
                    {
                        var namebook = feeBook[i].TenSach;
                        input = (List<CHITIET_NHAP>)context.CHITIET_NHAP.Where(b => b.TENSACH == namebook).ToList();
                    }
                    if (input != null)
                    {
                        inputMoney += (decimal)input.Sum(b => b.GIANHAP);
                    }
                }
            }
            catch (Exception)
            {

            }
            return inputMoney;
        }
    }
}
