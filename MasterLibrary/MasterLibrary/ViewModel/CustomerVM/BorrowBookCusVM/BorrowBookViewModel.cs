using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.CustomerVM.BorrowBookCusVM
{
    public class BorrowBookViewModel : BaseViewModel
    {
        #region Thuộc tính
        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookInBorrowDTO> _ListBookInBorrow;
        public ObservableCollection<BookInBorrowDTO> ListBookInBorrow
        {
            get { return _ListBookInBorrow; }
            set { _ListBookInBorrow = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookInBorrowDTO> _ListBookInBorrow1;
        public ObservableCollection<BookInBorrowDTO> ListBookInBorrow1
        {
            get { return _ListBookInBorrow1; }
            set { _ListBookInBorrow1 = value; OnPropertyChanged(); }
        }

        private int _QuantityBookBorrow;
        public int QuantityBookBorrow
        {
            get { return _QuantityBookBorrow; }
            set { _QuantityBookBorrow = value; OnPropertyChanged(); }
        }

        private int _QuantityOverdueBook;
        public int QuantityOverdueBook
        {
            get { return _QuantityOverdueBook; }
            set { _QuantityOverdueBook = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _ListStatusBookInBorrow;
        public ObservableCollection<string> ListStatusBookInBorrow
        {
            get { return _ListStatusBookInBorrow; }
            set { _ListStatusBookInBorrow = value; OnPropertyChanged(); }
        }

        private string _ChooseNameStatusBookInBorrow;
        public string ChooseNameStatusBookInBorrow
        {
            get { return _ChooseNameStatusBookInBorrow; }
            set { _ChooseNameStatusBookInBorrow = value; OnPropertyChanged(); }
        }
        
        private BookInBorrowDTO _SelectedBookInBorrow;
        public BookInBorrowDTO SelectedBookInBorrow
        {
            get { return _SelectedBookInBorrow; }
            set { _SelectedBookInBorrow = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadBorrowBookCM { get; set; }
        public ICommand MaskNameBorrowBoookCM { get; set; }
        public ICommand FilterBookInBorrowCM { get; set; }
        public ICommand OpenDetailBookCM { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskName { get; set; }

        #endregion

        public BorrowBookViewModel()
        {
            FirstLoadBorrowBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FirstLoadBorrowBook();
            });

            MaskNameBorrowBoookCM = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            FilterBookInBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FilterBookInBorrow();
            });

            OpenDetailBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenDetailBook();
            });
        }

        async void FirstLoadBorrowBook()
        {
            MaskName.Visibility = Visibility.Visible;
            IsLoading = true;

            ListBookInBorrow = new ObservableCollection<BookInBorrowDTO>(await BookInBorrowServices.Ins.GetBookBorrowCustomer(MainCustomerViewModel.CurrentCustomer.MAKH));
            ListBookInBorrow1 = new ObservableCollection<BookInBorrowDTO>(ListBookInBorrow);
            ListStatusBookInBorrow = new ObservableCollection<string>{
                Utils.BookInBorrow.STATUS.All,
                Utils.BookInBorrow.STATUS.Undue,
                Utils.BookInBorrow.STATUS.OutOfDay
            };

            CalcQuantityBook();

            MaskName.Visibility = Visibility.Collapsed;
            IsLoading = false;
        }

        void CalcQuantityBook()
        {
            int QuantityOverdueBookCurrent = 0;

            for (int i = 0; i < ListBookInBorrow.Count; ++i)
            {
                if ((DateTime.Now - ListBookInBorrow[i].NgayHetHan).Days > 0)
                {
                    QuantityOverdueBookCurrent += 1;
                }
            }

            QuantityBookBorrow = ListBookInBorrow.Count;
            QuantityOverdueBook = QuantityOverdueBookCurrent;
        }

        void FilterBookInBorrow()
        {
            IsLoading = true;

            ObservableCollection<BookInBorrowDTO> currentListBookInBorrow = new ObservableCollection<BookInBorrowDTO>();

            if (ChooseNameStatusBookInBorrow == Utils.BookInBorrow.STATUS.All || string.IsNullOrEmpty(ChooseNameStatusBookInBorrow))
            {
                ListBookInBorrow = new ObservableCollection<BookInBorrowDTO>(ListBookInBorrow1);
            }
            else
            {
                if (ChooseNameStatusBookInBorrow == Utils.BookInBorrow.STATUS.Undue)
                {
                    for (int i = 0; i < ListBookInBorrow1.Count; ++i)
                    {
                        if ((DateTime.Now - ListBookInBorrow1[i].NgayHetHan).Days <= 0)
                        {
                            currentListBookInBorrow.Add(ListBookInBorrow1[i]);
                        }
                    }
                }
                else if (ChooseNameStatusBookInBorrow == Utils.BookInBorrow.STATUS.OutOfDay)
                {
                    for (int i = 0; i < ListBookInBorrow1.Count; ++i)
                    {
                        if ((DateTime.Now - ListBookInBorrow1[i].NgayHetHan).Days > 0)
                        {
                            currentListBookInBorrow.Add(ListBookInBorrow1[i]);
                        }
                    }
                }

                ListBookInBorrow = new ObservableCollection<BookInBorrowDTO>(currentListBookInBorrow);
            }

            IsLoading = false;
        }

        void OpenDetailBook()
        {
            if (SelectedBookInBorrow == null) return;

            DetailBookViewModel._IdBook = SelectedBookInBorrow.MaSach;

            DetailBook w = new DetailBook();
            w.ShowDialog();
        }
    }
}
