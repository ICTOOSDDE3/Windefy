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
        private List<int> _playlistId = new List<string>(); 
        private int playlistID;

        //checks if there are any playlists of the user
        public void CheckIfUserHasPlaylists()
        {
            if (Convert.ToBoolean(ShowPlaylists(_userID)))
            {
                // show all playlists as a button
            }
            else
            {
                // show make playlist button
            }
        }

        // shows playlist that the user has made
        public List<string> ShowPlaylists(int userID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();


            //Build the query
            string query = $"SELECT playlistID FROM playlist Where ownerID = {userID}";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            string titles = cmd.CommandText;
            playlistID = 

            cmd.ExecuteScalar();
            DBConnection.CloseConnection();

            return titles;
        }

        public bool InsertToPlaylist(int playlistID, int trackID)
        {

        }
        //on click playlist name insert song
        //notify if inserted or not

    }
}
