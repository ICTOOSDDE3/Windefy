using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Controller;

namespace View.ViewModels
{
    public class Artist
    {
        public Model.Artist CurrentArtist { get; set; }
        public List<Model.Track> CurrentArtistSongs { get; set; } = new List< Model.Track>();
        public Artist(int id)
        {
            Controller.Artist ArtistController = new Controller.Artist();
            CurrentArtist = ArtistController.GetArtist(id);
            CurrentArtistSongs = ArtistController.GetArtistTracks(id);
        }
    }
}
