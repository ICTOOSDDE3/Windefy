using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_Favourite_IsFavourite
    {
        [SetUp]
        public void Init()
        {
            Controller.DBConnection.Initialize();
            Controller.DBConnection.OpenConnection();
            Model.User.UserID = 4;
            Controller.Favourite.AddFavouritePlaylist(1);
            Controller.Favourite.AddFavouriteArtist(1);
            Controller.DBConnection.CloseConnection();
        }

        [Test]
        public void IsFavouriteArtist()
        {
            Controller.DBConnection.OpenConnection();
            Assert.IsTrue(Controller.Favourite.IsFavouriteArtist(1));
            Controller.DBConnection.CloseConnection();
        }

        [Test]
        public void IsFavouritePlaylist()
        {
            Controller.DBConnection.OpenConnection();
            Assert.IsTrue(Controller.Favourite.IsFavouriteArtist(1));
            Controller.DBConnection.CloseConnection();
        }
    }
}
