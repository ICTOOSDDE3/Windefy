using Controller;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SearchSong.xaml
    /// </summary>
    public partial class SearchSong : UserControl
    {
        AddMusicToPlaylist addTrackToPlaylist = new AddMusicToPlaylist();
        public int userID = Model.User.UserID;
        public SearchSong()
        {
            InitializeComponent();
            
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            var addToPlaylist = (ToggleButton)e.OriginalSource;
            var data = addToPlaylist.DataContext as ViewModels.TrackInfo;
            int trackid = data.trackID;
            if (addTrackToPlaylist.FavoritesContainsTrack(trackid))
            {
                addTrackToPlaylist.InsertToFavorites(trackid);
                Trace.WriteLine("added to playlist");
            } else
            {
                Trace.WriteLine("already added to favorites");
            }
                
        }

        private void PlaylistStackPanel(object sender, RoutedEventArgs e)
        {
            var playlistStackPanel = (StackPanel)e.OriginalSource;
        }
    }
}
