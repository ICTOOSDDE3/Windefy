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
        [Test]
        public void GetExistingArtist()
        {
            var artist = controller.GetArtist(127);
            Assert.IsNotNull(artist);
        }
        /// <summary>
        /// This test checks whether the function returns null when you use an non existing Artist ID as parameter
        /// </summary>
        [Test]
        public void GetNonExistingArtist()
        {
            var artist = controller.GetArtist(-1);
            Assert.IsNull(artist);
        }
    }
}
