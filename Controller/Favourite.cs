using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public static class Favourite
    {
        /// <summary>
        /// Takes the artistID and checks if the current logged in user has artist added as favourite
        /// </summary>
        /// <param name="ArtistID">ID of the artist</param>
        /// <returns>Whether the artist is favourited by the current user or not (T/F)</returns>
        public static bool IsFavouriteArtist(int ArtistID)
        {
            DBConnection.OpenConnection();

            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                // Select the amount of rows
                CommandText = "SELECT COUNT(*) AS count " +
                "FROM user_favourite_artist " +
                "WHERE userID = @UID " +
                "AND artistID = @AID " 
            };

            SqlParameter aid = new SqlParameter("@AID", System.Data.SqlDbType.Int)
            {
                Value = ArtistID
            };

            SqlParameter uid = new SqlParameter("@UID", System.Data.SqlDbType.Int)
            {
                Value = Model.User.UserID
            };
            cmd.Parameters.Add(uid);
            cmd.Parameters.Add(aid);
            cmd.Prepare();

            SqlDataReader dataReader = cmd.ExecuteReader();

            int amount = 0;
            while (dataReader.Read())
            {
                amount = Convert.ToInt32(dataReader["count"]);
            }

            // If the amount of rows is more than 0 (There is a row), return true
            return amount > 0;
        }

        /// <summary>
        /// Takes the PlaylistID and checks if the current logged in user has playlist added as favourite
        /// </summary>
        /// <param name="PlaylistID">ID of the playlist</param>
        /// <returns>Whether the playlist is favourited by the current user or not (T/F)</returns>
        public static bool IsFavouritePlaylist(int PlaylistID)
        {
            SqlConnection con = new SqlConnection($"Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = {Passwords.GetPassword("DB")};");
            con.Open();

            SqlCommand cmd = new SqlCommand(null, con)
            {
                // Select the amount of rows
                CommandText = "SELECT COUNT(*) AS count " +
                "FROM user_favourite_playlist " +
                "WHERE userID = @UID " +
                "AND playlistID = @PID "
            };

            SqlParameter pid = new SqlParameter("@PID", System.Data.SqlDbType.Int)
            {
                Value = PlaylistID
            };

            SqlParameter uid = new SqlParameter("@UID", System.Data.SqlDbType.Int)
            {
                Value = Model.User.UserID
            };
            cmd.Parameters.Add(uid);
            cmd.Parameters.Add(pid);
            cmd.Prepare();

            SqlDataReader dataReader = cmd.ExecuteReader();

            int amount = 0;
            while (dataReader.Read())
            {
                amount = Convert.ToInt32(dataReader["count"]);
            }

            con.Close();
            con.Dispose();

            // If the amount of rows is more than 0 (There is a row), return true
            return amount > 0;
        }

        /// <summary>
        /// Removes the given artistID from the current user's favourites
        /// </summary>
        /// <param name="ArtistID">ID of the artist</param>
        public static void RemoveFavouriteArtist(int ArtistID)
        {
            if (IsFavouriteArtist(ArtistID))
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
                    // Delete artist entry from favourites
                    CommandText = "DELETE FROM user_favourite_artist " +
                    "WHERE userID = @UID " +
                    "AND artistID = @AID "
                };

                SqlParameter aid = new SqlParameter("@AID", System.Data.SqlDbType.Int)
                {
                    Value = ArtistID
                };

                SqlParameter uid = new SqlParameter("@UID", System.Data.SqlDbType.Int)
                {
                    Value = Model.User.UserID
                };
                cmd.Parameters.Add(uid);
                cmd.Parameters.Add(aid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds given artistID to the current user's favourites
        /// </summary>
        /// <param name="ArtistID">ID of the artist</param>
        public static void AddFavouriteArtist(int ArtistID)
        {
            if (!IsFavouriteArtist(ArtistID))
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
                    // Add artist entry to favourites
                    CommandText = "INSERT INTO user_favourite_artist (userID, artistID)" +
                    "VALUES (@UID, @AID) "
                };

                SqlParameter aid = new SqlParameter("@AID", System.Data.SqlDbType.Int)
                {
                    Value = ArtistID
                };

                SqlParameter uid = new SqlParameter("@UID", System.Data.SqlDbType.Int)
                {
                    Value = Model.User.UserID
                };
                cmd.Parameters.Add(uid);
                cmd.Parameters.Add(aid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Removes given playlist from the current user's favourites
        /// </summary>
        /// <param name="PlaylistID">ID of the Playlist</param>
        public static void RemoveFavouritePlaylist(int PlaylistID)
        {
            if (IsFavouritePlaylist(PlaylistID))
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
                    // Delete playlist entry from favourites
                    CommandText = "DELETE FROM user_favourite_playlist " +
                    "WHERE userID = @UID " +
                    "AND playlistID = @PID "
                };

                SqlParameter pid = new SqlParameter("@PID", System.Data.SqlDbType.Int)
                {
                    Value = PlaylistID
                };

                SqlParameter uid = new SqlParameter("@UID", System.Data.SqlDbType.Int)
                {
                    Value = Model.User.UserID
                };
                cmd.Parameters.Add(uid);
                cmd.Parameters.Add(pid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds given playlist to the current user's favourites
        /// </summary>
        /// <param name="PlaylistID">ID of the Playlist</param>
        public static void AddFavouritePlaylist(int PlaylistID)
        {
            if (!IsFavouritePlaylist(PlaylistID))
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
                    // Add playlist entry
                    CommandText = "INSERT INTO user_favourite_playlist (userID, playlistID)" +
                    "VALUES (@UID, @PID) "
                };

                SqlParameter pid = new SqlParameter("@PID", System.Data.SqlDbType.Int)
                {
                    Value = PlaylistID
                };

                SqlParameter uid = new SqlParameter("@UID", System.Data.SqlDbType.Int)
                {
                    Value = Model.User.UserID
                };
                cmd.Parameters.Add(uid);
                cmd.Parameters.Add(pid);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
