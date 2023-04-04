using MasterLibrary.ViewModel;
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

namespace MasterLibrary.UserControlML
{
    /// <summary>
    /// Interaction logic for controlbar_uc.xaml
    /// </summary>
    public partial class controlbar_uc : UserControl
    {
        public ControlBarViewModel cbViewModel { get; set; }
        public controlbar_uc()
        {
            InitializeComponent();
            this.DataContext = cbViewModel = new ControlBarViewModel();
        }
    }
}
