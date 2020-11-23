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
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);

            DBConnection.CloseConnection();





        }
    }
}
