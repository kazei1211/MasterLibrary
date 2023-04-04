using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MasterLibrary.Utils.Converters
{
    public class OperatorTroubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _TrangThai = value as string;

            if (_TrangThai == Utils.Trouble.STATUS.WAITTING)
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

    public class ForegroundStatusTroubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            // Retrieve the format string and use it to format the value.
            string _TrangThai = value as string;

            if (_TrangThai == Utils.Trouble.STATUS.WAITTING)
            {
                return "#ba1111";
            }
            else if (_TrangThai == Utils.Trouble.STATUS.DONE)
            {
                return "#428720";
            } 
            else
            {
                return "#666565";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
