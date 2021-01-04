using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_Track_GetGenres
    {
        Track controller;
        [SetUp]
        public void Init()
        {
            controller = new Track();
            DBConnection.Initialize();
        }
        /// <summary>
        /// Tests GetGenres method with an ID of an existing Track
        /// </summary>
        /// <param name="artistID"></param>
        [TestCase(210)]
        [TestCase(211)]
        [TestCase(212)]
        [Test]
        public void GetGenresFromExistingTrack(int trackID)
        {
            var genres = controller.GetGenres(trackID);
            Assert.IsTrue(genres is List<string>);
            Assert.That(genres, Has.Count.GreaterThan(0));
        }
        /// <summary>
        /// Tests GetGenres method with an ID of an non existing track
        /// </summary>
        /// <param name="trackID"></param>
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [Test]
        public void GetGenresFromNonExistingTrack(int trackID)
        {
            var genres = controller.GetGenres(trackID);
            Assert.IsNull(genres);
        }
    }
}
