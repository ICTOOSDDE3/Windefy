using Controller;
using System.Diagnostics;
using View.ViewModels;
using System;
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
        private Controller.Track track = new Controller.Track();
        Register registerAccount = new Register();
        Login login = new Login();
        private string email = "";
        private int playlistID;
        private bool rewFor;

        public MainWindow()
        {
            DBConnection.Initialize();

            ApacheConnection.Initialize();
            InitializeComponent();
            //DataContext = new Homepage();
            DataContext = new ViewModels.Artist(1);
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;



            // initialize and setup of timer
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void Add_PlayLists_To_Left_Sidebar()
        {
            LeftSideBarPlayLists.ItemsSource = SideBarList.sideBarList.playlists;
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginBackground.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Visible;
        }
        private void Playlist_Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginBackground.Visibility = Visibility.Visible;
            AddPlaylistGrid.Visibility = Visibility.Visible;
        }
        //A new playlist wants to be created
        private void PlaylistDetails_Button_Click(object sender, RoutedEventArgs e)
        {
            string title = Details_Title_Input.Text;
            bool isprivate = (bool)AddPlaylist_Private.IsChecked;
            //Check if title is filled out
            if(title != null)
            {
                Controller.Playlist newPlaylist = new Controller.Playlist();

                newPlaylist.CreateUserPlaylist(title, isprivate);


                //Call controller to make playlist
                LoginBackground.Visibility = Visibility.Hidden;
                AddPlaylistGrid.Visibility = Visibility.Hidden;

                //Load all the playlists from the db
                SideBarList.SetAllPlaylistsFromUser();
                Add_PlayLists_To_Left_Sidebar();

                //Reset the sidebar
                LeftSideBarPlayLists.ItemsSource = null;
                //Load all the playlists into the sidebar
                LeftSideBarPlayLists.ItemsSource = SideBarList.sideBarList.playlists;
            }
            //Give error if no title is filled in
            else
            {
                AddPlaylist_Comment.Visibility = Visibility.Visible;
            }                        
        }
        private void Close_AddPlaylist_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginBackground.Visibility = Visibility.Hidden;
            AddPlaylistGrid.Visibility = Visibility.Hidden;
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
            Updated_Text.Visibility = Visibility.Visible;
        }
        private void AccountDetails_Button_Click(object sender, RoutedEventArgs e)
        {
            string newEmail = Details_Email_Input.Text;
            string newName = Details_Username_Input.Text;

            //update email if different from current email
            if (newEmail != Model.User.Email)
            {
                Controller.User.UpdateEmail(newEmail);
            }
            //Update username if different from current name
            if (newName != Model.User.Name)
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
                if (registerAccount.IsPasswordEqual(password, repeatedPassword))
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
            if (registerAccount.IsVerificationCodeCorrect(Verify_TextBox.Text, email))
            {
                LoginBackground.Visibility = Visibility.Hidden;
                VerifyGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                Verification_Headsup.Content = "Code not valid!";
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
                    //Get all the playlists from the current users into a playlistlistobject
                    SideBarList.SetAllPlaylistsFromUser();

                    Add_PlayLists_To_Left_Sidebar();
                } else
                {
                    email = Model.User.Email.ToString();
                    LoginGrid.Visibility = Visibility.Hidden;
                    VerifyGrid.Visibility = Visibility.Visible;
                    //Get all the playlists from the current users into a playlistlistobject
                    SideBarList.SetAllPlaylistsFromUser();

                    Add_PlayLists_To_Left_Sidebar();
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
            }
            else if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                CurrentTime.Content = mediaPlayer.Position.ToString(@"mm\:ss");
            }
            if (SingleTrackClicked.TrackClicked)
            {
                clickedTrackUpdate();
            }
        }

        private void clickedTrackUpdate()
        {
            SingleTrackClicked.TrackClicked = false;
            UpdateMusicBar(SingleTrackClicked.TrackID);
            tbPlayPause.IsChecked = true;
            mediaPlayer.Play();
        }

        /// <summary>
        /// Loads the next track and plays it if play is toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (SingleTrackClicked.QueueTrackIDs.Count > 0)
            {
                rewFor = true;

                UpdateMusicBar(SingleTrackClicked.QueueTrackIDs.First());

                if (mediaPlaying)
                {
                    mediaPlayer.Play();
                }
            }
            else if (TrackQueue.trackQueue.Count() > 0) { 
                UpdateMusicBar(TrackQueue.Dequeue());

                if (mediaPlaying)
                {
                    mediaPlayer.Play();
                }
            }
            else
            {
                mediaPlayer.Close();
            }
        }
        /// <summary>
        /// Loads previous track and plays it if play is toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TrackHistory.trackHistory.Count > 0)
            {
                rewFor = false;

                SingleTrackClicked.QueueTrackIDs.AddFirst(CurrentTrack.TrackID);

                UpdateMusicBar(TrackHistory.trackHistory.Pop());

                if (mediaPlaying)
                {
                    mediaPlayer.Play();
                }
            }
            else if (TrackQueue.trackQueue.Count() > 0)
            {
                UpdateMusicBar(TrackHistory.trackHistory.Pop());

                if (mediaPlaying)
                {
                    mediaPlayer.Play();
                }
            }
            else
            {
                mediaPlayer.Stop();
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
            else if (SingleTrackClicked.QueueTrackIDs.Count > 0)
            {
                rewFor = true;

                UpdateMusicBar(SingleTrackClicked.QueueTrackIDs.First());

                if (mediaPlaying)
                {
                    mediaPlayer.Play();
                }
            }
            else if (TrackQueue.trackQueue.Count() > 0)
            {
                UpdateMusicBar(TrackQueue.Dequeue());

                if (mediaPlaying)
                {
                    mediaPlayer.Play();
                }
            }
            else
            {
                mediaPlayer.Close();
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
            mediaPlaying = false;
        }
        /// <summary>
        /// Enables rewind to listen to the current song again
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRewind_Checked(object sender, RoutedEventArgs e)
        {
            rewind = true;
        }
        /// <summary>
        /// Disables rewind to stop listening to the same song.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRewind_Unchecked(object sender, RoutedEventArgs e)
        {
            rewind = false;
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataContext = new ViewModels.Artist(2);
        }

        private void favoriteBtn_Checked(object sender, RoutedEventArgs e)
        {
            //controller aanroepen om track toe te voegen aan fav afspeellijst

        }

        private void favoriteBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            //controller aanroepen om track te verwijderen van fav afspeellijst
        }

        private void shuffleBtn_Checked(object sender, RoutedEventArgs e)
        {
            //TrackQueue.ShuffleEnabled = true;
        }

        private void shuffleBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            //TrackQueue.ShuffleEnabled = false;
        }

        /// <summary>
        /// Updates all music related data
        /// </summary>
        /// <param name="trackID"></param>
        private void UpdateMusicBar(int trackID)
        {
            if (CurrentTrack != null && rewFor)
            {
                TrackHistory.trackHistory.Push(CurrentTrack.TrackID);
                if (SingleTrackClicked.QueueTrackIDs.Count > 0)
                {
                    SingleTrackClicked.QueueTrackIDs.RemoveFirst();
                }
            }

            MusicBar.DataContext = track.GetTrack(trackID);
            CurrentTrack = (Model.Track)MusicBar.DataContext;
            icArtistList.ItemsSource = CurrentTrack.Artists;
            TrackImage.Source = new BitmapImage(new Uri(ApacheConnection.GetImageFullPath(CurrentTrack.Image_path), UriKind.RelativeOrAbsolute));
            mediaPlayer.Open(new Uri(ApacheConnection.GetAudioFullPath(CurrentTrack.File_path)));
        }

        private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            string searchBarValue = SearchBar.Text;

            if (searchBarValue.Length > 2)
            {
                string dropDownValue = SearchDropdown.SelectedItem.ToString();

                // If the searchvalue is 3 characters or more, search
                switch (dropDownValue)
                {
                    case "Artist":
                        DataContext = new SearchArtistViewModel(searchBarValue);
                        break;
                    case "Album":
                        DataContext = new SearchAlbumViewModel(searchBarValue, false);
                        break;
                    case "Playlist":
                        DataContext = new SearchAlbumViewModel(searchBarValue, true);
                        break;
                    default:
                        // Track as default
                        DataContext = new SearchSongModel(searchBarValue);
                        break;
                }
            }
            else
            {
                DataContext = new Homepage();
            }
        }
    }
}
