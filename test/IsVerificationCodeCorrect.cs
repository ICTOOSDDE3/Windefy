using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class IsVerificationCodeCorrect
    {
        Register register = new Register();

        [Test]
        // checks if verification code is the same as the code the user entered this returns true
        public void IsVerificationCode_EnteredSimilarCode_ReturnsTrue()
        {
            DBConnection.Initialize();
            bool codeIsEqual = register.IsVerificationCodeCorrect("abcd", "a@a.nl");
            Assert.IsTrue(codeIsEqual);
        }

        [Test]
        // checks if verification code is the same as the code the user entered this returns false
        public void IsVerificationCode_EnteredNonSimilarCode_ReturnsFalse()
        {
            DBConnection.Initialize();
            bool codeIsntEqual = register.IsVerificationCodeCorrect("a", "a@a.nl");
            Assert.IsFalse(codeIsntEqual);
        }
    }
}
