using MasterLibrary.DTOs;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Views.Admin.LocationPage;
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

namespace MasterLibrary.ViewModel.AdminVM.LocationVM
{
    public class LocationViewModel: BaseViewModel
    {
        #region Thuộc tính
        private string _VisibilityFloor;
        public string VisibilityFloor
        {
            get { return _VisibilityFloor; }
            set { _VisibilityFloor = value; OnPropertyChanged(); }
        }

        private string _AddVisibilityFloor;
        public string AddVisibilityFloor
        {
            get { return _AddVisibilityFloor; }
            set { _AddVisibilityFloor = value; OnPropertyChanged(); }
        }

        private string _DeleteVisibilityFloor;
        public string DeleteVisibilityFloor
        {
            get { return _DeleteVisibilityFloor; }
            set { _DeleteVisibilityFloor = value; OnPropertyChanged(); }
        }

        private string _VisibilityRow;
        public string VisibilityRow
        {
            get { return _VisibilityRow; }
            set { _VisibilityRow = value; OnPropertyChanged(); }
        }

        private string _AddVisibilityRow;
        public string AddVisibilityRow
        {
            get { return _AddVisibilityRow; }
            set { _AddVisibilityRow = value; OnPropertyChanged(); }
        }

        private string _DeleteVisibilityRow;
        public string DeleteVisibilityRow
        {
            get { return _DeleteVisibilityRow; }
            set { _DeleteVisibilityRow = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TangDTO> _ListTang;
        public ObservableCollection<TangDTO> ListTang
        {
            get { return _ListTang; }
            set { _ListTang = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DayDTO> _ListDay;
        public ObservableCollection<DayDTO> ListDay
        {
            get { return _ListDay; }
            set { _ListDay = value; OnPropertyChanged(); }
        }

        private string _CurrenFloorName; 
        public string CurrenFloorName
        {
            get { return _CurrenFloorName; }
            set { _CurrenFloorName = value; OnPropertyChanged(); }
        }

        private string _NewBuildingName;
        public string NewBuildingName
        {
            get { return _NewBuildingName; }
            set { _NewBuildingName = value; OnPropertyChanged(); }
        }

        private bool _CanMoveUp;
        public bool CanMoveUp
        {
            get { return _CanMoveUp; }
            set { _CanMoveUp = value; OnPropertyChanged(); }
        }

        private bool _CanMoveDown;
        public bool CanMoveDown
        {
            get { return _CanMoveDown; }
            set { _CanMoveDown = value; OnPropertyChanged(); }
        }

        private string _SelectedBuildingName;
        public string SelectedBuildingName
        {
            get { return _SelectedBuildingName; }
            set { _SelectedBuildingName = value; OnPropertyChanged(); }
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { _IsLoading = value; OnPropertyChanged(); }
        }

        #endregion

        #region Icommand
        public ICommand FirstLoadLocationPage { get; set; }
        public ICommand MaskNameLocation { get; set; }
        public ICommand LoadBookInRow { get; set; }
        public ICommand OpenFloorOrRow { get; set; }
        public ICommand AddOrDeleteCommand { get; set; }
        public ICommand AddBuildingCommand { get; set; }
        public ICommand DeleteBuildingCommand { get; set; }
        public ICommand ChangeFloorCommand { get; set; }

        #endregion

        #region Thuộc tính tạm thời
        public int CurretPositionFloor { get; set; }
        public DayDTO SelectedDay { get; set; }
        public Grid MaskName { get; set; }

        #endregion

        public LocationViewModel()
        {
            FirstLoadLocationPage = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                IsLoading = true;

                VisibilityFloor = AddVisibilityFloor = DeleteVisibilityFloor = VisibilityRow = AddVisibilityRow = DeleteVisibilityRow = "Collapsed";

                ListTang = new ObservableCollection<TangDTO>((await BuildingServices.Ins.GetAllFloor()).OrderBy(t => t.TenTang));

                CurretPositionFloor = 0;
                CanMoveDown = false;

                if (ListTang.Count > 0)
                {
                    CurrenFloorName = ListTang[0].TenTang;
                    ListDay = new ObservableCollection<DayDTO>((await BuildingServices.Ins.GetAllRowInFloor(ListTang[CurretPositionFloor].MaTang)).OrderBy(dk => dk.TenDay));
                }

                if (ListTang.Count > 1)
                {
                    CanMoveUp = true;
                }
                else
                {
                    CanMoveUp = false;
                }

                IsLoading = false;
            });

            MaskNameLocation = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            LoadBookInRow = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                BookInRowViewModel.currentDay = SelectedDay;

                BookInRow w = new BookInRow();
                MaskName.Visibility = Visibility.Visible;
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            OpenFloorOrRow = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                switch (p.Name)
                {
                    case "btnFloor":
                        VisibilityFloor = "Visible";
                        VisibilityRow = "Collapsed";
                        AddVisibilityFloor = DeleteVisibilityFloor = AddVisibilityRow = DeleteVisibilityRow = "Collapsed";
                        break;

                    case "btnRow":
                        VisibilityFloor = "Collapsed";
                        VisibilityRow = "Visible";
                        AddVisibilityFloor = DeleteVisibilityFloor = AddVisibilityRow = DeleteVisibilityRow = "Collapsed";
                        break;
                }
            });

            AddOrDeleteCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                if (VisibilityFloor == "Visible")
                {
                    switch (p.Name)
                    {
                        case "btnAddFloor":
                            AddVisibilityFloor = "Visible";
                            DeleteVisibilityFloor = "Collapsed";
                            break;

                        case "btnDeleteFloor":
                            AddVisibilityFloor = "Collapsed";
                            DeleteVisibilityFloor = "Visible";
                            break;
                    }
                }
                else if (VisibilityRow == "Visible")
                {
                    switch (p.Name)
                    {
                        case "btnAddRow":
                            AddVisibilityRow = "Visible";
                            DeleteVisibilityRow = "Collapsed";
                            break;

                        case "btnDeleteRow":
                            AddVisibilityRow = "Collapsed";
                            DeleteVisibilityRow = "Visible";
                            break;
                    }
                }
            });

            AddBuildingCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (AddVisibilityFloor == "Visible")
                {
                    string currentNewBuildingName = NewBuildingName;

                    if (currentNewBuildingName == "")
                    {
                        MessageBoxML ms = new MessageBoxML("Thông báo", "Tên tầng không được để trống", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();

                        return;
                    }

                    currentNewBuildingName = currentNewBuildingName.Trim();

                    for (int i = 0; i < ListTang.Count; ++i)
                    {
                        if (currentNewBuildingName == ListTang[i].TenTang)
                        {
                            MessageBoxML ms = new MessageBoxML("Thông báo", "Tên tầng đã tồn tại", MessageType.Error, MessageButtons.OK);
                            ms.ShowDialog();

                            return;
                        }
                    }

                    (bool isCreate, string lb, int _MaTang) = await BuildingServices.Ins.CreateNewFloor(currentNewBuildingName);

                    if (isCreate == true)
                    {
                        ListTang.Add(new TangDTO
                        {
                            MaTang = _MaTang,
                            TenTang = currentNewBuildingName
                        });

                        if (ListTang.Count == 1)
                        {
                            CurrenFloorName = ListTang[0].TenTang;
                        }

                        if (CurretPositionFloor + 1 < ListTang.Count)
                        {
                            CanMoveUp = true;
                        }                        

                        NewBuildingName = "";

                        MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                    else
                    {
                        MessageBoxML ms = new MessageBoxML("Lỗi", lb, MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                }
                else if (AddVisibilityRow == "Visible")
                {
                    string currentNewBuildingName = NewBuildingName;

                    if (currentNewBuildingName == "")
                    {
                        MessageBoxML ms = new MessageBoxML("Thông báo", "Tên dãy kệ không được để trống", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();

                        return;
                    }

                    currentNewBuildingName = currentNewBuildingName.Trim();

                    for (int i = 0; i < ListDay.Count; ++i)
                    {
                        if (currentNewBuildingName == ListDay[i].TenDay)
                        {
                            MessageBoxML ms = new MessageBoxML("Thông báo", "Tên dãy kệ đã tồn tại", MessageType.Error, MessageButtons.OK);
                            ms.ShowDialog();

                            return;
                        }
                    }

                    (bool isCreate, string lb, int _MaDay) = await BuildingServices.Ins.CreateNewRow(currentNewBuildingName, ListTang[CurretPositionFloor].MaTang);

                    if (isCreate == true)
                    {
                        ListDay.Add(new DayDTO
                        {
                            MaDay = _MaDay,
                            TenDay = currentNewBuildingName,
                            MaTang = ListTang[CurretPositionFloor].MaTang,
                        });

                        NewBuildingName = "";

                        MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                    else
                    {
                        MessageBoxML ms = new MessageBoxML("Lỗi", lb, MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                }
            });

            DeleteBuildingCommand = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (string.IsNullOrEmpty(SelectedBuildingName))
                {
                    MessageBoxML ms = new MessageBoxML("Lỗi", "Tên không thể để trống", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();

                    return;
                }

                bool isDelete = false;
                string lb = "Lỗi thực hiện";

                if (DeleteVisibilityFloor == "Visible")
                {
                    (isDelete, lb) = await BuildingServices.Ins.DeleteFloor(SelectedBuildingName);
                     
                    if (isDelete == true)
                    {
                        int indexFloor = -1;
                        for (int i = 0; i < ListTang.Count; ++i)
                        {
                            if (ListTang[i].TenTang == SelectedBuildingName)
                            {
                                indexFloor = i;
                                break;
                            }
                        }

                        if (SelectedBuildingName == ListTang[CurretPositionFloor].TenTang)
                        {
                            // Đang ở tầng hiện tại thì sẽ có xu hướng giảm xuống hoặc tăng lên một tầng
                            if (indexFloor != -1) ListTang.RemoveAt(indexFloor);

                            if (CurretPositionFloor > 0) CurretPositionFloor -= 1;
                            if (ListDay.Count > 0)
                            {
                                CurrenFloorName = ListTang[CurretPositionFloor].TenTang;
                                ListDay = new ObservableCollection<DayDTO>((await BuildingServices.Ins.GetAllRowInFloor(ListTang[CurretPositionFloor].MaTang)).OrderBy(dk => dk.TenDay));
                            }
                            else CurrenFloorName = "";
                        }
                        else
                        {
                            // Đang ở tầng khác thì sẽ xoá và cập nhật
                            if (indexFloor != -1) ListTang.RemoveAt(indexFloor);

                            for (int i = 0; i < ListTang.Count; ++i)
                            {
                                if (ListTang[i].TenTang == SelectedBuildingName)
                                {
                                    CurretPositionFloor = i;
                                    break;
                                }
                            }
                        }

                        if (CurretPositionFloor + 1 < ListTang.Count)
                        {
                            CanMoveUp = true;
                        }
                        else
                        {
                            CanMoveUp = false;
                        }

                        if (CurretPositionFloor - 1 >= 0)
                        {
                            CanMoveDown = true;
                        }
                        else
                        {
                            CanMoveDown = false;
                        }
                    }
                }
                else if (DeleteVisibilityRow == "Visible")
                {
                    List<DayDTO> daylist = new List<DayDTO>
                    {
                        new DayDTO
                        {
                            TenDay = SelectedBuildingName,
                            MaTang = ListTang[CurretPositionFloor].MaTang
                        }
                    };

                    (isDelete, lb) = await BuildingServices.Ins.DeleteRow(ListTang[CurretPositionFloor].MaTang, daylist);

                    if (isDelete == true)
                    {
                        int indexRow = -1;

                        for (int i = 0; i < ListDay.Count; ++i)
                        {
                            if (ListDay[i].TenDay == SelectedBuildingName)
                            {
                                indexRow = i;
                                break;
                            }
                        }

                        if (indexRow != -1) ListDay.RemoveAt(indexRow);
                    }
                }

                if (isDelete == true)
                {
                    MessageBoxML ms = new MessageBoxML("Thông báo", lb, MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();
                }
                else
                {
                    MessageBoxML ms = new MessageBoxML("Lỗi", lb, MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }
            });

            ChangeFloorCommand = new RelayCommand<Button>((p) => { return true; }, async (p) =>
            {
                if (p.Name == "btnUpFloor")
                {
                    CurretPositionFloor += 1;
                    CurrenFloorName = ListTang[CurretPositionFloor].TenTang;

                    CanMoveDown = true;

                    if (CurretPositionFloor + 1 < ListTang.Count)
                    {
                        CanMoveUp = true;
                    }
                    else
                    {
                        CanMoveUp = false;
                    }
                } else if (p.Name == "btnDownFloor")
                {
                    CurretPositionFloor -= 1;
                    CurrenFloorName = ListTang[CurretPositionFloor].TenTang;

                    CanMoveUp = true;

                    if (CurretPositionFloor - 1 >= 0)
                    {
                        CanMoveDown = true;
                    }
                    else
                    {
                        CanMoveDown = false;
                    }
                }

                ListDay = new ObservableCollection<DayDTO>((await BuildingServices.Ins.GetAllRowInFloor(ListTang[CurretPositionFloor].MaTang)).OrderBy(dk => dk.TenDay));
            });
        }

    }
}
