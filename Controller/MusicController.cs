using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public static class MusicController
    {
        public static List<string> MusicQueue { get; set; }
        public static int Current { get; set; }

        public static string GetSong(bool repeat)
        {
            if (repeat) return MusicQueue[Current];
            if (!repeat && MusicQueue.Count < Current + 1)
            {
                Current++;
                return MusicQueue[Current];
            }
            return null;
        }
        public static void SetSong(string song)
        {

            MusicQueue.Add(song);
        }
    }
}
