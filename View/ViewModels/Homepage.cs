using System;
using Controller;
using System.Collections.Generic;
using System.Text;

namespace View.ViewModels
{
    public class Homepage
    {
        public List<TrackInfo> tracks{ get; set; }
        private int playlistID { get; set; }

        public Homepage()
        {
            tracks = new List<TrackInfo>();
            List<Model.Track> modelTracks = TrackHistory.PlaylistTracks();
            playlistID = TrackHistory.getHistoryPlaylistID();
            TrackToTrackInfo(modelTracks);
        }

        public void TrackToTrackInfo(List<Model.Track> modelTracks)
        {
            int count = 0;

            foreach (var item in modelTracks)
            {
                if (count < 8)
                {
                    count++;
                    tracks.Add(new TrackInfo(item.Title, item.Duration, item.Image_path, item.TrackID, playlistID));
                }
                else
                {
                    return;
                }

            }
        }
    }
}
