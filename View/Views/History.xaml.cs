﻿using System.Windows.Controls;
using System.Windows.Input;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
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
    }
}