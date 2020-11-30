using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Controller
{
    public class Program
    {
        static void Main(string[] args)
        {
            Playlist playlist = new Playlist();

            playlist.createUserPlaylist("Dylan zijn test ding", true);
        }
    }
}
