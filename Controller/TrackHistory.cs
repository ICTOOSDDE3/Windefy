using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public static class TrackHistory
    { 
        public static Stack<int> trackHistory = new Stack<int>();
        private static int _userID = Model.User.UserID;
        public static int PlaylistID { get; set; }

        /// <summary>
        /// inserts a track in the history playlist of an user
        /// </summary>
        /// <param name="trackID">is the id from a track that has to be added</param>
        public static void InsertToHistory(int trackID)
        {

            //DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string query = $"INSERT INTO track_history (userID, trackID) VALUES({_userID}, {trackID})";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            
            cmd.ExecuteScalar();
            DBConnection.CloseConnection();
        }
        /// <summary>
        /// Gets history playlistID
        /// </summary>
        /// <returns></returns>
        public static int getHistoryPlaylistID()
        {
            DBConnection.OpenConnection();

            //Build the query
            string historyQuery = $"SELECT playlistID FROM playlist where title = 'History' AND ownerID = {_userID}";
            SqlCommand cmdHistory = new SqlCommand(historyQuery, DBConnection.Connection);

            int playlistID = Convert.ToInt32(cmdHistory.ExecuteScalar().ToString());

            DBConnection.CloseConnection();

            return playlistID;
        }

        /// <summary>
        /// Gets tracks from track_history using userID. Tracks are orderd by latest datetime.
        /// </summary>
        /// <returns>List of Model.track</returns>
        public static List<Model.Track> PlaylistTracks()
        {
            List<Model.Track> tracks = new List<Model.Track>();
            List<int> trackIDs = new List<int>();

            Track contrTrack = new Track();

            DBConnection.OpenConnection();
            var query = $"SELECT trackID FROM track_history WHERE userID = {_userID} ORDER BY date_time DESC";
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
                    //return null;
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
            else
            {
                tracks.Add(contrTrack.GetTrack(2));
            }

            return tracks;
        }
    }
}
