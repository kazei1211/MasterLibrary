using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.Admin.StatisticalPage;
using MasterLibrary.Views.Admin.BookManagePage;
using MasterLibrary.Views.Admin.HistoryPage;
using System.Windows;
using MasterLibrary.Views.Admin.ImportBookPage;
using MasterLibrary.Views.Admin.LocationPage;
using MasterLibrary.Views.MessageBoxML;
using MasterLibrary.Views.Admin.TroublePage;
using MasterLibrary.Views.Admin.BorrowBookPage;
using MasterLibrary.Views.Admin.SettingPage;
using MasterLibrary.Views.LoginWindow;
using MasterLibrary.Views.Admin.CustomerManagePage;
using System;

namespace MasterLibrary.ViewModel.AdminVM
{
    public partial class MainAdminViewModel : BaseViewModel
    {
        private string _CurrentTime;
        public string CurrentTime
        {
            get { return _CurrentTime; }
            set { _CurrentTime = value; OnPropertyChanged(); }
        }


        public ICommand FirstLoadML { get; set; }
        public ICommand LoadStatisticalPageML { get; set; }
        public ICommand LoadBookManagerPageML { get; set; }
        public ICommand LoadBookManagePageML { get; set; }
        public ICommand LoadHistoryPageML { get; set; }
        public ICommand LoadImportBookPageML { get; set; }
        public ICommand LoadLocationPageML { get; set; }
        public ICommand LoadCustomerManagePageML { get; set; }
        public ICommand LoadBorrowBookPageML { get; set; }
        public ICommand LoadTroublePageML { get; set; }
        public ICommand LoadSettingPageML { get; set; }
        public ICommand SignOutML { get; set; }
        public MainAdminViewModel()
        {
            // Real Time
            System.Windows.Threading.DispatcherTimer Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(Timer_Click);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();

            // Load trang phân tích
            LoadStatisticalPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new StatisticalPage();
            });

            // Load trang quản lý khách hàng
            LoadCustomerManagePageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new CustomerManagePage();
            });

            // Load trang quản lí sách
            LoadBookManagePageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BookManagePage1();
            });

            // Load trang lịch sử
            LoadHistoryPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new HistoryPage();
            });

            // Load trang nhập sách
            LoadImportBookPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ImportBookPage();
            });

            // Load trang tầng dẫy
            LoadLocationPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new LocationPage();
            });


            // Load trang thuê sách
            LoadBorrowBookPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BorrowBookPage();
            });

            // Load trang sự cố
            LoadTroublePageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new TroublePage();
            });

            LoadSettingPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {

                p.Content = new SettingPageAdmin();
            });

            SignOutML = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                MessageBoxML ms = new MessageBoxML("Xác nhận", "Bạn muốn đăng xuất", MessageType.Waitting, MessageButtons.YesNo);

                if (ms.ShowDialog() == true)
                {
                    p.Hide();

                    LoginWindow w = new LoginWindow();
                    w.Show();

                    p.Close();
                }
            });

        }  

        public void Timer_Click(object sender, EventArgs e)
        {
            DateTime d;
            d = DateTime.Now;
            CurrentTime = string.Format("{0}:{1}:{2}", d.Hour.ToString("00"), d.Minute.ToString("00"), d.Second.ToString("00"));
        }
    }
}
