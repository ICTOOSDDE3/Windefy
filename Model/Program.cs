using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using Controller;
using System.Threading;
using System.Data.SqlClient;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {

            //Example of how to use DBConnection
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            string query = "SELECT Count(*) FROM member";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            


            int count = int.Parse(cmd.ExecuteScalar() + "");

            Console.WriteLine(count);

            

            DBConnection.CloseConnection();





        }
    }
}
