using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Controller
{
    public class Track
    {
        private string title;
        private int listens;
        private int languageID;
        private int duration;
        private DateTime date_created;
        private List<int> genreIDs;
        private List<int> artistIDs;

        public Track(int numberID)
        {
            Model.Track track = getTrackFromDB(numberID);

            //naar view sturen
        }

        /// <summary>
        /// Haalt alle gegevens van track op uit de database en maakt hier een track object van
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>een Track object</returns>
        private Model.Track getTrackFromDB(int numberID)
        {
            SqlConnection myConnection = new SqlConnection();
            string dbString = $"Select * from track where trackID = {numberID}";

            SqlCommand oCmd = new SqlCommand(dbString, myConnection);
            myConnection.Open();

            using (SqlDataReader reader = oCmd.ExecuteReader())
            { 
                title = reader["title"].ToString();
                listens = (int)reader["listens"];
                languageID = (int)reader["languageID"];
                duration = (int)reader["duration"];
                date_created = (DateTime)reader["date_created"];
            }
            myConnection.Close();

            genreIDs = getGenreIDs(numberID);
            artistIDs = getArtistIDs(numberID);

            Model.Track track = new Model.Track(title, listens, languageID, duration, date_created, numberID, artistIDs, genreIDs);

            return track;
        }

        /// <summary>
        /// Haalt de artist id's uit de Database die bij het nummer hoort
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>list van artist id's</returns>
        private List<int> getArtistIDs(int numberID)
        {
            List<int> IDArtist = new List<int>();

            SqlConnection myConnection = new SqlConnection();
            string dbString = $"Select * from track_artist where trackID = {numberID}";

            SqlCommand oCmd = new SqlCommand(dbString, myConnection);
            myConnection.Open();

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    IDArtist.Add((int)reader["artistID"]);
                }
            }
            myConnection.Close();

            return IDArtist;
        }

        /// <summary>
        /// Haalt de genre id's uit de Database die bij het nummer hoort
        /// </summary>
        /// <param name="numberID"></param>
        /// <returns>List van genre id's</returns>
        private List<int> getGenreIDs(int numberID)
        {
            List<int> IDsGenre = new List<int>();

            SqlConnection myConnection = new SqlConnection();
            string dbString = $"Select * from track_genre where trackID = {numberID}";

            SqlCommand oCmd = new SqlCommand(dbString, myConnection);
            myConnection.Open();

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    IDsGenre.Add((int)reader["genreID"]);
                }
            }
            myConnection.Close();

            return IDsGenre;
        }
    }
}
