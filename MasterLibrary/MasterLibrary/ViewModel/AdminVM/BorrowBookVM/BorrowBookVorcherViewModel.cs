using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views;
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
        private ObservableCollection<BookInBorrowDTO> _ListBookInBorrow;
        public ObservableCollection<BookInBorrowDTO> ListBookInBorrow
        {
            get { return _ListBookInBorrow; }
            set { _ListBookInBorrow = value; OnPropertyChanged(); }
        }

        private BookInBorrowDTO _SelectedBookInBorrow;
        public BookInBorrowDTO SelectedBookInBorrow
        {
            get { return _SelectedBookInBorrow; }
            set
            { 
                if (value != null)
                {
                    _SelectedBookInBorrow = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _ExpirationDate;
        public DateTime ExpirationDate
        {
            get { return _ExpirationDate; }
            set { _ExpirationDate = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookDTO> _ListBook;
        public ObservableCollection<BookDTO> ListBook
        {
            get { return _ListBook; }
            set { _ListBook = value; OnPropertyChanged(); }
        }

        private BookDTO _SelectedBook;
        public BookDTO SelectedBook
        {
            get { return _SelectedBook; }
            set { _SelectedBook = value; OnPropertyChanged(); }
        }

        private int _ToTalBookInBorrow;
        public int ToTalBookInBorrow
        {
            get { return _ToTalBookInBorrow; }
            set { _ToTalBookInBorrow = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadBrrowBookVocherCM { get; set; }
        public ICommand ReSLBookInBorrowCurrentCM { get; set; }
        public ICommand MinusBookInBorrowCM { get; set; }
        public ICommand PlusBookInBorrowCM { get; set; }
        public ICommand DeleteBookInBorrowCM { get; set; }
        public ICommand AddBookToListBorrowCM { get; set; }
        public ICommand BorrowAllBookCM { get; set; }
        public ICommand DeleteAllBookInBorrowCM { get; set; }

        #endregion

        async void FirstLoadBrrowBookVocher()
        {
            MaskName.Visibility = Visibility.Visible;
            IsLoading = true;

            RoleLibrary = await RoleLibraryServices.Ins.GetARoleLibrary();

            ListBookInBorrow = new ObservableCollection<BookInBorrowDTO>();
            ListBook = new ObservableCollection<BookDTO>(await BookServices.Ins.GetAllbook());

            ToTalBookInBorrow = 0;
            MaKH = 0;
            TenKH = "";
            ExpirationDate = DateTime.Now.AddDays(RoleLibrary.Songaymuon);

            MaskName.Visibility = Visibility.Collapsed;
            IsLoading = false;
        }

        void FilterBookInBorrow()
        {
            ListBookInBorrow = new ObservableCollection<BookInBorrowDTO>(ListBookInBorrow.OrderBy(b => b.MaSach));
        }

        void ReCalTotalBookInBorrow()
        {
            int TotalBookInBorrowCurrent = 0;
            
            for (int i = 0; i < ListBookInBorrow.Count; ++i)
            {
                TotalBookInBorrowCurrent += ListBookInBorrow[i].SoLuong;
            }

            ToTalBookInBorrow = TotalBookInBorrowCurrent;
        }

        void ReSLBookInBorrowCurrent(TextBox p)
        {
            for (int i = 0; i < ListBookInBorrow.Count; i++)
            {
                if (ListBookInBorrow[i].SoLuong > ListBookInBorrow[i].SoLuongMax)
                {
                    ListBookInBorrow[i].SoLuong = ListBookInBorrow[i].SoLuongMax;
                    FilterBookInBorrow();
                    ReCalTotalBookInBorrow();
                }
                else if (string.IsNullOrEmpty(p.Text))
                {
                    ListBookInBorrow[i].SoLuong = 1;
                    FilterBookInBorrow();
                    ReCalTotalBookInBorrow();
                }
            }
        }

        void MinusBookInBorrow()
        {
            BookInBorrowDTO BookInBorrowCurrent = SelectedBookInBorrow;

            if (BookInBorrowCurrent != null)
            {
                int positionBookInBorrowDelete = -1;

                for (int i = 0; i < ListBookInBorrow.Count; i++)
                {
                    if (BookInBorrowCurrent.MaSach == ListBookInBorrow[i].MaSach)
                    {
                        if (BookInBorrowCurrent.SoLuong > 1)
                        {
                            ListBookInBorrow[i].SoLuong -= 1;
                            FilterBookInBorrow();
                            ReCalTotalBookInBorrow();
                        }
                        else
                        {
                            MessageBoxML ms = new MessageBoxML("Thông báo", "Bạn muốn xoá?", MessageType.Error, MessageButtons.YesNo);

                            if (ms.ShowDialog() == true)
                            {
                                positionBookInBorrowDelete = i;
                            }
                        }
                        break;
                    }
                }

                if (positionBookInBorrowDelete != -1)
                {
                    ListBookInBorrow.RemoveAt(positionBookInBorrowDelete);
                    FilterBookInBorrow();
                    ReCalTotalBookInBorrow();
                }
            }
        }

        void PlusBookInBorrow()
        {
            BookInBorrowDTO BookInBorrowCurrent = SelectedBookInBorrow;

            if (BookInBorrowCurrent != null)
            {
                for (int i = 0; i < ListBookInBorrow.Count; i++)
                {
                    if (BookInBorrowCurrent.MaSach == ListBookInBorrow[i].MaSach)
                    {
                        if (BookInBorrowCurrent.SoLuong + 1 <= ListBookInBorrow[i].SoLuongMax)
                        {
                            ListBookInBorrow[i].SoLuong += 1;
                            FilterBookInBorrow();
                            ReCalTotalBookInBorrow();
                        }
                        break;
                    }
                }
            }
        }

        void DeleteBookInBorrow()
        {
            BookInBorrowDTO BookInBorrowCurrent = SelectedBookInBorrow;

            MaskName.Visibility = Visibility.Visible;
            IsSaving = true;

            if (BookInBorrowCurrent != null)
            {
                int positionBookInBorrowDelete = -1;

                MessageBoxML ms = new MessageBoxML("Thông báo", "Bạn muốn xoá?", MessageType.Error, MessageButtons.YesNo);

                if (ms.ShowDialog() == true)
                {
                    for (int i = 0; i < ListBookInBorrow.Count; i++)
                    {
                        if (BookInBorrowCurrent.MaSach == ListBookInBorrow[i].MaSach)
                        {
                            positionBookInBorrowDelete = i;
                            break;
                        }
                    }

                    if (positionBookInBorrowDelete != -1)
                    {
                        ListBookInBorrow.RemoveAt(positionBookInBorrowDelete);
                        FilterBookInBorrow();
                        ReCalTotalBookInBorrow();
                    }
                }
            }

            MaskName.Visibility = Visibility.Collapsed;
            IsSaving = false;
        }

        

        void DeleteAllBookInBorrow()
        {
            MaskName.Visibility = Visibility.Visible;
            IsSaving = true;

            MessageBoxML ms = new MessageBoxML("Cảnh báo", "Bạn muốn xoá tất cả", MessageType.Error, MessageButtons.YesNo);
            
            if (ms.ShowDialog() == true)
            {
                ListBookInBorrow.Clear();
                FilterBookInBorrow();
                ReCalTotalBookInBorrow();
            }

            MaskName.Visibility = Visibility.Collapsed;
            IsSaving = false;
        }

        void AddBookToListBorrow()
        {
            BookDTO BookCurrent = SelectedBook;

            int positionBook = -1;

            for (int i = 0; i < ListBookInBorrow.Count; ++i)
            {
                if (ListBookInBorrow[i].MaSach == BookCurrent.MaSach)
                {
                    positionBook = i;
                    break;
                }
            }

            if (positionBook == -1)
            {
                if (BookCurrent.SoLuong > 0)
                {
                    ListBookInBorrow.Add(new BookInBorrowDTO
                    {
                        MaSach = BookCurrent.MaSach,
                        SoLuong = 1,
                        SoLuongMax = BookCurrent.SoLuong,
                        TenSach = BookCurrent.TenSach
                    });

                    FilterBookInBorrow();
                    ReCalTotalBookInBorrow();
                }
            }
            else
            {
                if (ListBookInBorrow[positionBook].SoLuong + 1 <= ListBookInBorrow[positionBook].SoLuongMax)
                {
                    ListBookInBorrow[positionBook].SoLuong += 1;
                    FilterBookInBorrow();
                    ReCalTotalBookInBorrow();
                }
            }
        }

        async void BorrowAllBook()
        {
            MaskName.Visibility = Visibility.Visible;
            IsSaving = true;

            (bool isBorrow, string lb) = await BookInBorrowServices.Ins.CreateNewCallCard(MaKH, ExpirationDate, DateTime.Now, ListBookInBorrow);

            if (isBorrow == true)
            {
                ListBookInBorrow.Clear();
                ListBook = new ObservableCollection<BookDTO>(await BookServices.Ins.GetAllbook());
                FilterBookInBorrow();
                MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
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
    }
}
