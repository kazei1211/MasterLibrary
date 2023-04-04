using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.ViewModel.CustomerVM;
using MasterLibrary.Views.Customer.ReportTroublePage;
using MasterLibrary.Views.MessageBoxML;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using MasterLibrary.Views.Admin.TroublePage;
using MaterialDesignThemes.Wpf;
using System.Linq;

namespace MasterLibrary.ViewModel.AdminVM.TroubleVM
{
    public partial class TroubleViewModel: BaseViewModel
    {
        #region Thuộc tính
        private ObservableCollection<TroubleDTO> _ListTrouble;
        public ObservableCollection<TroubleDTO> ListTrouble
        {
            get { return _ListTrouble; }
            set { _ListTrouble = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TroubleDTO> _ListTrouble1;
        public ObservableCollection<TroubleDTO> ListTrouble1
        {
            get { return _ListTrouble1; }
            set { _ListTrouble1 = value; OnPropertyChanged(); }
        }

        private ObservableCollection<StatusTroubleDTO> _ListStatusTrouble;
        public ObservableCollection<StatusTroubleDTO> ListStatusTrouble
        {
            get { return _ListStatusTrouble; }
            set { _ListStatusTrouble = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TypeTroubleDTO> _ListTypeTrouble;
        public ObservableCollection<TypeTroubleDTO> ListTypeTrouble
        {
            get { return _ListTypeTrouble; }
            set { _ListTypeTrouble = value; OnPropertyChanged(); }
        }

        private int _QuantityWattingTrouble;
        public int QuantityWattingTrouble
        {
            get { return _QuantityWattingTrouble; }
            set { _QuantityWattingTrouble = value; OnPropertyChanged(); }
        }

        private int _QuantityDoneTrouble;
        public int QuantityDoneTrouble
        {
            get { return _QuantityDoneTrouble; }
            set { _QuantityDoneTrouble = value; OnPropertyChanged(); }
        }

        private int _QuantityCancleTrouble;
        public int QuantityCancleTrouble
        {
            get { return _QuantityCancleTrouble; }
            set { _QuantityCancleTrouble = value; OnPropertyChanged(); }
        }
        
        private int _QuantityTrouble;
        public int QuantityTrouble
        {
            get { return _QuantityTrouble; }
            set { _QuantityTrouble = value; OnPropertyChanged(); }
        }

        private string _TotalFeeStr;
        public string TotalFeeStr
        {
            get { return _TotalFeeStr; }
            set { _TotalFeeStr = value; OnPropertyChanged(); }
        }

        private string _ChooseNameTypeTrouble;
        public string ChooseNameTypeTrouble
        {
            get { return _ChooseNameTypeTrouble; }
            set { _ChooseNameTypeTrouble = value; OnPropertyChanged(); }
        }

        private string _ChooseNameStatusTrouble;
        public string ChooseNameStatusTrouble
        {
            get { return _ChooseNameStatusTrouble; }
            set { _ChooseNameStatusTrouble = value; OnPropertyChanged(); }
        }

        private TroubleDTO _SelectedTrouble;
        public TroubleDTO SelectedTrouble
        {
            get { return _SelectedTrouble; }
            set { _SelectedTrouble = value; OnPropertyChanged(); }
        }

        private TroubleDTO _CurrentTrouble;
        public TroubleDTO CurrentTrouble
        {
            get { return _CurrentTrouble; }
            set { _CurrentTrouble = value; OnPropertyChanged(); }
        }

        private bool _IsSaving;
        public bool IsSaving
        {
            get { return _IsSaving; }
            set { _IsSaving = value; OnPropertyChanged(); }
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; OnPropertyChanged(); }
        }

        #endregion

        #region Icommand
        public ICommand FirstLoadTrouble { get; set; }
        public ICommand MaskNameTrouble { get; set; }
        public ICommand FilterTroubleCommand { get; set; }
        public ICommand OpenDetailTroubleCommand { get; set; }
        public ICommand OpenProcessTroubleCommand { get; set; }
        public ICommand UpdateTroubleCommand { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskName { get; set; }

        #endregion

        public TroubleViewModel()
        {
            #region Trouble
            FirstLoadTrouble = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;

                ListTrouble = new ObservableCollection<TroubleDTO>((await TroubleServices.Ins.GetAllTrouble()).OrderByDescending(sc => sc.NgayBaoCao));
                ListTrouble1 = new ObservableCollection<TroubleDTO>(ListTrouble);

                await loadStatusTrouble();
                await loadTypeTrouble();
                await LoadQuantityTrouble();

                decimal CurrentTotalFee = 0;

                for (int i = 0; i < ListTrouble1.Count; ++i)
                {
                    CurrentTotalFee += ListTrouble1[i].ChiPhi;
                }

                TotalFeeStr = Utils.Helper.FormatVNMoney(CurrentTotalFee);

                IsLoading = false;
            });

            MaskNameTrouble = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });


            FilterTroubleCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await FilterTroube();
            });

            OpenProcessTroubleCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await loadCurrentDataTrouble(SelectedTrouble);

                ProcessTrouble w = new ProcessTrouble();
                MaskName.Visibility = Visibility.Visible;
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            OpenDetailTroubleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentTrouble = SelectedTrouble;

                if (CurrentTrouble == null) return;

                CostStr = Utils.Helper.FormatVNMoney(CurrentTrouble.ChiPhi);

                DetailTroubleAdmin w = new DetailTroubleAdmin();
                MaskName.Visibility = Visibility.Visible;
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            #endregion

            #region DetailOrProcessTrouble
            UpdateTroubleCommand = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                IsSaving = true;

                TroubleDTO newTrouble = new TroubleDTO()
                {
                    MaSC = IdTrouble,
                    TieuDe = TitleTrouble,
                    MoTa = DescribeTrouble,
                    NgayBaoCao = DayReportTrouble,
                    ChiPhi = Cost,
                    TenTrangThaiSuCo = NameStatusTrouble,
                    TenLoaiSuCo = NameTypeTrouble,
                    Img = imgSourse
                };

                (bool isUpdate, string lb) = await TroubleServices.Ins.UpdateTrouble(newTrouble);

                IsSaving = false;

                if (isUpdate == true)
                {
                    await ReloadData();

                    MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();

                    p.Close();
                }
                else
                {
                    MessageBoxML ms = new MessageBoxML("Lỗi", lb, MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }
            });

            CloseDetailTroubleAdmin = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            CloseProcessTrouble = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            #endregion
        }

        public async Task loadStatusTrouble()
        {
            List<StatusTroubleDTO> _currentListStatusTrouble = new List<StatusTroubleDTO>
                {
                    new StatusTroubleDTO
                    {
                        MaTTSC = "0",
                        TenTrangThaiSuCo = "Toàn bộ"
                    }
                };

            _currentListStatusTrouble.AddRange(await TroubleServices.Ins.GetAllStatusTrouble());
            ListStatusTrouble = new ObservableCollection<StatusTroubleDTO>(_currentListStatusTrouble);
            ListStatusTroubleUpdate = new ObservableCollection<StatusTroubleDTO>(await TroubleServices.Ins.GetAllStatusTrouble());

        }

        public async Task loadTypeTrouble()
        {
            List<TypeTroubleDTO> _currentListTypeTrouble = new List<TypeTroubleDTO>
                {
                    new TypeTroubleDTO
                    {
                        MaLSC = 0,
                        TenLoaiSuCo = "Toàn bộ"
                    }
                };

            _currentListTypeTrouble.AddRange(await TroubleServices.Ins.GetAllTypeTrouble());
            ListTypeTrouble = new ObservableCollection<TypeTroubleDTO>(_currentListTypeTrouble);
        }

        public async Task LoadQuantityTrouble()
        {
            int _numWaiting = 0;
            int _numDone = 0;
            int _numCancle = 0;

            for (int i = 0; i < ListTrouble1.Count; ++i)
            {
                if (ListTrouble1[i].TenTrangThaiSuCo == Utils.Trouble.STATUS.WAITTING)
                {
                    ++_numWaiting;
                }
                else if (ListTrouble1[i].TenTrangThaiSuCo == Utils.Trouble.STATUS.DONE)
                {
                    ++_numDone;
                }
                else
                {
                    ++_numCancle;
                }
            }

            QuantityWattingTrouble = _numWaiting;
            QuantityDoneTrouble = _numDone;
            QuantityCancleTrouble = _numCancle;

            QuantityTrouble = QuantityWattingTrouble + QuantityDoneTrouble + QuantityCancleTrouble;
        }

        public async Task FilterTroube()
        {
            await Task.Run(() =>
            {
                ObservableCollection<TroubleDTO> currentListTrouble = new ObservableCollection<TroubleDTO>();

                if ((ChooseNameTypeTrouble == "Toàn bộ" || string.IsNullOrEmpty(ChooseNameTypeTrouble)) &&
                    (ChooseNameStatusTrouble == "Toàn bộ" || string.IsNullOrEmpty(ChooseNameStatusTrouble)))
                {
                    ListTrouble = new ObservableCollection<TroubleDTO>(ListTrouble1);
                }
                else
                {
                    if (ChooseNameTypeTrouble == "Toàn bộ" || string.IsNullOrEmpty(ChooseNameTypeTrouble))
                    {
                        for (int i = 0; i < ListTrouble1.Count; ++i)
                        {
                            if (ListTrouble1[i].TenTrangThaiSuCo == ChooseNameStatusTrouble)
                            {
                                currentListTrouble.Add(ListTrouble1[i]);
                            }
                        }
                    }
                    else if (ChooseNameStatusTrouble == "Toàn bộ" || string.IsNullOrEmpty(ChooseNameStatusTrouble))
                    {
                        for (int i = 0; i < ListTrouble1.Count; ++i)
                        {
                            if (ListTrouble1[i].TenLoaiSuCo == ChooseNameTypeTrouble)
                            {
                                currentListTrouble.Add(ListTrouble1[i]);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ListTrouble1.Count; ++i)
                        {
                            if (ListTrouble1[i].TenLoaiSuCo == ChooseNameTypeTrouble && ListTrouble1[i].TenTrangThaiSuCo == ChooseNameStatusTrouble)
                            {
                                currentListTrouble.Add(ListTrouble1[i]);
                            }
                        }
                    }

                    ListTrouble = new ObservableCollection<TroubleDTO>(currentListTrouble);
                }
            });
        }

        public async Task ReloadData()
        {
            ListTrouble = new ObservableCollection<TroubleDTO>((await TroubleServices.Ins.GetAllTrouble()).OrderByDescending(sc => sc.NgayBaoCao));
            ListTrouble1 = new ObservableCollection<TroubleDTO>(ListTrouble);

            await FilterTroube();
            await LoadQuantityTrouble();

            decimal CurrentTotalFee = 0;

            for (int i = 0; i < ListTrouble1.Count; ++i)
            {
                CurrentTotalFee += ListTrouble1[i].ChiPhi;
            }

            TotalFeeStr = Utils.Helper.FormatVNMoney(CurrentTotalFee);
        }
    }
}
