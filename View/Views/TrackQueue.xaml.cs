﻿using Controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Controller.TrackQueue.SetQueue(data.TrackID);
            SingleTrackClicked.TrackID = data.TrackID;
            SingleTrackClicked.TrackClicked = true;
        }
    }
}
