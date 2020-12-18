using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;
namespace Test
{
    [TestFixture]
    class Controller_TrackQueue_Shuffle
    {
        /// <summary>
        /// Tests Shuffle method with a filled Queue
        /// </summary>
        [Test]
        public void Shuffle_FilledQueue()
        {
            Random random = new Random();
            while (TrackQueue.trackQueue.Count < 10)
            {
                TrackQueue.trackQueue.Enqueue(random.Next(1000));
            }
            var firstqueue = TrackQueue.trackQueue;
            TrackQueue.Shuffle();
            var currentqueue = TrackQueue.trackQueue;
            Assert.That(firstqueue, Is.Not.EqualTo(currentqueue));
        }
        /// <summary>
        /// Tests Shuffle method with an empty Queue
        /// </summary>
        [Test]
        public void Shuffle_EmptyQueue()
        {
            var firstqueue = TrackQueue.trackQueue;
            TrackQueue.Shuffle();
            var currentqueue = TrackQueue.trackQueue;
            Assert.That(currentqueue, Has.Count.EqualTo(firstqueue.Count));
        }
    }
}
