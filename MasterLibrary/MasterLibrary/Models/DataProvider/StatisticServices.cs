using Haley.Models;
using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MasterLibrary.Models.DataProvider
{
    public partial class StatisticServices
    {
        private static StatisticServices _ins;
        public static StatisticServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new StatisticServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        //Tính tiền thu theo năm
        public async Task<(List<decimal>, decimal)> GetRevenueByYear(int year)
        {
            decimal inputMoney = (decimal)0;
            List<decimal> revenueByMonthList = new List<decimal>(new decimal[12]);

            using (var context = new MasterlibraryEntities())
            {
                var billList = context.HOADONs.Where(b => b.NGHD.Year == year);

                if (billList.ToList().Count != 0)
                {
                    inputMoney = (decimal) billList.Sum(b => b.TRIGIA);
                }
                

                var revenueByMonth = billList.GroupBy(b => b.NGHD.Month).Select(gr => new { Month = gr.Key, Income = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();
            
                foreach (var re in revenueByMonth)
                {
                    revenueByMonthList[re.Month - 1] = decimal.Truncate(re.Income);
                }
                return (revenueByMonthList, inputMoney);
            }
        }

        //Tính tiền chi theo năm
        public async Task<(List<decimal>, decimal)> GetExpenseByYear(int year)
        {
            decimal outputMoney = 0;
            List<decimal> expenseByMonthList = new List<decimal>(new decimal[12]);

            using (var context = new MasterlibraryEntities())
            {
                var receiptList = context.NHAPKHOes.Where(b => b.NGNHAP.Year == year);

                if (receiptList.ToList().Count != 0)
                {
                    outputMoney = (decimal)receiptList.Sum(b => b.TRIGIA);
                }
               
                var receiptByMonth = receiptList.GroupBy(b => b.NGNHAP.Month).Select(gr => new { Month = gr.Key, Output = gr.Sum(b => (decimal?)(b.TRIGIA) ?? 0)}).ToList();

                foreach (var re in receiptByMonth)
                {
                    expenseByMonthList[re.Month - 1] = decimal.Truncate(re.Output);
                }

                return (expenseByMonthList, outputMoney);
            }
        }

        //tính tiền sự cố theo năm
        public async Task<(List<decimal>, decimal)> GetExpenseTroubleByYear(int year)
        {
            decimal troubleMoney = 0;
            List<decimal> expenseTroubleList = new List<decimal>(new decimal[12]);

            using (var context = new MasterlibraryEntities())
            {
                var troubleList = context.SUCOes.Where(b => b.THOIGIANBAOCAO.Year == year);

                if (troubleList.ToList().Count != 0)
                {
                    troubleMoney = (decimal)troubleList.Sum(b => b.CHIPHI);
                }

                var troubleListByMonth = troubleList.GroupBy(b => b.THOIGIANBAOCAO.Month).Select(gr => new { Month = gr.Key, Output = gr.Sum(b => (decimal?)(b.CHIPHI) ?? 0) }).ToList();

                foreach (var tr in troubleListByMonth)
                {
                    expenseTroubleList[tr.Month - 1] = decimal.Truncate(tr.Output);
                }

                return (expenseTroubleList, troubleMoney);
            }
        }

        //tính tiền khi thu sách phát sinh theo năm
        public async Task<decimal> GetRevenueCollectByYear(int year)
        {
            decimal collectMoney = 0;

            using (var context = new MasterlibraryEntities())
            {
                var collectList = context.PHIEUTHUs.Where(b => b.NGAYTHU.Year == year);

                if (collectList.ToList().Count != 0)
                {
                    collectMoney += (decimal)collectList.Sum(b => b.TONGTIEN);
                }
                return collectMoney;
            }
        }

        //tính tiền thu theo tháng
        public async Task<(List<decimal>, decimal)> GetRevenueByMonth(int year, int month)
        {
            decimal inputMoney = (decimal)0;
            int days = DateTime.DaysInMonth(year, month);
            List<decimal> revenueByDayList = new List<decimal>(new decimal[days]);

            using (var context = new MasterlibraryEntities())
            {
                var billList = context.HOADONs.Where(b => b.NGHD.Year == year && b.NGHD.Month == month);

                if (billList.ToList().Count != 0)
                {
                    inputMoney = (decimal)billList.Sum(b => b.TRIGIA);
                }

                var revenueByDay = billList.GroupBy(b => b.NGHD.Day).Select(gr => new { Day = gr.Key, Income = gr.Sum(b => (decimal?)b.TRIGIA) ?? 0 }).ToList();

                foreach (var re in revenueByDay)
                {
                    revenueByDayList[re.Day - 1] = decimal.Truncate(re.Income);
                }

                return (revenueByDayList, inputMoney);
            }
        }

        //tính tiền thu sách theo tháng
        public async Task<(List<decimal>, decimal)> GetExpenseByMonth(int year, int month)
        {
            decimal outputMoney = (decimal)0;
            int days = DateTime.DaysInMonth(year, month);
            List<decimal> expenseByDayList = new List<decimal>(new decimal[days]);

            using (var context = new MasterlibraryEntities())
            {
                var receiptList = context.NHAPKHOes.Where(b => b.NGNHAP.Year == year && b.NGNHAP.Month == month);

                if (receiptList.ToList().Count != 0)
                {
                    outputMoney = (decimal)receiptList.Sum(b => (b.TRIGIA));
                }

                var revenueByDay = receiptList.GroupBy(b => b.NGNHAP.Day).Select(gr => new { Day = gr.Key, Income = gr.Sum(b => (decimal?)(b.TRIGIA) ?? 0 )}).ToList();

                foreach (var re in revenueByDay)
                {
                    expenseByDayList[re.Day - 1] = decimal.Truncate(re.Income);
                }

                return (expenseByDayList, outputMoney);
            }
        }

        //tính tiền sự cố theo tháng
        public async Task<(List<decimal>, decimal)> GetExpenseTroubleByMonth(int year, int month)
        {
            decimal troubleMoney = 0;
            int days = DateTime.DaysInMonth(year, month);
            List<decimal> expenseTroubleListByMonth = new List<decimal>(new decimal[days]);

            using (var context = new MasterlibraryEntities())
            {
                var troubleList = context.SUCOes.Where(b => b.THOIGIANBAOCAO.Year == year && b.THOIGIANBAOCAO.Month == month);

                if (troubleList.ToList().Count != 0)
                {
                    troubleMoney = (decimal)troubleList.Sum(b => b.CHIPHI);
                }

                var troubleListByMonth = troubleList.GroupBy(b => b.THOIGIANBAOCAO.Day).Select(gr => new { Day = gr.Key, Output = gr.Sum(b => (decimal?)(b.CHIPHI) ?? 0) }).ToList();

                foreach (var tr in troubleListByMonth)
                {
                    expenseTroubleListByMonth[tr.Day - 1] = decimal.Truncate(tr.Output);
                }

                return (expenseTroubleListByMonth, troubleMoney);
            }
        }

        //tính tiền khi thu sách phát sinh theo tháng
        public async Task<decimal> GetRevenueCollectByMonth(int year, int month)
        {
            decimal collectMoney = 0;

            using (var context = new MasterlibraryEntities())
            {
                var collectList = context.PHIEUTHUs.Where(b => b.NGAYTHU.Month == month && b.NGAYTHU.Year == year);

                if (collectList.ToList().Count != 0)
                {
                    collectMoney = (decimal)collectList.Sum(b => b.TONGTIEN);
                }

                return collectMoney;
            }
        }
    }
}
