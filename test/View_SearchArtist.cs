using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using View.ViewModels;

namespace Test
{
    [TestFixture]
    public class View_SearchArtist
    {

        [Test]
        public void is_Correct_ArtistID()
        {
            DBConnection.Initialize();
            SearchArtistViewModel searchArtist = new SearchArtistViewModel("Taal");

            Assert.AreEqual(50, searchArtist.Items[0].ArtistID);
        }

    }
}
