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
        public string Associated_Labels { get; set; }
        public string Location { get; set; }
        public DateTime active_year_begin { get; set; }
        public DateTime active_year_end { get; set; }

        public Artist(int artistID, string name)
        {
            ArtistID = artistID;
            Name = name;
        }
        public Artist()
        {
        }
    }
}