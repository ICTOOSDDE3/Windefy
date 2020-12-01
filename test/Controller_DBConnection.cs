using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using System.Data.SqlClient;

namespace Test
{
    [TestFixture]
    class Controller_DBConnection
    {

        [Test]
        public void GetMemberName()
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            string query = "SELECT name FROM member WHERE memberID = 10";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            string result = cmd.ExecuteScalar().ToString();
            DBConnection.CloseConnection();
            Assert.AreEqual("Weasel Walter", result);
        }

    }
}
