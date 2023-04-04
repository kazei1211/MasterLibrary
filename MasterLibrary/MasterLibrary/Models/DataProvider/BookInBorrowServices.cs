using MasterLibrary.DTOs;
using MasterLibrary.ViewModel.CustomerVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BookInBorrowServices
    {
        private static BookInBorrowServices _ins;
        public static BookInBorrowServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BookInBorrowServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string)> CreateNewCallCard(int idCustomer, DateTime _ExpirationDate, DateTime _DateNow, ObservableCollection<BookInBorrowDTO> BookInBorrowList)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    for (int i = 0; i < BookInBorrowList.Count; ++i)
                    {
                        PHIEUMUON newPhieuMuon = new PHIEUMUON();
                        newPhieuMuon.MAKH = idCustomer;
                        newPhieuMuon.MASACH = BookInBorrowList[i].MaSach;
                        newPhieuMuon.NGAYHETHAN = _ExpirationDate;
                        newPhieuMuon.SOLUONG = BookInBorrowList[i].SoLuong;
                        newPhieuMuon.NGAYMUON = _DateNow;

                        context.PHIEUMUONs.Add(newPhieuMuon);

                        // Trừ đi số lượng sách đã thuê
                        
                        var _sach = await context.SACHes.FindAsync(BookInBorrowList[i].MaSach);

                        if (_sach.SL < BookInBorrowList[i].SoLuong)
                        {
                            return (false, BookInBorrowList[i].TenSach + "vượt số lượng cho phép vui lòng làm mới trang hoặc chỉnh lại số lượng cho phép");
                        }

                        _sach.SL -= BookInBorrowList[i].SoLuong;
                    }

                    context.SaveChanges();

                    return (true, "Thuê thành công");
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

        public async Task<List<BookInBorrowDTO>> GetBookBorrowCustomer(int _makh)
        {
            List<BookInBorrowDTO> bookborrows = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    bookborrows = await (from sachmuon in context.PHIEUMUONs
                                   join sach in context.SACHes on sachmuon.MASACH equals sach.MASACH
                                   where sachmuon.MAKH == _makh
                                   select new BookInBorrowDTO
                                   {
                                       MaPhieuMuon = sachmuon.MAPHIEUMUON,
                                       MaSach = (int)sachmuon.MASACH,
                                       TenSach = sach.TENSACH,
                                       img = sach.IMAGESOURCE,
                                       NgayHetHan = (DateTime)sachmuon.NGAYHETHAN,
                                       SoLuong = (int)sachmuon.SOLUONG,
                                       Gia = (int)sach.GIA
                                   }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return bookborrows;
        }

        public async Task<List<BookInBorrowDTO>> GetAllBookBorrow()
        {
            List<BookInBorrowDTO> borrowList = new List<BookInBorrowDTO>();
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    borrowList = await (from sm in context.PHIEUMUONs
                                        select new BookInBorrowDTO
                                        {
                                            MaPhieuMuon = sm.MAPHIEUMUON,
                                            TenKH = sm.KHACHHANG.TENKH,
                                            TenSach = sm.SACH.TENSACH,
                                            NgayMuon = (DateTime)sm.NGAYMUON,
                                            NgayHetHan = (DateTime)sm.NGAYHETHAN,
                                            SoLuong = (int)sm.SOLUONG,
                                            Gia = (int)sm.SACH.GIA
                                        }).ToListAsync();
                }
            }
            catch (Exception)
            {

            }
            return borrowList;
        }

        public async Task<List<BookInBorrowDTO>> GetBookBorrowByMonth(int month, int year)
        {
            List<BookInBorrowDTO> borrowList = null;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    borrowList = await (from sm in context.PHIEUMUONs
                                        where sm.NGAYMUON.Month == month && sm.NGAYMUON.Year == year 
                                        select new BookInBorrowDTO
                                        {
                                            MaPhieuMuon = sm.MAPHIEUMUON,
                                            TenKH = sm.KHACHHANG.TENKH,
                                            TenSach = sm.SACH.TENSACH,
                                            NgayMuon = (DateTime)sm.NGAYMUON,
                                            NgayHetHan = (DateTime)sm.NGAYHETHAN,
                                            SoLuong = (int)sm.SOLUONG,
                                            Gia = (int)sm.SACH.GIA
                                        }).ToListAsync();
                }
            }
            catch(Exception)
            {

            }
            return borrowList;
        }

        public async Task<List<BookInCollectDTO>> GetAllCollectBook()
        {
            List<BookInCollectDTO> collectList = null;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    collectList = await (from thusach in context.PHIEUTHUs
                                         select new BookInCollectDTO
                                         {
                                             MaPhieuMuon = thusach.MAPHIEUTHU,
                                             TenKH = thusach.KHACHHANG.TENKH,
                                             TenSach = thusach.SACH.TENSACH,
                                             NgayTra = thusach.NGAYTHU,
                                             SoLuong = (int)thusach.SOLUONG,
                                             SoLuongHong = (int)thusach.SOLUONGHONG,
                                             TienHong = (int)thusach.TIENPHATHONG,
                                             TienTre = (int)thusach.TIENTREMOTNGAY,
                                             TongTienTra = (int)thusach.TONGTIEN
                                         }
                    ).ToListAsync();
                }
            }
            catch (Exception) 
            {
                
            }

            return collectList;
        }

        public async Task<List<BookInCollectDTO>> GetCollectFeeBook()
        {
            List<BookInCollectDTO> collectList = new List<BookInCollectDTO>();
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    collectList = await (from ct in context.PHIEUTHUs
                                         where ct.TIENPHATHONG > 0
                                         select new BookInCollectDTO
                                         {
                                             TenSach = ct.SACH.TENSACH,
                                         }).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return collectList;
        }

        public async Task<List<BookInCollectDTO>> GetCollectFeeBookByMonth(int year, int month)
        {
            List<BookInCollectDTO> collectList = new List<BookInCollectDTO>();
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    collectList = await (from ct in context.PHIEUTHUs
                                         where ct.TIENPHATHONG > 0 && ct.NGAYTHU.Year == year && ct.NGAYTHU.Month == month
                                         select new BookInCollectDTO
                                         {
                                             TenSach = ct.SACH.TENSACH
                                         }).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return collectList;
        }

        public async Task<List<BookInCollectDTO>> GetCollectBookByMonth(int year, int month)
        {
            List<BookInCollectDTO> collectList = null;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    collectList = await (from thusach in context.PHIEUTHUs
                                         where thusach.NGAYTHU.Month == month && thusach.NGAYTHU.Year == year
                                         select new BookInCollectDTO
                                         {
                                             MaPhieuMuon = thusach.MAPHIEUTHU,
                                             TenKH = thusach.KHACHHANG.TENKH,
                                             TenSach = thusach.SACH.TENSACH,
                                             NgayTra = thusach.NGAYTHU,
                                             SoLuong = (int)thusach.SOLUONG,
                                             SoLuongHong = (int)thusach.SOLUONGHONG,
                                             TienHong = (int)thusach.TIENPHATHONG,
                                             TienTre = (int)thusach.TIENTREMOTNGAY,
                                             TongTienTra = (int)thusach.TONGTIEN
                                         }).ToListAsync();
                }
            }
            catch (Exception) 
            { 
            
            }

            return collectList;
        }

        public async Task<(bool, string)> CreateNewReceipt(int idCustomer, ObservableCollection<BookInCollectDTO> BookInCollectList)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    List<PHIEUTHU> PhieuThuList = new List<PHIEUTHU>();

                    for (int i = 0; i < BookInCollectList.Count; ++i)
                    {
                        PHIEUTHU newPhieuThu = new PHIEUTHU();
                        newPhieuThu.MAKH = idCustomer;
                        newPhieuThu.MASACH = BookInCollectList[i].MaSach;
                        newPhieuThu.NGAYTHU = BookInCollectList[i].NgayTra;
                        newPhieuThu.NGAYHETHAN = BookInCollectList[i].NgayHetHan;
                        newPhieuThu.SOLUONG = BookInCollectList[i].SoLuong;
                        newPhieuThu.TIENPHATHONG = BookInCollectList[i].TienHong;
                        newPhieuThu.SOLUONGHONG = BookInCollectList[i].SoLuongHong;
                        newPhieuThu.TIENTREMOTNGAY = BookInCollectList[i].TienTre;
                        newPhieuThu.TONGTIEN = BookInCollectList[i].TongTienTra;

                        PhieuThuList.Add(newPhieuThu);

                        var _phieumuon = await context.PHIEUMUONs.FindAsync(BookInCollectList[i].MaPhieuMuon);
                        
                        if (_phieumuon != null)
                        {
                            _phieumuon.SOLUONG -= BookInCollectList[i].SoLuong;

                            if (_phieumuon.SOLUONG == 0)
                            {
                                context.PHIEUMUONs.Remove(_phieumuon);
                            }
                        }

                        // cộng lại số lượng sách đã thuê
                        var _sach = await context.SACHes.FindAsync(BookInCollectList[i].MaSach);
                        if (_sach != null) _sach.SL += BookInCollectList[i].SoLuong;
                    }

                    context.PHIEUTHUs.AddRange(PhieuThuList);

                    context.SaveChanges();

                    return (true, "Thu thành công");
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
