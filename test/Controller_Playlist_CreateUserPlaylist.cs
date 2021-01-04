using Controller;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_Playlist_CreateUserPlaylist
    {
        int privatePlaylistId = 0;
        int publicPlaylistId = 0;

        //Test if you can create a private playlist
        [Test]
        public void CreatePrivatePlaylist()
        {
           
            string PlaylistName = "Nunit private playlist";
            Playlist playlist = new Playlist();

            privatePlaylistId = playlist.CreateUserPlaylist(PlaylistName, false);

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

            publicPlaylistId = playlist.CreateUserPlaylist(PlaylistName, true);

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

        [TearDown]
        public void delete()
        {
            DBConnection.OpenConnection();

            string query = $"DELETE FROM playlist_track WHERE playlistID = {privatePlaylistId}";
            string query2 = $"DELETE FROM playlist WHERE playlistID = {privatePlaylistId}";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand(query2, DBConnection.Connection);
            cmd2.ExecuteNonQuery();

            string query3 = $"DELETE FROM playlist_track WHERE playlistID = {publicPlaylistId}";
            string query4 = $"DELETE FROM playlist WHERE playlistID = {publicPlaylistId}";

            SqlCommand cmd3 = new SqlCommand(query3, DBConnection.Connection);
            cmd3.ExecuteNonQuery();

            SqlCommand cmd4 = new SqlCommand(query4, DBConnection.Connection);
            cmd4.ExecuteNonQuery();

            DBConnection.CloseConnection();
        }
    }
}
