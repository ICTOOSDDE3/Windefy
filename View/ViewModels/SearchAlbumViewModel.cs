using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace View.ViewModels
{
    public class SearchAlbumViewModel
    {
        public List<PlaylistInfo> items { get; set; }

        private string _NoResultsVisibility;
        internal Action<object, int> AlbumClickEvent;

        public string NoResultsVisibility
        {
            get { return _NoResultsVisibility; }
            set { _NoResultsVisibility = value; }
        }

        public SearchAlbumViewModel()
        {
            items = new List<PlaylistInfo>();
        }

        public SearchAlbumViewModel(string q, bool playlist)
        {
            NoResultsVisibility = "Hidden";
            // Get playlists or albums depening on the boolean given
            if (!playlist)
            {
                ApacheConnection.Initialize();
                DBConnection.OpenConnection();

                items = new List<PlaylistInfo>();


                // Get playlists based on query
                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
                    CommandText = "SELECT playlistID, title " +
                    "FROM playlist " +
                    "WHERE playlist_typeID != 5 " +
                    "AND title LIKE '%' + @que + '%' " +
                    "ORDER BY playlistID " +
                    "OFFSET 0 ROWS " +
                    "FETCH NEXT 50 ROWS ONLY"
                };

                SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255)
                {
                    Value = q
                };
                cmd.Parameters.Add(que);
                cmd.Prepare();

                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    PlaylistInfo playlistInfo = new PlaylistInfo(Convert.ToString(dataReader["title"]),
                        Convert.ToInt32(dataReader["playlistID"]), false);

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


                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
                    CommandText = "SELECT playlistID, title " +
                    "FROM playlist " +
                    "WHERE playlist_typeID = 5 " +
                    "AND title LIKE '%' + @que + '%' " +
                    "ORDER BY playlistID " +
                    "OFFSET 0 ROWS " +
                    "FETCH NEXT 50 ROWS ONLY"
                };

                SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255)
                {
                    Value = q
                };
                cmd.Parameters.Add(que);
                cmd.Prepare();

                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    PlaylistInfo playlistInfo = new PlaylistInfo(Convert.ToString(dataReader["title"]),
                        Convert.ToInt32(dataReader["playlistID"]), true);

                    items.Add(playlistInfo);
                }

                dataReader.Close();

                DBConnection.CloseConnection();
            }

            if (items.Count > 0)
            {
                NoResultsVisibility = "Hidden";
            }
            else
            {
                NoResultsVisibility = "Visible";
            }
        }

        public void OnAlbumClick(int artistId)
        {
            AlbumClickEvent?.Invoke(this, artistId);
        }

        public void GetFavourites(int userID)
        {
            NoResultsVisibility = "Hidden";
            ApacheConnection.Initialize();

            items = new List<PlaylistInfo>();
            DBConnection.OpenConnection();


            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                CommandText = "SELECT playlistID, title " +
                "FROM playlist " +
                "WHERE playlistID IN (" +
                "   SELECT playlistID " +
                "   FROM user_favourite_playlist " +
                "   WHERE userID = @ID " +
                ") " +
                "ORDER BY playlistID " +
                "OFFSET 0 ROWS " +
                "FETCH NEXT 50 ROWS ONLY"
            };

            SqlParameter id = new SqlParameter("@ID", System.Data.SqlDbType.VarChar, 255)
            {
                Value = userID
            };
            cmd.Parameters.Add(id);
            cmd.Prepare();

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                PlaylistInfo playlistInfo = new PlaylistInfo(Convert.ToString(dataReader["title"]),
                    Convert.ToInt32(dataReader["playlistID"]), true);

                items.Add(playlistInfo);
            }

            dataReader.Close();

            DBConnection.CloseConnection();

            if (items.Count > 0)
            {
                NoResultsVisibility = "Hidden";
            }
            else
            {
                NoResultsVisibility = "Visible";
            }
        }
    }


    // Data template for the playlist/album info on the search screen
    public class PlaylistInfo
    {
        public string Title { get; set; }
        public string Quantity { get; set; }
        public string ArtistName { get; set; }
        public int PlaylistID { get; set; }
        public bool IsUserPlaylist { get; set; }
        public bool Liked { get; set; }

        public PlaylistInfo(string T, int ID, bool userPlaylist)
        {
            PlaylistID = ID;
            Title = T;
            IsUserPlaylist = userPlaylist;

            // Fetch the amount of tracks in a playlist
            SqlConnection con = new SqlConnection($"Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = {Passwords.GetPassword("DB")};");
            con.Open();
            SqlCommand cmd = new SqlCommand(null, con)
            {
                CommandText = "SELECT COUNT(*) count FROM Playlist_track " +
                $"WHERE playlistID = {ID}"
            };

            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                Quantity = Convert.ToString(dataReader["count"]);
            }

            dataReader.Close();
            con.Close();

            // Open a new connection, init the artistname and SQLcommand
            ArtistName = "";
            con.Open();
            SqlCommand command;

            // Get user or artists depending on if it is a usermade playlist or an
            // artist made playlist
            if (IsUserPlaylist)
            {
                command = new SqlCommand(null, con)
                {
                    CommandText = "SELECT name FROM users " +
                    "WHERE user_ID IN (" +
                    "  SELECT ownerID" +
                    "  FROM playlist" +
                    $"  WHERE playlistID = {ID})"
                };
            }
            else
            {
                command = new SqlCommand(null, con)
                {
                    CommandText = "SELECT name FROM artist " +
                    "WHERE artistID IN (" +
                    "   SELECT artistID" +
                    "   FROM artist_album" +
                    $"   WHERE playlistID = {ID})"
                };
            }

            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                ArtistName += Convert.ToString(dr["name"]) + ", ";
            }

            // Remove last 2 characters (', ') from the string
            if (ArtistName.Length > 2)
            {
                ArtistName = ArtistName[0..^2];
            }
            else
            {
                ArtistName = "Anonymous";
            }

            dr.Close();
            con.Close();
            con.Dispose();

            Liked = Favourite.IsFavouritePlaylist(PlaylistID);
        }
    }
}
