using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;

namespace Test
{
    [TestFixture]
    class IsEmailUnique
    {
        Register register = new Controller.Register();

        // email doesn't exist in the database, the email is unique
        [Test]
        public void IsEmailUnique_UniqueEmail_True()
        {
            DBConnection.Initialize();
            bool unique = register.IsEmailUnique("123@123.nl");
            Assert.IsTrue(unique);
        }

        //email exists in the database, the email isn't unique
        [Test]
        public void IsEmailUnique_NonUniqueEmail_False()
        {
            DBConnection.Initialize();
            bool unique = register.IsEmailUnique("a@a.nl");
            Assert.IsFalse(unique);
        }
    }
}
