using MasterLibrary.DTOs;
using MasterLibrary.Views.MessageBoxML;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BookInCartServices
    {
        private static BookInCartServices _ins;
        public static BookInCartServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BookInCartServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string)> AddBookInCart(int _makh, int _masach, int _soluong, int _soluongMax)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var currentBookInCart = context.GIOHANGs.SingleOrDefault(s => s.MAKH == _makh && s.MASACH == _masach);

                    if (currentBookInCart != null)
                    {
                        // Nếu giỏ hàng đã tồn tại thì thêm vào
                        if (currentBookInCart.SOLUONGHT + _soluong <= _soluongMax)
                        {
                            currentBookInCart.SOLUONGHT += _soluong;

                            await context.SaveChangesAsync();

                            return (true, "Thêm vào giỏ hàng thành công");
                        }
                        else
                        {
                            return (false, "Số lượng trong giỏ vượt quá số lượng cho phép");
                        }
                    }
                    else
                    {
                        // Nếu chưa có thì thêm giỏ hàng mới
                        GIOHANG newBookInCart = new GIOHANG();
                        newBookInCart.MAKH = _makh;
                        newBookInCart.MASACH = _masach;
                        newBookInCart.SOLUONGHT = _soluong;

                        context.GIOHANGs.Add(newBookInCart);

                        await context.SaveChangesAsync();

                        return (true, "Thêm vào giỏ hàng thành công");
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Không thể kết nối được với cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Gặp lỗi trong việc thực hiện thao tác");
            }
        }

        public async Task<(bool, string)> ReduceBookInCart(int _makh, int _masach, int _soluong)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var currentBookInCart = context.GIOHANGs.SingleOrDefault(s => s.MAKH == _makh && s.MASACH == _masach);

                    if (currentBookInCart != null)
                    {
                        if (currentBookInCart.SOLUONGHT - _soluong > 0)
                        {
                            currentBookInCart.SOLUONGHT -= _soluong;

                            await context.SaveChangesAsync();

                            return (true, "Giảm số lượng thành công");
                        }
                        else
                        {
                            return (false, "Số lượng không thể giảm nữa");
                        }
                    }
                    else
                    {
                        return (false, "Không có vật phẩm để giảm số lượng");
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Không thể kết nối được với cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Gặp lỗi trong việc thực hiện thao tác");
            }
        }

        public async Task<List<BookInCartDTO>> GetAllBookInCart(int _makh)
        {
            List<BookInCartDTO> books = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    books = await (from gh in context.GIOHANGs
                                   join s in context.SACHes on gh.MASACH equals s.MASACH
                                   where gh.MAKH == _makh
                                   select new BookInCartDTO
                                   {
                                       MaKH = _makh,
                                       MaSach = s.MASACH,
                                       TenSach = s.TENSACH,
                                       TacGia = s.TACGIA,
                                       TheLoai = s.THELOAI,
                                       SourceImg = s.IMAGESOURCE,
                                       SoLuongMax = (int)s.SL,
                                       Gia = (decimal)s.GIA,
                                       SoLuongHT = (int)gh.SOLUONGHT,
                                   }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {
                MessageBoxML ms = new MessageBoxML("Lỗi", "Không kết nối được cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            }

            return books;
        }

        public async Task<(bool, string)> DeleteBookInCart(int _makh, int _masach)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var BookInCartRemove = context.GIOHANGs.SingleOrDefault(s => s.MAKH == _makh && s.MASACH == _masach);

                    if (BookInCartRemove != null)
                    {
                        context.GIOHANGs.Remove(BookInCartRemove);
                        await context.SaveChangesAsync();

                        return (true, "Xoá vật phẩm thành công");
                    }
                    else
                    {
                        return (false, "Không có vật phẩm để xoá");
                    }

                }

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Không thể kết nối được với cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Gặp lỗi trong việc thực hiện thao tác");
            }
        }

        public async Task<(bool, string)> DeleteAllBookInCart(int _makh)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var ListBookInCartRemove = context.GIOHANGs.Where(s => s.MAKH == _makh).ToList();

                    context.GIOHANGs.RemoveRange(ListBookInCartRemove);

                    await context.SaveChangesAsync();

                    return (true, "Đã xoá tất cả vật phẩm");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Không thể kết nối được với cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Gặp lỗi trong việc thực hiện thao tác");
            }
        }

        public async Task<(bool, string)> SetQuantity(int _makh, int _masach, int _sl)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var BookInCartCurrent = context.GIOHANGs.SingleOrDefault(s => s.MAKH == _makh && s.MASACH == _masach);

                    if (BookInCartCurrent != null)
                    {
                        BookInCartCurrent.SOLUONGHT = _sl;
                        await context.SaveChangesAsync();

                        return (true, "Thay đổi số lượng thành công");
                    }
                    else
                    {
                        return (false, "Thay đổi số lượng thất bại");
                    }
                }

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Không thể kết nối được với cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Gặp lỗi trong việc thực hiện thao tác");
            }
        }
    }
}
