using MySql.Data.MySqlClient;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace Controller
{
    public static class DBConnection
    {
        private static ForwardedPortLocal PortFwld = new ForwardedPortLocal("127.0.0.1", 1433, "127.0.0.1", 1433);
        private static PasswordConnectionInfo ConnectionInfo = new PasswordConnectionInfo("145.44.235.109", "student", Passwords.GetPassword("DB"));
        public static SqlConnection Connection
        {
            get;
            set;
        }

        public static void Initialize()
        {
            ConnectionInfo.Timeout = TimeSpan.FromSeconds(30);

            var client = new SshClient(ConnectionInfo);

            try
            {
                Console.WriteLine("Trying SSH connection...");
                client.Connect();
                if (client.IsConnected)
                {
                    Console.WriteLine("SSH connection is active: {0}", client.ConnectionInfo.ToString());
                }
                else
                {
                    Console.WriteLine("SSH connection has failed: {0}", client.ConnectionInfo.ToString());
                }

                Console.WriteLine("\r\nTrying port forwarding...");

                client.AddForwardedPort(PortFwld);
                PortFwld.Start();
                if (PortFwld.IsStarted)
                {
                    Console.WriteLine("Port forwarded: {0}", PortFwld.ToString());
                }
                else
                {
                    Console.WriteLine("Port forwarding has failed.");
                }

                Console.WriteLine("\r\nTrying database connection...");


            }
            catch (SshException e)
            {
                Console.WriteLine("SSH client connection error: {0}", e.Message);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine("Socket connection error: {0}", e.Message);
            }

            string connectionString;
            connectionString = $"Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = {Passwords.GetPassword("DB")};";

            Connection = new SqlConnection(connectionString);

            Console.WriteLine(Connection.State.ToString());
            CloseConnection();
        }

        //open connection to database
        public static bool OpenConnection()
        {
            try
            {
                Connection.Open();
                Console.WriteLine("MySQL connected.");
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;

                    default:
                        Console.WriteLine("Unhandled exception: {0}.", ex.Message);
                        break;

                }
                return false;
            }
        }

        //Close connection
        public static bool CloseConnection()
        {
            try
            {
                Connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



    }



}

