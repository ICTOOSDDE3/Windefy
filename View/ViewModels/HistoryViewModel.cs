using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Controller;

namespace View.ViewModels
{
    public class HistoryViewModel
    {
        public List<TrackInfo> trackHistory { get; set; }
        private int playlistID { get; set; }

        public HistoryViewModel()
        {
            trackHistory = new List<TrackInfo>();
            List<Model.Track> modelTracks = TrackHistory.PlaylistTracks();
            playlistID = TrackHistory.getHistoryPlaylistID();
            TrackToTrackInfo(modelTracks);
        }

        public void TrackToTrackInfo(List<Model.Track> modelTracks)
        {
            foreach (var item in modelTracks)
            {
                trackHistory.Add(new TrackInfo(item.Title, item.Duration, item.Image_path, item.TrackID, playlistID));
            }
        }
    }
}
