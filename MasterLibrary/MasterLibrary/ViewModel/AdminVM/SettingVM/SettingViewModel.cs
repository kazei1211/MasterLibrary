using MasterLibrary.Views.Admin.SettingPage;
using MasterLibrary.Views.LoginWindow;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.MessageBoxML;
using System.Text.RegularExpressions;
using MasterLibrary.Views.Admin;

namespace MasterLibrary.ViewModel.AdminVM.SettingVM
{
    public class SettingViewModel : BaseViewModel
    {

        #region Property

        private static Frame InFr;

        private Frame _rolFr;
        public Frame RolFr
        {
            get { return _rolFr; }
            set { _rolFr = value; OnPropertyChanged(); }
        }

        private Frame _confFr;
        public Frame ConfFr
        {
            get { return _confFr; }
            set { _confFr = value; OnPropertyChanged(); }
        }

        private int _maKH;
        public int MaKH
        {
            get { return _maKH; }
            set { _maKH = value; OnPropertyChanged(); }
        }

        private string _hoVaTen;
        public string HoVaTen
        {
            get { return _hoVaTen; }
            set { _hoVaTen = value; OnPropertyChanged();}
        }

        private string _newHoVaTen;
        public string NewHoVaTen
        {
            get { return _newHoVaTen; }
            set { _newHoVaTen = value; OnPropertyChanged(); }
        }

        private string _tenTK;
        public string TenTK
        {
            get { return _tenTK; }
            set { _tenTK = value; OnPropertyChanged();}
        }

        private string _newTenTK;
        public string NewTenTK
        {
            get { return _newTenTK; }
            set { _newTenTK = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged();}
        }

        private string _newEmail;
        public string NewEmail
        {
            get { return _newEmail; }
            set { _newEmail = value; OnPropertyChanged(); }
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
        private bool _IsSaving;
        public bool IsSaving
        {
            get { return _IsSaving; }
            set { _IsSaving = value; OnPropertyChanged(); }
        }

        private string _passForChangingSomething;
        public string PassForChangingSomething
        {
            get { return _passForChangingSomething; }
            set { _passForChangingSomething = value; OnPropertyChanged(); }
        }

        private string _passForChangingSomething1;
        public string PassForChangingSomething1
        {
            get { return _passForChangingSomething1; }
            set { _passForChangingSomething1 = value; OnPropertyChanged(); }
        }

        private string _moneyForLate;
        public string MoneyForLate
        {
            get { return _moneyForLate; }
            set { _moneyForLate = value; OnPropertyChanged(); }
        }

        private string _newMoneyForLate;
        public string NewMoneyForLate
        {
            get { return _newMoneyForLate; }
            set { _newMoneyForLate = value; OnPropertyChanged(); }
        }

        private string _soNgayMuon;
        public string SoNgayMuon
        {
            get { return _soNgayMuon;}
            set { _soNgayMuon = value; OnPropertyChanged(); }
        }

        private string _newSoNgayMuon;
        public string NewSoNgayMuon
        {
            get { return _newSoNgayMuon; }
            set { _newSoNgayMuon = value; OnPropertyChanged(); }
        }
        #endregion

        #region ICommand
        public ICommand LoadedInfor { get; set; }
        public ICommand LoadedFrame { get; set; }
        public ICommand Logout { get; set; }
        public ICommand LoadChangePass { get; set; }
        public ICommand LoadChangeInfor { get; set; }
        public ICommand CurrentPasswordChange { get; set; }
        public ICommand NewPasswordChange { get; set; }
        public ICommand ConfirmNewPasswordChange { get; set; }
        public ICommand SaveNewPasswordCommand { get; set; }
        public ICommand PassChanging { get; set; }
        public ICommand PassChanging1 { get; set; }
        public ICommand UpdateInforAdmin { get; set; }
        public ICommand Back1 { get; set; }
        public ICommand Back2 { get; set; }
        public ICommand OK { get; set; }
        public ICommand OK1 { get; set; }
        public ICommand BackChangePass { get; set; }
        public ICommand BackRolePage { get; set; }
        public ICommand LoadChangeRole { get; set; }
        public ICommand SaveChangeRole { get; set; }
        public ICommand Cancel { get; set; }

        #endregion
        public SettingViewModel()
        {
            LoadedInfor = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                InFr = p;
                MaKH = AdminServices.MaNhanVien;
                HoVaTen = AdminServices.TenNhanVien;
                TenTK = AdminServices.UserNameNhanVien;
                Email = AdminServices.EmailNhanVien;
                p.Content = new InformationPage();
            });

            
            LoadedFrame = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                RolFr = p;
                using (var context = new MasterlibraryEntities())
                {
                    SoNgayMuon = (from s in context.LUATTHUVIENs select s.SONGAYMUON).FirstOrDefault().ToString();
                    MoneyForLate = ((int)(from s in context.LUATTHUVIENs select s.TIENTRASACHMUONMOTNGAY).FirstOrDefault()).ToString();
                }    
                p.Content = new RolePage();
            });

            LoadChangeRole = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                RolFr.Content = new ChangeRolePage();
                NewMoneyForLate = MoneyForLate;
                NewSoNgayMuon = SoNgayMuon;
            });

            BackChangePass = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadedInfor.Execute(InFr);
            });

            Logout = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                MessageBoxML msb = new MessageBoxML("Cảnh báo", "Bạn có muốn đăng xuất không?", MessageType.Waitting, MessageButtons.YesNo);

                if (msb.ShowDialog() == true)
                {
                    MainAdminWindow w = Application.Current.Windows.OfType<MainAdminWindow>().FirstOrDefault();
                    w.Hide();

                    LoginWindow nw = new LoginWindow();
                    nw.Show();

                    w.Close();
                }
            });

            LoadChangePass = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InFr.Content = new ChangePass();
            });

            LoadChangeInfor = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InFr.Content = new ChangeInforPage();
                NewHoVaTen = HoVaTen; NewEmail = Email; NewTenTK = TenTK;

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

                string HashCurrentPassword = Utils.Helper.HashPassword(CurrentPassword);

                if (AdminServices.PasswordNhanVien != HashCurrentPassword)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Mật khẩu hiện tại không chính xác", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }
                else
                {
                    (bool isChangePassword, string lb) = await Task.Run(() => AdminServices.Ins.ChangePassword(MaKH, NewPassword));

                    if (isChangePassword == true)
                    {
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
            });

            PassChanging = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                PassForChangingSomething = p.Password;
                
            });

            PassChanging1 = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                PassForChangingSomething1 = p.Password;
            });

            UpdateInforAdmin = new RelayCommand<Frame>((p) => { return true; },  (p) =>
            {
                p.Content = new ConfirmPage();
            });

            Back1 = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                InFr.Content = new InformationPage();
            });

            Back2 = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                InFr.Content = new ChangeInforPage();
                NewHoVaTen = HoVaTen; NewEmail = Email; NewTenTK = TenTK;
            });

            OK = new RelayCommand<Object>((p) => { return true; }, async (p) =>
            {
                if (await Task.Run(() => AdminServices.Ins.checkPass(MaKH, PassForChangingSomething)))
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

                    if (await Task.Run(() => AdminServices.Ins.CheckEmailAdmin(NewEmail, MaKH)))
                    {
                        MessageBoxML ms = new MessageBoxML("Thông báo", "Email đã tồn tại", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                    else
                    if (await Task.Run(() => AdminServices.Ins.updateAdmin(MaKH, NewHoVaTen, NewEmail, NewTenTK)))
                    {
                        MessageBoxML ms = new MessageBoxML("Thông báo", "Chỉnh sửa thông tin thành công", MessageType.Accept, MessageButtons.OK);
                        ms.ShowDialog();
                        InFr.Content = new InformationPage();
                        HoVaTen = NewHoVaTen; TenTK = NewTenTK; Email = NewEmail;
                    }
                    else
                    {
                        MessageBoxML ms = new MessageBoxML("Lỗi", "Xảy ra lỗi khi thực hiện thao tác", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                    }

                    IsSaving = false;
                    
                }
                else
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", "Mật khẩu không chính xác", MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();
                }
            });

            BackRolePage = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                RolFr.Content = new RolePage();
            });

            SaveChangeRole = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ConfirmPassForRole();
                ConfFr = p;
            });

            OK1 = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                string se;
                using (var context = new MasterlibraryEntities())
                {
                    se = (from c in context.KHACHHANGs where c.MAKH == MaKH select c.USERPASSWORD).FirstOrDefault();
                }    
                if(Utils.Helper.HashPassword(PassForChangingSomething1) == se)
                {
                    MessageBoxML msb = new MessageBoxML("Cảnh báo", "Bạn có muốn thay đổi thông tin?", MessageType.Waitting, MessageButtons.YesNo);

                    if (msb.ShowDialog() == true)
                    {
                        using (var context = new MasterlibraryEntities())
                        {
                            var rol = context.LUATTHUVIENs.SingleOrDefault(s => s.MALUAT == 1);
                            rol.SONGAYMUON = int.Parse(NewSoNgayMuon);
                            rol.TIENTRASACHMUONMOTNGAY = decimal.Parse(NewMoneyForLate);
                            context.SaveChanges();
                        }
                        SoNgayMuon = NewSoNgayMuon; MoneyForLate = NewMoneyForLate;
                        RolFr.Content = new RolePage();
                        MessageBoxML msb1 = new MessageBoxML("Thông báo", "Sửa đổi thông tin thành công!", MessageType.Waitting, MessageButtons.OK);
                        msb1.ShowDialog();
                        ConfFr.Content = null;
                    }
                }    

                else
                {
                    MessageBoxML msb1 = new MessageBoxML("Thông báo", "Mật khẩu không chính xác!", MessageType.Waitting, MessageButtons.OK);
                    msb1.ShowDialog();
                }    
            });

            Cancel = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ConfFr.Content = null;
            });
        }
    }
}
