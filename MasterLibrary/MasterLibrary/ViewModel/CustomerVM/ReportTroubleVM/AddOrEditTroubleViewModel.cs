using MasterLibrary.DTOs;
using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MasterLibrary.ViewModel.CustomerVM.ReportTroubleVM
{
    public partial class ReportTroubleViewModel: BaseViewModel
    {
        #region Thuộc tính
        private int _IdTrouble;
        public int IdTrouble
        {
            get { return _IdTrouble; }
            set { _IdTrouble = value; OnPropertyChanged(); }
        }

        private string _TitleTrouble;
        public string TitleTrouble
        {
            get { return _TitleTrouble; }
            set { _TitleTrouble = value; OnPropertyChanged(); }
        }

        private string _DescribeTrouble;
        public string DescribeTrouble
        {
            get { return _DescribeTrouble; }
            set { _DescribeTrouble = value; OnPropertyChanged(); }
        }

        private string _NameTypeTrouble;
        public string NameTypeTrouble
        {
            get { return _NameTypeTrouble; }
            set { _NameTypeTrouble = value; OnPropertyChanged(); }
        }

        private string _NameStatusTrouble;
        public string NameStatusTrouble
        {
            get { return _NameStatusTrouble; }
            set { _NameStatusTrouble = value; OnPropertyChanged(); }
        }

        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get { return _ImageSource; }
            set { _ImageSource = value; OnPropertyChanged(); }
        }

        private DateTime _DayReportTrouble;
        public DateTime DayReportTrouble
        {
            get { return _DayReportTrouble; }
            set { _DayReportTrouble = value; OnPropertyChanged(); }
        }

        private decimal _Cost;
        public decimal Cost
        {
            get { return _Cost; }
            set { _Cost = value; OnPropertyChanged(); }
        }

        private string _CostStr;
        public string CostStr
        {
            get { return _CostStr; }
            set { _CostStr = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TypeTroubleDTO> _ListTypeTroubleAddOrEdit;
        public ObservableCollection<TypeTroubleDTO> ListTypeTroubleAddOrEdit
        {
            get { return _ListTypeTroubleAddOrEdit; }
            set { _ListTypeTroubleAddOrEdit = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand FirstLoadAddReport { get; set; }
        public ICommand FirstLoadEditReport { get; set; }
        public ICommand UploadImageCM { get; set; }
        public ICommand AddTroubleCommand { get; set; }
        public ICommand UpdateTroubleCommand { get; set; }
        public ICommand CloseAddWindow { get; set; }
        public ICommand CloseEditWindow { get; set; }
        public ICommand CloseDetailTrouble { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public string filepath { get; set; }

        #endregion

        public void resetPropertie()
        {
            TitleTrouble = null;
            DescribeTrouble = null;
            ImageSource = null;
            filepath = null;
        }

        public void LoadImage()
        {
            BitmapImage _image = new BitmapImage();
            _image.BeginInit();
            _image.CacheOption = BitmapCacheOption.None;
            _image.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            _image.CacheOption = BitmapCacheOption.OnLoad;
            _image.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            _image.UriSource = new Uri(filepath, UriKind.RelativeOrAbsolute);
            _image.EndInit();
            ImageSource = _image;
        }

        public async Task loadCurrentDataTrouble(TroubleDTO TroubleCurrent)
        {
            IdTrouble = SelectedTrouble.MaSC;
            TitleTrouble = TroubleCurrent.TieuDe;
            DescribeTrouble = TroubleCurrent.MoTa;
            DayReportTrouble = DateTime.Now;
            NameTypeTrouble = TroubleCurrent.TenLoaiSuCo;
            ImageSource = await CloudinaryService.Ins.LoadImageFromURL(TroubleCurrent.Img);
        }
    }
}
