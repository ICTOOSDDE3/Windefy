using System;
using Controller;
using System.Collections.Generic;
using System.Text;

namespace View.ViewModels
{
    public class Homepage
    {
        public List<TrackInfo> Tracks{ get; set; }
        private int PlaylistID { get; set; }

        public Homepage(int playlistID)
        {
            PlaylistID = playlistID;
            Tracks = new List<TrackInfo>();
            List<Model.Track> modelTracks = TrackHistory.PlaylistTracks();
            TrackToTrackInfo(modelTracks);
        }

        public void TrackToTrackInfo(List<Model.Track> modelTracks)
        {
            int count = 0;

            foreach (var item in modelTracks)
            {
                if (count < 10)
                {
                    count++;
                    Tracks.Add(new TrackInfo(item.Title, item.Duration, item.Image_path, item.TrackID, PlaylistID));
                }
                else
                {
                    return;
                }
            }
        }
    }
}
