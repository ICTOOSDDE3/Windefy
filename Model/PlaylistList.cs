using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlaylistList
    {
        private List<Playlist> playlists = new List<Playlist>();

        public void AddPlaylistToList(Playlist playlist)
        {
            playlists.Add(playlist);
        }
    }
}
