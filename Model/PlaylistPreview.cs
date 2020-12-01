using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlaylistPreview
    {
        // space for an image variable
        public int PlaylistId { get; set; }
        public string PlaylistTitle { get; set; }

        public PlaylistPreview(int playlistId, string playlistTitle)
        {
            PlaylistId = playlistId;
            PlaylistTitle = playlistTitle;
        }
    }
}
