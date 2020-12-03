using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model
{
    public class Playlist
    {
        private int playlistID { get; set; }
        private string title { get; set; }
        private DateTime release_date { get; set; }
        private int listens { get; set; }
        private PlaylistType playlist_type {get; set;}
        private int information { get; set; }
        private bool is_Public { get; set; }
        private int ownerID { get; set; }

        public void createUserPlaylist(SqlCommand cmd)
        {

        }


    }
}
