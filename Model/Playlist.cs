using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Model
{
    public class Playlist
    {
        public Playlist(int playlistID, string title, DateTime release_date, int listens, int playlist_type, string information, bool is_Public, int ownerID)
        {
            this.playlistID = playlistID;
            this.title = title;
            this.release_date = release_date;
            this.listens = listens;
            this.playlist_type = (PlaylistType)playlist_type;
            this.information = information;
            this.is_Public = is_Public;
            this.ownerID = ownerID;
        }
      
        public int playlistID { get; set; }
        public string title { get; set; }
        public DateTime release_date { get; set; }
        public int listens { get; set; }
        public PlaylistType playlist_type {get; set;}
        public string information { get; set; }
        public bool is_Public { get; set; }
        public int ownerID { get; set; }

        
    }
}
