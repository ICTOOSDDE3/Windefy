using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;

namespace Test
{
    [TestFixture]
    class Controller_Register_GenerateSalt
    {
        Register register = new Register();

        [Test]
        // checks if it returns a string with a salt code
        public void GenerateSalt_GeneratesSaltString_ChecksIfreturnsString()
        {
            string salt = register.GenerateSalt(20);
            Assert.IsInstanceOf(typeof(string), salt);
        }
    }
}
