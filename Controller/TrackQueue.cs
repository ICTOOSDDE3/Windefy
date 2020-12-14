﻿using System;
using System.Collections.Generic;
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
        /// <returns></returns>
        public static int Dequeue()
        {
            if (trackQueue.Count > 0) return trackQueue.Dequeue();
            else return -1;
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
            Model.Playlist p = new Model.Playlist(playlistID, "test", DateTime.Now, 100000, 0, "test", true, 1);
            Trace.WriteLine("Test");
            if (p != null)
            {
                Trace.WriteLine("p != null");
                Track TrackController = new Track();
                if (p.playlist_type == Model.PlaylistType.Album || p.playlist_type == Model.PlaylistType.UserPlaylists)
                {
                    var playlist_query = $"SELECT trackID FROM playlist_track WHERE playlistID = {playlistID} AND trackID != {trackID} {(ShuffleEnabled ? "ORDER BY RAND()" : "")}";
                    trackQueue = TrackController.GetTracksToQueue(playlist_query);
                }
                if (p.playlist_type == Model.PlaylistType.SingleTracks)
                {
                    var genres = TrackController.GetGenres(trackID);
                    if (genres != null)
                    {
                       var single_query = $"SELECT TOP 10 T.trackID FROM track as T JOIN track_genre as TG ON T.trackID = TG.trackID WHERE TG.genreID IN (SELECT genreID  FROM genre WHERE genre.name = '{genres[0]}')  ORDER BY NEWID()";
                        trackQueue = TrackController.GetTracksToQueue(single_query);
                    }
                }
            }
        }
        public static void SetQueue(int trackID)
        {
            bool RemoveFromQueue = true;
            while (RemoveFromQueue && trackQueue.Count > 0)
            {
                var CurrentItem = trackQueue.Dequeue();
                if(CurrentItem == trackID)
                {
                    RemoveFromQueue = false;
                }
            }
        }
    }
}
