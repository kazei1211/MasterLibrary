using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MasterLibrary.ViewModel
{
    public class DetailBookViewModel : BaseViewModel
    {
        #region Thuộc tính
        private BookDTO _BookCurrent;
        public BookDTO BookCurrent
        {
            get { return _BookCurrent; }
            set
            {
                _BookCurrent = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadDetailBookCM { get; set; }
        public ICommand CloseDetailBookCM { get; set; }

        #endregion

        #region thuộc tính tạm thời
        public static int _IdBook { get; set; }

        #endregion

        public DetailBookViewModel()
        {
            // Load lần đầu
            FirstLoadDetailBookCM = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                BookCurrent = await BookServices.Ins.GetBook(_IdBook);
            });

            // Đóng trang detailBook
            CloseDetailBookCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DetailBook w = Application.Current.Windows.OfType<DetailBook>().FirstOrDefault();
                w.Close();
            });
        }
    }
}
