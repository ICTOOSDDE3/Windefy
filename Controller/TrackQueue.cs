//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Text;

//namespace Controller
//{
//    public static class TrackQueue
//    {
//        public static Queue<int> trackQueue = new Queue<int>();
//        public static bool ShuffleEnabled = false;
//        /// <summary>
//        /// 
//        /// </summary>
//        public static Model.Track Dequeue()
//        {
//            Track TrackController = new Track();
//            return TrackController.GetTrack(trackQueue.Dequeue());
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
//            Model.Track T = TrackController.GetGenres(trackID);

//            string query = "";
//            if (p.playlist_type == Model.PlaylistType.Album || p.playlist_type == Model.PlaylistType.UserPlaylists)
//            {
//                query = $"SELECT trackID FROM playlist_track WHERE playlistID = {playlistID}){(ShuffleEnabled ? "ORDER BY RAND()" : "")}";
//            }
//            if (p.playlist_type == Model.PlaylistType.SingleTracks)
//            {
//                query = $"SELECT title,listens,languageID,duration,date_created,file_path,image_path FROM track WHERE IN (SELECT trackid FROM track_genre WHERE genreID = {T.Genres[0]}) ORDER BY RAND()";
//            }
//            trackQueue = TrackController.GetTracksToQueue(query);

//        }
//    }
//}
