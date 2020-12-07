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
        Controller.Track controller;
        [SetUp]
        public void Init()
        {
            controller = new Controller.Track();
            DBConnection.Initialize();
        }
        /// <summary>
        /// This test checks whether the function returns a List with strings when you use an existing Track ID as parameter
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

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [Test]
        // This test checks whether the function returns null when you use an non-existed Track ID as parameter
        public void GetGenresFromNonExistingTrack(int trackID)
        {
            var genres = controller.GetGenres(trackID);
            Assert.IsNull(genres);
        }
    }
}
