using Controller;
using System.Collections.Generic;

namespace View.ViewModels
{
    public class HistoryViewModel
    {
        public List<Model.Track> Tracks { get; set; }
        private int PlaylistID { get; set; }

        public HistoryViewModel()
        {
            Tracks = new List<Model.Track>();
            Tracks = TrackHistory.HistoryTracks();
            PlaylistID = TrackHistory.GetHistoryPlaylistID();
        }
       
    }
}
