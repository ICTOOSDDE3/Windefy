using Caliburn.Micro;
using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace View.ViewModels
{
    public class SearchAlbumViewModel
    {
        public List<PlaylistInfo> items { get; set; }

        public SearchAlbumViewModel(string q, bool playlist)
        {
            if (!playlist)
            {
                ApacheConnection.Initialize();

                items = new List<PlaylistInfo>();
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);
                cmd.CommandText = "SELECT playlist.playlistID playlistID, title, COUNT(trackID) as trackCount, name " +
                    "FROM playlist_track " +
                    "JOIN playlist ON playlist_track.playlistID = playlist.playlistID " +
                    "JOIN artist_album ON playlist_track.playlistID = artist_album.playlistID " +
                    "JOIN artist ON artist_album.artistID = artist.artistID " +
                    "WHERE playlist_typeID = 5 " +
                    "AND title LIKE '%' + @que + '%' " +
                    "GROUP BY title, name";

                SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255);
                que.Value = q;

                cmd.Parameters.Add(que);

                cmd.Prepare();

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    PlaylistInfo playlistInfo = new PlaylistInfo(Convert.ToString(dataReader["title"]),
                        Convert.ToString(dataReader["trackCount"]),
                        Convert.ToString(dataReader["name"]),
                        Convert.ToInt32(dataReader["playlistID"]));

                    items.Add(playlistInfo);
                }

                dataReader.Close();

                DBConnection.CloseConnection();
            }
            else
            {
                ApacheConnection.Initialize();

                items = new List<PlaylistInfo>();
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);
                cmd.CommandText = "SELECT playlist.playlistID playlistID, title, COUNT(trackID) as trackCount, name " +
                    "FROM playlist_track " +
                    "JOIN playlist ON playlist_track.playlistID = playlist.playlistID " +
                    "JOIN artist_album ON playlist_track.playlistID = artist_album.playlistID " +
                    "JOIN artist ON artist_album.artistID = artist.artistID " +
                    "WHERE playlist_typeID != 5 " +
                    "AND title LIKE '%' + @que + '%' " +
                    "GROUP BY title, name";

                SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255);
                que.Value = q;

                cmd.Parameters.Add(que);

                cmd.Prepare();

                SqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    PlaylistInfo playlistInfo = new PlaylistInfo(Convert.ToString(dataReader["title"]),
                        Convert.ToString(dataReader["trackCount"]),
                        Convert.ToString(dataReader["name"]),
                        Convert.ToInt32(dataReader["playlistID"]));

                    items.Add(playlistInfo);
                }

                dataReader.Close();

                DBConnection.CloseConnection();
            }
        }
    }

    public class PlaylistInfo
    {
        public string Title { get; set; }
        public string Quantity { get; set; }
        public string ArtistName { get; set; }
        public int PlaylistID { get; set; }

        public PlaylistInfo(string T, string Q, string A, int ID)
        {
            PlaylistID = ID;
            Title = T;
            Quantity = Q;
            ArtistName = A;
        }
    }
}
