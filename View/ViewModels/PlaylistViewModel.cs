using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace View.ViewModels
{
    public class PlaylistViewModel
    {
        public Model.Playlist Playlist { get; set; }
        public int PlaylistID { get; set; }

        public PlaylistViewModel(int playlistID)
        {
            Playlist PlaylistController = new Playlist();
            PlaylistID = playlistID;
            Playlist = PlaylistController.GetPlaylistData(playlistID);
            Playlist.tracks = PlaylistController.FillPlaylist(playlistID);
        }

        
    }
}
