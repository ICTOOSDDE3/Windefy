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
            track.Genres = GetGenres(numberID);
            track.Artists = GetArtists(numberID);
            return track;
        }

        /// <summary>
        /// Gets artists from database and makes a list of it
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>list of artist names </returns>
        private List<Model.Artist> GetArtists(int numberID)
        {
            List<Model.Artist> artistList = new List<Model.Artist>();

            DBConnection.OpenConnection();
            string query = $"Select name, artist.artistID from track_artist join artist on track_artist.artistID = artist.artistID where trackID = {numberID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {

                    Model.Artist artist = new Model.Artist();
                    artist.Name = reader["name"].ToString();
                    artist.ArtistID = (int)reader["artistID"];

                    artistList.Add(artist);
                }
            }
            DBConnection.CloseConnection();

            return artistList;
        }

        /// <summary>
        /// Gets genre ID's from database and makes a list of it
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>List of genre id's</returns>
        private List<string> GetGenres(int numberID)
        {
            List<string> genreList = new List<string>();

            DBConnection.OpenConnection();
            string query = $"Select name from track_genre join genre on track_genre.genreID = genre.genreID where trackID = {numberID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    genreList.Add(reader["name"].ToString());
                }
            }
            DBConnection.CloseConnection();

            return genreList;
        }
    }
}
