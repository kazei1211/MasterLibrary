using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterLibrary.Views.Admin.HistoryPage
{
    /// <summary>
    /// Interaction logic for RevenuePage.xaml
    /// </summary>
    public partial class RevenuePage : Page
    {
        public RevenuePage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvRevenue.ItemsSource).Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvRevenue.ItemsSource);
            view.Filter = Filter;
        }

        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(FilterBox.Text)) return true;
            else
            {
                return ((item as BillDTO).MAHD.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as BillDTO).cusName.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbb = sender as ComboBox;

            if (cbbMonth != null && timePicker != null)
            {
                switch (cbb.SelectedIndex)
                {
                    case 0:
                        {
                            cbbMonth.Visibility = Visibility.Collapsed;
                            cbbYear.Visibility = Visibility.Collapsed;
                            timePicker.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case 1:
                        {
                            cbbMonth.Visibility = Visibility.Collapsed;
                            cbbYear.Visibility = Visibility.Collapsed;
                            timePicker.Visibility = Visibility.Visible;
                            break;
                        }
                    case 2:
                        {
                            cbbMonth.Visibility = Visibility.Visible;
                            cbbYear.Visibility = Visibility.Visible;
                            timePicker.Visibility = Visibility.Collapsed;
                            break;
                        }
                }
            }
        }

        private void cbbYear_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbbYear is null) return;

            List<string> list = new List<string>();

            int now = -1;
            for (int i = 2021; i <= DateTime.Now.Year; i++)
            {
                now++;
                list.Add(i.ToString());
            }

            cbbYear.ItemsSource = list;
            cbbYear.SelectedIndex = now;
        }
    }
}
