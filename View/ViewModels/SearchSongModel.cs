using Controller;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace View.ViewModels
{
    public class SearchSongModel
    {
        public List<TrackInfo> items { get; set; }

        public SearchSongModel(string q)
        {
            ApacheConnection.Initialize();
            DBConnection.OpenConnection();

            // Initialize or empty the items
            items = new List<TrackInfo>();

            // Fetch all tracks
            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                // Select needed variables from track, limiting to 50 results
                CommandText = "SELECT title, duration, image_path, trackID " +
                "FROM track " +
                "WHERE title LIKE '%' + @que + '%' " +
                "ORDER BY track.trackID " +
                "OFFSET 0 ROWS " +
                "FETCH NEXT 50 ROWS ONLY"
            };

            SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255)
            {
                Value = q
            };

            cmd.Parameters.Add(que);
            cmd.Prepare();

            // Fetch all rows based on the search query and put them in items
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                TrackInfo trackInfo = new TrackInfo(Convert.ToString(dataReader["title"]),
                    Convert.ToInt32(dataReader["duration"]),
                    Convert.ToString(dataReader["image_path"]),
                    Convert.ToInt32(dataReader["trackID"]));
                items.Add(trackInfo);
            }

            dataReader.Close();
            DBConnection.CloseConnection();
        }
    }

    // Data template for tracksinfo for the search screen
    public class TrackInfo
    {
        public int TrackID { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string ImagePath { get; set; }
        public string ArtistName { get; set; }

        public TrackInfo(string T, int D, string I, int ID)
        {
            TrackID = ID;
            string seconds = (D % 60).ToString();
            if (seconds.Length == 1)
            {
                seconds = "0" + seconds;
            }

            Title = T;
            Duration = $"{ Math.Floor(Convert.ToDouble(D) / 60)}:{seconds}";
            ImagePath = $"{ApacheConnection.GetImageFullPath(I)}";


            // TODO: Make artistName an array instead of a string so that it can be
            // linked to artist pages in the XAML
            ArtistName = "";

            
            SqlConnection con = new SqlConnection($"Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = {Passwords.GetPassword("DB")};");
            con.Open();

            // Fetch all artists that worked on a track based on the ID of the track
            SqlCommand cmd = new SqlCommand(null, con)
            {
                CommandText = "SELECT name " +
                "FROM artist " +
                "WHERE artistID IN (" +
                "   SELECT artistID " +
                "   FROM track_artist " +
                $"   WHERE trackID = {ID} " +
                ")"
            };

            // Put all of the artists into a string
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                ArtistName += Convert.ToString(dataReader["name"]) + ", ";
            }

            // Remove last 2 characters (', ') from the string
            ArtistName = ArtistName[0..^2];

            dataReader.Close();
            con.Close();
            con.Dispose();
        }
    }
}
