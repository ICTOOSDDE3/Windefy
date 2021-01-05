using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace View.ViewModels
{
    public class SearchSongModel
    {
        public event EventHandler<int> ArtistClickEvent;
        public List<TrackInfo> items { get; set; }

        private string _NoResultsVisibility;

        public string NoResultsVisibility
        {
            get { return _NoResultsVisibility; }
            set { _NoResultsVisibility = value; }
        }

        public void OnArtistClick(int artistId)
        {
            ArtistClickEvent?.Invoke(this, artistId);
        }

        public SearchSongModel(string q)
        {
            NoResultsVisibility = "Hidden";
            ApacheConnection.Initialize();
            DBConnection.OpenConnection();

            // Initialize or empty the items
            items = new List<TrackInfo>();



            // Fetch all tracks
            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                // Select needed variables from track, limiting to 50 results
                CommandText = "SELECT title, duration, image_path, track.trackID, playlistID " +
                "FROM track " +
                "JOIN playlist_track ON track.trackID = playlist_track.trackID " +
                "WHERE title LIKE '%' + @que + '%' " +
                "AND playlistID IN (" +
                "   SELECT playlistID FROM playlist " +
                "   WHERE playlist_typeID != 5" +
                ") " +
                "ORDER BY track.trackID " +
                "OFFSET 0 ROWS " +
                "FETCH NEXT 50 ROWS ONLY"
            };

            SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255)
            {
                Value = q
            };

            cmd.Parameters.Add(que);
            cmd.Prepare();

            AddMusicToPlaylist addMusicToPlaylist = new AddMusicToPlaylist();

            // Fetch all rows based on the search query and put them in items
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                TrackInfo trackInfo = new TrackInfo(Convert.ToString(dataReader["title"]),
                    Convert.ToInt32(dataReader["duration"]),
                    Convert.ToString(dataReader["image_path"]),

                    Convert.ToInt32(dataReader["trackID"]),
                    Convert.ToInt32(dataReader["playlistID"]),
                    addMusicToPlaylist.IsTrackInFavorites(Convert.ToInt32(dataReader["trackID"]), Model.User.UserID));

                items.Add(trackInfo);
            }

            dataReader.Close();
            DBConnection.CloseConnection();

            if (items.Count > 0)
            {
                NoResultsVisibility = "Hidden";
            }
            else
            {
                NoResultsVisibility = "Visible";
            }
        }
    }

    // Data template for tracksinfo for the search screen
    public class TrackInfo
    {
        public AddMusicToPlaylist a1 = new AddMusicToPlaylist();
        private int userID = Model.User.UserID;
        public int TrackID { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string ImagePath { get; set; }
        public List<Model.Artist> Artists { get; set; } = new List<Model.Artist>();
        public int PlaylistID { get; set; }
        public Dictionary<int, string> playlists { get; set; } = new Dictionary<int, string>();
        public bool Liked { get; set; }

        public TrackInfo(string T, int D, string I, int ID, int P_ID, bool liked)
        {
            a1.ShowPlaylists(Model.User.UserID);
            foreach (var item in a1.Playlists)
            {
                if(!item.PlaylistTitle.Equals("Favorites")) playlists.Add(item.PlaylistID,item.PlaylistTitle);
            }


            TrackID = ID;
            string seconds = (D % 60).ToString();
            if (seconds.Length == 1)
            {
                seconds = "0" + seconds;
            }

            Title = T;
            Duration = $"{ Math.Floor(Convert.ToDouble(D) / 60)}:{seconds}";
            ImagePath = $"{ApacheConnection.GetImageFullPath(I)}";
            PlaylistID = P_ID;

            Liked = liked;
    
            SqlConnection con = new SqlConnection($"Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = {Passwords.GetPassword("DB")};");
            con.Open();

            // Fetch all artists that worked on a track based on the ID of the track
            SqlCommand cmd = new SqlCommand(null, con)
            {
                CommandText = "SELECT name, artistID " +
                "FROM artist " +
                "WHERE artistID IN (" +
                "   SELECT artistID " +
                "   FROM track_artist " +
                $"   WHERE trackID = {ID} " +
                ")"
            };

            // Put all of the artists into a string
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                Model.Artist artist = new Model.Artist();
                artist.Name = dataReader["name"].ToString();
                artist.ArtistID = (int)dataReader["artistID"];
                Artists.Add(artist);
            }

            dataReader.Close();
            con.Close();
            con.Dispose();
        }

    }
}
