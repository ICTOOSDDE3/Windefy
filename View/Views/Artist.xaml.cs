﻿using Controller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

            this.Loaded += new RoutedEventHandler(Artist_Loaded);            
        }

        private void Artist_Loaded(object sender, RoutedEventArgs e)
        {
            if (Favourite.IsFavouriteArtist(((ViewModels.Artist)DataContext).CurrentArtist.ArtistID))
            {
                LikeButton.IsChecked = true;
            }
        }


        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Label)sender;

            Controller.TrackQueue.SetQueue((int)x.Tag, 1);

            SingleTrackClicked.TrackID = (int)x.Tag;
            SingleTrackClicked.TrackClicked = true;

            SingleTrackClicked.QueueTrackIDs.Clear();
            SingleTrackClicked.QueueTrackIDs.AddLast((int)x.Tag);
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var label = (Label)sender;
            DataContext = new ViewModels.Artist((int)label.Tag);
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            var addedToFavourites = (ToggleButton)e.OriginalSource;
            int artistID = ((ViewModels.Artist)DataContext).CurrentArtist.ArtistID;

            if ((bool)addedToFavourites.IsChecked)
            {
                if (!Favourite.IsFavouriteArtist(artistID))
                {
                    Favourite.AddFavouriteArtist(artistID);
                }
            }
            else
            {
                Favourite.RemoveFavouriteArtist(artistID);
            }
        }
    }
}
