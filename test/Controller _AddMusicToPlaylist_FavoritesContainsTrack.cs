using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;

namespace Test
{
    [TestFixture]
    class Controller__AddMusicToPlaylist_FavoritesContainsTrack
    {
        AddMusicToPlaylist playlist = new AddMusicToPlaylist();   
        [Test]
        public void FavoritesContainsTrack_ChecksIfFavoritesHasThisTrack_ReturnsFalse()
        {
            bool contains = playlist.FavoritesContainsTrack(-1);
            Assert.IsFalse(contains);
        }
    }
}
