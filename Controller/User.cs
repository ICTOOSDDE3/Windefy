using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public static class User
    {

        public static void UpdateEmail(string newEmail)
        {
            
            //Validate if email is not in use yet
            if(IsUniqueMail(newEmail, Model.User.UserID) && IsValidEmail(newEmail))
            {
                //Update the email in the object & on the server
                Model.User.Email = newEmail;
                UpdateEmailInDB(newEmail, Model.User.UserID);
                
            }            
        }
        public static void UpdateName(string newName)
        {
            //Update the name in the object & on the server
            Model.User.Email = newName;
            //Update name in db
            UpdateNameInDB(newName, Model.User.UserID);
        }

        public static void UpdateNameInDB(string name, int userID)
        {
            //Initialize and open a db connection
            //DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string query = $"UPDATE users SET name = '{name}' WHERE user_Id = {userID}";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            cmd.ExecuteScalar();

            //Close the connection
            DBConnection.CloseConnection();
        }
        public static void UpdateEmailInDB(string mail, int userID)
        {
            //Initialize and open a db connection
            //DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string query = $"UPDATE users SET _email = '{mail}' WHERE user_Id = {userID}";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            cmd.ExecuteScalar();

            //Close the connection
            DBConnection.CloseConnection();

        }
        //Check if mail is already in the database
        public static bool IsUniqueMail(string mail, int userID)
        {
            //Initialize and open a db connection
            DBConnection.Initialize();
            DBConnection.OpenConnection();

            //Build the query
            string query = $"SELECT COUNT(*) FROM users WHERE _email = '{mail}'";

            //Prepare the query
            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            //Run the query
            int result = Convert.ToInt32(cmd.ExecuteScalar() +  "");
            
            //Close the connection
            DBConnection.CloseConnection();

            //If the email is not in the db return true else return false
            if (result == 0) return true; else return false;
        }
        //Check if email is valid
        public static bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
        public static void UpdateUserModel(int id, string email, string name, int language, bool verified)
        {
            Model.User.UserID = id;
            Model.User.Email = email;
            Model.User.Name = name;
            Model.User.Language = language;
            Model.User.Verified = verified;
        }
    }
}
