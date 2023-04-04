using MasterLibrary.Views.LoginWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.DTOs;
using System.Data.Entity;

namespace MasterLibrary.ViewModel.LoginVM
{
    public class ForgotPassViewModel: BaseViewModel
    {
        #region Icommand
        public ICommand CancelForgotPass { get; set; }
        public ICommand TypingYourEmail { get; set; }
        public ICommand TypingVerificationNum { get; set; }
        public ICommand CreateNewPass { get; set; }
        public ICommand NewpassChanged { get; set; }
        public ICommand ComfirmNewPassChanged { get; set; }

        #endregion

        #region Property
        private int Number;
        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set { _mail = value; OnPropertyChanged(); }
        }

        private string _verificationnumber;
        public string Verificationnumber
        {
            get { return _verificationnumber; }
            set { _verificationnumber = value; OnPropertyChanged(); }
        }

        private string _newpass;
        public string Newpass
        {
            get { return _newpass; }
            set { _newpass = value; OnPropertyChanged(); }
        }

        private string _confirmnewpass;
        private string Confirmnewpass
        {
            get { return _confirmnewpass; }
            set { _confirmnewpass = value; OnPropertyChanged();}
        }
        #endregion
        public ForgotPassViewModel()
        {
            CancelForgotPass = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoginViewModel.MainFrame.Content = new LoginPage();
            });

            TypingYourEmail = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                using (var context = new MasterlibraryEntities())
                {
                    // Kiểm tra email có tồn tại không
                    var ema = await (from s in context.KHACHHANGs
                                     where s.EMAIL == Mail
                                     select new CustomerDTO
                                     {
                                         MAKH = s.MAKH
                                     }).FirstOrDefaultAsync();



                    if (Mail == null)
                        p.Content = "*Điền địa chỉ email liên kết tài khoản";
                    else if (ema == null)
                        p.Content = "*Email chưa được đăng ký tài khoản";
                    else
                    {
                        Random random = new Random();
                        Number = random.Next(1, 999999);
                        SendEmail(Number.ToString());
                        LoginViewModel.MainFrame.Content = new VerificationPage();
                    }    
                }

            });

            TypingVerificationNum = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                if (Number.ToString() == null)
                    p.Content = "*Vui lòng điền mã xác nhận";
                else if (Number.ToString() != Verificationnumber)
                    p.Content = "*Mã xác nhận không chính xác";
                else
                {
                    MasterLibrary.ViewModel.LoginVM.LoginViewModel.MainFrame.Content = new CreateNewPassPage();
                }
            });

            NewpassChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Newpass = p.Password;
            });

            ComfirmNewPassChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Confirmnewpass = p.Password;
            });

            CreateNewPass = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                if (Newpass == null || Confirmnewpass == null)
                    p.Content = "*Vui lòng nhập đủ thông tin";
                else if (Newpass != Confirmnewpass)
                    p.Content = "*Mật khẩu xác nhận không chính xác";
                else if(Newpass == Confirmnewpass)
                {                    
                    using (var context = new MasterlibraryEntities())
                    {
                        var changepass = context.KHACHHANGs.Where(x => x.EMAIL == Mail).FirstOrDefault();
                        changepass.USERPASSWORD = Utils.Helper.HashPassword(Newpass);
                        context.SaveChanges();
                        LoginViewModel.MainFrame.Content = new LoginPage();
                    }
                }       
            });
        }

        public void  SendEmail(string content)
        {
            MailMessage mailMessage = new MailMessage("helpercusml@gmail.com", Mail, "Khôi phục mật khẩu Masterlibrary", "Mã xác nhận của bạn là: " + content);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("helpercusml@gmail.com", "tgotlxfalndcoiux");
            smtpClient.EnableSsl = true;

            smtpClient.Send(mailMessage);
        }
    }
}
