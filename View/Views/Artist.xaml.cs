using Controller;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

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
            // If the artist is favourites, fill in the heart button upon load
            if (Favourite.IsFavouriteArtist(((ViewModels.Artist)DataContext).CurrentArtist.ArtistID))
            {
                LikeButton.IsChecked = true;
            }
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

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            var addedToFavourites = (ToggleButton)e.OriginalSource;
            int artistID = ((ViewModels.Artist)DataContext).CurrentArtist.ArtistID;

            // Add / remove artist from favourites depending on the state of the button
            if ((bool)addedToFavourites.IsChecked)
            {
                Favourite.AddFavouriteArtist(artistID);
            }
            else
            {
                Favourite.RemoveFavouriteArtist(artistID);
            }
        }
    }
}
