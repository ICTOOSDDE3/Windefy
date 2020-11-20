using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Register : Window
    {
        Controller.Controller_Register ControlRegister = new Controller.Controller_Register();
        public Register()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var loginWindow = new Login();
            loginWindow.ShowDialog();

        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            ControlRegister.RegisterAccount(Email_TextBox.Text, Username_TextBox.Text, Password_TextBox.Text, PasswordRepeat_TextBox.Text);
        }
    }
}
