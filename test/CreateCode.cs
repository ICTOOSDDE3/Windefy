using Controller;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class CreateCode
    {
        Register register = new Register();

        [Test]
        // checks if createCode returns a string with a verification code in it
        public void CreateCode_CreatesVerificationCode_returnsString()
        {
            string code = register.CreateCode();
            Assert.IsInstanceOf(typeof(string), code);
        }

        [Test]
        // checks if code length is same as code length that is given
        public void CreateCode_ChecksIfCodeIsEqualLength_ReturnsTrue()
        {
            string code = register.CreateCode();
            bool isTrue = (code.Length == 10);
            Assert.IsTrue(isTrue);
        }
    }
}
