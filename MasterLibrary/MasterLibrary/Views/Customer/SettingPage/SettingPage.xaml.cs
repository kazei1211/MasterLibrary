using System.Windows.Controls;

namespace MasterLibrary.Views.Customer.SettingPage
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            this.Language = System.Windows.Markup.XmlLanguage.GetLanguage("vi-VN");
        }
    }
}
