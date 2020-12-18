using Controller;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_Playlist_FillPlaylist
    {

        //Test if you can fill a playlist with tracks
        [Test]
        public void FillPlaylistData()
        {
            Model.User.UserID = 4;

            Playlist testPlaylist = new Playlist();

            var playlist = testPlaylist.FillPlaylist(1);

            Assert.AreEqual(3, playlist.Count);

        }
    }
}
