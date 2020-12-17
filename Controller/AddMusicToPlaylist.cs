using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Controller
{
    public class AddMusicToPlaylist
    {
        //this is the logged in user ID
        //public List<PlaylistPreview> _playlists = new List<PlaylistPreview>();
        public List<PlaylistPreview> Playlists { get; set; } = new List<PlaylistPreview>();

        /// <summary>
        /// puts the users playlist in a list
        /// </summary>
        /// <param name="userID"></param>
        public void ShowPlaylists(int userID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();


            //Build the query
            string query = $"SELECT playlistID, title FROM playlist Where ownerID = {userID}";

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

        /// <summary>
        /// Inserts a track to the selected playlist
        /// </summary>
        /// <param name="playlistID">id from playlist</param>
        /// <param name="trackID">id from track</param>
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
        /// <param name="trackID">id from track</param>
        public void InsertToFavorites(int trackID, int userID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query to get playlistID from favorites
            string favoritesQuery = $"SELECT playlistID FROM playlist where title = 'Favorites' AND ownerID = {userID}";
            SqlCommand cmdFavorites = new SqlCommand(favoritesQuery, DBConnection.Connection);
            int playlistID = Convert.ToInt32(cmdFavorites.ExecuteScalar().ToString());

            //Build the query
            string query = $"INSERT INTO playlist_track (trackID, playlistID, number) VALUES({trackID}, {playlistID}, -1)";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);


            cmd.ExecuteScalar();
            DBConnection.CloseConnection();
        }

        /// <summary>
        /// Checks if a track is already in favorites
        /// </summary>
        /// <param name="trackID"></param>
        /// <returns>true or false</returns>
        public bool IsTrackInFavorites(int trackID, int userID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string favoritesQuery = $"SELECT playlistID FROM playlist where title = 'Favorites' AND ownerID = {userID}";
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
                    return true;
                }
            }
            //DBConnection.CloseConnection();
            return false;
        }

        /// <summary>
        /// Deletes a track from the selected playlist
        /// </summary>
        /// <param name="trackID">id from track</param>
        public void DeleteFromPlaylist(int playlistID, int trackID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string DeleteQuery = $"DELETE FROM playlist_track WHERE playlistID = {playlistID} AND trackID = {trackID}";
            SqlCommand cmdDelete = new SqlCommand(DeleteQuery, DBConnection.Connection);


            cmdDelete.ExecuteNonQuery();
            DBConnection.CloseConnection();
        }

        /// <summary>
        /// Deletes a track from the favorites playlist
        /// </summary>
        /// <param name="trackID">id from track</param>
        public void DeleteFromFavorites(int trackID, int userID)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string favoritesQuery = $"SELECT playlistID FROM playlist where title = 'Favorites' AND ownerID = {userID}";
            SqlCommand cmdFavorites = new SqlCommand(favoritesQuery, DBConnection.Connection);
            int playlistID = Convert.ToInt32(cmdFavorites.ExecuteScalar().ToString());

            //Build the query
            string DeleteQuery = $"DELETE FROM playlist_track WHERE playlistID = {playlistID} AND trackID = {trackID}";
            SqlCommand cmdDelete = new SqlCommand(DeleteQuery, DBConnection.Connection);


            cmdDelete.ExecuteNonQuery();
            DBConnection.CloseConnection();
        }

    }
}
