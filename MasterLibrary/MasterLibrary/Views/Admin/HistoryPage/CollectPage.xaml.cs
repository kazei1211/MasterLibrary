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
    /// Interaction logic for CollectPage.xaml
    /// </summary>
    public partial class CollectPage : Page
    {
        public CollectPage()
        {
            InitializeComponent();
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvCollect.ItemsSource).Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvCollect.ItemsSource);
            view.Filter = Filter;
        }

        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(FilterBox.Text)) return true;
            else
            {
                return ((item as BookInCollectDTO).MaPhieuMuon.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as BookInCollectDTO).TenKH.ToString().IndexOf(FilterBox.Text, StringComparison.Ordinal) >= 0) ||
                    ((item as BookInCollectDTO).MaSach.ToString().IndexOf(FilterBox.Text, StringComparison.Ordinal) >= 0);
            }
        }

        private void filtercbb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbb = sender as ComboBox;
            if (cbbMonth != null)
            {
                switch(cbb.SelectedIndex)
                {
                    case 0:
                        {
                            cbbMonth.Visibility = Visibility.Collapsed;
                            cbbYear.Visibility = Visibility.Collapsed;
                            break;
                        }
                    case 1:
                        {
                            cbbMonth.Visibility = Visibility.Visible;
                            cbbYear.Visibility = Visibility.Visible;
                            break;
                        }
                }
            }
        }

        private void cbbYear_Loaded(object sender, RoutedEventArgs e)
        {
            if (cbbYear == null) return;

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
