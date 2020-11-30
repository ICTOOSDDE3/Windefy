using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    class Playlist
    {
        public void createUserPlaylist(string playlistTitle, bool playlist_is_Public)
        {
            Model.Playlist NewPlaylist = new Model.Playlist();

            //Initialize and open a db connection
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            int PublicPlaylist = 0;

            if(playlist_is_Public == true) PublicPlaylist = 1; 

            //Build the query
            string query = $"INSERT INTO playlist (title, listens, playlist_typeID, is_public, ownerID) VALUES ('{playlistTitle}', 0, 5, {PublicPlaylist}, {Model.User.UserID})";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            cmd.ExecuteScalar();

            //NewPlaylist.createUserPlaylist(cmd, playlistTitle, playlist_is_Public, Model.User.UserID);

            DBConnection.CloseConnection();
        }
    }
}
