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

namespace MasterLibrary.Views.Admin.CustomerManagePage
{
    /// <summary>
    /// Interaction logic for CustomerManagePage.xaml
    /// </summary>
    public partial class CustomerManagePage : Page
    {
        public CustomerManagePage()
        {
            InitializeComponent();
        }

        private bool Filter(object item)
        {
            if (String.IsNullOrEmpty(FilterBox.Text))
                return true;
            else
                return ((item as CustomerDTO).TENKH.IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as CustomerDTO).MAKH.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as CustomerDTO).USERNAME.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    ((item as CustomerDTO).EMAIL.ToString().IndexOf(FilterBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void FilterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvCustomer.ItemsSource).Refresh();
            CreateTextBoxFilter();
        }

        public void CreateTextBoxFilter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvCustomer.ItemsSource);
            view.Filter = Filter;
        }
    }
}
