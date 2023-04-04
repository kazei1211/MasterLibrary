using MasterLibrary.DTOs;
using MasterLibrary.Views.MessageBoxML;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MasterLibrary.Models.DataProvider
{
    public class AdminServices
    {
        private AdminServices() { }
        public static string TenNhanVien { get; set; }
        public static int MaNhanVien { get; set; }
        public static string EmailNhanVien { get; set; }
        public static string UserNameNhanVien { get; set; }
        public static string PasswordNhanVien { get; set; }

        private static AdminServices _ins;
        public static AdminServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new AdminServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string)> Login(string username, string password)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    string _HashPassword = Utils.Helper.HashPassword(password);

                    // lây thông tin nếu tài khoản, mật khẩu đúng
                    var admin = await (from s in context.KHACHHANGs
                                       where s.USERNAME == username && s.USERPASSWORD == _HashPassword && s.IDROLE == 1
                                       select new AdminDTO
                                       {
                                           MAKH = s.MAKH,
                                           TENKH = s.TENKH,
                                           EMAIL = s.EMAIL,
                                           USERNAME= s.USERNAME,
                                           USERPASSWORD= s.USERPASSWORD,
                                       }).FirstOrDefaultAsync();


                    if (admin == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu");
                    }
                    else
                    {
                        TenNhanVien = admin.TENKH;
                        MaNhanVien = admin.MAKH;
                        EmailNhanVien = admin.EMAIL;
                        UserNameNhanVien = admin.USERNAME;
                        PasswordNhanVien = admin.USERPASSWORD;
                        return (true, "");
                    }
                }

            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống");
            }
        }

        public async Task<bool> CheckEmailAdmin(string _email, int _makh)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    // Tìm khách hàng có mã khách hàng (MaKH)
                    var adm = await (from s in context.KHACHHANGs
                                     where s.EMAIL == _email && s.MAKH != _makh
                                     select new CustomerDTO
                                     {
                                         MAKH = s.MAKH,
                                         TENKH = s.TENKH,
                                         EMAIL = s.EMAIL,
                                         USERNAME = s.USERNAME,
                                         USERPASSWORD = s.USERPASSWORD,
                                         DIACHI = s.DIACHI,
                                     }).FirstOrDefaultAsync();

                    if (adm == null) return false;

                    return true;
                }
            }
            catch (Exception)
            {
                MessageBoxML ms = new MessageBoxML("Lỗi", "Xảy ra lỗi khi thực hiện thao tác", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return true;
            }
        }
        public async Task<bool> updateAdmin(int _makh, string _tenkh, string _email, string _username)
        {
            try
            {
                // Cập nhật thông tin
                using (var context = new MasterlibraryEntities())
                {
                    var adm = context.KHACHHANGs.SingleOrDefault(s => s.MAKH == _makh);

                    if (adm == null) return false;
                    adm.TENKH = _tenkh;
                    adm.EMAIL = _email;
                    adm.USERNAME = _username;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<(bool, string)> ChangePassword(int _makh, string _newpassword)
        {
            try
            {
                // Đổi mật khẩu
                using (var context = new MasterlibraryEntities())
                {
                    var adm = context.KHACHHANGs.SingleOrDefault(s => s.MAKH == _makh);

                    if (adm == null) return (false, "Quản trị viên không tồn tại");

                    adm.USERPASSWORD = Utils.Helper.HashPassword(_newpassword);
                    PasswordNhanVien = adm.USERPASSWORD;

                    context.SaveChanges();
                    return (true, "đổi mật khẩu thành công");
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

        public async Task<bool> checkPass(int _makh, string _currentPass)
        {
            try
            {
                
                using (var context = new MasterlibraryEntities())
                {
                    var cus = context.KHACHHANGs.SingleOrDefault(s => s.MAKH == _makh);

                    if (cus == null) return false;

                    if (cus.USERPASSWORD != Utils.Helper.HashPassword(_currentPass)) return false;
                    
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
