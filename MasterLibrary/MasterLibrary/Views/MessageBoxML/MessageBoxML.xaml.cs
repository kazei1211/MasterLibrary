using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MasterLibrary.Views.MessageBoxML
{
    /// <summary>
    /// Interaction logic for MessageBoxML.xaml
    /// </summary>
    public partial class MessageBoxML : Window
    {
        public MessageBoxML(string Title, string Message, MessageType Type, MessageButtons Buttons)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            txtTitle.Text = Title;

            switch (Type)
            {

                case MessageType.Accept:
                    System.Media.SystemSounds.Beep.Play();
                    BackgroundBorder.ImageSource = new BitmapImage(new Uri("pack://application:,,,/MasterLibrary;component/Resources/Images/AcceptBackground.png"));
                    break;
                case MessageType.Waitting:
                    System.Media.SystemSounds.Beep.Play();
                    BackgroundBorder.ImageSource = new BitmapImage(new Uri("pack://application:,,,/MasterLibrary;component/Resources/Images/WaittingBackground.png"));
                    break;
                case MessageType.Error:
                    System.Media.SystemSounds.Beep.Play();
                    BackgroundBorder.ImageSource = new BitmapImage(new Uri("pack://application:,,,/MasterLibrary;component/Resources/Images/ErrorBackground.png"));
                    break;
            }
            switch (Buttons)
            {
                case MessageButtons.YesNo:
                    btnOk.Visibility = Visibility.Collapsed; btnCancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageButtons.OK:
                    btnOk.Visibility = Visibility.Visible;
                    btnCancel.Visibility = Visibility.Collapsed;
                    btnYes.Visibility = Visibility.Collapsed;
                    btnNo.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        public void ChangeBackGround(Color newcolor)
        {
            BackGroundTittle.Background = new SolidColorBrush(newcolor);
            btnOk.Background = new SolidColorBrush(newcolor);
            btnYes.Background = new SolidColorBrush(newcolor);
            btnClose.Foreground = new SolidColorBrush(newcolor);
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
    public enum MessageButtons
    {
        YesNo,
        OK
    }
    public enum MessageType
    {
        Accept,
        Waitting,
        Error
    }
}

