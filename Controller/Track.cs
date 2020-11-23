using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Controller
{
    public class Track
    {
        public Track()
        {
            DBConnection.Initialize();
        }
        public Model.Track GetTrack(int numberID)
        {
            Model.Track track = GetTrackFromDB(numberID);
            return track;
        }

        /// <summary>
        /// Haalt alle gegevens van track op uit de database en maakt hier een track object van
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>een Track object</returns>
        private Model.Track GetTrackFromDB(int numberID)
        {
            DBConnection.OpenConnection();
            string query = $"Select * from track where trackID = {numberID}";
            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            Model.Track track = new Model.Track();
            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    track.Title = reader["title"].ToString();
                    track.Listens = (int)reader["listens"];
                    track.LanguageID = (int)reader["languageID"];
                    track.Duration = (int)reader["duration"];
                    track.Date_Created = (DateTime)reader["date_created"];
                    track.File_path = reader["file_path"].ToString();
                }
            }
            DBConnection.CloseConnection();

            track.GenreIDs = GetGenreIDs(numberID);
            track.ArtistIDs = GetArtistIDs(numberID);
            return track;
        }

        /// <summary>
        /// Haalt de artist id's uit de Database die bij het nummer hoort
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>list van artist id's</returns>
        private List<int> GetArtistIDs(int numberID)
        {
            List<int> IDArtist = new List<int>();

            DBConnection.OpenConnection();
            string query = $"Select * from track_artist where trackID = {numberID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    IDArtist.Add((int)reader["artistID"]);
                }
            }
            DBConnection.CloseConnection();

            return IDArtist;
        }

        /// <summary>
        /// Haalt de genre id's uit de Database die bij het nummer hoort
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>List van genre id's</returns>
        private List<int> GetGenreIDs(int numberID)
        {
            List<int> IDsGenre = new List<int>();

            DBConnection.OpenConnection();
            string query = $"Select * from track_genre where trackID = {numberID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    IDsGenre.Add((int)reader["genreID"]);
                }
            }
            DBConnection.CloseConnection();

            return IDsGenre;
        }
    }
}
