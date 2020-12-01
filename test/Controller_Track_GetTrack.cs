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
        //This test checks whether the function returns a track object when you use an existing trackID as parameter
        [Test]
        public void GetExistingTrack()
        {
            var track = controller.GetTrack(211);
            Assert.IsNotNull(track);
        }
        [Test]
        // This test checks whether the function returns a track object when you use an non existing ArtistID as parameter
        public void GetNonExistingTrack()
        {
            var track = controller.GetTrack(-1);
            Assert.IsNull(track);
        }
    }
}
