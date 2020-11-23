using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class Controller_Passwords
    {
        //Test to check if you can get an excisting password with the correct search term
        [Test]
        public void GetExcistingPW()
        {
            var password = Controller.Passwords.GetPassword("Nunit");

            Assert.AreEqual(password, "test");
        }
        //Test to check if you can get an non excisting password with the incorrect search term
        [Test]
        public void GetNonEsxcistingPW()
        {
            var password = Controller.Passwords.GetPassword("Wrong!");

            Assert.AreEqual(password, null);
        }
    }
}
