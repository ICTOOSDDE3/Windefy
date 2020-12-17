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
        /// <summary>
        /// Click on title to play track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Button)e.OriginalSource;
            var data = x.DataContext as ViewModels.TrackInfo;

            TrackQueue.SetQueue(data.TrackID, data.PlaylistID);

            SingleTrackFill(data);
        }

        private void SingleTrackFill(ViewModels.TrackInfo data)
        {
            Model.SingleTrackClicked.TrackID = data.TrackID;
            Model.SingleTrackClicked.TrackClicked = true;
            bool clickedTrack = false;
            Model.SingleTrackClicked.QueueTrackIDs.Clear();

            foreach (var item in items.Items)
            {
                var test = item as ViewModels.TrackInfo;
                if (item == data)
                {
                    clickedTrack = true;
                }
                if (item != data && clickedTrack)
                {
                    Model.SingleTrackClicked.QueueTrackIDs.AddLast(test.TrackID);
                }
                else if (!clickedTrack)
                {
                    Model.SingleTrackClicked.HistoryTrackIDs.Push(test.TrackID);
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
