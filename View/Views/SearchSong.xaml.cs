using Controller;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using View.ViewModels;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SearchSong.xaml
    /// </summary>
    public partial class SearchSong : UserControl
    {
        AddMusicToPlaylist addTrackToPlaylist = new AddMusicToPlaylist();

        public SearchSong()
        {
            InitializeComponent();

        }
        /// <summary>
        /// Click on title to play track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Button)e.OriginalSource;
            var data = x.DataContext as TrackInfo;

            Controller.TrackQueue.SetQueue(data.TrackID, data.PlaylistID);

            SingleTrackFill(data);
        }
        /// <summary>
        /// fills single track queue in case of single track click after search
        /// </summary>
        /// <param name="data"></param>
        private void SingleTrackFill(ViewModels.TrackInfo data)
        {
            Model.SingleTrackClicked.TrackID = data.TrackID;
            Model.SingleTrackClicked.TrackClicked = true;
            bool clickedTrack = false;
            Model.SingleTrackClicked.QueueTrackIDs.Clear();

            foreach (ViewModels.TrackInfo item in items.Items)
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

        
        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            var addToPlaylist = (ToggleButton)e.OriginalSource;
            var data = addToPlaylist.DataContext as ViewModels.TrackInfo;
            int trackid = data.TrackID;
            if (addToPlaylist.IsChecked == true)
            {
                if (!addTrackToPlaylist.IsTrackInFavorites(trackid, Model.User.UserID))
                {
                    addTrackToPlaylist.InsertToFavorites(trackid, Model.User.UserID);
                    Trace.WriteLine("added to playlist");
                }
                else
                {
                    Trace.WriteLine("already added to favorites");
                }
            }
            else if (addToPlaylist.IsChecked == false)
            {
                addTrackToPlaylist.DeleteFromFavorites(trackid, Model.User.UserID);
                Trace.WriteLine("Deleted");
            }
        }


        private void LoadedEvent_Combobox(object sender, SelectionChangedEventArgs e)
        {
            var itemCombobox = (ComboBox)e.OriginalSource;
            var trackInfo = itemCombobox.DataContext as TrackInfo;
            var info = itemCombobox.SelectedItem;
            foreach (var item in trackInfo.playlists)
            {
                if (item.Equals(info))
                {
                    addTrackToPlaylist.InsertToPlaylist(item.Key, trackInfo.TrackID);
                }
            }
        }

        private void LikeButton_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton)e.OriginalSource;
            var data = toggleButton.DataContext as ViewModels.TrackInfo;

            toggleButton.IsChecked = data.Liked;
        }

        private void Label_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var textBlock = (TextBlock)sender;

            int artistId = (int)textBlock.Tag;

            if (artistId != 0)
            {
                ((ViewModels.SearchSongModel)DataContext).OnArtistClick(artistId);
            }

        }
    }
}
