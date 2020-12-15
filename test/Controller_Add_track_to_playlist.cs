using Controller;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_Add_track_to_playlist
    {
        AddMusicToPlaylist controller = new AddMusicToPlaylist();
        int playlistId = 0;

        [SetUp]
        public void Init()
        {
            DBConnection.Initialize();
            Playlist playlist = new Playlist();
            playlistId = playlist.CreateUserPlaylist("TESTaaa", false);
        }

        [Test]
        public void add_tracks_to_playplist()
        {
            controller.InsertToPlaylist(playlistId, 10);
            controller.InsertToPlaylist(playlistId, 140);

            DBConnection.OpenConnection();

            string query = $"SELECT count(*) FROM playlist_track WHERE playlistID = {playlistId}";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            int total = (int)cmd.ExecuteScalar();

            DBConnection.CloseConnection();

            Assert.AreEqual(total, 2);
        }

        [TearDown]
        public void delete()
        {
            DBConnection.OpenConnection();

            string query = $"DELETE FROM playlist_track WHERE playlistID = {playlistId}";
            string query2 = $"DELETE FROM playlist WHERE playlistID = {playlistId}";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            cmd.ExecuteNonQuery();

            SqlCommand cmd2 = new SqlCommand(query2, DBConnection.Connection);
            cmd2.ExecuteNonQuery();

            DBConnection.CloseConnection();
        }
    }
}
