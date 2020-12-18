using System;
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
        public double Duration { get; set; }
        public string DurationView { get; set; }
        public DateTime Date_Created { get; set; }
        public int NumberID { get; set; }
        public List<Artist> Artists { get; set; }
        public List<string> Genres { get; set; }
        public string File_path { get; set; }
        public string Image_path { get; set; }
        public bool Liked { get; set; }


        public Track(int trackID, string title, int durationInSec, bool liked)
        {
            TrackID = trackID;
            Title = title;
            Duration = durationInSec / 60;
            var tempDur = Math.Round((double)durationInSec / 60, 2, MidpointRounding.AwayFromZero);
            DurationView = string.Format("{0:00.00}", tempDur).Replace(".", ":");
            Artists = new List<Artist>();

            this.Liked = liked;
        }

        public Track()
        {

        }
    }
}