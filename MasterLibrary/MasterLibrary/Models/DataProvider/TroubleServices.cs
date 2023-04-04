using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MasterLibrary.Models.DataProvider
{
    public class TroubleServices
    {
        private static TroubleServices _ins;
        public static TroubleServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new TroubleServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<TroubleDTO>> GetAllTrouble()
        {
            List<TroubleDTO> Troubles = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    Troubles = await (from sc in context.SUCOes
                                      join lsc in context.LOAISUCOes on sc.MALSC equals lsc.MALSC
                                      join ttsc in context.TRANGTHAISCs on sc.MATTSC equals ttsc.MATT
                                      join kh in context.KHACHHANGs on sc.MAKH equals kh.MAKH
                                      select new TroubleDTO
                                      {
                                          MaSC = sc.MASC,
                                          MaKH = (int)sc.MAKH,
                                          TenKH = kh.TENKH,
                                          TieuDe = sc.TIEUDE,
                                          MoTa = sc.MOTA,
                                          NgayBaoCao = (DateTime)sc.THOIGIANBAOCAO,
                                          Img = sc.IMG,
                                          MaLSC = (int)sc.MALSC,
                                          TenLoaiSuCo = lsc.TENLSC,
                                          MaTTSC = sc.MATTSC,
                                          TenTrangThaiSuCo = ttsc.TENTT,
                                          ChiPhi = (decimal)sc.CHIPHI,
                                      }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return Troubles;
        }

        public async Task<List<TroubleDTO>> GetTroubleByMonth(int month, int year)
        {
            List<TroubleDTO> Troubles = null;
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    Troubles = await (from ct in context.SUCOes
                                      where ct.THOIGIANBAOCAO.Year == year && ct.THOIGIANBAOCAO.Month == month
                                      select new TroubleDTO
                                      {
                                          MaSC = ct.MASC,
                                          TenLoaiSuCo = ct.LOAISUCO.TENLSC,
                                          NgayBaoCao = ct.THOIGIANBAOCAO,
                                          ChiPhi = (decimal)ct.CHIPHI,
                                          MoTa = ct.MOTA,
                                      }).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Troubles;
        }

        public async Task<List<TroubleDTO>> GetAllTroubleOfCustomer(int _MaKH)
        {
            List<TroubleDTO> Troubles = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    Troubles = await (from sc in context.SUCOes
                                      join lsc in context.LOAISUCOes on sc.MALSC equals lsc.MALSC
                                      join ttsc in context.TRANGTHAISCs on sc.MATTSC equals ttsc.MATT
                                      where sc.MAKH == _MaKH
                                      select new TroubleDTO
                                      {
                                          MaSC = sc.MASC,
                                          MaKH = _MaKH,
                                          TieuDe = sc.TIEUDE,
                                          MoTa = sc.MOTA,
                                          NgayBaoCao = (DateTime)sc.THOIGIANBAOCAO,
                                          Img = sc.IMG,
                                          MaLSC = (int)sc.MALSC,
                                          TenLoaiSuCo = lsc.TENLSC,
                                          MaTTSC = sc.MATTSC,
                                          TenTrangThaiSuCo = ttsc.TENTT,
                                          ChiPhi = (decimal)sc.CHIPHI,
                                      }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return Troubles;
        }

        public async Task<(bool, string, int)> CreateTrouble(TroubleDTO newTrouble)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    SUCO _SuCo = new SUCO()
                    {
                        MAKH = newTrouble.MaKH,
                        TIEUDE = newTrouble.TieuDe,
                        MOTA = newTrouble.MoTa,
                        THOIGIANBAOCAO = newTrouble.NgayBaoCao,
                        IMG = newTrouble.Img,
                        CHIPHI = newTrouble.ChiPhi,
                        MALSC = (from lsc in context.LOAISUCOes where lsc.TENLSC == newTrouble.TenLoaiSuCo select lsc.MALSC).FirstOrDefault(),
                        MATTSC = (from ttsc in context.TRANGTHAISCs where ttsc.TENTT == newTrouble.TenTrangThaiSuCo select ttsc.MATT).FirstOrDefault(),
                    };

                    context.SUCOes.Add(_SuCo);
                    context.SaveChanges();

                    int newIdTrouble = await context.SUCOes.MaxAsync(sc => sc.MASC);

                    return (true, "Báo cáo sự cố thành công", newIdTrouble);
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

        public async Task<(bool, string)> UpdateTrouble(TroubleDTO newTrouble)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var currentTrouble = context.SUCOes.FirstOrDefault(sc => sc.MASC == newTrouble.MaSC);

                    currentTrouble.TIEUDE = newTrouble.TieuDe;
                    currentTrouble.MOTA = newTrouble.MoTa;
                    currentTrouble.THOIGIANBAOCAO = newTrouble.NgayBaoCao;
                    currentTrouble.IMG = newTrouble.Img;
                    currentTrouble.CHIPHI = newTrouble.ChiPhi;
                    currentTrouble.MALSC = (from lsc in context.LOAISUCOes where lsc.TENLSC == newTrouble.TenLoaiSuCo select lsc.MALSC).FirstOrDefault();
                    currentTrouble.MATTSC = (from ttsc in context.TRANGTHAISCs where ttsc.TENTT == newTrouble.TenTrangThaiSuCo select ttsc.MATT).FirstOrDefault();

                    context.SaveChanges();

                    return (true, "Lưu sự cố thành công");
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

        public async Task<(bool, string)> DeleteTrouble(int _MaSC)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var TroubleRemove = context.SUCOes.FirstOrDefault(sc => sc.MASC == _MaSC);

                    if (TroubleRemove != null)
                    {
                        context.SUCOes.Remove(TroubleRemove);
                        await context.SaveChangesAsync();

                        return (true, "Xoá sự cố thành công");
                    }
                    else
                    {
                        return (false, "Không có sự cố để xoá");
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return (false, "Xãy ra lỗi khi lưu dữ liệu vào cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Xãy ra lỗi khi thực hiện thao tác");
            }
        }

        public async Task<List<StatusTroubleDTO>> GetAllStatusTrouble()
        {
            List<StatusTroubleDTO> statusTroubles = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    statusTroubles = await (from ttsc in context.TRANGTHAISCs
                                            select new StatusTroubleDTO
                                            {
                                                MaTTSC = ttsc.MATT,
                                                TenTrangThaiSuCo = ttsc.TENTT
                                            }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return statusTroubles;
        }

        public async Task<List<TypeTroubleDTO>> GetAllTypeTrouble()
        {
            List<TypeTroubleDTO> TypeTroubles = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    TypeTroubles = await (from lsc in context.LOAISUCOes
                                            select new TypeTroubleDTO
                                            {
                                                MaLSC = lsc.MALSC,
                                                TenLoaiSuCo = lsc.TENLSC
                                            }
                     ).ToListAsync();
                }
            }
            catch (Exception)
            {

            }

            return TypeTroubles;
        }
    }
}
