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

namespace MasterLibrary.Views.Admin.BorrowBookPage
{
    /// <summary>
    /// Interaction logic for CollectionBookVorcherPage.xaml
    /// </summary>
    public partial class CollectionBookVorcherPage : Page
    {
        public CollectionBookVorcherPage()
        {
            InitializeComponent();
        }

        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(FilterBox.Text))
                return true;
            else
                return ((item as BookInBorrowDTO).TenSach.IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as BookInBorrowDTO).MaSach.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxBookInBorrow.ItemsSource).Refresh();
            CreateTextBoxFilter();
        }

        public void CreateTextBoxFilter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxBookInBorrow.ItemsSource);
            view.Filter = Filter;
        }
    }
}
