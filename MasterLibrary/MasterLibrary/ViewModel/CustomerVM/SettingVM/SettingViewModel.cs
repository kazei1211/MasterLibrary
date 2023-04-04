using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.Customer;
using MasterLibrary.Views.LoginWindow;
using MasterLibrary.Views.MessageBoxML;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.CustomerVM.SettingVM
{
    public class SettingViewModel: BaseViewModel
    {
        #region Thuộc tính
        private CustomerDTO _Cus;
        public CustomerDTO Cus
        {
            get { return _Cus; }
            set { _Cus = value; OnPropertyChanged(); }
        }

        private int _MaKH;
        public int MaKH
        {
            get { return _MaKH; }
            set { _MaKH = value; OnPropertyChanged(); }
        }

        private string _TenKH;
        public string TenKH
        {
            get { return _TenKH; }
            set { _TenKH = value; OnPropertyChanged(); }
        }

      

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }

        

        private string _DiaChi;
        public string DiaChi
        {
            get { return _DiaChi; }
            set { _DiaChi = value; OnPropertyChanged(); }
        }

        private string _CurrentPassword;
        public string CurrentPassword
        {
            get { return _CurrentPassword; }
            set { _CurrentPassword = value; OnPropertyChanged(); }
        }

        private string _NewPassword;
        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; OnPropertyChanged(); }
        }

        private string _ConfirmNewPassword;
        public string ConfirmNewPassword
        {
            get { return _ConfirmNewPassword; }
            set { _ConfirmNewPassword = value; OnPropertyChanged(); }
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; OnPropertyChanged(); }
        }

        private bool _IsSaving;
        public bool IsSaving
        {
            get { return _IsSaving; }
            set { _IsSaving = value; OnPropertyChanged(); }
        }

        #endregion

        #region Icommand
        public ICommand FirstLoadML { get; set; }
        public ICommand MaskNameSetting { get; set; }
        public ICommand UpdateInfo { get; set; }
        public ICommand CurrentPasswordChange { get; set; }
        public ICommand NewPasswordChange { get; set; }
        public ICommand ConfirmNewPasswordChange { get; set; }
        public ICommand SaveNewPasswordCommand { get; set; }
        public ICommand Logout { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskName { get; set; }
        public string MatKhauKH { get; set; }
        #endregion

        public SettingViewModel()
        {
            FirstLoadML = new RelayCommand<object> ((p) => { return true; }, async (p) => 
            {
                IsLoading = true;

                int MaKHcur = MainCustomerViewModel.CurrentCustomer.MAKH;
                Cus = await Task.Run(() => CustormerServices.Ins.FindCustomer(MaKHcur));
                MaKH = Cus.MAKH;
                TenKH = Cus.TENKH;
                Email = Cus.EMAIL;
                DiaChi = Cus.DIACHI;
                MatKhauKH = Cus.USERPASSWORD;

                IsLoading = false;
            });

            UpdateInfo = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenKH) ||
                    string.IsNullOrEmpty(Email))
                {
                    return false;
                }

                return true;
            }, async (p) =>
            {
                string match = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex reg = new Regex(match);

                if (reg.IsMatch(Email) == false)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Email không hợp lệ", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                IsSaving = true;
                MaskName.Visibility = Visibility.Visible;

                if (await Task.Run(() => CustormerServices.Ins.CheckEmailCustormer(Email, MaKH)))
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Email đã tồn tại", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }
                else
                if (await Task.Run(() => CustormerServices.Ins.updateCustomer(MaKH, TenKH, Email, DiaChi)))
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Chỉnh sửa thông tin thành công", MessageType.Accept, MessageButtons.OK);

                    MainCustomerWindow w = Application.Current.Windows.OfType<MainCustomerWindow>().FirstOrDefault();
                    w._CustomerName.Text = TenKH;

                    MainCustomerViewModel.CurrentCustomer = await Task<CustomerDTO>.Run(() => CustormerServices.Ins.FindCustomer(MaKH));

                    ms.ShowDialog();
                }
                else
                {
                    MessageBoxML ms = new MessageBoxML("Lỗi", "Xảy ra lỗi khi thực hiện thao tác", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }

                IsSaving = false;
                MaskName.Visibility = Visibility.Collapsed;
            });

            CurrentPasswordChange = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                CurrentPassword = p.Password;
            });

            NewPasswordChange = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                NewPassword = p.Password;
            });

            ConfirmNewPasswordChange = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                ConfirmNewPassword = p.Password;
            });

            SaveNewPasswordCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(CurrentPassword) ||
                    string.IsNullOrEmpty(NewPassword) ||
                    string.IsNullOrEmpty(ConfirmNewPassword))
                {
                    return false;
                }

                return true; 
            }, async (p) =>
            {
                if (NewPassword != ConfirmNewPassword)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Mật khẩu xác nhận không chính xác", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                IsSaving = true;
                MaskName.Visibility = Visibility.Visible;

                string HashCurrentPassword = Utils.Helper.HashPassword(CurrentPassword);

                if (MatKhauKH != HashCurrentPassword)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Mật khẩu hiện tại không chính xác", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }
                else
                {
                    (bool isChangePassword, string lb) = await Task.Run(() => CustormerServices.Ins.ChangePassword(MaKH, NewPassword));

                    if (isChangePassword == true)
                    {
                        MatKhauKH = Utils.Helper.HashPassword(NewPassword);

                        MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                    else
                    {
                        MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                }
                
                IsSaving = false;
                MaskName.Visibility = Visibility.Collapsed;
            });

            Logout = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MainCustomerWindow w = Application.Current.Windows.OfType<MainCustomerWindow>().FirstOrDefault();
                w.Hide();

                LoginWindow nw = new LoginWindow();
                nw.Show();

                w.Close();
            });

            MaskNameSetting = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });
        }
    }
}
