using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.LoginWindow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Admin;
using MasterLibrary.Views.Customer;
using MaterialDesignThemes.Wpf;
using MasterLibrary.ViewModel.CustomerVM;
using MasterLibrary.Views.MessageBoxML;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Management;

namespace MasterLibrary.ViewModel.LoginVM
{
    public class LoginViewModel: BaseViewModel
    {
        public static Frame MainFrame { get; set; }
        public static Grid Mask { get; set; }

        public static DataGrid import_dtg;
        public static ObservableCollection<InputBookDTO> ListInputbook = new ObservableCollection<InputBookDTO>();

        public ICommand LoadLoginPage { get; set; }
        public ICommand LoadForgotPassPage { get; set; }
        public ICommand LoadRegister { get; set; }
        public ICommand LoadVerificationPage { get; set; }
        public ICommand LoadMask { get; set; }
        public ICommand LoginML { get; set; }
        public ICommand PasswordChangedML { get; set; }
        public ICommand RegisterML { get; set; }
        public ICommand PasswordRegChangedML { get; set; }

        #region property
        private string _usernamelog;
        public string Usernamelog
        {
            get { return _usernamelog; }
            set { _usernamelog = value; OnPropertyChanged(); }
        }
        private string _passwordlog;

        public string Passwordlog
        {
            get { return _passwordlog; }
            set { _passwordlog = value; OnPropertyChanged(); }
        }

        private string _fullnamereg;
        public string Fullnamereg
        {
            get { return _fullnamereg; }
            set { _fullnamereg = value; OnPropertyChanged(); }
        }

        private string _emailreg;
        public string Emailreg
        {
            get { return _emailreg; }
            set { _emailreg = value; OnPropertyChanged(); }
        }

        private string _usernamereg;
        public string Usernamereg
        {
            get { return _usernamereg; }
            set { _usernamereg = value; OnPropertyChanged(); }
        }
        private string _passwordreg;
        public string Passwordreg
        {
            get { return _passwordreg; }
            set { _passwordreg = value;OnPropertyChanged(); }
        }

        private bool _IsSaving;
        public bool IsSaving
        {
            get { return _IsSaving; }
            set { _IsSaving = value; OnPropertyChanged(); }
        }

        private bool _IsNullNameReg;
        public bool IsNullNameReg
        {
            get { return _IsNullNameReg; }
            set { _IsNullNameReg = value; OnPropertyChanged(); }
        }

        private bool _IsNullEmailReg;
        public bool IsNullEmailReg
        {
            get { return _IsNullEmailReg; }
            set { _IsNullEmailReg = value; OnPropertyChanged(); }
        }

        private bool _IsNullUserReg;
        public bool IsNullUserReg
        {
            get { return _IsNullUserReg; }
            set { _IsNullUserReg = value; OnPropertyChanged(); }
        }

        private bool _IsNullPasswordReg;
        public bool IsNullPasswordReg
        {
            get { return _IsNullPasswordReg; }
            set { _IsNullPasswordReg = value; OnPropertyChanged(); }
        }

        #endregion
        public LoginViewModel()
        {
            // Load page đăng nhập
            LoadLoginPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
                p.Content = new LoginPage();
                Passwordlog = "";
            });

            // Load page quên mật khẩu
            LoadForgotPassPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ForgotPassPage();
            });

            // Load mặt nạ làm mở window hiện tại khi mở window khác
            LoadMask = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                Mask = p;
            });

            // Bật window đăng kí
            LoadRegister = new RelayCommand<System.Windows.Window>((p) => { return true; }, (p) =>
            {
                Window w1 = new RegisterWindow();

                Mask.Visibility = Visibility.Visible;

                w1.ShowDialog();
            });

            // Bật page xác thực
            LoadVerificationPage = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                MainFrame.Content = new VerificationPage();
            });

            #region Login
            // Thực hiện đăng nhập tài khoản
            LoginML = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                string username = Usernamelog;
                string password = Passwordlog;

                // thực hiện đăng nhập
                IsSaving = true;
                Mask.Visibility = Visibility.Visible;

                await checkValidateAccount(username, password, p);

                IsSaving = false;
                Mask.Visibility = Visibility.Hidden;
            });

            // Nhận mật khẩu mỗi lần thay đổi
            PasswordChangedML = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Passwordlog = p.Password;
            });
            #endregion

            #region Register
            RegisterML = new RelayCommand<Label>((p) => { return true; }, async (p) =>
            {
                IsNullNameReg = IsNullEmailReg = IsNullUserReg = IsNullPasswordReg = false;

                if (string.IsNullOrEmpty(Fullnamereg)) IsNullNameReg = true;
                if (string.IsNullOrEmpty(Emailreg)) IsNullEmailReg = true;
                if (string.IsNullOrEmpty(Usernamereg)) IsNullUserReg = true;
                if (string.IsNullOrEmpty(Passwordreg)) IsNullPasswordReg = true;

                if (IsNullNameReg || IsNullEmailReg || IsNullUserReg || IsNullPasswordReg) return;

                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);

                if (reg.IsMatch(Emailreg) == false)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Email không hợp lệ", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                if (await Task.Run(() => CustormerServices.Ins.CheckEmailCustormer(Emailreg, -1)))
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Email đã tồn tại", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                string fullname = Fullnamereg;
                string email = Emailreg;
                string usernamereg = Usernamereg;
                string passwordreg = Passwordreg;

                //thực hiện đăng ký tài khoản
                CustormerServices.Ins.Register(fullname, email, usernamereg, passwordreg);
            });

            // Nhận mật khẩu mỗi lần thay đổi
            PasswordRegChangedML = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Passwordreg = p.Password;
            });

            #endregion
        }

        public async Task checkValidateAccount(string usr, string pwd, Label lbl)
        {
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pwd))
            {
                lbl.Content = "Sai tài khoản hoặc mật khẩu";
                return;
            }

            // Thực hiện Login tài khoản customer
            (bool loginCus, string messCus, CustomerDTO cus) = await Task.Run(() => CustormerServices.Ins.Login(usr, pwd));

            // thực hiện Login tài khoản admin
            (bool loginAdmin, string messAdmin) = await Task.Run(() => AdminServices.Ins.Login(usr, pwd));

            if (loginCus)
            {
                
                MainCustomerWindow w1 = new MainCustomerWindow();
                MainCustomerViewModel.CurrentCustomer = cus;
                w1._CustomerName.Text = cus.TENKH;
                w1.Show();

                LoginWindow w = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                w.Close();
            }
            else if (loginAdmin)
            {
                MainAdminWindow w1 = new MainAdminWindow();
                w1.Show();

                LoginWindow w = Application.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
                w.Close();
            }
            else
            {
                lbl.Content = messCus;
            }
        }

    }
}
