using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        Controller.AddMusicToPlaylist playlist = new Controller.AddMusicToPlaylist();
        public History()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click on title to play track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Track_Click(object sender, MouseButtonEventArgs e)
        {
            var x = (TextBlock)e.OriginalSource;
            var data = x.DataContext as ViewModels.TrackInfo;

            Controller.TrackQueue.trackQueue.Clear();

            SingleTrackFill(data);

        }

        /// <summary>
        /// fills single track queue in case of history view track click
        /// </summary>
        /// <param name="data"></param>
        private void SingleTrackFill(ViewModels.TrackInfo data)
        {
            Model.SingleTrackClicked.TrackID = data.TrackID;
            Model.SingleTrackClicked.TrackClicked = true;
            Model.SingleTrackClicked.QueueTrackIDs.Clear();

            bool clickedTrack = false;

            foreach (var item in Tracks.Items)
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
    }
}
