using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasterLibrary.DTOs;
using System.Data.Entity;
using MasterLibrary.Views.MessageBoxML;
using System.Text.RegularExpressions;

namespace MasterLibrary.Models.DataProvider
{
    public class CustormerServices
    {
        private CustormerServices() { }
        private static CustormerServices _ins;
        public static CustormerServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CustormerServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string, CustomerDTO)> Login(string username, string password)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    string _HashPassword = Utils.Helper.HashPassword(password);

                    // lây thông tin nếu tài khoản, mật khẩu đúng
                    var cus = await(from s in context.KHACHHANGs
                                      where s.USERNAME == username && s.USERPASSWORD == _HashPassword && s.IDROLE == 2 && s.ISEXIST == 1
                                      select new CustomerDTO
                                      {
                                          MAKH = s.MAKH,
                                          TENKH= s.TENKH,
                                          EMAIL= s.EMAIL,
                                          USERNAME= s.USERNAME,
                                          USERPASSWORD= s.USERPASSWORD,
                                          DIACHI = s.DIACHI,
                                      }).FirstOrDefaultAsync();

                    if (cus == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu", null);
                    }
                    else
                    {
                        return (true, "", cus);
                    }
                }

            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu", null);
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống", null);
            }
        }

        public async void Register(string fullname, string email, string username, string pass)
        {
            if (string.IsNullOrEmpty(fullname) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(pass))
            {
                MessageBoxML ms = new MessageBoxML("Thông báo", "Thông tin bị trống vui lòng nhập thêm.", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return;
            }

            if (await Task.Run(() => Ins.CheckUserNameCustormer(username, -1)))
            {
                MessageBoxML ms = new MessageBoxML("Thông báo", "Tên tài khoản đã tồn tại", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return;
            }

            KHACHHANG cus = new KHACHHANG();
            cus.USERNAME = username;
            cus.USERPASSWORD = Utils.Helper.HashPassword(pass);
            cus.TENKH = fullname;
            cus.IDROLE = 2;
            cus.EMAIL = email;
            cus.ISEXIST = 1;

            using (var context = new MasterlibraryEntities())
            {
                context.KHACHHANGs.Add(cus);
                context.SaveChanges();
                MessageBoxML ms = new MessageBoxML("Thông báo", "Đăng ký thành công!", MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();
            }    
        }

        public async Task<(bool, string)> CreateNewCustomer(string fullname, string username, string pass, string email, string address)
        {
            try
            {
                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);

                if (reg.IsMatch(email) == false)
                {
                    return (false, "Email không hợp lệ");
                }

                if (await Task.Run(() => Ins.CheckEmailCustormer(email, -1)))
                {
                    return (false, "Email đã tồn tại");
                }
                
                if (await Task.Run(() => Ins.CheckUserNameCustormer(username, -1)))
                {
                    return (false, "Tên tài khoản đã tồn tại");
                }

                KHACHHANG cus = new KHACHHANG();
                cus.USERNAME = username;
                cus.USERPASSWORD = Utils.Helper.HashPassword(pass);
                cus.TENKH = fullname;
                cus.IDROLE = 2;
                cus.EMAIL = email;
                cus.ISEXIST = 1;
                cus.DIACHI = address;

                using (var context = new MasterlibraryEntities())
                {
                    context.KHACHHANGs.Add(cus);
                    context.SaveChanges();
                }

                return (true, "Tạo thành công");
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

        public async Task<CustomerDTO> FindCustomer(int MaKH)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    // Tìm khách hàng có mã khách hàng (MaKH)
                    var cus = await (from s in context.KHACHHANGs
                                     where s.MAKH == MaKH && s.ISEXIST == 1
                                     select new CustomerDTO
                                     {
                                         MAKH = s.MAKH,
                                         TENKH = s.TENKH,
                                         EMAIL = s.EMAIL,
                                         USERNAME = s.USERNAME,
                                         USERPASSWORD = s.USERPASSWORD,
                                         DIACHI = s.DIACHI,
                                     }).FirstOrDefaultAsync();

                    return cus;
                }
            }
            catch (Exception)
            {
                MessageBoxML ms = new MessageBoxML("Lỗi", "Không tìm thấy khách hàng", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return null;
            }
        }

        public async Task<bool> CheckEmailCustormer(string _email, int _makh)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    // Tìm khách hàng có mã khách hàng (MaKH)
                    var cus = await (from s in context.KHACHHANGs
                                     where s.EMAIL == _email && s.MAKH != _makh && s.ISEXIST == 1
                                     select new CustomerDTO
                                     {
                                         MAKH = s.MAKH,
                                         TENKH = s.TENKH,
                                         EMAIL = s.EMAIL,
                                         USERNAME = s.USERNAME,
                                         USERPASSWORD = s.USERPASSWORD,
                                         DIACHI = s.DIACHI,
                                     }).FirstOrDefaultAsync();
                    if (cus == null) return false;
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

        public async Task<bool> CheckUserNameCustormer(string _username, int _makh)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    // Tìm khách hàng có mã khách hàng (MaKH)
                    var cus = await (from s in context.KHACHHANGs
                                     where s.USERNAME == _username && s.MAKH != _makh && s.ISEXIST == 1
                                     select new CustomerDTO
                                     {
                                         MAKH = s.MAKH,
                                         TENKH = s.TENKH,
                                         EMAIL = s.EMAIL,
                                         USERNAME = s.USERNAME,
                                         USERPASSWORD = s.USERPASSWORD,
                                         DIACHI = s.DIACHI,
                                     }).FirstOrDefaultAsync();
                    if (cus == null) return false;
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

        public async Task<bool> updateCustomer(int _makh, string _tenkh, string _email, string _diachi)
        {
            try
            {
                // Cập nhật thông tin
                using (var context = new MasterlibraryEntities())
                {
                    var cus = context.KHACHHANGs.SingleOrDefault(s => s.MAKH == _makh);

                    if (cus == null) return false;

                    cus.TENKH = _tenkh;
                    cus.EMAIL = _email;
                    cus.DIACHI= _diachi;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<(bool, string)> SaveCustomer(int _makh, string _tenkh, string username, string pass, string _email, string _diachi)
        {
            try
            {
                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);

                if (reg.IsMatch(_email) == false)
                {
                    return (false, "Email không hợp lệ");
                }

                if (await Task.Run(() => Ins.CheckEmailCustormer(_email, _makh)))
                {
                    return (false, "Email đã tồn tại");


                }

                if (await Task.Run(() => Ins.CheckUserNameCustormer(username, _makh)))
                {
                    return (false, "Tên tài khoản đã tồn tại");
                }

                // Cập nhật thông tin
                using (var context = new MasterlibraryEntities())
                {
                    var cus = context.KHACHHANGs.SingleOrDefault(s => s.MAKH == _makh);

                    if (cus == null) return (false, "Không tìm thấy khách hàng");

                    cus.TENKH = _tenkh;
                    cus.USERNAME= username;
                    cus.USERPASSWORD = Utils.Helper.HashPassword(pass);
                    cus.EMAIL = _email;
                    cus.DIACHI = _diachi;

                    context.SaveChanges();

                    return (true, "Lưu thông tin thành công");
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

        public async Task<(bool, string)> ChangePassword(int _makh, string _newpassword)
        {
            try
            {
                // Đổi mật khẩu
                using (var context = new MasterlibraryEntities())
                {
                    var cus = context.KHACHHANGs.SingleOrDefault(s => s.MAKH == _makh);

                    if (cus == null) return (false, "Không tìm thấy khách hàng");

                    cus.USERPASSWORD = Utils.Helper.HashPassword(_newpassword);
                    
                    context.SaveChanges();
                    return (true, "Đổi mật khẩu thành công");
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

        public async Task<List<CustomerDTO>> GetAllCustomer()
        {
            List<CustomerDTO> customers = null;

            try
            {
                // Lấy danh sách khách hàng
                using (var context = new MasterlibraryEntities())
                {
                    customers =  (from customer in context.KHACHHANGs.AsEnumerable()
                                       where customer.IDROLE == 2 && customer.ISEXIST == 1
                                       select new CustomerDTO
                                       {
                                           MAKH = customer.MAKH,
                                           TENKH = customer.TENKH,
                                           USERNAME= customer.USERNAME,
                                           USERPASSWORD= customer.USERPASSWORD,
                                           EMAIL= customer.EMAIL,
                                           DIACHI= customer.DIACHI,
                                           DeCodeUSERPASSWORD = Utils.Helper.DePassword(customer.USERPASSWORD),
                                       }).ToList();
                }
            }
            catch (Exception)
            {

            }

            return customers;
        }

        public async Task<(bool, string)> DeleteCustomer(int _makh)
        {
            try
            {
                using (var context = new MasterlibraryEntities())
                {
                    var CustomerRemove = context.KHACHHANGs.FirstOrDefault(c => c.MAKH == _makh);

                    if (CustomerRemove != null)
                    {
                        CustomerRemove.ISEXIST = 0;
                        context.SaveChanges();

                        return (true, "Xoá khách hàng thành công");
                    }
                    else
                    {
                        return (false, "Không khách hàng để xoá");
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
    }
}
