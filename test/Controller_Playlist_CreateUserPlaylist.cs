using Controller;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_Playlist_CreateUserPlaylist
    {
        //Test if you can create a private playlist
        [Test]
        public void CreatePrivatePlaylist()
        {
           
            string PlaylistName = "Nunit private playlist";
            Playlist playlist = new Playlist();

            playlist.CreateUserPlaylist(PlaylistName, false);

            DBConnection.OpenConnection();
            //Build the query
            string query = $"SELECT title FROM playlist WHERE playlistID = (SELECT max(playlistID) FROM playlist)";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            var result = cmd.ExecuteScalar();

            string query2 = $"SELECT is_public FROM playlist WHERE playlistID = (SELECT max(playlistID) FROM playlist)";

            //Prepare the query
            SqlCommand cmd2 = new SqlCommand(query2, DBConnection.Connection);

            var result2 = cmd2.ExecuteScalar();
            DBConnection.CloseConnection();

            Assert.AreEqual(PlaylistName, result);
            Assert.AreEqual(false, result2);

        }
        //Test if you can create a public playlist
        [Test]
        public void CreatePublicPlaylist()
        {
            string PlaylistName = "Nunit private playlist";
            Playlist playlist = new Playlist();

            playlist.CreateUserPlaylist(PlaylistName, true);

            DBConnection.OpenConnection();
            //Build the query
            string query = $"SELECT title FROM playlist WHERE playlistID = (SELECT max(playlistID) FROM playlist)";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            var result = cmd.ExecuteScalar();

            string query2 = $"SELECT is_public FROM playlist WHERE playlistID = (SELECT max(playlistID) FROM playlist)";

            //Prepare the query
            SqlCommand cmd2 = new SqlCommand(query2, DBConnection.Connection);

            var result2 = cmd2.ExecuteScalar();
            DBConnection.CloseConnection();

            Assert.AreEqual(PlaylistName, result);
            Assert.AreEqual(true, result2);
        }
    }
}
