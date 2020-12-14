using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace View.ViewModels
{
    public class PlaylistViewModel
    {
        public Model.Playlist Playlist { get; set; }
        public int PlaylistID { get; set; }

        public PlaylistViewModel(int playlistID)
        {
            PlaylistID = playlistID;
            Playlist = GetPlaylistData(playlistID);
            FillPlaylist(playlistID);
        }

        public Model.Playlist GetPlaylistData(int playlistID)
        {           
            DBConnection.OpenConnection();
            Model.Playlist PlaylistModel = null;

            // Fetch all artists based on the search query
            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                CommandText = "SELECT * " +
                "FROM playlist " +
                "WHERE playlistID = @id"                
            };

            SqlParameter id = new SqlParameter("@id", System.Data.SqlDbType.Int, 4)
            {
                Value = playlistID
            };

            cmd.Parameters.Add(id);
            cmd.Prepare();

            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                PlaylistModel = new Model.Playlist(Convert.ToInt32(dataReader["playlistID"]), Convert.ToString(dataReader["title"]), Convert.ToDateTime(dataReader["release_date"]), Convert.ToInt32(dataReader["listens"]), Convert.ToInt32(dataReader["playlist_typeID"]), Convert.ToString(dataReader["information"]), Convert.ToBoolean(dataReader["is_public"]), Convert.ToInt32(dataReader["ownerID"]));                
            }

            dataReader.Close();

            DBConnection.CloseConnection();

            return PlaylistModel;
        }

        public void FillPlaylist(int playlistID)
        {
            DBConnection.OpenConnection();

            // Get playlists based on query
            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                CommandText = "SELECT t.trackID, t.title " +
                "FROM track t " +
                "LEFT JOIN playlist_track pt ON t.trackID = pt.trackID " +
                "WHERE pt.playlistID = @id"
            };

            SqlParameter id = new SqlParameter("@id", System.Data.SqlDbType.Int, 4)
            {
                Value = playlistID
            };
            cmd.Parameters.Add(id);
            cmd.Prepare();

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {

                Playlist.tracks.Add(new Model.Track(Convert.ToInt32(dataReader["trackID"]), Convert.ToString(dataReader["title"])));
            }

            dataReader.Close();

            DBConnection.CloseConnection();
        }
    }
}
