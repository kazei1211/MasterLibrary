using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MasterLibrary.Utils.ConverterValue
{
    public class ChangePasswordMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string _FirstValue = values[0] as string;
            string _SecondValue = values[1] as string;
            string _ThirdValue = values[2] as string;

            if (string.IsNullOrEmpty(_FirstValue) == true || string.IsNullOrEmpty(_SecondValue))
            {
                return Visibility.Hidden;
            }
            else
            {
                if (string.IsNullOrEmpty(_ThirdValue) == true)
                {
                    return Visibility.Visible;

                }
                else
                {
                    return Visibility.Hidden;
                }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
