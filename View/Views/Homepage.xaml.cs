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
            var x = (Button)e.OriginalSource;
            var data = x.DataContext as ViewModels.TrackInfo;

            TrackQueue.SetQueue(data.TrackID, data.PlaylistID);

            SingleTrackClicked.TrackID = data.TrackID;
            SingleTrackClicked.TrackClicked = true;
            SingleTrackClicked.QueueTrackIDs.Clear();

            bool clickedTrack = false;

            foreach (var item in tracks.Items)
            {
                var test = item as ViewModels.TrackInfo;
                if (item == data)
                {
                    clickedTrack = true;
                }
                if (item != data && clickedTrack)
                {
                    SingleTrackClicked.QueueTrackIDs.AddLast(test.TrackID);
                }
                else if (!clickedTrack)
                {
                    SingleTrackClicked.HistoryTrackIDs.Push(test.TrackID);
                }
            }
        }
    }
}
