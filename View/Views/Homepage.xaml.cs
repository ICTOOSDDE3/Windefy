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
        /// <summary>
        /// Click on title or image to play track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Track_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            TrackInfo trackInfo = button.DataContext as TrackInfo;

            Controller.TrackQueue.trackQueue.Clear();

            SingleTrackFill(trackInfo);
        }
        /// <summary>
        /// fills single track queue in case of homepage history track click
        /// </summary>
        /// <param name="data"></param>
        private void SingleTrackFill(TrackInfo trackInfo)
        {
            Controller.TrackQueue.trackQueue.Clear();
            Model.SingleTrackClicked.TrackID = trackInfo.TrackID;
            Model.SingleTrackClicked.TrackClicked = true;
            Model.SingleTrackClicked.QueueTrackIDs.Clear();

            bool clickedTrack = false;

            foreach (TrackInfo item in tracks.Items)
            {
                if (item == trackInfo)
                {
                    clickedTrack = true;
                }
                if (item != trackInfo && clickedTrack)
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
