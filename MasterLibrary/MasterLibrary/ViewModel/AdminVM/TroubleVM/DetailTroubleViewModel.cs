using MasterLibrary.DTOs;
using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace MasterLibrary.ViewModel.AdminVM.TroubleVM
{
    public partial class TroubleViewModel: BaseViewModel
    {
        #region Thuộc tính
        private int _IdTrouble;
        public int IdTrouble
        {
            get { return _IdTrouble; }
            set { _IdTrouble = value; OnPropertyChanged(); }
        }

        private string _NameCustomer;
        public string NameCustomer
        {
            get { return _NameCustomer; }
            set { _NameCustomer = value; OnPropertyChanged(); }
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

        private DateTime _DayReportTrouble;
        public DateTime DayReportTrouble
        {
            get { return _DayReportTrouble; }
            set { _DayReportTrouble = value; OnPropertyChanged(); }
        }

        private string _imgSourse;
        public string imgSourse
        {
            get { return _imgSourse; }
            set { _imgSourse= value; OnPropertyChanged(); }
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

        private ObservableCollection<StatusTroubleDTO> _ListStatusTroubleUpdate;
        public ObservableCollection<StatusTroubleDTO> ListStatusTroubleUpdate
        {
            get { return _ListStatusTroubleUpdate; }
            set { _ListStatusTroubleUpdate = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand CloseDetailTroubleAdmin { get; set; }
        public ICommand CloseProcessTrouble { get; set; }

        #endregion

        public async Task loadCurrentDataTrouble(TroubleDTO TroubleCurrent)
        {
            IdTrouble = TroubleCurrent.MaSC;
            NameCustomer = TroubleCurrent.TenKH;
            TitleTrouble = TroubleCurrent.TieuDe;
            DescribeTrouble = TroubleCurrent.MoTa;
            imgSourse = TroubleCurrent.Img;
            NameStatusTrouble = TroubleCurrent.TenTrangThaiSuCo;
            NameTypeTrouble = TroubleCurrent.TenLoaiSuCo;
            DayReportTrouble = TroubleCurrent.NgayBaoCao;
            Cost = TroubleCurrent.ChiPhi;
            Cost = ((int)Cost);
        }
    }
}
