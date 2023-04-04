using MasterLibrary.Models.DataProvider;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace MasterLibrary.Views.Admin.BookManagePage
{
    /// <summary>
    /// Interaction logic for updatingwindow.xaml
    /// </summary>
    public partial class updatingwindow : Window
    {
        public static string masach;
        public static Image Image;

        public updatingwindow()
        {
            InitializeComponent();
        }

        public updatingwindow(string a)
        {
            InitializeComponent();
            masach = a;
            Image = image_img;
        }
    }
}
