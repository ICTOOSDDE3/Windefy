using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public static class TrackHistory
    {
        public static Stack<int> trackHistory = new Stack<int>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackID"></param>
        /// <param name="playlistID"></param>
        public static void SetHistory(int trackID)
        {
            trackHistory.Push(trackID);
        } 
    }
}
