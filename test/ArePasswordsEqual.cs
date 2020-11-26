using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    [TestFixture]
    class ArePasswordsEqual
    {
        Controller.Register register = new Controller.Register();
        [Test]
        // test if passwords are equal should return false
        public void ArePasswordsEqual_SimilarPasswords_ReturnsFalse()
        {
            string password1 = "password";
            string password2 = "password2";

            var isNotEqual = register.ArePasswordsEqual(password1, password2);
            Assert.AreEqual(isNotEqual, false);
        }

        [Test]
        // test if passwords are equal should return true
        public void ArePasswordsEqual_SimilarPasswords_ReturnsTrue()
        {
            string password1 = "password";
            string password2 = "password";

            var isNotEqual = register.ArePasswordsEqual(password1, password2);
            Assert.AreEqual(isNotEqual, true);
        }
    }
}
