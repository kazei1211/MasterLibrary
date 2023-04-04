using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MasterLibrary.Utils.ConverterValue
{
    public class NotNullValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _Value = value as string;

            if (string.IsNullOrEmpty(_Value) == true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
