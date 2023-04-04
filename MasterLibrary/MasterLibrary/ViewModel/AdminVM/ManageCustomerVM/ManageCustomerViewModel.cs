using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.Admin.CustomerManagePage;
using MasterLibrary.Views.MessageBoxML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.AdminVM.ManageCustomerVM
{
    public partial class ManageCustomerViewModel: BaseViewModel
    {
        #region Thuộc tính
        private ObservableCollection<CustomerDTO> _ListCustomer;
        public ObservableCollection<CustomerDTO> ListCustomer
        {
            get { return _ListCustomer; }
            set { _ListCustomer = value; OnPropertyChanged(); }
        }

        private CustomerDTO _SelectedCustomer;
        public CustomerDTO SelectedCustomer
        {
            get { return _SelectedCustomer; }
            set { _SelectedCustomer = value; OnPropertyChanged(); }
        }

        private bool _IsLoadingMain;
        public bool IsLoadingMain
        {
            get { return _IsLoadingMain; }
            set { _IsLoadingMain = value; OnPropertyChanged(); }
        }

        private bool _IsSavingMain;
        public bool IsSavingMain
        {
            get { return _IsSavingMain; }
            set { _IsSavingMain = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand MaskNameManageCustomer { get; set; }
        public ICommand FirstLoadManageCustomerCM { get; set; }
        public ICommand OpenAddCustomerCM { get; set; }
        public ICommand OpenEditCustomerCM { get; set; }
        public ICommand DeleteCustomerCM { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskMain { get; set; }

        #endregion

        public ManageCustomerViewModel()
        {
            #region ManageCustomer
            FirstLoadManageCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FirstLoadManageCustomer();
            });

            MaskNameManageCustomer = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskMain = p;
            });

            OpenAddCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenAddCustomer();
            });

            OpenEditCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenEditCustomer();
            });

            DeleteCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteCustomerAsync();
            });



            #endregion

            #region AddCustomer
            FirstLoadAddCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FirstLoadAddCustomer();
            });

            MaskNameAddCustomer = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskAdd = p;
            });

            AddCustomerCM = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameCustomer) ||
                    string.IsNullOrEmpty(UserNameCustomer) ||
                    string.IsNullOrEmpty(DePasswordCustomer) ||
                    string.IsNullOrEmpty(EmailCustomer))
                {
                    return false;
                }

                return true;
            },
            (p) =>
            {
                AddCustomer();
            });

            #endregion

            #region EditCustomer
            FirstLoadEditCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FirstLoadEditCustomer();
            });

            MaskNameEditCustomer = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskEdit = p;
            });

            CloseEditWindowCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            SaveCustomerCM = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(NameCustomer) ||
                    string.IsNullOrEmpty(UserNameCustomer) ||
                    string.IsNullOrEmpty(DePasswordCustomer) ||
                    string.IsNullOrEmpty(EmailCustomer))
                {
                    return false;
                }

                return true;
            },
            (p) =>
            {
                SaveCustomer();
            });
            #endregion
        }

        async void FirstLoadManageCustomer()
        {
            IsLoadingMain = true;
            MaskMain.Visibility = Visibility.Visible;

            ListCustomer = new ObservableCollection<CustomerDTO>(await CustormerServices.Ins.GetAllCustomer());

            IsLoadingMain = false;
            MaskMain.Visibility = Visibility.Hidden;
        }

        void OpenAddCustomer()
        {
            MaskMain.Visibility = Visibility.Visible;

            AddCustomerWindow w = new AddCustomerWindow();
            w.ShowDialog();

            MaskMain.Visibility = Visibility.Hidden;
        }
        
        void OpenEditCustomer()
        {
            MaskMain.Visibility = Visibility.Visible;

            EditCustomerWindow w = new EditCustomerWindow();
            w.ShowDialog();

            MaskMain.Visibility = Visibility.Hidden;
        }

        async Task DeleteCustomerAsync()
        {
            IsSavingMain = true;
            MaskMain.Visibility = Visibility.Visible;

            MessageBoxML ms = new MessageBoxML("Thông báo", "Xác nhận xoá", MessageType.Error, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                (bool isDelete, string lb) = await CustormerServices.Ins.DeleteCustomer(SelectedCustomer.MAKH);

                if (isDelete == true)
                {
                    ListCustomer = new ObservableCollection<CustomerDTO>(await CustormerServices.Ins.GetAllCustomer());

                    MessageBoxML mss = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                    mss.ShowDialog();
                }
                else
                {
                    MessageBoxML mss = new MessageBoxML("Thông báo", lb, MessageType.Error, MessageButtons.OK);
                    mss.ShowDialog();
                }

            }

            MaskMain.Visibility = Visibility.Hidden;
            IsSavingMain = false;
        }
    }
}
