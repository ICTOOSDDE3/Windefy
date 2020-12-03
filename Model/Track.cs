﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public int TrackID { get; set; }
        public string Title { get; set; }
        public int Listens { get; set; }
        public int LanguageID { get; set; }
        public int Duration { get; set; }
        public DateTime Date_Created { get; set; }
        public List<Artist> Artists { get; set; }
        public List<string> Genres { get; set; }
        public string File_path { get; set; }
        public string Image_path { get; set; }
    }
}