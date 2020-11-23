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
        private static PasswordConnectionInfo ConnectionInfo = new PasswordConnectionInfo("145.44.235.109", "student", "");
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
            connectionString = "Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = ;";

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

        //Insert statement
        /*public static void Insert(string table, string[] columns, string[] values)
        {
            int columnCount = columns.Length;
            int valuesCount = values.Length;

            //build the query using stringbuilder
            if (columnCount == valuesCount)
            {
                StringBuilder sb = new StringBuilder($"INSERT INTO { table }(");

                int count = 0;

                foreach (var column in columns)
                {
                    sb.Append(column);

                    count++;

                    if (count != columnCount)
                    {
                        sb.Append(", ");
                    }
                }

                count = 0;

                sb.Append(") VALUES (");

                foreach (var value in columns)
                {
                    sb.Append("@" + value);

                    count++;

                    if (valuesCount != count)
                    {
                        sb.Append(", ");
                    }
                }

                sb.Append(")");

                Console.WriteLine(sb.ToString());


                //open connection
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    //create command and assign the query and connection from the constructor
                    SqlCommand cmd = new SqlCommand(null, Connection);

                    cmd.CommandText = sb.ToString();


                    for (int i = 0; i < columnCount; i++)
                    {
                        if (int.TryParse(values[i], out _))
                        {
                            SqlParameter number = new SqlParameter("@" + columns[i], SqlDbType.Int, 0);

                            number.Value = values[i];

                            cmd.Parameters.Add(number);

                        } else
                        {
                            SqlParameter txt = new SqlParameter("@" + columns[i], SqlDbType.Text, 1000);

                            txt.Value = values[i];

                            cmd.Parameters.Add(txt);
                        }
                    }

                    //prepare the query
                    cmd.Prepare();
                    
                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    CloseConnection();
                }
            } else
            {
                Console.WriteLine("The amount of columns and values are not equal!");
            }
        }

        //Update statement
        public static void Update(string tableName, List<KeyValuePair<string, string>> setArgs, List<KeyValuePair<string, string>> whereArgs)
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (Connection.State == ConnectionState.Open)
            {
                //create mysql command
                SqlCommand cmd = new SqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = Connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                CloseConnection();
            }
        }

        //Delete statement
        public static void Delete(string tableName, List<KeyValuePair<string, string>> whereArgs)
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (Connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }

        //Select statement
        public static List<string> Select(string queryString)
        {
            string query = queryString;

            //Create a list to store the result
            List<string> list = new List<string>();

            //Open connection
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(query, Connection);
                //Create a data reader and Execute the command
                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                int fieldCOunt = dataReader.FieldCount;
                while (dataReader.Read())
                {
                    for (int i = 0; i < fieldCOunt; i++)
                    {
                        list.Add(dataReader.GetValue(i).ToString());
                    }
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                CloseConnection();

                //return list to be displayed
                return list;
            }

            return list;

        }

        //Count statement
        public static int Count(string tableName)
        {
            string query = "SELECT Count(*) FROM " + tableName;
            int Count = -1;

            //Open Connection
            if (Connection.State == System.Data.ConnectionState.Open)
            {
                //Create Mysql Command
                SqlCommand cmd = new SqlCommand(query, Connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                CloseConnection();

                return Count;
            }

            return Count;

        }*/

    }



}

