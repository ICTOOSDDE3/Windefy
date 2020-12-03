using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Model;

namespace Controller
{
    public class AddMusicToPlaylist
    {
        //this is the logged in user ID
        private int _userID = Model.User.UserID;
        private List<PlaylistPreview> _playlists = new List<PlaylistPreview>();
        private bool _isPlaylistMade = false;

        //checks if there are any playlists of the user
        public bool CheckIfUserHasPlaylists()
        {
            if (_playlists.Count > 0)
            {
                return _isPlaylistMade = true;
            }
            return _isPlaylistMade;
        }

        // shows playlist that the user has made
        public void ShowPlaylists(int userID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();


            //Build the query
            string query = $"SELECT playlistID, title FROM playlist Where ownerID = {_userID}";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int playlistId = Convert.ToInt32(dataReader["playlistID"]);
                string playlistTitle = dataReader["title"].ToString();
                _playlists.Add(new PlaylistPreview(playlistId, playlistTitle));

            }
            
            DBConnection.CloseConnection();

        }

        public void InsertToPlaylist(int playlistID, int trackID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string query = $"INSERT INTO playlist_track (trackID, playlistID, number) VALUES({trackID}, {playlistID}, -1)";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            cmd.ExecuteScalar();
            DBConnection.CloseConnection();

        }
        //on click playlist name insert song
        //notify if inserted or not

    }
}
