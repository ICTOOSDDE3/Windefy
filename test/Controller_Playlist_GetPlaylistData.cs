using Controller;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_Playlist_GetPlaylistData
    {

        //Test if you can get a playlist
        [Test]
        public void GetPlaylistData()
        {

            

            Playlist testPlaylist = new Playlist();

            var playlist = testPlaylist.GetPlaylistData(1);
            
            Assert.AreEqual("Nunit", playlist.title);

        }
    }
}
