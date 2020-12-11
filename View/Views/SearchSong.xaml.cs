using Controller;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SearchSong.xaml
    /// </summary>
    public partial class SearchSong : UserControl
    {
        AddMusicToPlaylist addTrackToPlaylist = new AddMusicToPlaylist();
        public SearchSong()
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
            bool itemData = false;

            SingleTrackClicked.QueueTrackIDs.Clear();
            foreach (var item in items.Items)
            {
                if (item == data)
                {
                    itemData = true;
                }
                if (item != data && itemData)
                {
                    var test = item as ViewModels.TrackInfo;
                    SingleTrackClicked.QueueTrackIDs.AddLast(test.TrackID);
                }
            }
         }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            var addToPlaylist = (ToggleButton)e.OriginalSource;
            var data = addToPlaylist.DataContext as ViewModels.TrackInfo;
            int trackid = data.TrackID;
            if (addTrackToPlaylist.FavoritesContainsTrack(trackid))
            {
                addTrackToPlaylist.InsertToFavorites(trackid);
                Trace.WriteLine("added to playlist");
            } else
            {
                Trace.WriteLine("already added to favorites");
            }       
        }

        private void Label_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var label = (Label)sender;

            string artistName = (string)label.Content;


            SqlConnection con = new SqlConnection($"Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = {Passwords.GetPassword("DB")};");
            con.Open();

            // Fetch all artists that worked on a track based on the ID of the track
            SqlCommand cmd = new SqlCommand(null, con)
            {
                CommandText = "SELECT artistID " +
                "FROM artist " +
                $"WHERE name = '{artistName}'"
            };

            SqlDataReader dataReader = cmd.ExecuteReader();
            int artistId = 0;
            while (dataReader.Read())
            {
                artistId = (int)dataReader["artistID"];
            }

            dataReader.Close();
            DBConnection.CloseConnection();

            if(artistId != 0)
            {
                ((ViewModels.SearchSongModel)DataContext).OnArtistClick(artistId);
            }

        }
    }
}
