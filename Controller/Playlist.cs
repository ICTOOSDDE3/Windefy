using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public class Playlist
    {
        public int CreateUserPlaylist(string playlistTitle, bool playlist_is_Public)
        {

            //Initialize and open a db connection
            DBConnection.OpenConnection();

            int PublicPlaylist = 0;

            if(playlist_is_Public == true) PublicPlaylist = 1; 

            //SQL injection prepared query builder
            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);
            cmd.CommandText = $"INSERT INTO playlist (title, listens, playlist_typeID, is_public, ownerID) output INSERTED.playlistID " +
                $"VALUES(@title, @listens, @playlist_typeID, @is_public, @ownerID)";


            SqlParameter paramTitle = new SqlParameter("@title", System.Data.SqlDbType.Text, 255);
            SqlParameter paramListens = new SqlParameter("@listens", System.Data.SqlDbType.Int, 4);
            SqlParameter paramPlaylistType = new SqlParameter("@playlist_typeID", System.Data.SqlDbType.Int, 4);
            SqlParameter paramIsPublic = new SqlParameter("@is_public", System.Data.SqlDbType.Bit, 1);
            SqlParameter paramOwnerID = new SqlParameter("@ownerID", System.Data.SqlDbType.Int, 4);

            paramTitle.Value = playlistTitle;
            paramListens.Value = 0;
            paramPlaylistType.Value = 5;
            paramIsPublic.Value = PublicPlaylist;
            paramOwnerID.Value = Model.User.UserID;

            cmd.Parameters.Add(paramTitle);
            cmd.Parameters.Add(paramListens);
            cmd.Parameters.Add(paramPlaylistType);
            cmd.Parameters.Add(paramIsPublic);
            cmd.Parameters.Add(paramOwnerID);

            cmd.Prepare();
            SqlDataReader dataReader = cmd.ExecuteReader();
            int modified = 0;
            while (dataReader.Read())
            {
                modified = Convert.ToInt32(dataReader["playlistID"]);
            }

            DBConnection.CloseConnection();

            return modified;
        }
    }
}
