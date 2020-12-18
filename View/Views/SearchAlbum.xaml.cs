using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for SearchAlbum.xaml
    /// </summary>
    public partial class SearchAlbum : UserControl
    {
        public SearchAlbum()
        {
            InitializeComponent();
        }

        private void AlbumClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            ((ViewModels.SearchAlbumViewModel)DataContext).OnAlbumClick(((ViewModels.PlaylistInfo)button.DataContext).PlaylistID);
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            var addedToFavourites = (ToggleButton)e.OriginalSource;
            int playlistID = ((ViewModels.PlaylistInfo)addedToFavourites.DataContext).PlaylistID;

            if ((bool)addedToFavourites.IsChecked)
            {
                if (!Favourite.IsFavouritePlaylist(playlistID))
                {
                    Favourite.AddFavouritePlaylist(playlistID);
                }
            }
            else
            {
                Favourite.RemoveFavouritePlaylist(playlistID);
            }
        }

        private void LikeButton_Loaded(object sender, RoutedEventArgs e)
        {
            var toggleButton = (ToggleButton)e.OriginalSource;
            var data = toggleButton.DataContext as ViewModels.PlaylistInfo;

            toggleButton.IsChecked = data.Liked;
        }
    }
}
