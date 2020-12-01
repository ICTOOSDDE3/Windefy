using NUnit.Framework;
using System.Collections.Generic;

namespace Test
{
    [TestFixture]
    class Controller_Artist_GetArtistsByList
    {
        Controller.Artist controller;
        [SetUp]
        public void Init()
        {
            controller = new Controller.Artist();
        }
        /// <summary>
        /// This test checks whether the function returns null when you use an empty list as parameter
        /// </summary>
        [Test]
        public void GetArtistsByEmptyList()
        {
            List<int> EmptyList = new List<int>();
            var artists = controller.GetArtistsByList(EmptyList);
            Assert.IsNull(artists);
        }
        /// <summary>
        ///  This test checks whether the function returns null when you use a list with non-existend Artist_IDs as parameter
        /// </summary>
        [Test]
        public void GetArtistsByList_NonExistingArtists()
        {
            List<int> List = new List<int>();
            List.Add(-1);
            List.Add(-2);
            var artists = controller.GetArtistsByList(List);
            Assert.IsNull(artists);
        }
        /// <summary>
        ///   This test checks whether the function returns a list with artists when you use a list with existing Artist_IDs as parameter
        /// </summary>
         [Test]
        public void GetArtistsByList_ExistingArtist()
        {
            List<int> List = new List<int>();
            List.Add(1);
            List.Add(2);
            var artists = controller.GetArtistsByList(List);
            Assert.IsTrue(artists is List<Model.Artist>);
            Assert.That(artists, Has.Count.GreaterThan(0));
        }
    }
}
