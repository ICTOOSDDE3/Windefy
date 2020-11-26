using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class Controller_Track
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
        public void GetNonExcistingTrack()
        {
            var track = controller.GetTrack(-1);
            Assert.IsNull(track);
        }
    }
}
