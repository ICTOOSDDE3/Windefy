using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public static class Favourite
    {
        public static bool IsFavouriteArtist(int ArtistID)
        {
            DBConnection.OpenConnection();

            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
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

            return amount > 0;
        }

        public static bool IsFavouritePlaylist(int PlaylistID)
        {
            SqlConnection con = new SqlConnection($"Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = {Passwords.GetPassword("DB")};");
            con.Open();

            SqlCommand cmd = new SqlCommand(null, con)
            {
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

            return amount > 0;
        }

        public static void RemoveFavouriteArtist(int ArtistID)
        {
            if (IsFavouriteArtist(ArtistID))
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
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

        public static void AddFavouriteArtist(int ArtistID)
        {
            if (!IsFavouriteArtist(ArtistID))
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
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

        public static void RemoveFavouritePlaylist(int PlaylistID)
        {
            if (IsFavouritePlaylist(PlaylistID))
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
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

        public static void AddFavouritePlaylist(int PlaylistID)
        {
            if (!IsFavouritePlaylist(PlaylistID))
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
                {
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
