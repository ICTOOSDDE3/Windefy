using System.Windows;
using System.Windows.Controls;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SearchArtist.xaml
    /// </summary>
    public partial class SearchArtist : UserControl
    {
        public SearchArtist()
        {
            InitializeComponent();
        }

        private void btnLight_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            int artistId = (int)button.Tag;

            ((ViewModels.SearchArtistViewModel)DataContext).OnArtistClick(artistId);
        }
    }
}
