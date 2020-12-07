using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public static class TrackClicked
    {
        public static int TrackID { get; set; }
        public static LinkedList<int> QueueTrackIDs = new LinkedList<int>();
    }
}
