using Controller;
using System.Collections.Generic;

namespace View.ViewModels
{
    public class HistoryViewModel
    {
        public List<Model.Track> tracks { get; set; }
        private int playlistID { get; set; }

        public HistoryViewModel()
        {
            tracks = new List<Model.Track>();
            tracks = TrackHistory.HistoryTracks();
            playlistID = TrackHistory.GetHistoryPlaylistID();
        }
       
    }
}
