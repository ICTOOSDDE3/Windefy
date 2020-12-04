using Controller;
using NUnit.Framework;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_SideBarList_SettAllPlaylistsFromUser
    {
        //Check if you can get all playlists from a user
        [Test]
        public void GetAllPlaylistsFromUser()
        {
            Model.User.UserID = 4;
            DBConnection.OpenConnection();
            //Build the query
            string query = $"SELECT COUNT(*) FROM playlist WHERE ownerID = 4";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            var result = cmd.ExecuteScalar();
            DBConnection.CloseConnection();

            SideBarList.SetAllPlaylistsFromUser();
            
            Assert.AreEqual(result, SideBarList.sideBarList.playlists.Count);

        }
    }
}
