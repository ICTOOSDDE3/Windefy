using Controller;
using NUnit.Framework;
using System;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_TrackHistory_InsertIntoHistory
    {
        [Test]
        public void InsertTrackWithID2IntoHistory()
        {
            DBConnection.Initialize();
            Model.User.UserID = 15;

            TrackHistory.InsertToHistory(2);

            DBConnection.OpenConnection();

            string GetLatestHistoryTrackQuerry = $"SELECT TOP 1 trackID FROM track_history WHERE userID = {Model.User.UserID} ORDER BY date_time DESC";

            SqlCommand cmdHistoryGet = new SqlCommand(GetLatestHistoryTrackQuerry, DBConnection.Connection);

            int trackID = Convert.ToInt32(cmdHistoryGet.ExecuteScalar().ToString());

            DBConnection.CloseConnection();

            Assert.AreEqual(2, trackID);

            DeleteInsert();
        }

        public void DeleteInsert()
        {
            DBConnection.OpenConnection();

            string DeleteLatestHistoryTrackQuery = $"DELETE FROM track_history WHERE date_time = (SELECT TOP 1 date_time FROM track_history WHERE userID = {Model.User.UserID} ORDER BY date_time DESC)";

            SqlCommand cmdHistoryDelete = new SqlCommand(DeleteLatestHistoryTrackQuery, DBConnection.Connection);

            cmdHistoryDelete.ExecuteScalar();

            DBConnection.CloseConnection();
        }
    }
}