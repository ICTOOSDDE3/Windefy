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
        [Test]
        public void GetExistingTrack()
        {
            var track = controller.GetTrack(211);
            Assert.IsNotNull(track);
        }
        [Test]
        public void GetNonExistingTrack()
        {
            var track = controller.GetTrack(-1);
            Assert.IsNull(track);
        }
    }
}
