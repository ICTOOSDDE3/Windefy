using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public string Title { get; set; }
        public int Listens { get; set; }
        public int LanguageID { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberID { get; set; }
        public List<int> ArtistIDs { get; set; }
        public List<int> GenreIDs { get; set; } 

        public Track(string title, int listens, int languageID, int duration, DateTime releaseDate, int numberID, List<int> artistIDs, List<int> genreIDs)
        {
            Title = title;
            Listens = listens;
            LanguageID = languageID;
            Duration = duration;
            ReleaseDate = releaseDate;
            NumberID = numberID;
            ArtistIDs = artistIDs;
            GenreIDs = genreIDs;
        }
    }
}
