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
        /// <summary>
        /// Click on title to play track
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Track_Click(object sender, RoutedEventArgs e)
        {
            var x = (Button)e.OriginalSource;
            var data = x.DataContext as ViewModels.TrackInfo;

            Controller.TrackQueue.SetQueue(data.TrackID, data.PlaylistID);

            SingleTrackFill(data);
        }

        private void SingleTrackFill(ViewModels.TrackInfo data)
        {
            Model.SingleTrackClicked.TrackID = data.TrackID;
            Model.SingleTrackClicked.TrackClicked = true;
            bool clickedTrack = false;
            Model.SingleTrackClicked.QueueTrackIDs.Clear();

            foreach (ViewModels.TrackInfo item in items.Items)
            {
                if (item == data)
                {
                    clickedTrack = true;
                }
                if (item != data && clickedTrack)
                {
                    Model.SingleTrackClicked.QueueTrackIDs.AddLast(item.TrackID);
                }
                else if (!clickedTrack)
                {
                    Model.SingleTrackClicked.HistoryTrackIDs.Push(item.TrackID);
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
