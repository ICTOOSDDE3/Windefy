﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public static class SingleTrackClicked
    {
        public static int TrackID { get; set; }
        public static LinkedList<int> QueueTrackIDs = new LinkedList<int>();
        public static bool TrackClicked { get; set; }
    }
}
