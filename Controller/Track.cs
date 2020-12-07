using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Controller
{
    public class Track
    {
        /// <summary>
        /// Calls methods to create track object
        /// </summary>
        /// <param name="trackID"></param>
        /// <returns>Track</returns>
        public Model.Track GetTrack(int trackID)
        {
            DBConnection.OpenConnection();
            string query = $"SELECT title,listens,languageID,duration,date_created,file_path,image_path FROM track WHERE trackID = {trackID}";
            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            Model.Track track = new Model.Track();

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        track.Title = reader["title"].ToString();
                        track.Listens = (int)reader["listens"];
                        if (reader["languageID"].GetType().GetProperties().Length > 0) track.LanguageID = (int)reader["languageID"];
                        track.Duration = (int)reader["duration"];
                        track.Date_Created = (DateTime)reader["date_created"];
                        track.File_path = reader["file_path"].ToString();
                        track.Image_path = reader["image_path"].ToString();
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                    return null;

                }
            }
            DBConnection.CloseConnection();
            track.TrackID = trackID;
            track.Genres = GetGenres(trackID);
            track.Artists = GetArtists(trackID);
            return track;
        }

        /// <summary>
        /// Gets artists from database and makes a list of it
        /// </summary>
        /// <param name="trackID"></param>
        /// <returns>list of artist names </returns>
        private List<Model.Artist> GetArtists(int trackID)
        {
            List<Model.Artist> artistList = new List<Model.Artist>();

            DBConnection.OpenConnection();
            string query = $"SELECT name, artist.artistID FROM track_artist JOIN artist ON track_artist.artistID = artist.artistID WHERE trackID = {trackID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.Artist artist = new Model.Artist();
                        artist.Name = reader["name"].ToString();
                        artist.ArtistID = (int)reader["artistID"];
                        artistList.Add(artist);
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                    return null;
                }
            }
            DBConnection.CloseConnection();

            return artistList;
        }

        /// <summary>
        /// Gets genre ID's from database and makes a list of it
        /// </summary>
        /// <param name="trackID"></param>
        /// <returns>List of genre id's</returns>
        public List<string> GetGenres(int trackID)
        {
            List<string> genreList = new List<string>();

            DBConnection.OpenConnection();
            string query = $"SELECT name FROM track_genre JOIN genre ON track_genre.genreID = genre.genreID WHERE trackID = {trackID}";
            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);
            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        genreList.Add(reader["name"].ToString());
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                    return null;
                }
            }
            DBConnection.CloseConnection();

            return genreList;
        }
        /// <summary>
        /// Gets all tracks who matches with query and pushes everything in Queue
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Queue<int> GetTracksToQueue(string query)
        {
            Queue<int> returnQueue = new Queue<int>();
            DBConnection.OpenConnection();
            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);
            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        returnQueue.Enqueue((int)reader["trackID"]);
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                    return null;
                }
            }
            DBConnection.CloseConnection();
            return returnQueue;
        }
    }
}