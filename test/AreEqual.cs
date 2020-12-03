using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class AreEqual
    {
        Login login = new Login();

        [Test]
        // checks if hashed password in database is equal to the input returns true
        public void AreEqual_UserInputMatchesDatabasePassword_ReturnsTrue()
        {
            bool passwordsMatch = login.IsEqual("a", "qWN7A5d9BWW+kyVlUCjCkkyJi9Is8Yv6O45AQlGSoL0=", "llxNfV0hdqTtgJ4aHezVL/WXaeI=");
            Assert.IsTrue(passwordsMatch);
        }

        [Test]
        // checks if hashed password in database is equal to the input returns false
        public void AreEqual_UserInputDatabasePassword_ReturnsFalse()
        {
            bool passwordsMatch = login.IsEqual("123", "qWN7A5d9BWW+kyVlUCjCkkyJi9Is8Yv6O45AQlGSoL0=", "llxNfV0hdqTtgJ4aHezVL/WXaeI=");
            Assert.IsFalse(passwordsMatch);
        }
    }
}
