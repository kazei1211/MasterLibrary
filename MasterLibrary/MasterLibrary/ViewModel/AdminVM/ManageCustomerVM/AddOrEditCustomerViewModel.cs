using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.MessageBoxML;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.AdminVM.ManageCustomerVM
{
    public partial class ManageCustomerViewModel : BaseViewModel
    {
        #region Thuộc tính
        private int _IdCustomer;
        public int IdCustomer
        {
            get { return _IdCustomer; }
            set { _IdCustomer = value; OnPropertyChanged(); }
        }

        private string _NameCustomer;
        public string NameCustomer
        {
            get { return _NameCustomer; }
            set { _NameCustomer = value; OnPropertyChanged(); }
        }

        private string _UserNameCustomer;
        public string UserNameCustomer
        {
            get { return _UserNameCustomer; }
            set { _UserNameCustomer = value; OnPropertyChanged(); }
        }

        private string _DePasswordCustomer;
        public string DePasswordCustomer
        {
            get { return _DePasswordCustomer; }
            set { _DePasswordCustomer = value; OnPropertyChanged(); }
        }

        private string _EmailCustomer;
        public string EmailCustomer
        {
            get { return _EmailCustomer; }
            set { _EmailCustomer = value; OnPropertyChanged(); }
        }

        private string _AddressCustomer;
        public string AddressCustomer
        {
            get { return _AddressCustomer; }
            set { _AddressCustomer = value; OnPropertyChanged(); }
        }

        private bool _IsLoadingAdd;
        public bool IsLoadingAdd
        {
            get { return _IsLoadingAdd; }
            set { _IsLoadingAdd = value; OnPropertyChanged(); }
        }

        private bool _IsSavingAdd;
        public bool IsSavingAdd
        {
            get { return _IsSavingAdd; }
            set { _IsSavingAdd = value; OnPropertyChanged(); }
        }

        private bool _IsSavingEdit;
        public bool IsSavingEdit
        {
            get { return _IsSavingEdit; }
            set { _IsSavingEdit = value; OnPropertyChanged(); }
        }

        private bool _IsLoadingEdit;
        public bool IsLoadingEdit
        {
            get { return _IsLoadingEdit; }
            set { _IsLoadingEdit = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand AddCustomerCM { get; set; }
        public ICommand SaveCustomerCM { get; set; }
        public ICommand FirstLoadAddCustomerCM { get; set; }
        public ICommand MaskNameAddCustomer { get; set; }
        public ICommand MaskNameEditCustomer { get; set; }
        public ICommand FirstLoadEditCustomerCM { get; set; }
        public ICommand CloseEditWindowCM { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskAdd { get; set; }
        public Grid MaskEdit { get; set; }

        #endregion

        async void AddCustomer()
        {
            IsSavingAdd = true; 
            MaskAdd.Visibility = Visibility.Visible;

            (bool isCreate, string lb) = await CustormerServices.Ins.CreateNewCustomer(NameCustomer, UserNameCustomer, DePasswordCustomer, EmailCustomer, AddressCustomer);

            if (isCreate == true)
            {
                ListCustomer = new ObservableCollection<CustomerDTO>(await CustormerServices.Ins.GetAllCustomer());

                MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();
            }
            else
            {
                MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            }

            MaskAdd.Visibility = Visibility.Hidden;
            IsSavingAdd = false;
        }

        void loadDataCustomer()
        {
            IdCustomer = SelectedCustomer.MAKH;
            NameCustomer = SelectedCustomer.TENKH;
            UserNameCustomer = SelectedCustomer.USERNAME;
            DePasswordCustomer = SelectedCustomer.DeCodeUSERPASSWORD;
            EmailCustomer = SelectedCustomer.EMAIL;
            AddressCustomer = SelectedCustomer.DIACHI;
        }

        void ResetDataCustomer()
        {
            IdCustomer = 0;
            NameCustomer = "";
            UserNameCustomer = "";
            DePasswordCustomer = "";
            EmailCustomer = "";
            AddressCustomer = "";
        }

        void FirstLoadAddCustomer()
        {
            IsLoadingAdd = true;
            MaskAdd.Visibility = Visibility.Visible;

            ResetDataCustomer();

            IsLoadingAdd = false;
            MaskAdd.Visibility = Visibility.Hidden;
        }

        void FirstLoadEditCustomer()
        {
            IsLoadingEdit = true;
            MaskEdit.Visibility = Visibility.Visible;

            loadDataCustomer();

            IsLoadingEdit = false;
            MaskEdit.Visibility = Visibility.Hidden;
        }

        async void SaveCustomer()
        {
            IsSavingEdit = true;
            MaskEdit.Visibility = Visibility.Visible;

            (bool isSave, string lb) = await CustormerServices.Ins.SaveCustomer(IdCustomer, NameCustomer, UserNameCustomer, DePasswordCustomer, EmailCustomer, AddressCustomer);

            if (isSave == true)
            {
                ListCustomer = new ObservableCollection<CustomerDTO>(await CustormerServices.Ins.GetAllCustomer());

                MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();
            }
            else
            {
                MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            }

            IsSavingEdit = false;
            MaskEdit.Visibility = Visibility.Hidden;
        }
    }
}
