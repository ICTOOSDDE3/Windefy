using System;
using System.Collections.Generic;
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
using View.ViewModels;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : UserControl
    {
        public Homepage()
        {
            InitializeComponent();
        }

        private void Track_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            TrackInfo trackInfo = button.DataContext as TrackInfo;

            TrackQueue.SetQueue(trackInfo.TrackID, trackInfo.PlaylistID);

            SingleTrackFill(trackInfo);
        }

        private void SingleTrackFill(TrackInfo trackInfo)
        {
            TrackQueue.trackQueue.Clear();
            SingleTrackClicked.TrackID = trackInfo.TrackID;
            SingleTrackClicked.TrackClicked = true;
            SingleTrackClicked.QueueTrackIDs.Clear();

            bool clickedTrack = false;

            foreach (TrackInfo item in tracks.Items)
            {
                if (item == trackInfo)
                {
                    clickedTrack = true;
                }
                if (item != trackInfo && clickedTrack)
                {
                    SingleTrackClicked.QueueTrackIDs.AddLast(item.TrackID);
                }
                else if (!clickedTrack)
                {
                    SingleTrackClicked.HistoryTrackIDs.Push(item.TrackID);
                }
            }
        }
    }
}
