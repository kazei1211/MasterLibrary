using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MasterLibrary.Views.Admin.HistoryPage
{
    /// <summary>
    /// Interaction logic for BorrowPage.xaml
    /// </summary>
    public partial class BorrowPage : Page
    {
        public BorrowPage()
        {
            InitializeComponent();
        }
        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvBorrow.ItemsSource).Refresh();
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvBorrow.ItemsSource);
            view.Filter = Filter;
        }

        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(FilterBox.Text)) return true;
            else
            {
                return ((item as BookInBorrowDTO).MaPhieuMuon.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                        ((item as BookInBorrowDTO).MaSach.ToString().IndexOf(FilterBox.Text, StringComparison.Ordinal) >= 0);
            }
        }

        private void filtercbb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbb = sender as ComboBox;
            if (cbbMonth != null)
            {
                switch (cbb.SelectedIndex)
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
