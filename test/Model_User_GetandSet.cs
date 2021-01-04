using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class Model_User_GetandSet
    {
        //Test to check if you can set and get all properties
        [Test]
        public void GetSetProperties()
        {

            Model.User.Language = 1;
            Model.User.Email = "test@test.com";
            Model.User.Name = "Test";
            Model.User.UserID = 1;
            Model.User.Verified = true;

            Assert.AreEqual(Model.User.Language, 1);
            Assert.AreEqual(Model.User.Email, "test@test.com");
            Assert.AreEqual(Model.User.Name, "Test");
            Assert.AreEqual(Model.User.UserID, 1);
            Assert.AreEqual(Model.User.Verified, true);

        }
    }
}
