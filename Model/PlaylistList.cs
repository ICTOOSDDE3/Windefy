using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlaylistList
    {

        public List<Playlist> Playlists { get; set; } = new List<Playlist>();

        public void AddPlaylistToList(Playlist playlist)
        {
            Playlists.Add(playlist);
        }
    }
}
