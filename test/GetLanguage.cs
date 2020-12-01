using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class Model_User_GetLanguage
    {
        //Test to check if you can get the set Dutch language
        [Test]
        public void GetSetDutchLanguage()
        {

            Model.User.Language = 1;

            Assert.AreEqual(Model.User.GetLanguage(), "Dutch");
        }
        //Test to check if you can get a set and unset password as English
        [Test]
        public void GetNonSetLanguage()
        {
            Model.User.Language = 0;

            Assert.AreEqual(Model.User.GetLanguage(), "English");
        }
    }
}
