using MasterLibrary.DTOs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MasterLibrary.Models.DataProvider;
using System;
using System.Windows;
using System.Linq;
using System.Windows.Input;
using MasterLibrary.Utils;
using System.Windows.Controls;
using MasterLibrary.Views.Customer.BuyBookPage;

namespace MasterLibrary.ViewModel.CustomerVM.BuyBookVM
{
    public class BuyBookViewModel : BaseViewModel
    {
        #region Thuộc tính
        private ObservableCollection<BookDTO> _ListBook;
        public ObservableCollection<BookDTO> ListBook
        {
            get { return _ListBook; }
            set { _ListBook = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookDTO> _ListBook1;
        public ObservableCollection<BookDTO> ListBook1
        {
            get { return _ListBook1; }
            set { _ListBook1 = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _GenreBook;
        public ObservableCollection<string> GenreBook
        {
            get { return _GenreBook; }
            set { _GenreBook = value; OnPropertyChanged(); }
        }

        private string _SelectedGenre;
        public string SelectedGenre
        {
            get { return _SelectedGenre; }
            set { _SelectedGenre = value; OnPropertyChanged(); }
        }

        private BookDTO _SelectedItem;
        public BookDTO SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; OnPropertyChanged(); }
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadBuyBook { get; set; }
        public ICommand MaskNameBuyBook { get; set; }
        public ICommand SelectedGenreML { get; set; }
        public ICommand SortBookByMoney { get; set; }
        public ICommand SaveIconAscending { get; set; }
        public ICommand SaveIconDecreasing { get; set; }
        public ICommand LoadDetailBook { get; set; }
        

        #endregion

        public static Grid MaskName { get; set; }
        public bool isAscending { get; set; }
        public MaterialDesignThemes.Wpf.PackIcon iconAscending { get; set; }
        public MaterialDesignThemes.Wpf.PackIcon iconDecreasing { get; set; }

        public BuyBookViewModel()
        {
            // Load ban đầu
            FirstLoadBuyBook = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;

                ListBook1 = new ObservableCollection<BookDTO>(await BookServices.Ins.GetAllbook());
                GenreBook = new ObservableCollection<string>(baseBook.ListTheLoai);
                isAscending = false;

                await LoadMainListBox(0);

                IsLoading = false;
            });

            // Lọc thông tin theo thể loại
            SelectedGenreML = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await LoadMainListBox(1);
            });

            // Mở window detail book
            LoadDetailBook = new RelayCommand<object>((p) => { return true; }, (p) =>
            {

                MaskName.Visibility = Visibility.Visible;

                DetailBookViewModel.selectBookId = SelectedItem.MaSach;

                DetailBookService w = new DetailBookService();
                w.ShowDialog();
            });

            // Load mặt nạ
            MaskNameBuyBook = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            // Sắp xếp sách theo tiền
            SortBookByMoney = new RelayCommand<MaterialDesignThemes.Wpf.PackIcon>((p) => { return true; }, async (p) =>
            {
                await SortBook(isAscending);

                if (isAscending)
                {
                    iconDecreasing.Visibility = Visibility.Collapsed;
                    iconAscending.Visibility = Visibility.Visible;
                }
                else
                {
                    iconDecreasing.Visibility = Visibility.Visible;
                    iconAscending.Visibility = Visibility.Collapsed;
                }

                isAscending = !isAscending;
            });

            // Lưu lại icon tăng dần
            SaveIconAscending = new RelayCommand<MaterialDesignThemes.Wpf.PackIcon>((p) => { return true; }, async (p) =>
            {
                iconAscending = p;
                p.Visibility = Visibility.Visible;
            });

            // Lưu lại icon giảm dần
            SaveIconDecreasing = new RelayCommand<MaterialDesignThemes.Wpf.PackIcon>((p) => { return true; }, async (p) =>
            {
                iconDecreasing = p;
                p.Visibility = Visibility.Collapsed;
            });
        }

        public async Task LoadMainListBox(int func)
        {
            if (ListBook != null)
            {
                ListBook.Clear();
            }

            switch (func)
            {
                case 0:
                    // Load hết sách
                    try
                    {
                        ListBook = new ObservableCollection<BookDTO>(ListBook1);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Lỗi", "Mất kết nối cơ sở dữ liệu");
                    }
                    break;
                case 1:
                    await FilterBookByGenre();
                    break;
            }
        }

        public async Task FilterBookByGenre()
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(SelectedGenre))
                {
                    ListBook = new ObservableCollection<BookDTO>(ListBook1);
                }
                else
                {
                    // Load sách theo thể loại
                    ObservableCollection<BookDTO> tmpListBook = new ObservableCollection<BookDTO>();

                    foreach (var item in ListBook1)
                    {
                        if (item.TheLoai == SelectedGenre)
                        {
                            tmpListBook.Add(item);
                        }
                    }

                    ListBook = new ObservableCollection<BookDTO>(tmpListBook);
                }
            });
        }

        public async Task SortBook(bool ascending)
        {
            await Task.Run(() =>
            {
                ObservableCollection<BookDTO> tmpListBook = new ObservableCollection<BookDTO>(ListBook);

                if (ascending) ListBook = new ObservableCollection<BookDTO>(tmpListBook.OrderBy(a => a.Gia));
                else ListBook = new ObservableCollection<BookDTO>(tmpListBook.OrderByDescending(a => a.Gia));
            });
        }
    }
}
