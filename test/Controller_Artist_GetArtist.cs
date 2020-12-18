using Controller;
using NUnit.Framework;
namespace Test
{
    [TestFixture]
    class Controller_Artist_GetArtist
    {
        Artist controller;
        [SetUp]
        public void Init()
        {
            controller = new Artist();
            DBConnection.Initialize();
        }
        /// <summary>
        /// Tests GetArtist method with an ID of an existing Artist
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
        /// Tests  GetArtist method with an ID of NonExistingArtist
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
