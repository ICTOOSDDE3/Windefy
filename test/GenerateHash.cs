using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class GenerateHash
    {
        Register register = new Register();

        [Test]
        // checks if it returns a string with a hash
        public void GenerateHash_GeneratesHashString_ChecksIfreturnsString()
        {
            string password = "password";
            string salt = "A&d#afjk1D2";
            string hash = register.GenerateHash(password,salt);
            Assert.IsInstanceOf(typeof(string), hash);
        }
    }
}
