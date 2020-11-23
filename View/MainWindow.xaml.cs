
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
        Register registerAccount = new Register();
        Login login = new Login();
        private string email = "";
        public MainWindow()
        {
            InitializeComponent();
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

            //Email.Text = User.Email;
            //Username.Text = User.Name;
            //Language.Text = User.GetLanguage();
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
            email = Email_Input.Text;
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
        }

        private void Resend_Code_Button(object sender, RoutedEventArgs e)
        {
            //registerAccount.ResendVerificationCode();
        }

        private void Verify_Button_Click(object sender, RoutedEventArgs e)
        {
            if (registerAccount.IsVerificationCodeCorrect(Verify_TextBox.Text, email)) {
                LoginBackground.Visibility = Visibility.Hidden;
            }
            else
            {
                Trace.WriteLine("Code not valid!");
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if(login.IsLogin(Email_TextBox.Text, Wachtwoord_TextBox.Text))
            {
                LoginBackground.Visibility = Visibility.Hidden;
            } else
            {
                Login_HeadsUp.Content = "Email or password invalid!";
            }
        }
    }
}
