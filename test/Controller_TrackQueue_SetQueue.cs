using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_TrackQueue_SetQueue
    {
        [SetUp]
        public void Init()
        {
            DBConnection.Initialize();
            TrackQueue.trackQueue.Clear();
        }
        /// <summary>
        /// Tests SetQueue function with 2 parameters with existing playlist
        /// </summary>
        /// <param name="trackid"></param>
        /// <param name="playlistID"></param>
        [Test]
        [TestCase(190, 84)]
        [TestCase(207, 84)]
        
        public void SetQueue_Playlist_ExistingPlaylist(int trackid, int playlistID)
        {
            TrackQueue.SetQueue(trackid, playlistID);

            Assert.That(TrackQueue.trackQueue, Has.Count.GreaterThan(0));
            Assert.That(TrackQueue.PlayListID != 0);
        }
        /// <summary>
        /// Tests SetQueue function with 2 parameters with not existing playlist
        /// </summary>
        /// <param name="trackID"></param>
        /// <param name="playlistID"></param>
        [Test]
        [TestCase(10, -1)]
        public void SetQueue_Playlist_NotExistingPlaylist(int trackID,int playlistID)
        {
            TrackQueue.SetQueue(trackID, playlistID);
            Assert.That(TrackQueue.trackQueue, Has.Count.EqualTo(0));
            //Assert.That(TrackQueue.PlayListID, Is.EqualTo(0));
        }
        /// <summary>
        /// Tests SetQueue method with 1 parameter with a filled queue
        /// </summary>
        [Test]
        public void SetQueue_Queue_FilledQueue()
        {
            TrackQueue.trackQueue.Enqueue(1);
            TrackQueue.trackQueue.Enqueue(2);
            TrackQueue.trackQueue.Enqueue(3);
            TrackQueue.trackQueue.Enqueue(4);
            int QueueCount = TrackQueue.trackQueue.Count;
            TrackQueue.SetQueue(2);
            Assert.That(TrackQueue.trackQueue, Has.Count.LessThan(QueueCount));
        }
        /// <summary>
        /// Tests SetQueue Method with 1 parameter with an empty Queue
        /// </summary>
        [Test]
        public void SetQueue_Queue_EmptyQueue()
        {
            int QueueCount = TrackQueue.trackQueue.Count;
            TrackQueue.SetQueue(2);
            Assert.LessOrEqual(TrackQueue.trackQueue.Count,QueueCount);
        }


    }
}
