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
    }
}
