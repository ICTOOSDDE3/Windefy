using Controller;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class Controller_Track_GetTrack
    {
        Track controller;
        [SetUp]
        public void Init()
        {
            controller = new Track();
            DBConnection.Initialize();
        }
        /// <summary>
        /// Tests GetTrack method with an ID of an existing Track
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
        /// Tests  GetTrack method with an ID of NonExistingTrack
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
