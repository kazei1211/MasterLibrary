using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.ViewModel.CustomerVM;
using MasterLibrary.Views.Admin.BorrowBookPage;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MasterLibrary.ViewModel.AdminVM.BorrowBookVM
{
    public partial class BorrowBookViewModel: BaseViewModel
    {
        #region Thuộc tính
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

        

        #endregion

        #region ICommand
        public ICommand MaskNameBrrowBook { get; set; }
        public ICommand LoadBorrowBookVorcherPage { get; set; }
        public ICommand LoadCollectionBookVorcherPage { get; set; }
        public ICommand FindNameCustomerCM { get; set; }
        public ICommand FirstLoadBrrowBookCM { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskName { get; set; }
        public RoleLibraryDTO RoleLibrary { get; set; }

        #endregion

        public BorrowBookViewModel()
        {
            #region BorrowBookViewModel
            MaskNameBrrowBook = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            LoadBorrowBookVorcherPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new BorrowBookVorcherPage();
            });

            LoadCollectionBookVorcherPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new CollectionBookVorcherPage();
            });

            FindNameCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FindNameCustomer();
            });

            FirstLoadBrrowBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FirstLoadBrrowBook();
            });

            #endregion

            #region BorrowBookVorcher
            FirstLoadBrrowBookVocherCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                FirstLoadBrrowBookVocher();
            });

            ReSLBookInBorrowCurrentCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                ReSLBookInBorrowCurrent(p);
            });

            MinusBookInBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MinusBookInBorrow();
            });

            PlusBookInBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PlusBookInBorrow();
            });

            DeleteBookInBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteBookInBorrow();
            });

            DeleteAllBookInBorrowCM = new RelayCommand<object>((p) =>
            {
                if (ListBookInBorrow is null || ListBookInBorrow.Count <= 0) return false;

                return true; 
            },
            (p) =>
            {
                DeleteAllBookInBorrow();
            });

            AddBookToListBorrowCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddBookToListBorrow();
            });

            BorrowAllBookCM = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(TenKH) || ListBookInBorrow.Count <= 0)
                {
                    return false;
                }

                return true;
            },
            (p) =>
            {
                BorrowAllBook();
            });

            #endregion

            #region CollectionBookVorcher
            CollectionBookVorcherCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CollectionBookVorcher();
            });

            LoadBookInBorrowCustomerCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                LoadBookInBorrowCustomer();
            });

            AddBookToListCollectCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddBookToListCollect();
            });

            DeleteBookInCollectCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DeleteBookInCollect();
            });

            MinusBookInCollectCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MinusBookInCollect();
            });

            PlusBookInCollectCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                PlusBookInCollect();
            });

            ReSLCurrentBookInCollectCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                ReSLCurrentBookInCollect(p);
            });

            ReSLBookBreakInCollectCM = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                ReSLBookBreakInCollect(p);
            });

            DeleteAllBookInCollectCM = new RelayCommand<object>((p) =>
            {
                if (ListBookInCollect is null || ListBookInCollect.Count <= 0) return false;

                return true;
            },
            (p) =>
            {
                DeleteAllBookInCollect();
            });

            CollectAllBookCM = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(TenKH) || ListBookInCollect is null || ListBookInCollect.Count <= 0) return false;

                return true;
            },
            (p) =>
            {
                CollectAllBook();
            });

            #endregion
        }

        async void FindNameCustomer()
        {
            CustomerDTO CustomerCurrent = await Task.Run(() => CustormerServices.Ins.FindCustomer(MaKH));

            if (CustomerCurrent is null)
            {
                TenKH = "";
            }
            else
            {
                TenKH = CustomerCurrent.TENKH;
            }
        }

        async void FirstLoadBrrowBook()
        {
           
        }
    }
}
