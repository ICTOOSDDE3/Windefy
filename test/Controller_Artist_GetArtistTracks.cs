using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_Artist_GetArtistTracks
    {
        Artist controller;
        [SetUp]
        public void Init()
        {
            controller = new Artist();
            DBConnection.Initialize();
        }
        /// <summary>
        /// This test checks whether the function returns a List with Tracks when you use an existing Artist ID as parameter
        /// </summary>
        /// <param name="artistID"></param>
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(127)]
        [Test]
        public void GetTracksFromExistingArtist(int artistID)
        {
            var tracks = controller.GetArtistTracks(artistID);
            Assert.IsTrue(tracks is List<Model.Track>);
            Assert.That(tracks, Has.Count.GreaterThan(0));
        }

        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [Test]
        public void GetTracksFromNonExistingArtist(int artistID)
        {
            var tracks = controller.GetArtistTracks(artistID);
            Assert.IsNull(tracks);
        }


    }
}
