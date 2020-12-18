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
            if (Favourite.IsFavouritePlaylist(((ViewModels.PlaylistViewModel)DataContext).PlaylistID))
            {
                LikeButton.IsChecked = true;
            }
        }

        /// <summary>
        /// removes track from the playlist you are on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            DataContext = new ViewModels.PlaylistViewModel(playlistid);
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

            if ((bool)addedToFavourites.IsChecked)
            {
                if (!Favourite.IsFavouritePlaylist(playlistID))
                {
                    Favourite.AddFavouritePlaylist(playlistID);
                }
            }
            else
            {
                Favourite.RemoveFavouritePlaylist(playlistID);
            }
        }
    }
}
