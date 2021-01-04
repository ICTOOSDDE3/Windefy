using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_Favourite_AddFavourite
    {
        [SetUp]
        public void Init()
        {
            Controller.DBConnection.Initialize();
            Controller.DBConnection.OpenConnection();
            Model.User.UserID = 4;
            Controller.Favourite.RemoveFavouriteArtist(1);
            Controller.Favourite.RemoveFavouritePlaylist(1);
            Controller.DBConnection.CloseConnection();
        }

        [Test]
        public void AddFavouriteArtist()
        {
            Controller.DBConnection.OpenConnection();
            Controller.Favourite.AddFavouriteArtist(1);
            Assert.IsTrue(Controller.Favourite.IsFavouriteArtist(1));
            Controller.DBConnection.CloseConnection();
        }

        [Test]
        public void AddFavouritePlaylist()
        {
            Controller.DBConnection.OpenConnection();
            Controller.Favourite.AddFavouritePlaylist(1);
            Assert.IsTrue(Controller.Favourite.IsFavouritePlaylist(1));
            Controller.DBConnection.CloseConnection();
        }
    }
}
