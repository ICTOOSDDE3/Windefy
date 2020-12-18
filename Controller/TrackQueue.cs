using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Controller
{
    public static class TrackQueue
    {
        public static Queue<int> trackQueue = new Queue<int>();
        public static int PlayListID = 0;

        /// <summary>
        /// Setting Queue for SearchResults/playlists and albums.
        /// </summary>
        /// <param name="trackID"></param>
        /// <param name="playlistID"></param>
        public static void SetQueue(int trackID, int playlistID)
        {
            Playlist PlaylistController = new Playlist();
            Model.Playlist p = PlaylistController.GetPlaylistData(playlistID);
            if (p != null)
            {
                Track TrackController = new Track();
                if (p.playlist_type == Model.PlaylistType.Album || p.playlist_type == Model.PlaylistType.UserPlaylists)
                {
                    var playlist_query = $"SELECT trackID FROM playlist_track WHERE playlistID = {playlistID} AND trackID != {trackID}";
                    trackQueue = TrackController.GetTracksToQueue(playlist_query);
                    PlayListID = playlistID;
                }
                if (p.playlist_type == Model.PlaylistType.SingleTracks)
                {
                    var genres = TrackController.GetGenres(trackID);
                    if (genres != null)
                    {
                        var single_query = $"SELECT TOP 10 T.trackID FROM track as T JOIN track_genre as TG ON T.trackID = TG.trackID WHERE TG.genreID IN (SELECT genreID  FROM genre WHERE genre.name = '{genres[0]}')  ORDER BY NEWID()";
                        trackQueue = TrackController.GetTracksToQueue(single_query);
                        PlayListID = playlistID;
                    }
                }
            }
        }
        /// <summary>
        /// Setting queue for QueuePage
        /// </summary>
        /// <param name="trackID"></param>
        public static void SetQueue(int trackID)
        {
            bool RemoveFromQueue = true;
            while (RemoveFromQueue && trackQueue.Count > 0)
            {
                var CurrentItem = trackQueue.Dequeue();
                if (CurrentItem == trackID)
                {
                    RemoveFromQueue = false;
                }
            }
        }
        /// <summary>
        /// Shuffles the Queue;
        /// </summary>
        public static void Shuffle()
        {
            if (trackQueue.Count > 0)
            {
                var rnd = new Random();
                var result = trackQueue.ToList().OrderBy(item => rnd.Next());
                trackQueue = new Queue<int>(result);
            }
        }
    }
}
