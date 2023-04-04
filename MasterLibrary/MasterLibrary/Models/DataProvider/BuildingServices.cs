using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class BuildingServices
    {
        private static BuildingServices _ins;
        public static BuildingServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BuildingServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<TangDTO>> GetAllFloor()
        {
            List<TangDTO> floors = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    floors = await (from t in context.TANGs
                                   select new TangDTO
                                   {
                                       MaTang = t.MATANG,
                                       TenTang = t.TENTANG
                                   }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return floors;
        }

        public async Task<List<DayDTO>> GetAllRowInFloor(int _MaTang)
        {
            List<DayDTO> rows = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    rows = await (from d in context.DAYKEs
                                  where d.IDTANG == _MaTang
                                  select new DayDTO
                                  {
                                        MaDay = d.MADAY,
                                        TenDay = d.TENDAY,
                                        MaTang = _MaTang
                                  }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return rows;
        }

        public async Task<(bool, string, int)> CreateNewFloor(string _TenTang)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    TANG newFloor = new TANG();
                    newFloor.TENTANG = _TenTang;

                    context.TANGs.Add(newFloor);

                    await context.SaveChangesAsync();

                    int newIdFloor = await context.TANGs.MaxAsync(t => t.MATANG);

                    return (true, "Tạo tầng mới thành công", newIdFloor);
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Xãy ra lỗi khi thao tác dữ liệu trên cơ sở dữ liệu", -1);
            }
            catch (Exception)
            {
                return (false, "Xãy ra lỗi khi thực hiện thao tác", -1);
            }
        }

        public async Task<(bool, string, int)> CreateNewRow(string _TenDay, int _MaTang)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    DAYKE newRow = new DAYKE();
                    newRow.TENDAY = _TenDay;
                    newRow.IDTANG = _MaTang;

                    context.DAYKEs.Add(newRow);

                    await context.SaveChangesAsync();

                    int newIdRow = await context.DAYKEs.MaxAsync(d => d.MADAY);

                    return (true, "Tạo dãy kệ mới thành công", newIdRow);
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Xãy ra lỗi khi thao tác dữ liệu trên cơ sở dữ liệu", -1);
            }
            catch (Exception)
            {
                return (false, "Xãy ra lỗi khi thực hiện thao tác", -1);
            }
        }

        public async Task<(bool, string)> DeleteFloor(string _TenTang)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var FloorRemove = context.TANGs.FirstOrDefault(t => t.TENTANG == _TenTang);

                    if (FloorRemove != null)
                    {
                        var haveBook = context.SACHes.FirstOrDefault(s => s.VITRITANG == FloorRemove.MATANG);

                        // Không có sách tại tầng đó
                        if (haveBook == null)
                        {
                            List<DayDTO> days =await GetAllRowInFloor(FloorRemove.MATANG);

                            (bool isDeleteRow, string lb) = await DeleteRow(FloorRemove.MATANG, days);

                            // xoá các dãy kệ thành công
                            if (isDeleteRow == true)
                            {
                                context.TANGs.Remove(FloorRemove);
                                await context.SaveChangesAsync();

                                return (true, "Xoá tầng thành công");
                            }
                            else
                            {
                                return (false, "Xoá tầng thất bại");
                            }
                        }
                        else
                        {
                            return (false, "Tầng này còn sách không thể xoá");
                        }
                    }
                    else
                    {
                        return (false, "Tầng không tồn tại");
                    }

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

        public async Task<(bool, string)> DeleteRow(int _MaTang, List<DayDTO> DayList)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    if (DayList != null)
                    {
                        for (int i = 0; i < DayList.Count; ++i)
                        {
                            var currentMaDay = DayList[i].MaDay;
                            var haveBook = context.SACHes.FirstOrDefault(s => s.VITRIDAY == currentMaDay);

                            // Có sách tại dãy đó thì không xoá được
                            if (haveBook != null)
                            {
                                return (false, "Dãy kệ còn sách không thể xoá");
                            }
                        }

                        var ListDayRemove = context.DAYKEs.Where(dk => dk.IDTANG == _MaTang).ToList();

                        context.DAYKEs.RemoveRange(ListDayRemove);

                        context.SaveChanges();

                        return (true, "Xoá dãy kệ này thành công");
                    }
                    else
                    {
                        return (false, "Dãy kệ không tồn tại");
                    }

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
