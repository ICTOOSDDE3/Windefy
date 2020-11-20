using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using Controller;
using System.Threading;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {

            DBConnection.Initialize();
            DBConnection.Insert("member", new string[] { "name"}, new string[] { "test1"} );
            //DBConnection.Initialize();
            //Console.WriteLine(DBConnection.Count("member"));

            
        }
    }
}
