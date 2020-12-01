using NUnit.Framework;
namespace Test
{
    [TestFixture]
    class Controller_Artist_GetArtist
    {
        Controller.Artist controller;
        [SetUp]
        public void Init()
        {
            controller = new Controller.Artist();
        }
        /// <summary>
        /// This test checks whether the function returns a Artist object when you use an existing Artist ID as parameter
        /// </summary> 
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(127)]
        [Test]
        public void GetExistingArtist(int trackID)
        {
            var artist = controller.GetArtist(trackID);
            Assert.IsNotNull(artist);
        }
        /// <summary>
        /// This test checks whether the function returns null when you use an non existing Artist ID as parameter
        /// </summary>
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(-3)]
        [Test]
        public void GetNonExistingArtist(int trackID)
        {
            var artist = controller.GetArtist(trackID);
            Assert.IsNull(artist);
        }
    }
}
