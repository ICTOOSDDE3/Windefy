using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Controller;

namespace Controller
{
    public class DBCommands
    {

        private SqlConnection connection;

        

        //Constructor
        public DBCommands()
        {
            DBConnection.Initialize();

            string connectionString;
            connectionString = "Server = 127.0.0.1; Database = WindefyDB; User Id = SA; Password = S3q8yCze6NL;";

            connection = new SqlConnection(connectionString);

            Console.WriteLine(connection.State.ToString());
            CloseConnection();
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                OpenConnection();
            }

        }

        


        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
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
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert(string query)
        {
            //string query = "INSERT INTO Inventory (id, name, quantity) VALUES('4', 'test', '104')";

            //open connection
            if (connection.State == System.Data.ConnectionState.Open)
            {
                //create command and assign the query and connection from the constructor
                SqlCommand cmd = new SqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string tableName, List<KeyValuePair<string, string>> setArgs, List<KeyValuePair<string, string>> whereArgs)
        {
            string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (connection.State == System.Data.ConnectionState.Open)
            {
                //create mysql command
                SqlCommand cmd = new SqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string tableName, List<KeyValuePair<string, string>> whereArgs)
        {
            string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (connection.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Select statement
        public List<string> Select(string queryString)
        {
            string query = queryString;

            //Create a list to store the result
            List<string> list = new List<string>();

            //Open connection
            if (connection.State == System.Data.ConnectionState.Open)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(query, connection);
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
                this.CloseConnection();

                //return list to be displayed
                return list;
            }

            return list;

        }

        //Count statement
        public int Count(string tableName)
        {
            string query = "SELECT Count(*) FROM " + tableName;
            int Count = -1;

            //Open Connection
            if (connection.State == System.Data.ConnectionState.Open)
            {
                //Create Mysql Command
                SqlCommand cmd = new SqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }

            return Count;

        }

    }
}
