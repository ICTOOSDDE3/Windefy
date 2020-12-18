using Controller;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class Controller_TrackHistory_GetHistoryPlaylistID
    {
        [Test]
        public void GetHistoryPlaylistID_FromDbWithUserIDs15and5()
        {
            DBConnection.Initialize();
            Model.User.UserID = 15;
            int result = TrackHistory.getHistoryPlaylistID();

            Assert.AreEqual(22978, result);
        }
    }
}
