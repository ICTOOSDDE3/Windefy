using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using View.ViewModels;

namespace Test
{
    [TestFixture]
    public class View_SearchSong
    {

        [Test]
        public void is_Correct_trackID()
        {
            DBConnection.Initialize();

            SearchSongModel searchSong = new SearchSongModel("Blackout 2");

            Assert.AreEqual(148, searchSong.items[0].TrackID);
        }

    }
}
