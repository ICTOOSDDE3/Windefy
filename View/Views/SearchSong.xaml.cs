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
        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Button)e.OriginalSource;
            var data = x.DataContext as TrackInfo;

            Controller.TrackQueue.SetQueue(data.TrackID, data.PlaylistID);
            
            SingleTrackClicked.TrackID = data.TrackID;
            SingleTrackClicked.TrackClicked = true;
            bool itemData = false;

            SingleTrackClicked.QueueTrackIDs.Clear();
            foreach (var item in items.Items)
            {
                if (item == data)
                {
                    itemData = true;
                }
                if (item != data && itemData)
                {
                    var test = item as ViewModels.TrackInfo;
                    SingleTrackClicked.QueueTrackIDs.AddLast(test.TrackID);
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

            if(artistId != 0)
            {
                ((ViewModels.SearchSongModel)DataContext).OnArtistClick(artistId);
            }

        }
    }
}
