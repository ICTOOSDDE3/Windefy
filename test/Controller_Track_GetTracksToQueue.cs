﻿using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_Track_GetTracksToQueue
    {
        Track controller;
        [SetUp]
        public void Init()
        {
            controller = new Track();
            DBConnection.Initialize();
        }
        /// <summary>
        /// Tests GetTracksToQueue method with a Query where no tracks found.
        /// </summary>
        /// <param name="query"></param>
        [TestCase("SELECT trackID FROM playlist_track WHERE playlistID = -1 ")]
        [Test]
        public void GetTracksToQueue_QueryWhereZeroTracksFound(string query)
        {
            var tracks = controller.GetTracksToQueue(query);
            Assert.IsNull(tracks);
        }
        /// <summary>
        /// Tests GetTracksToQueue method with a Query where tracks found. 
        /// </summary>
        /// <param name="query"></param>
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
