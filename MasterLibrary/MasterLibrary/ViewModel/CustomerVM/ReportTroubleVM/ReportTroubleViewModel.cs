using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using MasterLibrary.Views.Customer.ReportTroublePage;
using MasterLibrary.Views.MessageBoxML;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MasterLibrary.ViewModel.CustomerVM.ReportTroubleVM
{
    public partial class ReportTroubleViewModel: BaseViewModel
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
        public ICommand FirstLoadReportTrouble { get; set; }
        public ICommand MaskNameReportTrouble { get; set; }
        public ICommand OpenAddTroubleCommand { get; set; }
        public ICommand FilterTroubleCommand { get; set; }
        public ICommand DeleteTroubleCommand { get; set; }
        public ICommand OpenEditTroubleCommand { get; set; }
        public ICommand OpenDetailTroubleCommand { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public Grid MaskName { get; set; }
        public bool isImageChange { get; set; }

        #endregion

        public ReportTroubleViewModel()
        {
            #region ReportTrouble
            FirstLoadReportTrouble = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;

                ListTrouble = new ObservableCollection<TroubleDTO>((await TroubleServices.Ins.GetAllTroubleOfCustomer(MainCustomerViewModel.CurrentCustomer.MAKH)).OrderByDescending(sc => sc.NgayBaoCao));
                ListTrouble1 = new ObservableCollection<TroubleDTO>(ListTrouble);

                await loadStatusTrouble();
                await loadTypeTrouble();
                await LoadQuantityTrouble();

                IsLoading = false;
            });

            MaskNameReportTrouble = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            OpenAddTroubleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MaskName.Visibility = Visibility.Visible;
                AddRTrouble w = new AddRTrouble();
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            OpenEditTroubleCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await loadCurrentDataTrouble(SelectedTrouble);

                isImageChange = false;
                EditTrouble w = new EditTrouble();
                MaskName.Visibility = Visibility.Visible;
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            FilterTroubleCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await FilterTroube();
            });

            DeleteTroubleCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                TroubleDTO TroubleCurrent = SelectedTrouble;

                if (TroubleCurrent != null)
                {
                    MessageBoxML ms = new MessageBoxML("Xác nhận", "Xoá sự cố này", MessageType.Waitting, MessageButtons.YesNo);

                    if (ms.ShowDialog() == true)
                    {
                        (bool isDelete, string lb) = await TroubleServices.Ins.DeleteTrouble(TroubleCurrent.MaSC);

                        if (isDelete == true)
                        {
                            await ReloadData();

                            MessageBoxML mb = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                            mb.ShowDialog();
                        }
                        else
                        {
                            MessageBoxML mb = new MessageBoxML("Lỗi", lb, MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                        }
                    }
                }
            });

            OpenDetailTroubleCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentTrouble = SelectedTrouble;

                if (CurrentTrouble == null) return;

                CostStr = Utils.Helper.FormatVNMoney(CurrentTrouble.ChiPhi);

                DetailTrouble w = new DetailTrouble();
                MaskName.Visibility = Visibility.Visible;
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            #endregion

            #region AddOrEditTrouble
            FirstLoadAddReport = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DayReportTrouble = DateTime.Now;
                Cost = 0;
                NameStatusTrouble = Utils.Trouble.STATUS.WAITTING;

                resetPropertie();
            });

            FirstLoadEditReport = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                Cost = 0;
                NameStatusTrouble = Utils.Trouble.STATUS.WAITTING;
            });

            AddTroubleCommand = new RelayCommand<Window>((p) =>
            { 
                if (string.IsNullOrEmpty(TitleTrouble) ||
                    string.IsNullOrEmpty(filepath))
                {
                    return false;
                }

                return true;
            }, async (p) =>
            {
                IsSaving = true;

                string troubleImage = await CloudinaryService.Ins.UploadImage(filepath);

                if (troubleImage is null)
                {
                    IsSaving = false;
                    MessageBoxML ms = new MessageBoxML("Lỗi", "Gặp vấn đề trong quá trình lưu ảnh. Vui lòng thử lại", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    return;
                }

                TroubleDTO newTrouble = new TroubleDTO()
                {
                    MaKH = MainCustomerViewModel.CurrentCustomer.MAKH,
                    TieuDe = TitleTrouble,
                    MoTa = DescribeTrouble,
                    NgayBaoCao = DayReportTrouble,
                    Img = troubleImage,
                    ChiPhi = Cost,
                    TenTrangThaiSuCo = NameStatusTrouble,
                    TenLoaiSuCo = NameTypeTrouble
                };

                (bool isCreate, string lb, int newIdTrouble) = await TroubleServices.Ins.CreateTrouble(newTrouble);

                IsSaving = false;

                if (isCreate == true)
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

            UpdateTroubleCommand = new RelayCommand<Window>((p) =>
            {
                if (string.IsNullOrEmpty(TitleTrouble))
                {
                    return false;
                }

                return true;
            }, async (p) =>
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
                    TenLoaiSuCo = NameTypeTrouble
                };

                if (isImageChange == true)
                {
                    string troubleImage = await CloudinaryService.Ins.UploadImage(filepath);

                    if (troubleImage is null)
                    {
                        IsSaving = false;

                        MessageBoxML ms = new MessageBoxML("Lỗi", "Gặp vấn đề trong quá trình lưu ảnh. Vui lòng thử lại", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                        return;
                    }

                    newTrouble.Img = troubleImage;
                }
                else
                {
                    newTrouble.Img = SelectedTrouble.Img;
                }

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

            UploadImageCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openfile = new OpenFileDialog();
                openfile.Title = "Chọn một tấm ảnh";
                openfile.Filter = "Image File (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg; *.png";

                isImageChange = false;

                if (openfile.ShowDialog() == true)
                {
                    filepath = openfile.FileName;
                    LoadImage();

                    isImageChange = true;
                }
            });

            CloseAddWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            
            CloseEditWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            CloseDetailTrouble = new RelayCommand<Window>((p) => { return true; }, (p) =>
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
            ListTypeTroubleAddOrEdit = new ObservableCollection<TypeTroubleDTO>(await TroubleServices.Ins.GetAllTypeTrouble());
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
                } else
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
            ListTrouble = new ObservableCollection<TroubleDTO>((await TroubleServices.Ins.GetAllTroubleOfCustomer(MainCustomerViewModel.CurrentCustomer.MAKH)).OrderByDescending(sc => sc.NgayBaoCao));
            ListTrouble1 = new ObservableCollection<TroubleDTO>(ListTrouble);

            await FilterTroube();
            await LoadQuantityTrouble();
        }
    }
}
