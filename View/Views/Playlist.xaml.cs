using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
using Controller;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for Playlist.xaml
    /// </summary>
    public partial class Playlist : UserControl
    {
        AddMusicToPlaylist playlist = new AddMusicToPlaylist();
        public Playlist()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Artist_Loaded);
        }

        private void Artist_Loaded(object sender, RoutedEventArgs e)
        {
            // Fill in the heart button if the playlist is within favourites
            if (Favourite.IsFavouritePlaylist(((ViewModels.PlaylistViewModel)DataContext).PlaylistID))
            {
                LikeButton.IsChecked = true;
            }
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            // gets the current playlistID
            var context = (ViewModels.PlaylistViewModel)DataContext;
            var playlistid = context.PlaylistID;

            //gets the track id that should be removed
            var button = (Button)e.OriginalSource;
            var trackInfo = button.DataContext as Model.Track;
            var trackId = trackInfo.TrackID;

            playlist.DeleteFromPlaylist(playlistid, trackId);

            //refreshes view
            App.Current.MainWindow.DataContext = new ViewModels.PlaylistViewModel(playlistid);


        }
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            var addToPlaylist = (ToggleButton)e.OriginalSource;
            var data = addToPlaylist.DataContext as Model.Track;
            int trackid = data.TrackID;
            if (addToPlaylist.IsChecked == true)
            {

                if (!playlist.IsTrackInFavorites(trackid, Model.User.UserID))
                {
                    playlist.InsertToFavorites(trackid, Model.User.UserID);
                    Trace.WriteLine("added to playlist");
                }
                else
                {
                    Trace.WriteLine("already added to favorites");
                }
            }
            else if (addToPlaylist.IsChecked == false)
            {
                playlist.DeleteFromFavorites(trackid, Model.User.UserID);
                Trace.WriteLine("Deleted");
            }
        }
        private void LikeButton_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton)e.OriginalSource;
            var data = toggleButton.DataContext as Model.Track;

            //toggleButton.IsChecked = data.isSongLiked((int)toggleButton.Tag);
            toggleButton.IsChecked = data.Liked;
        }

        private void LikeButtonPlaylist_Click(object sender, RoutedEventArgs e)
        {
            var addedToFavourites = (ToggleButton)e.OriginalSource;
            int playlistID = ((ViewModels.PlaylistViewModel)DataContext).PlaylistID;
            
            // Add or remove the playlist from favourites depending on the current state of the button
            if ((bool)addedToFavourites.IsChecked)
            {
                Favourite.AddFavouritePlaylist(playlistID);
            }
            else
            {
                Favourite.RemoveFavouritePlaylist(playlistID);
            }
        }

        /// <summary>
        /// Click on title to play track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Track_Click(object sender, MouseButtonEventArgs e)
        {
            var x = (TextBlock)e.OriginalSource;
            var data = x.DataContext as Model.Track;

            Controller.TrackQueue.trackQueue.Clear();
            SingleTrackFill(data);
        }

        /// <summary>
        /// fills single track queue in case of single track click after search
        /// </summary>
        /// <param name="data"></param>
        private void SingleTrackFill(Model.Track data)
        {
            Model.SingleTrackClicked.TrackID = data.TrackID;
            Model.SingleTrackClicked.TrackClicked = true;
            bool clickedTrack = false;
            Model.SingleTrackClicked.QueueTrackIDs.Clear();

            foreach (Model.Track item in Tracks.Items)
            {
                if (item == data)
                {
                    clickedTrack = true;
                }
                if (item != data && clickedTrack)
                {
                    Model.SingleTrackClicked.QueueTrackIDs.AddLast(item.TrackID);
                }
                else if (!clickedTrack)
                {
                    Model.SingleTrackClicked.HistoryTrackIDs.Push(item.TrackID);
                }
            }
        }
    }
}
