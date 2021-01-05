using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public static class TrackHistory
    { 
        private static int UserID = Model.TrackHistory.UserID;
        /// <summary>
        /// inserts a track in the history playlist of an user
        /// </summary>
        /// <param name="trackID">is the id from a track that has to be added</param>
        public static void InsertToHistory(int trackID)
        {

            //DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string query = $"INSERT INTO track_history (userID, trackID) VALUES({UserID}, {trackID})";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            
            cmd.ExecuteScalar();
            DBConnection.CloseConnection();
        }
        /// <summary>
        /// Gets history playlistID
        /// </summary>
        /// <returns></returns>
        public static int GetHistoryPlaylistID()
        {
            DBConnection.OpenConnection();

            //Build the query
            string historyQuery = $"SELECT playlistID FROM playlist where title = 'History' AND ownerID = {UserID}";
            SqlCommand cmdHistory = new SqlCommand(historyQuery, DBConnection.Connection);

            int playlistID = Convert.ToInt32(cmdHistory.ExecuteScalar().ToString());

            DBConnection.CloseConnection();

            return playlistID;
        }

        /// <summary>
        /// Gets tracks from track_history using userID. Tracks are orderd by latest datetime.
        /// </summary>
        /// <returns>List of Model.track</returns>
        public static List<Model.Track> HistoryTracks()
        {
            List<Model.Track> tracks = new List<Model.Track>();
            List<int> trackIDs = new List<int>();

            Track contrTrack = new Track();

            DBConnection.OpenConnection();
            var query = $"SELECT TOP 20 trackID FROM track_history WHERE userID = {UserID} ORDER BY date_time DESC";
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        trackIDs.Add((int)reader["trackID"]);
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                    return null;
                }
            }
            DBConnection.CloseConnection();
            if (trackIDs.Count > 0)
            {
                foreach (var item in trackIDs)
                {
                    tracks.Add(contrTrack.GetTrack(item));
                }
            }

            return tracks;
        }
    }
}
