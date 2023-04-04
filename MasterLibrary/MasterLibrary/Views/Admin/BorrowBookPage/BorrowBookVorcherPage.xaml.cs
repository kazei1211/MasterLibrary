using MasterLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for BorrowBookVorcherPage.xaml
    /// </summary>
    public partial class BorrowBookVorcherPage : Page
    {
        public BorrowBookVorcherPage()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(FilterBox.Text))
                return true;
            else
                return ((item as BookDTO).TenSach.IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as BookDTO).MaSach.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxBook.ItemsSource).Refresh();
            CreateTextBoxFilter();
        }

        public void CreateTextBoxFilter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxBook.ItemsSource);
            view.Filter = Filter;
        }
    }
}
