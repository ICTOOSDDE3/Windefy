using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using View.ViewModels;

namespace Test
{
    [TestFixture]
    public class View_SearchAlbum
    {

        [Test]
        public void is_Correct_playlistID()
        {
            DBConnection.Initialize();
            SearchAlbumViewModel searchAlbum = new SearchAlbumViewModel("blue piano", true);

            Assert.AreEqual(84, searchAlbum.items[0].PlaylistID);
        }

    }
}
