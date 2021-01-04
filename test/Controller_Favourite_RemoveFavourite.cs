using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class Controller_Favourite_RemoveFavourite
    {
        [SetUp]
        public void Init()
        {
            Controller.DBConnection.Initialize();
            Controller.DBConnection.OpenConnection();
            Model.User.UserID = 4;
            Controller.Favourite.AddFavouriteArtist(1);
            Controller.Favourite.AddFavouritePlaylist(1);
            Controller.DBConnection.CloseConnection();
        }

        [Test]
        public void RemoveFavouriteArtist()
        {
            Controller.DBConnection.OpenConnection();
            Controller.Favourite.RemoveFavouriteArtist(1);
            Assert.IsTrue(!Controller.Favourite.IsFavouriteArtist(1));
            Controller.DBConnection.CloseConnection();
        }

        [Test]
        public void RemoveFavouritePlaylist()
        {
            Controller.DBConnection.OpenConnection();
            Controller.Favourite.RemoveFavouritePlaylist(1);
            Assert.IsTrue(!Controller.Favourite.IsFavouritePlaylist(1));
            Controller.DBConnection.CloseConnection();
        }
    }
}
