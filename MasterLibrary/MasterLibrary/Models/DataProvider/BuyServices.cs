using MasterLibrary.DTOs;
using MasterLibrary.Views.MessageBoxML;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MasterLibrary.Models.DataProvider
{
    public class BuyServices
    {
        private static BuyServices _ins;
        public static BuyServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BuyServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<int> CreateNewBill(BillDTO bill)
        {
            using (var context = new MasterlibraryEntities())
            {
                HOADON newBill = new HOADON();
                newBill.NGHD = bill.NGHD;
                newBill.MAKH = bill.MAKH;
                newBill.TRIGIA = bill.TRIGIA;

                context.HOADONs.Add(newBill);

                context.SaveChanges();

                int newIdBill = await context.HOADONs.MaxAsync(hd => hd.MAHD);

                return newIdBill;
            }
        }

        public async Task<(bool, string)> CreateNewBillDetail(int IdBill, List<BillDetailDTO> BillDetailList)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    List<CTHD> newBillDetailList = new List<CTHD>();

                    for (int i = 0; i < BillDetailList.Count; ++i)
                    {
                        CTHD newCTHD = new CTHD();
                        newCTHD.MAHD = IdBill;
                        newCTHD.MASACH = BillDetailList[i].MaSach;
                        newCTHD.SOLUONG = BillDetailList[i].SoLuong;

                        newBillDetailList.Add(newCTHD);

                        // Trừ đi số lượng sách đã mua

                        var _sach = await context.SACHes.FindAsync(BillDetailList[i].MaSach);

                        if (_sach != null)
                        {
                            if (_sach.SL < BillDetailList[i].SoLuong)
                            {
                                return (false, context.SACHes.Find(BillDetailList[i].MaSach).TENSACH + "vượt số lượng cho phép vui lòng làm mới trang hoặc chỉnh lại số lượng cho phép");
                            }

                            _sach.SL -= BillDetailList[i].SoLuong;
                        }

                    }

                    context.CTHDs.AddRange(newBillDetailList);

                    context.SaveChanges();

                    return (true, "Mua thành công");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Xãy ra lỗi khi thao tác dữ liệu trên cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Xãy ra lỗi khi thực hiện thao tác");
            }
        }
    }
}
