using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Artist
    {
        public int ArtistID { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public List<int> MemberIDs { get; set; }
        public string Label { get; set; }
        public string Location { get; set; }
        public DateTime active_year_begin { get; set; }
        public DateTime active_year_end { get; set; }
        //public List<Track> Tracks { get; set; }
        //public List<Album> Albums { get; set; }

        public Artist(int artistID, string name, string bio, List<int> memberIDs, string label, string location, DateTime active_year_begin, DateTime active_year_end)
        {
            ArtistID = artistID;
            Name = name;
            Bio = bio;
            MemberIDs = memberIDs;
            Label = label;
            Location = location;
            this.active_year_begin = active_year_begin;
            this.active_year_end = active_year_end;
        }
    }
}