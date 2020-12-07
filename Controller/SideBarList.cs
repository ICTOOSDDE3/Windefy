using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public static class SideBarList
    {
        public static PlaylistList sideBarList = new PlaylistList();
        public static void SetAllPlaylistsFromUser()
        {           
            string query = $"SELECT * FROM playlist WHERE ownerID = {Model.User.UserID}";

            DBConnection.OpenConnection();
            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            sideBarList.playlists.Clear();
            while (dataReader.Read())
            {
                sideBarList.AddPlaylistToList(new Model.Playlist(Convert.ToInt32(dataReader["playlistID"]), Convert.ToString(dataReader["title"]), Convert.ToDateTime(dataReader["release_date"]), Convert.ToInt32(dataReader["listens"]), Convert.ToInt32(dataReader["playlist_typeID"]), Convert.ToString(dataReader["information"]), Convert.ToBoolean(dataReader["is_public"]), Convert.ToInt32(dataReader["ownerID"])));
            }

            dataReader.Close();
            DBConnection.CloseConnection();         
        }
    }
}
