using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_TrackHistory_HistoryTracks
    {
        [Test]
        public void GetHistoryTracksFromOwnerTest()
        {
            DBConnection.Initialize();

            Model.User.UserID = 17;

            TrackHistory.InsertToHistory(2);
            TrackHistory.InsertToHistory(207);
            TrackHistory.InsertToHistory(190);

            List<Model.Track> tracks = new List<Model.Track>();

            tracks.Add(new Track().GetTrack(190));
            tracks.Add(new Track().GetTrack(207));
            tracks.Add(new Track().GetTrack(2));

            List<Model.Track> result = TrackHistory.HistoryTracks();

            DeleteInsert();

            if (result.Count == tracks.Count)
            {
                    Assert.True(AreEqual(result, tracks));
            }
        }

        private bool AreEqual(List<Model.Track> result, List<Model.Track> tracks)
        {
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].TrackID != tracks[i].TrackID)
                {                   
                    return false;
                }
            }

            return true;
        }

        public void DeleteInsert()
        {
            DBConnection.OpenConnection();

            string DeleteLatestHistoryTrackQuery = $"DELETE FROM track_history WHERE userID = {Model.User.UserID}";

            SqlCommand cmdHistoryDelete = new SqlCommand(DeleteLatestHistoryTrackQuery, DBConnection.Connection);

            cmdHistoryDelete.ExecuteScalar();

            DBConnection.CloseConnection();
        }
    }
}
