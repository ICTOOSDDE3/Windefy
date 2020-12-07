using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
namespace Controller
{
    public static class TrackQueue
    {
        public static Queue<int> trackQueue = new Queue<int>();
        public static bool ShuffleEnabled = false;
        /// <summary>
        /// 
        /// </summary>
        public static int Dequeue()
        {
            //if(trackQueue.Count > 0) return trackQueue.Dequeue();
            //else { return false; }
            return trackQueue.Dequeue();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackID"></param>
        /// <param name="playlistID"></param>
        public static void SetQueue(int trackID, int playlistID)
        {
            //Playlist PlaylistController = new Playlist();
            //Model.Playlist p = PlaylistController.GetPlaylist(playlistID);
            Model.Playlist p = new Model.Playlist(playlistID,"test",DateTime.Now,100000,0,"test",true,1);
            Track TrackController = new Track();
            var genres = TrackController.GetGenres(trackID);

            string query = "";
            if (p.playlist_type == Model.PlaylistType.Album || p.playlist_type == Model.PlaylistType.UserPlaylists)
            {
                query = $"SELECT trackID FROM playlist_track WHERE playlistID = {playlistID} AND trackID != {trackID} {(ShuffleEnabled ? "ORDER BY RAND()" : "")}";
            }
            if (p.playlist_type == Model.PlaylistType.SingleTracks)
            {
                query = $"SELECT TOP 10 T.trackID FROM track as T JOIN track_genre as TG ON T.trackID = TG.trackID WHERE TG.genreID IN (SELECT genreID  FROM genre WHERE genre.name = '{genres[0]}')  ORDER BY NEWID()";
            }
            trackQueue = TrackController.GetTracksToQueue(query);
            foreach(int item in trackQueue)
            {
                Trace.WriteLine(item);
            }
        }
    }
}
