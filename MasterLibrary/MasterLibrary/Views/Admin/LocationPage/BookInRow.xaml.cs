using MasterLibrary.DTOs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MasterLibrary.Views.Admin.LocationPage
{
    /// <summary>
    /// Interaction logic for BookInRow.xaml
    /// </summary>
    public partial class BookInRow : Window
    {
        public BookInRow()
        {
            InitializeComponent();
        }

        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(FilterBox.Text))
                return true;
            else
                return ((item as BookDTO).TenSach.IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as BookDTO).TacGia.IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(MainListBox.ItemsSource).Refresh();
            CreateTextBoxFilter();
        }

        public void CreateTextBoxFilter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MainListBox.ItemsSource);
            view.Filter = Filter;
        }
    }
}
