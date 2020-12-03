using Controller;
using System;
using System.Collections.Generic;
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

            items = new List<TrackInfo>();
            DBConnection.OpenConnection();

            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);
            cmd.CommandText = "SELECT title, duration, image_path, name " +
                "FROM track_artist " +
                "JOIN artist ON track_artist.artistID = artist.artistID " +
                "JOIN track ON track_artist.trackID = track.trackID " +
                "WHERE title LIKE '%' + @que + '%' " +
                "ORDER BY track.trackID " +
                "OFFSET 0 ROWS " +
                "FETCH NEXT 50 ROWS ONLY";

            SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255);
            que.Value = q;

            cmd.Parameters.Add(que);
            cmd.Prepare();

            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                TrackInfo trackInfo = new TrackInfo(Convert.ToString(dataReader["title"]), Convert.ToInt32(dataReader["duration"]), Convert.ToString(dataReader["image_path"]), Convert.ToString(dataReader["name"]));

                items.Add(trackInfo);
            }

            dataReader.Close();
            DBConnection.CloseConnection();
        }
    }
    public class TrackInfo
    {
        public string Title { get; set; }
        public string Duration { get; set; }
        public string ImagePath { get; set; }
        public string ArtistName { get; set; }

        public TrackInfo(string T, int D, string I, string A)
        {
            string seconds = (D % 60).ToString();
            if (seconds.Length == 1)
            {
                seconds = "0" + seconds;
            }

            Title = T;
            Duration = $"{ Math.Floor(Convert.ToDouble(D) / 60)}:{seconds}";
            ImagePath = $"{ApacheConnection.GetImageFullPath(I)}";
            ArtistName = A;
        }
    }
}
