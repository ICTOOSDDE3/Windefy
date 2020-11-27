using Model;
﻿using Controller;
using System.Diagnostics;
using View.ViewModels;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool userIsDraggingSlider = false;
        private bool mediaPlaying = false;
        private bool rewind = false;
        private Model.Track CurrentTrack;
        private Controller.AudioPath audioPath = new Controller.AudioPath();
        private Controller.Track track = new Controller.Track();
        Register registerAccount = new Register();
        Login login = new Login();
        private string email = "";
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new Homepage();
            MusicBar.DataContext = track.GetTrack(210);
            CurrentTrack = (Model.Track)MusicBar.DataContext;
            //icArtistList.ItemsSource = CurrentTrack.ArtistIDs;

            string strin = CurrentTrack.File_path;
            
            mediaPlayer.Open(new Uri(audioPath.GetAudioPath(strin)));

            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(0);
            timer.Tick += timer_Tick;
            timer.Start();
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

            Details_Email_Input.Text = Model.User.Email;
            Details_Username_Input.Text = Model.User.Name;
            Details_Language_Input.Text = Model.User.GetLanguage();
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
        private void AccountDetails_Button_Click(object sender, RoutedEventArgs e)
        {
            string newEmail = Details_Email_Input.Text;
            string newName = Details_Username_Input.Text;

            //update email if different from current email
            if(newEmail != Model.User.Email)
            {
                Controller.User.UpdateEmail(newEmail);
            }
            //Update username if different from current name
            if (newEmail != Model.User.Name)
            {
                Controller.User.UpdateName(newName);
            }

            Updated_Text.Visibility = Visibility.Visible;
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            email = Email_Input.Text;
            string userName = Username_Input.Text;
            string password = Password_Input.Password;
            string repeatedPassword = PasswordRepeat_Input.Password;
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
            registerAccount.ResendVerificationCode(Model.User.Email.ToString());
            Verification_Headsup.Content = "A verification code has been send to your email adres";
        }

        private void Verify_Button_Click(object sender, RoutedEventArgs e)
        {
            if (registerAccount.IsVerificationCodeCorrect(Verify_TextBox.Text, email)) {
                LoginBackground.Visibility = Visibility.Hidden;
                VerifyGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                Trace.WriteLine("Code not valid!");
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if(login.IsLogin(Email_TextBox.Text, Wachtwoord_TextBox.Password))
            {
                bool verified = Model.User.Verified;
                if (verified)
                {
                    LoginBackground.Visibility = Visibility.Hidden;
                } else
                {
                    email = Model.User.Email.ToString();
                    LoginGrid.Visibility = Visibility.Hidden;
                    VerifyGrid.Visibility = Visibility.Visible;
                }
            } else
            {
                Login_HeadsUp.Content = "Email or password invalid!";
            }
        }
        /// <summary>
        /// Updates time on screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null && !userIsDraggingSlider && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                CurrentTime.Content = mediaPlayer.Position.ToString(@"mm\:ss");
                TotalTime.Content = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                TimeStatus.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                TimeStatus.Value = mediaPlayer.Position.TotalSeconds;

                //TimeStatus.Foreground = Brushes.Red;
            }
            else if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                CurrentTime.Content = mediaPlayer.Position.ToString(@"mm\:ss");
            }
        }
        /// <summary>
        /// Loads the next track and plays it if play is toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            MusicBar.DataContext = track.GetTrack(CurrentTrack.NumberID + 1);
            CurrentTrack = (Model.Track)MusicBar.DataContext;
            icArtistList.ItemsSource = CurrentTrack.ArtistIDs;
            mediaPlayer.Open(new Uri(audioPath.GetAudioPath(CurrentTrack.File_path)));

            if (mediaPlaying)
            {
                mediaPlayer.Play();
            }
        }
        /// <summary>
        /// Loads previous track and plays it if play is toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MusicBar.DataContext = track.GetTrack(CurrentTrack.NumberID - 1);
            CurrentTrack = (Model.Track)MusicBar.DataContext;
            icArtistList.ItemsSource = CurrentTrack.ArtistIDs;

            mediaPlayer.Open(new Uri(audioPath.GetAudioPath(CurrentTrack.File_path)));

            if (mediaPlaying)
            {
                mediaPlayer.Play();
            }
        }
        /// <summary>
        /// Stops the current track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();

            if (mediaPlaying)
            {
                mediaPlayer.Play();
            }
        }
        /// <summary>
        /// Change the volume track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = Volume.Value / 100;
        }

        /// <summary>
        /// Plays next track when media is ended. If rewind is true, stops and plays the current track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (rewind)
            {
                mediaPlayer.Stop();
                mediaPlayer.Play();
            }
            else
            {
                MusicBar.DataContext = track.GetTrack(CurrentTrack.NumberID + 1);
                CurrentTrack = (Model.Track)MusicBar.DataContext;
                icArtistList.ItemsSource = CurrentTrack.ArtistIDs;
                mediaPlayer.Open(new Uri(audioPath.GetAudioPath(CurrentTrack.File_path)));

                mediaPlayer.Play();
            }
        }

        /// <summary>
        /// Sets new position of track and set the drag boolean to completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeStatus_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(TimeStatus.Value);
        }
        /// <summary>
        /// Starts drag boolean
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeStatus_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }
        /// <summary>
        /// Plays current track and changes play/pause button to pause
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TogglePlay(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
            tbPlayPause.Content = "Pause";
            mediaPlaying = true;
        }
        /// <summary>
        /// Pauses current track and changes play/button to play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TogglePause(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
            tbPlayPause.Content = "Play";
            mediaPlaying = false;
        }

        private void btnRewind_Checked(object sender, RoutedEventArgs e)
        {
            rewind = true;
            //Volgend nummer wordt het huidige nummer
        }

        private void btnRewind_Unchecked(object sender, RoutedEventArgs e)
        {
            rewind = false;
        }
    }
}