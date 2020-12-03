using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public static class TrackQueue
    {
        public static Queue<Model.Track> trackQueue = new Queue<Model.Track>();
        public static bool ShuffleEnabled = false;
        /// <summary>
        /// 
        /// </summary>
        public static Model.Track Dequeue()
        {
            return trackQueue.Dequeue();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackID"></param>
        /// <param name="playlistID"></param>
        public static void SetQueue(int trackID, int playlistID)
        {
            Playlist PlaylistController = new Playlist();
            Model.Playlist p = PlaylistController.GetPlaylist(playlistID);

            Track TrackController = new Track();
            Model.Track T = TrackController.GetTrack(trackID);

            string query = "";
            if (p.playlist_type == Model.PlaylistType.Album || p.playlist_type == Model.PlaylistType.UserPlaylists)
            {
                query = $"SELECT title,listens,languageID,duration,date_created,file_path,image_path FROM track WHERE IN (SELECT trackID FROM playlist_track WHERE playlistID = {playlistID}){(ShuffleEnabled? "ORDER BY RAND()":"")}";
            }
            if (p.playlist_type == Model.PlaylistType.SingleTracks)
            {
                query = $"SELECT title,listens,languageID,duration,date_created,file_path,image_path FROM track WHERE IN (SELECT trackid FROM track_genre WHERE genreID = {T.Genres[0]}) ORDER BY RAND()";
            }
            getTracksFromDB(query);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        private static void getTracksFromDB(string query)
        {
            DBConnection.OpenConnection();
            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);
            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.Track track = new Model.Track();
                        track.Title = reader["title"].ToString();
                        track.Listens = (int)reader["listens"];
                        track.LanguageID = (int)reader["languageID"];
                        track.Duration = (int)reader["duration"];
                        track.Date_Created = (DateTime)reader["date_created"];
                        track.File_path = reader["file_path"].ToString();
                        trackQueue.Enqueue(track);
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                }
            }
            DBConnection.CloseConnection();
        }
    }
}
