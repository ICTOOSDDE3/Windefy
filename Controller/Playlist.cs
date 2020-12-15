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

        public List<Model.Track> FillPlaylist(int playlistID)
        {
            List<Model.Track> tracks = new List<Model.Track>();
            DBConnection.OpenConnection();

            // Get playlists based on query
            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                CommandText = "SELECT t.trackID, t.title, t.duration " +
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

            AddMusicToPlaylist AddMusicToPlaylistInstance = new AddMusicToPlaylist();

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {

                tracks.Add(new Model.Track(Convert.ToInt32(dataReader["trackID"]), Convert.ToString(dataReader["title"]), Convert.ToInt32(dataReader["duration"]), AddMusicToPlaylistInstance.IsTrackInFavorites(Convert.ToInt32(dataReader["trackID"]), Model.User.UserID)));
            }

            dataReader.Close();


            foreach (Model.Track track in tracks)
            {             
            // Get artists based on trackID
                SqlCommand cmd1 = new SqlCommand(null, DBConnection.Connection)
            {
                CommandText = "SELECT a.name " +
                "FROM track_artist ta " +
                "LEFT JOIN artist a ON ta.artistID = a.artistID " +
                "WHERE ta.trackID = @id1"
            };

                SqlParameter id1 = new SqlParameter("@id1", System.Data.SqlDbType.Int, 4)
                {
                    Value = track.TrackID
                };
                cmd1.Parameters.Add(id1);
                cmd1.Prepare();

                SqlDataReader dataReader1 = cmd1.ExecuteReader();

                while (dataReader1.Read())
                {
                    track.Artists.Add(new Model.Artist(track.TrackID, Convert.ToString(dataReader1["name"])));
                }
                dataReader1.Close();
            }
            


            DBConnection.CloseConnection();

            return tracks;
        }
    }
}
