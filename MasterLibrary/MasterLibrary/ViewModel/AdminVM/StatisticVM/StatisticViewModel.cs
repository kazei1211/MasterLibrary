using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using MasterLibrary.Models.DataProvider;
using MasterLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MasterLibrary.Views.MessageBoxML;
using MasterLibrary.DTOs;
using System.Collections.ObjectModel;

namespace MasterLibrary.ViewModel.AdminVM.StatisticVM
{
    public class StatisticViewModel : BaseViewModel
    {

        public ICommand ChangePeriodML { get; set; }

        #region variable
        private SeriesCollection _IncomeData;
        public SeriesCollection IncomeData
        {
            get { return _IncomeData; }
            set { _IncomeData = value; OnPropertyChanged(); }
        }

        private ComboBoxItem _SelectedPeriod;
        public ComboBoxItem SelectedPeriod
        {
            get { return _SelectedPeriod; }
            set { _SelectedPeriod = value; OnPropertyChanged(); }
        }

        private string _SelectedTime;
        public string SelectedTime
        {
            get { return _SelectedTime;}
            set { _SelectedTime = value; OnPropertyChanged(); }
        }

        private int _SelectedYear;
        public int SelectedYear
        {
            get { return _SelectedYear; }
            set { _SelectedYear = value; }
        }

        private string _TrueIncome;
        public string TrueIncome
        {
            get { return _TrueIncome; }
            set { _TrueIncome = value; OnPropertyChanged(); }
        }

        private string _TotalIn;
        public string TotalIn
        {
            get { return _TotalIn; }
            set { _TotalIn = value; OnPropertyChanged(); }
        }

        private string _TotalOut;
        public string TotalOut
        {
            get { return _TotalOut; }
            set { _TotalOut = value; OnPropertyChanged(); }
        }

        private int _LabelMaxValue;
        public int LabelMaxValue
        {
            get { return _LabelMaxValue; }
            set { _LabelMaxValue = value; OnPropertyChanged(); }
        }

        private ObservableCollection<BookInCollectDTO> _ListBookInCollect;
        public ObservableCollection<BookInCollectDTO> ListBookInCollect
        {
            get { return _ListBookInCollect; }
            set { _ListBookInCollect = value; OnPropertyChanged(); }
        }

        void fillBookInCollect()
        {
            ListBookInCollect = new ObservableCollection<BookInCollectDTO>(ListBookInCollect.OrderBy(b=>b.MaSach));
        }

        decimal TinhTien()
        {
            decimal totalMoney = 0;
            for (int i = 0; i < ListBookInCollect.Count; ++i)
            {
                totalMoney += ListBookInCollect[i].TongTienTra;
            }
            return totalMoney;
        }

        #endregion
        public StatisticViewModel()
        {
            ChangePeriodML = new RelayCommand<ComboBox>((p) => { return true; }, async(p) =>
            {
                if (SelectedPeriod != null)
                {
                    switch (SelectedPeriod.Content.ToString())
                    {
                        case "Theo năm":
                            {
                                if (SelectedTime != null)
                                {
                                    if (SelectedTime.Length == 4)
                                        SelectedYear = int.Parse(SelectedTime);
                                    await LoadIncomeByYear();
                                }
                                return;
                            }
                        case "Theo tháng":
                            {
                                if (SelectedTime != null)
                                {
                                    LoadIncomeByMonth();
                                }
                                return;
                            }
                    }
                }
            }
            );
        }

        public async Task LoadIncomeByYear()
        {
            if (SelectedTime.Length != 4) return;
            LabelMaxValue = 12;

            try
            {
                (List<decimal> MonthlyRevenue, decimal totalin) = await Task.Run(() => StatisticServices.Ins.GetRevenueByYear(int.Parse(SelectedTime)));
                (List<decimal> MonthlyExpense, decimal totalout) = await Task.Run(() => StatisticServices.Ins.GetExpenseByYear(int.Parse(SelectedTime)));
                (List<decimal> MonthlyTrouble, decimal troublemoney) = await Task.Run(() => StatisticServices.Ins.GetExpenseTroubleByYear(int.Parse(SelectedTime)));
                decimal collectMoney = await Task.Run(() => StatisticServices.Ins.GetRevenueCollectByYear(int.Parse(SelectedTime)));
                List<BookInCollectDTO> feeBook = await Task.Run(() => BookInBorrowServices.Ins.GetCollectFeeBook());
                decimal inputMoney = await Task.Run(() => InputBookServices.Ins.GetInputMoneyList(feeBook));

                TotalIn = (totalin + collectMoney) == 0 ? "Không có giao dịch" : Helper.FormatVNMoney(totalin + collectMoney);
                TotalOut = (totalout + troublemoney) == 0 ? "Không có giao dịch" : Helper.FormatVNMoney(totalout + troublemoney);
                TrueIncome = (totalin + collectMoney - totalout) == 0 ? "Không có giao dịch" : Helper.FormatVNMoney(totalin + collectMoney - inputMoney - totalout);

                MonthlyRevenue.Insert(0, 0);
                MonthlyExpense.Insert(0, 0);
                //MonthlyTrouble.Insert(0, 0);

                for (int i = 1; i <= 12; i++)
                {
                    MonthlyRevenue[i] /= 1000000;
                    MonthlyExpense[i] /= 1000000;
                    //MonthlyTrouble[i] /= 1000000;
                }

                IncomeData = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Thu",
                    Values = new ChartValues<decimal>(MonthlyRevenue),
                    Fill = Brushes.Transparent,
                },
                new LineSeries
                {
                    Title = "Chi",
                    Values = new ChartValues<decimal>(MonthlyExpense),
                    Fill = Brushes.Transparent,
                }
            };
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Console.WriteLine(e);
                MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
        }

        public async void LoadIncomeByMonth()
        {
            if (SelectedTime.Length == 4) return;
            LabelMaxValue = 31;

            try
            {
                (List<decimal> DailyRevenue, decimal totalin) = await Task.Run(() => StatisticServices.Ins.GetRevenueByMonth(SelectedYear, int.Parse(SelectedTime.Remove(0, 6))));
                (List<decimal> DailyExpense, decimal totalout) = await Task.Run(() => StatisticServices.Ins.GetExpenseByMonth(SelectedYear, int.Parse(SelectedTime.Remove(0, 6))));
                (List<decimal> DailyTrouble, decimal troublemoney) = await Task.Run(() => StatisticServices.Ins.GetExpenseTroubleByMonth(SelectedYear, int.Parse(SelectedTime.Remove(0, 6))));
                decimal collectMoney = await Task.Run(() => StatisticServices.Ins.GetRevenueCollectByMonth(SelectedYear, int.Parse(SelectedTime.Remove(0, 6))));
                List<BookInCollectDTO> feeBook = await Task.Run(() => BookInBorrowServices.Ins.GetCollectFeeBookByMonth(SelectedYear, int.Parse(SelectedTime.Remove(0, 6))));
                decimal inputMoney = await Task.Run(() => InputBookServices.Ins.GetInputMoneyList(feeBook));

                collectMoney -= inputMoney;

                TotalIn = (totalin + collectMoney) == 0 ? "Không có giao dịch" : Helper.FormatVNMoney(totalin + collectMoney);
                TotalOut = (totalout + troublemoney) == 0 ? "Không có giao dịch" : Helper.FormatVNMoney(totalout + troublemoney);
                TrueIncome = (totalin + collectMoney - totalout) == 0 ? "Không có giao dịch" : Helper.FormatVNMoney(totalin + collectMoney - totalout);

                DailyRevenue.Insert(0, 0);
                DailyExpense.Insert(0, 0);
                //DailyTrouble.Insert(0, 0);

                for (int i = 1; i <= DailyRevenue.Count - 1; i++)
        {
                    DailyRevenue[i] /= 1000000;
                    DailyExpense[i] /= 1000000;
                    //DailyTrouble[i] /= 1000000;
                }

                IncomeData = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Thu",
                        Values = new ChartValues<decimal> (DailyRevenue),
                        Fill = Brushes.Transparent
                    },
                    new LineSeries
                    {
                        Title = "Chi",
                        Values = new ChartValues<decimal> (DailyExpense),
                        Fill = Brushes.Transparent
                    }
                };
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                Console.WriteLine(e);
                MessageBoxML mb = new MessageBoxML("Lỗi", "Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBoxML mb = new MessageBoxML("Lỗi", "Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                mb.ShowDialog();
                throw;
            }
        }
    }
}
