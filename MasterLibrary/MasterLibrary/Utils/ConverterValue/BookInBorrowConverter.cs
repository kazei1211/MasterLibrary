using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MasterLibrary.Utils.ConverterValue
{
    public class ForegroundStatusBookInBorrowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime _NgayHetHan = (DateTime)value;

            if ((DateTime.Now - _NgayHetHan).Days > 0)
            {
                return "#ba1111";
            }
            else
            {
                return "#428720";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextDayBookInBorrowCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime _NgayHetHan = (DateTime)value;

            int _dayCurrent = (_NgayHetHan - DateTime.Now).Days;

            if (_dayCurrent == 0)
            {
                return "Ngày hôm nay";
            }
            else if (_dayCurrent > 0)
            {
                return "Còn " + (_dayCurrent + 1).ToString() + " ngày";
            }
            else
            {
                return "Quá hạn";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextDayLateBookInCollectCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime _NgayHetHan = (DateTime)value;

            int _dayCurrent = (DateTime.Now - _NgayHetHan).Days;

            if (_dayCurrent > 0)
            {
                return "Sách trễ " + _dayCurrent.ToString() + " ngày";
            }
            else
            {
                return "Quá hạn";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HiddenLateBookInCollectCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime _NgayHetHan = (DateTime)value;

            int _dayCurrent = (DateTime.Now - _NgayHetHan).Days;

            if (_dayCurrent > 0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
