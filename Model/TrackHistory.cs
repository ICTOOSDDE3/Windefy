using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public static class TrackHistory
    {
        public static int PlaylistID { get; set; }
        public static Stack<int> trackHistory = new Stack<int>();
        public static int UserID = User.UserID;
    }
}
