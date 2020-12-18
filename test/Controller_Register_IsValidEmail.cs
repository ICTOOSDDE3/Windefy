using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class Controller_Register_IsValidEmail
    {
        Register register = new Register();
        [Test]
        // checks if email adres matches the naming conventions returns true
        public void IsValidEmail_MatchesNamingConventions_ReturnsTrue()
        {
            bool validEmail = register.IsValidEmail("a@gmail.nl");
            Assert.IsTrue(validEmail);
        }

        [Test]
        // checks if email adres matches the naming conventions returns false
        public void IsValidEmail_DoesNotMatchNamingConventions_ReturnsFalse()
        {
            bool invalidEmail = register.IsValidEmail("aaaaaa");
            Assert.IsFalse(invalidEmail);
        }
    }
}
