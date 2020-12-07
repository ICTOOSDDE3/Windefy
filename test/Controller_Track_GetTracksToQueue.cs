using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_Track_GetTracksToQueue
    {
        Controller.Track controller;
        [SetUp]
        public void Init()
        {
            controller = new Controller.Track();
            DBConnection.Initialize();
        }
        [TestCase("SELECT trackID FROM playlist_track WHERE playlistID = -1 ")]
        [Test]
        public void GetTracksToQueue_QueryWhereZeroTracksFound(string query)
        {
            var tracks = controller.GetTracksToQueue(query);
            Assert.IsNull(tracks);
        }
        [TestCase("SELECT trackID FROM playlist_track WHERE playlistID = 84 ")]
        [Test]
        public void GetTracksToQueue_QueryWhereTracksFound(string query)
        {
            var tracks = controller.GetTracksToQueue(query);
            Assert.IsTrue(tracks is Queue<int>);
            Assert.That(tracks, Has.Count.GreaterThan(0));
        }
          

    }
}
