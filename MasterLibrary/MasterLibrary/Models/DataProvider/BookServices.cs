using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BookServices
    {
        private static BookServices _ins;
        public static BookServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BookServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<BookDTO>> GetAllbook()
        {
            List<BookDTO> books = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    books = await (from sach in context.SACHes
                                   join t in context.TANGs on sach.VITRITANG equals t.MATANG
                                   join d in context.DAYKEs on sach.VITRIDAY equals d.MADAY
                                   where sach.ISEXIST == 1
                                   select new BookDTO
                                   {
                                       MaSach = sach.MASACH,
                                       TenSach = sach.TENSACH,
                                       TacGia = sach.TACGIA,
                                       MoTa = sach.MOTA,
                                       NXB = sach.NXB,
                                       NamXB = (int)sach.NAMXB,
                                       TheLoai = sach.THELOAI,
                                       Gia = (decimal)sach.GIA,
                                       SoLuong = (int)sach.SL,
                                       ImageSource = sach.IMAGESOURCE,
                                       MaTang = (int)sach.VITRITANG,
                                       TenTang = t.TENTANG,
                                       MaDay = (int)sach.VITRIDAY,
                                       TenDay = d.TENDAY
                                   }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return books;
        }

        public async Task<List<BookDTO>> GetBookInRow(int _Matang, int _MaDay)
        {
            List<BookDTO> books = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    books = await (from sach in context.SACHes
                                   join t in context.TANGs on sach.VITRITANG equals t.MATANG
                                   join d in context.DAYKEs on sach.VITRIDAY equals d.MADAY
                                   where sach.VITRITANG == _Matang && sach.VITRIDAY == _MaDay
                                   where sach.ISEXIST == 1
                                   select new BookDTO
                                   {
                                       MaSach = sach.MASACH,
                                       TenSach = sach.TENSACH,
                                       TacGia = sach.TACGIA,
                                       MoTa = sach.MOTA,
                                       NXB = sach.NXB,
                                       NamXB = (int)sach.NAMXB,
                                       TheLoai = sach.THELOAI,
                                       Gia = (decimal)sach.GIA,
                                       SoLuong = (int)sach.SL,
                                       ImageSource = sach.IMAGESOURCE,
                                       MaTang = (int)sach.VITRITANG,
                                       MaDay = (int)sach.VITRIDAY,
                                       TenTang = (from tang in context.TANGs where tang.MATANG == sach.VITRITANG select tang.TENTANG).FirstOrDefault() ,
                                       TenDay = (from day in context.DAYKEs where day.MADAY == sach.VITRIDAY select day.TENDAY).FirstOrDefault()
                                   }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return books;
        }

        public async Task<BookDTO> GetBook(int _BookId)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var book = await (from sach in context.SACHes
                                     where sach.MASACH == _BookId && sach.ISEXIST == 1
                                     select new BookDTO
                                     {
                                         MaSach = sach.MASACH,
                                         TenSach = sach.TENSACH,
                                         TacGia = sach.TACGIA,
                                         MoTa = sach.MOTA,
                                         NXB = sach.NXB,
                                         NamXB = (int)sach.NAMXB,
                                         TheLoai = sach.THELOAI,
                                         Gia = (decimal)sach.GIA,
                                         SoLuong = (int)sach.SL,
                                         ImageSource = sach.IMAGESOURCE,
                                         MaTang = (int)sach.VITRITANG,
                                         MaDay = (int)sach.VITRIDAY,
                                         TenTang = (from tang in context.TANGs where tang.MATANG == sach.VITRITANG select tang.TENTANG).FirstOrDefault(),
                                         TenDay = (from day in context.DAYKEs where day.MADAY == sach.VITRIDAY select day.TENDAY).FirstOrDefault()
                                     }).FirstOrDefaultAsync();

                    return book;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
