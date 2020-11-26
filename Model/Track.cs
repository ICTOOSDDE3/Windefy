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
        public DateTime Date_Created { get; set; }
        public int NumberID { get; set; }
        public List<int> ArtistIDs { get; set; }
        public List<int> GenreIDs { get; set; } 
        public string File_path { get; set; }
    }
}
