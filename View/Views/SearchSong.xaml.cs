using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SearchSong.xaml
    /// </summary>
    public partial class SearchSong : UserControl
    {
        public SearchSong()
        {
            InitializeComponent();
            
        }
        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Button)e.OriginalSource;
            var data = x.DataContext as ViewModels.TrackInfo;
            TrackQueue.SetQueue(data.TrackID, data.PlaylistID);
            TrackClicked.TrackID = data.TrackID;
            bool itemData = false;

            TrackClicked.QueueTrackIDs.Clear();
            foreach (var item in items.Items)
            {
                if (item == data)
                {
                    itemData = true;
                }
                if (item != data && itemData)
                {
                    var test = item as ViewModels.TrackInfo;
                    TrackClicked.QueueTrackIDs.AddLast(test.TrackID);
                }
            }


        }
    }
}
