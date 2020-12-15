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

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var context = (ViewModels.PlaylistViewModel)DataContext;
            var playlistid = context.PlaylistID;
            var button = (Button)e.OriginalSource;
            var trackInfo = button.DataContext as Model.Track;
            var trackId = trackInfo.TrackID;
            Trace.WriteLine(playlistid);
            Trace.WriteLine(trackId);
            playlist.DeleteFromPlaylist(playlistid, trackId);
            DataContext = new ViewModels.PlaylistViewModel(playlistid);
        }
    }
}
