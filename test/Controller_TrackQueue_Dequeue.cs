using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_TrackQueue_Dequeue
    {
        [SetUp]
        public void Init()
        { 

        }
        [Test]
        public void Dequeue_ReturnsTrackID()
        {
            TrackQueue.trackQueue.Enqueue(211);
            TrackQueue.trackQueue.Enqueue(212);
            TrackQueue.trackQueue.Enqueue(213);

            Assert.Positive(TrackQueue.trackQueue.Dequeue());
        }
    }
}
