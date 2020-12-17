using Controller;
using System.Collections.Generic;

namespace View.ViewModels
{
    public class HistoryViewModel
    {
        public List<TrackInfo> tracks { get; set; }
        private int playlistID { get; set; }

        public HistoryViewModel()
        {
            tracks = new List<TrackInfo>();
            List<Model.Track> modelTracks = TrackHistory.HistoryTracks();
            playlistID = TrackHistory.getHistoryPlaylistID();
            TrackToTrackInfo(modelTracks);
        }
        /// <summary>
        /// Makes an object TrackInfo of Model.Track
        /// </summary>
        /// <param name="modelTracks"></param>
        public void TrackToTrackInfo(List<Model.Track> modelTracks)
        {
            foreach (var item in modelTracks)
            {
                tracks.Add(new TrackInfo(item.Title, item.Duration, item.Image_path, item.TrackID, playlistID));
            }
        }
    }
}
