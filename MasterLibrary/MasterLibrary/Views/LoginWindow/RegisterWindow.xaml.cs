using MasterLibrary.ViewModel.LoginVM;
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
using System.Windows.Shapes;

namespace MasterLibrary.Views.LoginWindow
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : System.Windows.Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btn_Click_Close(object sender, RoutedEventArgs e)
        {
            LoginViewModel.Mask.Visibility = Visibility.Collapsed;
            this.Close();
        }
    }
}
