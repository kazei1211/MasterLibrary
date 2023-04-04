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

namespace MasterLibrary.Views.Admin.StatisticalPage
{
    /// <summary>
    /// Interaction logic for StatisticalPage.xaml
    /// </summary>
    public partial class StatisticalPage : Page
    {
        public StatisticalPage()
        {
            InitializeComponent();
        }

        private void categoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)categoryBox.SelectedItem;
            switch (cbi.Content.ToString())
            {
                case "Theo năm":
                    {
                        GetYearSource(TimeBox);
                        return;
                    }
                case "Theo tháng":
                    {
                        GetMonthSource(TimeBox);
                        return;
                    }
            }
        }

        private void categoryBox_Loaded(object sender, RoutedEventArgs e)
        {
            GetYearSource(TimeBox);
        }

        public void GetYearSource(ComboBox cbb)
        {
            if (cbb is null) return;

            List<string> list = new List<string>();

            int now = -1;
            for (int i = 2021; i <= System.DateTime.Now.Year; i++)
            {
                now++;
                list.Add(i.ToString());
            }

            cbb.ItemsSource = list;
            cbb.SelectedIndex = now;
        }

        public void GetMonthSource(ComboBox cbb)
        {
            if (cbb is null) return;

            List<string> list = new List<string>();

            list.Add("Tháng 1");
            list.Add("Tháng 2");
            list.Add("Tháng 3");
            list.Add("Tháng 4");
            list.Add("Tháng 5");
            list.Add("Tháng 6");
            list.Add("Tháng 7");
            list.Add("Tháng 8");
            list.Add("Tháng 9");
            list.Add("Tháng 10");
            list.Add("Tháng 11");
            list.Add("Tháng 12");

            cbb.ItemsSource = list;
            cbb.SelectedIndex = DateTime.Today.Month - 1;
        }
    }
}
