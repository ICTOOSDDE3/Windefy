using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Controller
{
    public class AddMusicToPlaylist
    {
        //this is the logged in user ID
        private int _userID = Model.User.UserID;
        //public List<PlaylistPreview> _playlists = new List<PlaylistPreview>();
        public List<PlaylistPreview> Playlists { get; set; } = new List<PlaylistPreview>();

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
                Playlists.Add(new PlaylistPreview(playlistId, playlistTitle));

            }
        }

        public void InsertToPlaylist(int playlistID, int trackID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            // checks if track is already in selected playlist
            string favoritesQuery = $"SELECT * FROM playlist_track where playlistID = {playlistID} AND trackID = {trackID}";
            SqlCommand cmdInPlaylist = new SqlCommand(favoritesQuery, DBConnection.Connection);
            var contains = cmdInPlaylist.ExecuteScalar();

            if (contains == null)
            {
                //Build the query
                string query = $"INSERT INTO playlist_track (trackID, playlistID, number) VALUES({trackID}, {playlistID}, -1)";

                //Prepare the query
                SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

                cmd.ExecuteScalar();
                DBConnection.CloseConnection();
            } else
            {
                Console.WriteLine("already contains track");
                DBConnection.CloseConnection();
            }
        }

        /// <summary>
        /// inserts a track in the favorites playlist of an user
        /// </summary>
        /// <param name="trackID">is the id from a track that has to be added</param>
        public void InsertToFavorites(int trackID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string favoritesQuery = $"SELECT playlistID FROM playlist where title = 'Favorites' AND ownerID = {_userID}";
            SqlCommand cmdFavorites = new SqlCommand(favoritesQuery, DBConnection.Connection);
            int playlistID = Convert.ToInt32(cmdFavorites.ExecuteScalar().ToString());

            //Build the query
            string query = $"INSERT INTO playlist_track (trackID, playlistID, number) VALUES({trackID}, {playlistID}, -1)";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);


            cmd.ExecuteScalar();
            DBConnection.CloseConnection();
        }

        public bool FavoritesContainsTrack(int trackID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string favoritesQuery = $"SELECT playlistID FROM playlist where title = 'Favorites' AND ownerID = {_userID}";
            SqlCommand cmdFavorites = new SqlCommand(favoritesQuery, DBConnection.Connection);
            int playlistID = Convert.ToInt32(cmdFavorites.ExecuteScalar().ToString());

            //Build the query
            string query = $"SELECT * FROM playlist_track where playlistID = '{playlistID}' AND trackID = {trackID}";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                if (Convert.ToInt32(dataReader["trackID"]) == trackID)
                {
                    //DBConnection.CloseConnection();
                    return false;
                }
            }
            //DBConnection.CloseConnection();
            return true;
        }
        //on click playlist name insert song
        //notify if inserted or not

        public void DeleteFromFavorites(int trackID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string favoritesQuery = $"SELECT playlistID FROM playlist where title = 'Favorites' AND ownerID = {_userID}";
            SqlCommand cmdFavorites = new SqlCommand(favoritesQuery, DBConnection.Connection);
            int playlistID = Convert.ToInt32(cmdFavorites.ExecuteScalar().ToString());

            //Build the query
            string DeleteQuery = $"DELETE FROM playlist_track WHERE playlistID = {playlistID} AND trackID = {trackID}";
            SqlCommand cmdDelete = new SqlCommand(favoritesQuery, DBConnection.Connection);


            cmdDelete.ExecuteNonQuery();
            DBConnection.CloseConnection();
        }

    }
}
