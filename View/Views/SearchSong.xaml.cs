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
        public SearchSong()
        {
            InitializeComponent();
            
        }
        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Button)e.OriginalSource;
            var data = x.DataContext as ViewModels.TrackInfo;

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
            if (addTrackToPlaylist.FavoritesContainsTrack(trackid))
            {
                addTrackToPlaylist.InsertToFavorites(trackid);
                Trace.WriteLine("added to playlist");
            } else
            {
                Trace.WriteLine("already added to favorites");
            }       
        }
    }
}
