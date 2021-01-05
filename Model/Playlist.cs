using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Model
{
    public class Playlist
    {
        public Playlist()
        {

        }
        public Playlist(int playlistID, string title, DateTime release_date, int listens, int playlist_type, string information, bool is_Public, int ownerID)
        {
            this.PlaylistID = playlistID;
            this.Title = title;
            this.ReleaseDate = release_date;
            this.Listens = listens;
            this.PlaylistType = (PlaylistType)playlist_type;
            this.Information = information;
            this.IsPublic = is_Public;
            this.OwnerID = ownerID;
        }
      
        public int PlaylistID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Listens { get; set; }
        public PlaylistType PlaylistType {get; set;}
        public string Information { get; set; }
        public bool IsPublic { get; set; }
        public int OwnerID { get; set; }
        public List<Track> Tracks { get; set; } = new List<Track>();

        
    }
}
