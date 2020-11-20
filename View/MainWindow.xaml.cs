using Controller;
using System.Diagnostics;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller_Register registerAccount = new Controller_Register();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginBackground.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            LoginGrid.Visibility = Visibility.Hidden;
            RegisterGrid.Visibility = Visibility.Visible;
        }

        private void Login_Button_Register(object sender, RoutedEventArgs e)
        {
            RegisterGrid.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void Close_Login_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginBackground.Visibility = Visibility.Hidden;
            LoginGrid.Visibility = Visibility.Visible;
            RegisterGrid.Visibility = Visibility.Hidden;
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            string email = Email_Input.Text;
            if (registerAccount.IsValidEmail(email))
            {
                string userName = Username_Input.Text;
                string password = Password_Input.Text;
                string repeatedPassword = PasswordRepeat_Input.Text;
                registerAccount.RegisterAccount(email, userName, password, repeatedPassword);
            }
            Trace.WriteLine(registerAccount.IsValidEmail(email));
        }
    }
}
