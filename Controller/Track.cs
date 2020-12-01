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
            

        }
        /// <summary>
        /// Calls methods to create track object
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>Track</returns>
        public Model.Track GetTrack(int numberID)
        {
            Model.Track track = GetTrackFromDB(numberID);
            return track;
        }

        /// <summary>
        /// Gets track data from database and makes a track object
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>Track object</returns>
        private Model.Track GetTrackFromDB(int numberID)
        {
            DBConnection.OpenConnection();
            string query = $"SELECT title,listens,languageID,duration,date_created,file_path,image_path FROM track WHERE trackID = {numberID}";
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

            track.NumberID = numberID;
            track.GenreIDs = GetGenreIDs(numberID);
            track.ArtistIDs = GetArtistIDs(numberID);
            return track;
        }

        /// <summary>
        /// Gets artist ID's from database and makes a list of it
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>list of artist ID's </returns>
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
        /// Gets genre ID's from database and makes a list of it
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>List of genre id's</returns>
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
