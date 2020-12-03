using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class Controller_Track_GetTrack
    {
        Controller.Track controller;
        [SetUp]
        public void Init()
        {
            controller = new Controller.Track();
        }
        /// <summary>
        /// This test checks whether the function returns a track object when you use an existing trackID as parameter
        /// </summary>
        [TestCase(210)]
        [TestCase(211)]
        [TestCase(212)]
        [Test]
        public void GetExistingTrack(int trackID)
        {
            var track = controller.GetTrack(trackID);
            Assert.IsNotNull(track);
        }
        /// <summary>
        /// This test checks whether the function returns a track object when you use an non existing ArtistID as parameter
        /// </summary>
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [Test]
        public void GetNonExistingTrack(int trackID)
        {
            var track = controller.GetTrack(trackID);
            Assert.IsNull(track);
        }
    }
}
