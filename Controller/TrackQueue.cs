//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Text;

//namespace Controller
//{
//    public static class TrackQueue
//    {
//        public static Queue<Track> trackQueue = new Queue<Track>();
//        /// <summary>
//        /// 
//        /// </summary>
//        public static void Enqueue()
//        {
//            //trackQueue.Enqueue();
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="trackID"></param>
//        /// <param name="playlistID"></param>
//        public static void SetQueue(int trackID, int playlistID)
//        {
//            Playlist PlaylistController = new Playlist();
//            Model.Playlist p = PlaylistController.GetPlaylist(playlistID);

//            Track TrackController = new Track();
//            Model.Track T = TrackController.GetTrack(trackID);

//            string query = "";
//            if (p.playlist_type == Model.PlaylistType.Album || p.playlist_type == Model.PlaylistType.UserPlaylists)
//            {
//                query = $"SELECT * FROM track WHERE IN (SELECT trackID FROM playlist_track WHERE playlistID = {playlistID})";
//            }
//            if (p.playlist_type == Model.PlaylistType.SingleTracks)
//            {
//                query = $"SELECT * FROM track WHERE IN (SELECT trackid FROM track_genre WHERE genreID = {T.Genres[0]}) ORDER BY RAND()";
//            }
//            getTracksFromDB(query);

//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="query"></param>
//        private static void getTracksFromDB(string query)
//        {
//            DBConnection.OpenConnection();
//            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);
//            using (SqlDataReader reader = oCmd.ExecuteReader())
//            {
//                if (reader.HasRows)
//                {
//                    while (reader.Read())
//                    {

//                    }
//                }
//                else
//                {
//                    DBConnection.CloseConnection();
//                }
//            }
//        }
//    }
//}
