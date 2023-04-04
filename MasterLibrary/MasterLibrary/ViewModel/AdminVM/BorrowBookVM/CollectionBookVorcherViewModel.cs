using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.ViewModel.CustomerVM;
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

namespace MasterLibrary.ViewModel.AdminVM.BorrowBookVM
{
    public partial class BorrowBookViewModel: BaseViewModel
    {
        #region Thuộc tính
        private DateTime _DateNow;
        public DateTime DateNow
        {
            get { return _DateNow; }
            set { _DateNow = value; OnPropertyChanged(); }
        }
        
        private int _ToTalBookInCollect;
        public int ToTalBookInCollect
        {
            get { return _ToTalBookInCollect; }
            set { _ToTalBookInCollect = value; OnPropertyChanged(); }
        }

        private decimal _ToTalMoneyInCollect;
        public decimal ToTalMoneyInCollect
        {
            get { return _ToTalMoneyInCollect; }
            set { _ToTalMoneyInCollect = value; OnPropertyChanged(); }
        }

        private string _ToTalMoneyInCollectStr;
        public string ToTalMoneyInCollectStr
        {
            get { return _ToTalMoneyInCollectStr; }
            set { _ToTalMoneyInCollectStr = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookInBorrowDTO> _ListBookBorrow;
        public ObservableCollection<BookInBorrowDTO> ListBookBorrow
        {
            get { return _ListBookBorrow; }
            set { _ListBookBorrow = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookInCollectDTO> _ListBookInCollect;
        public ObservableCollection<BookInCollectDTO> ListBookInCollect
        {
            get { return _ListBookInCollect; }
            set { _ListBookInCollect = value; OnPropertyChanged(); }
        }

        private BookInCollectDTO _SelectedBookInCollect;
        public BookInCollectDTO SelectedBookInCollect
        {
            get { return _SelectedBookInCollect; }
            set { _SelectedBookInCollect = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand CollectionBookVorcherCM { get; set; }
        public ICommand LoadBookInBorrowCustomerCM { get; set; }
        public ICommand AddBookToListCollectCM { get; set; }
        public ICommand CollectAllBookCM { get; set; }
        public ICommand DeleteAllBookInCollectCM { get; set; }
        public ICommand ReSLCurrentBookInCollectCM { get; set; }
        public ICommand DeleteBookInCollectCM { get; set; }
        public ICommand MinusBookInCollectCM { get; set; }
        public ICommand PlusBookInCollectCM { get; set; }
        public ICommand ReSLBookBreakInCollectCM { get; set; }

        #endregion

        void CollectionBookVorcher()
        {
            IsLoading = true;
            MaskName.Visibility = Visibility.Visible;

            ListBookBorrow = new ObservableCollection<BookInBorrowDTO>();
            ListBookInCollect = new ObservableCollection<BookInCollectDTO>();

            MaKH = 0;
            TenKH = "";
            DateNow = DateTime.Now;
            ToTalBookInCollect = 0;

            IsLoading = false;
            MaskName.Visibility = Visibility.Collapsed;
        }

        async void LoadBookInBorrowCustomer()
        {
            if (string.IsNullOrEmpty(TenKH))
            {
                IsLoading = true;

                ListBookBorrow = new ObservableCollection<BookInBorrowDTO>(await BookInBorrowServices.Ins.GetBookBorrowCustomer(MaKH));

                IsLoading = false;
            }
            else
            {
                ListBookBorrow = new ObservableCollection<BookInBorrowDTO>();
            }
        }

        void FilterBookInCollect()
        {
            ListBookInCollect = new ObservableCollection<BookInCollectDTO>(ListBookInCollect.OrderBy(b => b.MaSach));
            ReCalTotalBookInCollect();
        }

        void ReCalTotalBookInCollect()
        {
            int ToTalBookInCollectCurrent = 0;
            decimal ToTalMoneyInCollectCurrent = 0;

            for (int i = 0; i < ListBookInCollect.Count; ++i)
            {
                ToTalBookInCollectCurrent += ListBookInCollect[i].SoLuong;
                ToTalMoneyInCollectCurrent += ListBookInCollect[i].TongTienTra; 
            }

            ToTalBookInCollect = ToTalBookInCollectCurrent;
            ToTalMoneyInCollect = ToTalMoneyInCollectCurrent;
            ToTalMoneyInCollectStr = Utils.Helper.FormatVNMoney(ToTalMoneyInCollect);
        }

        void AddBookToListCollect()
        {
            BookInBorrowDTO BookCurrent = SelectedBookInBorrow;

            int positionBook = -1;

            for (int i = 0; i < ListBookInCollect.Count; ++i)
            {
                if (ListBookInCollect[i].MaSach == BookCurrent.MaSach)
                {
                    positionBook = i;
                    break;
                }
            }

            if (positionBook == -1)
            {
                if (BookCurrent.SoLuong > 0)
                {
                    BookInCollectDTO NewBookInCollect = new BookInCollectDTO
                    {
                        MaPhieuMuon = SelectedBookInBorrow.MaPhieuMuon,
                        MaSach = SelectedBookInBorrow.MaSach,
                        TenSach = SelectedBookInBorrow.TenSach,
                        NgayHetHan = SelectedBookInBorrow.NgayHetHan,
                        NgayTra = DateNow,
                        SoLuong = 1,
                        SoLuongHong = 0,
                        SoLuongMax = SelectedBookInBorrow.SoLuong,
                        TienHong = SelectedBookInBorrow.Gia,
                        TienTre = (int)RoleLibrary.TienTraTreMotNgay,
                        TongTienTreMotCuon = 0,
                        TongTienTra = 0
                    };

                    if ((NewBookInCollect.NgayTra - NewBookInCollect.NgayHetHan).Days > 0)
                    {
                        NewBookInCollect.TongTienTreMotCuon = (NewBookInCollect.NgayTra - NewBookInCollect.NgayHetHan).Days * NewBookInCollect.TienTre;
                    }

                    NewBookInCollect.TongTienTra = NewBookInCollect.TongTienTreMotCuon * NewBookInCollect.SoLuong;

                    ListBookInCollect.Add(NewBookInCollect);

                    FilterBookInCollect();
                }
            }
            else
            {
                if (ListBookInCollect[positionBook].SoLuong + 1 <= ListBookInCollect[positionBook].SoLuongMax)
                {
                    ListBookInCollect[positionBook].SoLuong += 1;

                    ListBookInCollect[positionBook].TongTienHong = ListBookInCollect[positionBook].TienHong * ListBookInCollect[positionBook].SoLuongHong;

                    ListBookInCollect[positionBook].TongTienTra = ListBookInCollect[positionBook].TongTienTreMotCuon * ListBookInCollect[positionBook].SoLuong +
                                                                    ListBookInCollect[positionBook].TongTienHong;

                    FilterBookInCollect();
                }
            }
        }
        
        async void CollectAllBook()
        {
            MaskName.Visibility = Visibility.Visible;
            IsSaving = true;

            (bool isCollect, string lb) = await BookInBorrowServices.Ins.CreateNewReceipt(MaKH, ListBookInCollect);

            if (isCollect == true)
            {
                MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);

                ListBookInCollect.Clear();
                FilterBookInCollect();
                ListBookBorrow = new ObservableCollection<BookInBorrowDTO>(await BookInBorrowServices.Ins.GetBookBorrowCustomer(MaKH));

                ms.ShowDialog();
            }
            else
            {
                MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            }

            MaskName.Visibility = Visibility.Collapsed;
            IsSaving = false;
        }

        void DeleteAllBookInCollect()
        {
            MaskName.Visibility = Visibility.Visible;
            IsSaving = true;

            MessageBoxML ms = new MessageBoxML("Cảnh báo", "Bạn muốn xoá tất cả", MessageType.Error, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                ListBookInCollect = new ObservableCollection<BookInCollectDTO>();
                FilterBookInCollect();
            }

            MaskName.Visibility = Visibility.Collapsed;
            IsSaving = false;
        }

        void ReSLCurrentBookInCollect(TextBox p)
        {
            for (int i = 0; i < ListBookInCollect.Count; i++)
            {
                if (ListBookInCollect[i].SoLuong > ListBookInCollect[i].SoLuongMax)
                {
                    ListBookInCollect[i].SoLuong = ListBookInCollect[i].SoLuongMax;
                }
                else if (string.IsNullOrEmpty(p.Text))
                {
                    ListBookInCollect[i].SoLuong = 1;

                    if (ListBookInCollect[i].SoLuongHong > ListBookInCollect[i].SoLuong)
                    {
                        ListBookInCollect[i].SoLuongHong = ListBookInCollect[i].SoLuong;
                        ListBookInCollect[i].TongTienHong = ListBookInCollect[i].TienHong * ListBookInCollect[i].SoLuongHong;
                    }

                    ListBookInCollect[i].TongTienTra = ListBookInCollect[i].TongTienTreMotCuon * ListBookInCollect[i].SoLuong +
                                                                    ListBookInCollect[i].TongTienHong;

                }
            }

            FilterBookInCollect();
        }

        void ReSLBookBreakInCollect(TextBox p)
        {
            for (int i = 0; i < ListBookInCollect.Count; i++)
            {
                if (ListBookInCollect[i].SoLuongHong > ListBookInCollect[i].SoLuong)
                {
                    ListBookInCollect[i].SoLuongHong = ListBookInCollect[i].SoLuong;
                }
                else if (string.IsNullOrEmpty(p.Text))
                {
                    ListBookInCollect[i].SoLuongHong = 0;
                }

                ListBookInCollect[i].TongTienHong = ListBookInCollect[i].TienHong * ListBookInCollect[i].SoLuongHong;
                ListBookInCollect[i].TongTienTra = ListBookInCollect[i].TongTienTreMotCuon * ListBookInCollect[i].SoLuong +
                                                                ListBookInCollect[i].TongTienHong;
            }

            FilterBookInCollect();
        }

        void DeleteBookInCollect()
        {
            BookInCollectDTO BookInCollectCurrent = SelectedBookInCollect;

            MaskName.Visibility = Visibility.Visible;
            IsSaving = true;

            if (BookInCollectCurrent != null)
            {
                int positionBookInCollectDelete = -1;

                MessageBoxML ms = new MessageBoxML("Thông báo", "Bạn muốn xoá?", MessageType.Error, MessageButtons.YesNo);

                if (ms.ShowDialog() == true)
                {
                    for (int i = 0; i < ListBookInCollect.Count; i++)
                    {
                        if (BookInCollectCurrent.MaSach == ListBookInCollect[i].MaSach)
                        {
                            positionBookInCollectDelete = i;
                            break;
                        }
                    }

                    if (positionBookInCollectDelete != -1)
                    {
                        ListBookInCollect.RemoveAt(positionBookInCollectDelete);
                        FilterBookInCollect();
                    }
                }
            }

            MaskName.Visibility = Visibility.Collapsed;
            IsSaving = false;
        }

        void MinusBookInCollect()
        {
            BookInCollectDTO BookInCollectCurrent = SelectedBookInCollect;

            if (BookInCollectCurrent != null)
            {
                int positionBookInCollectDelete = -1;

                for (int i = 0; i < ListBookInCollect.Count; i++)
                {
                    if (BookInCollectCurrent.MaSach == ListBookInCollect[i].MaSach)
                    {
                        if (BookInCollectCurrent.SoLuong > 1)
                        {
                            ListBookInCollect[i].SoLuong -= 1;

                            if (ListBookInCollect[i].SoLuongHong > ListBookInCollect[i].SoLuong)
                            {
                                ListBookInCollect[i].SoLuongHong = ListBookInCollect[i].SoLuong;
                                ListBookInCollect[i].TongTienHong = ListBookInCollect[i].TienHong * ListBookInCollect[i].SoLuongHong;
                            }

                            ListBookInCollect[i].TongTienTra = ListBookInCollect[i].TongTienTreMotCuon * ListBookInCollect[i].SoLuong +
                                                                            ListBookInCollect[i].TongTienHong;

                            FilterBookInCollect();
                        }
                        else
                        {
                            MessageBoxML ms = new MessageBoxML("Thông báo", "Bạn muốn xoá?", MessageType.Error, MessageButtons.YesNo);

                            if (ms.ShowDialog() == true)
                            {
                                positionBookInCollectDelete = i;
                            }
                        }
                        break;
                    }
                }

                if (positionBookInCollectDelete != -1)
                {
                    ListBookInCollect.RemoveAt(positionBookInCollectDelete);
                    FilterBookInCollect();
                }
            }
        }

        void PlusBookInCollect()
        {
            BookInCollectDTO BookInCollectCurrent = SelectedBookInCollect;

            if (BookInCollectCurrent != null)
            {
                for (int i = 0; i < ListBookInCollect.Count; i++)
                {
                    if (BookInCollectCurrent.MaSach == ListBookInCollect[i].MaSach)
                    {
                        if (BookInCollectCurrent.SoLuong + 1 <= ListBookInCollect[i].SoLuongMax)
                        {
                            ListBookInCollect[i].SoLuong += 1;

                            ListBookInCollect[i].TongTienTra = ListBookInCollect[i].TongTienTreMotCuon * ListBookInCollect[i].SoLuong +
                                                                            ListBookInCollect[i].TongTienHong;

                            FilterBookInCollect();
                        }
                        break;
                    }
                }
            }
        }
    }

}
