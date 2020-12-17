using Controller;
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

namespace View.Views
{
    /// <summary>
    /// Interaction logic for Artist.xaml
    /// </summary>
    public partial class Artist : UserControl
    {
        public Artist()
        {
            InitializeComponent();
        }

        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Label)sender;

            Controller.TrackQueue.SetQueue((int)x.Tag, 1);

            Model.SingleTrackClicked.TrackID = (int)x.Tag;
            Model.SingleTrackClicked.TrackClicked = true;

            Model.SingleTrackClicked.QueueTrackIDs.Clear();
            Model.SingleTrackClicked.QueueTrackIDs.AddLast((int)x.Tag);
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var label = (Label)sender;
            DataContext = new ViewModels.Artist((int)label.Tag);
        }
    }
}
