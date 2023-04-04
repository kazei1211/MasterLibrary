using MasterLibrary.DTOs;
using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterLibrary.Models.DataProvider
{
    public class RoleLibraryServices
    {
        private static RoleLibraryServices _ins;
        public static RoleLibraryServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new RoleLibraryServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<RoleLibraryDTO> GetARoleLibrary()
        {
            RoleLibraryDTO CurrentRoleLibrary = null;

            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    CurrentRoleLibrary = await (from ltv in context.LUATTHUVIENs
                                     select new RoleLibraryDTO
                                     {
                                         Songaymuon = (int)ltv.SONGAYMUON,
                                         TienTraTreMotNgay = (decimal)ltv.TIENTRASACHMUONMOTNGAY
                                     }
                     ).FirstOrDefaultAsync();
                }
            }
            catch (Exception)
            {

            }

            return CurrentRoleLibrary;
        }
    }
}
