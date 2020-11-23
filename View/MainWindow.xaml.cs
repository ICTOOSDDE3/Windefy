
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using Controller;
using System.Diagnostics;
using System.Windows;
using View.ViewModels;

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
            User.Name = "Pietje";
            User.Email = "Pietje@gmail.com";
            User.Language = 1;
            DataContext = new Homepage();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginBackground.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void Account_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginBackground.Visibility = Visibility.Visible;
            AccountDetailsGrid.Visibility = Visibility.Visible;

            Email.Text = User.Email;
            Username.Text = User.Name;
            Language.Text = User.GetLanguage();
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
        private void Close_Account_Details_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginBackground.Visibility = Visibility.Hidden;
            AccountDetailsGrid.Visibility = Visibility.Hidden;
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            string email = Email_Input.Text;
            string userName = Username_Input.Text;
            string password = Password_Input.Text;
            string repeatedPassword = PasswordRepeat_Input.Text;
            if (registerAccount.IsValidEmail(email))
            {
                if (registerAccount.ArePasswordsEqual(password, repeatedPassword))
                {
                    registerAccount.RegisterAccount(email, userName, password, repeatedPassword);
                    RegisterGrid.Visibility = Visibility.Hidden;
                    VerifyGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    Register_Headsup.Content = "Passwords do not match!";
                }
            }
            else
            {
                Register_Headsup.Content = "Email adres is invalid";
            }
            Trace.WriteLine(registerAccount.IsValidEmail(email));
        }
    }
}
