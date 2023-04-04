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
using System.Windows.Shapes;

namespace MasterLibrary.Views.Admin.TroublePage
{
    /// <summary>
    /// Interaction logic for ProcessTrouble.xaml
    /// </summary>
    public partial class ProcessTrouble : Window
    {
        public ProcessTrouble()
        {
            InitializeComponent();
        }

        private void costval_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void costval_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.Text.Length == 0)
                tb.Text = "0";
        }

        private void OutlinedComboBoxStatusTroubleInProcessTrouble_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbb = sender as ComboBox;

            if (cbb.SelectedValue is null) 
            {
                if (!(costval is null)) costval.IsEnabled = false;
                return;
            }

            if (cbb.SelectedValue.ToString() == Utils.Trouble.STATUS.DONE)
            {
                if (!(costval is null)) costval.IsEnabled = true;
            }
            else
            {
                if (!(costval is null)) costval.IsEnabled = false;
            }
        }
    }
}
