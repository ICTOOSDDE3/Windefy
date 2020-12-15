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
        public static int PlaylistID;

        /// <summary>
        /// inserts a track in the history playlist of an user
        /// </summary>
        /// <param name="trackID">is the id from a track that has to be added</param>
        public static void InsertToHistory(int trackID)
        {

            //DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string query = $"INSERT INTO playlist_track (trackID, playlistID, number) VALUES({trackID}, {PlaylistID}, -1)";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            
            cmd.ExecuteScalar();
            DBConnection.CloseConnection();
        }

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

        public static List<Model.Track> PlaylistTracks()
        {
            List<Model.Track> tracks = new List<Model.Track>();
            List<int> trackIDs = new List<int>();

            Track contrTrack = new Track();

            DBConnection.OpenConnection();
            var query = $"SELECT trackID FROM playlist_track WHERE playlistID = {PlaylistID}";
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
