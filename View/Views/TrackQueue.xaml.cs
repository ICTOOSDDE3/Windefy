using System.Windows;
using System.Windows.Controls;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for TrackQueue.xaml
    /// </summary>
    public partial class TrackQueue : UserControl
    {
        public TrackQueue()
        {
            InitializeComponent();
        }
        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Button)e.OriginalSource;
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

            foreach (Model.Track item in trackQueue.Items)
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
