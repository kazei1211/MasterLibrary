using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using MasterLibrary.Views.MessageBoxML;
using MasterLibrary.Views.Admin.LocationPage;
using MasterLibrary.Views;

namespace MasterLibrary.ViewModel.AdminVM.LocationVM
{
    public class BookInRowViewModel : BaseViewModel
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
        #endregion

        #region ICommand
        public ICommand FirstLoadBookInRow { get; set; }
        public ICommand MaskNameBookInRow { get; set; }
        public ICommand SelectedGenreML { get; set; }
        public ICommand LoadDetailBookInRow { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskName { get; set; }
        public static DayDTO currentDay { get; set; }

        #endregion

        public BookInRowViewModel()
        {
            // Load ban đầu
            FirstLoadBookInRow = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ListBook1 = new ObservableCollection<BookDTO>(await BookServices.Ins.GetBookInRow(currentDay.MaTang, currentDay.MaDay));
                GenreBook = new ObservableCollection<string>(baseBook.ListTheLoai);

                await LoadMainListBox(0);
            });

            // Lọc thông tin theo thể loại
            SelectedGenreML = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await LoadMainListBox(1);
            });

            // Mở window detail book
            LoadDetailBookInRow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailBook w = new DetailBook();
                DetailBookViewModel._IdBook = SelectedItem.MaSach;

                MaskName.Visibility = Visibility.Visible;
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            // Load mặt nạ
            MaskNameBookInRow = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
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
                        MessageBoxML ms = new MessageBoxML("Lỗi", "Mất kết nối cơ ở dữ liệu", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
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
    }
}