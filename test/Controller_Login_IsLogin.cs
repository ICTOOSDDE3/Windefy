using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;

namespace Test
{
    [TestFixture]
    class Controller_Login_IsLogin
    {
        Login login = new Login();
        [Test]
        //check if user is able to login this should return true
        public void IsLogin_InputExistsInDataBase_ReturnsTrue()
        {
            bool userExists = login.IsLogin("a@a.nl", "a");
            Assert.IsTrue(userExists);
        }

        [Test]
        //check if user is able to login this should return false
        public void IsLogin_InputNonExistingInDataBase_ReturnsFalse()
        {
            bool userExists = login.IsLogin("123@123.nl", "a");
            Assert.IsFalse(userExists);
        }
    }
}
