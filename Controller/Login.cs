using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Controller
{
    public class Login
    {
        public bool IsLogin(string email, string password)
        {
            //DBConnection.Initialize();
            DBConnection.OpenConnection();
                bool passed = false;
            try
            {
                string query = $"SELECT * FROM users WHERE email = '{email}'";

                SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    var temp = dataReader["email"].ToString();

                    string DataBasePassword = dataReader["password"].ToString();
                    string DataBaseSalt = dataReader["saltcode"].ToString();

                    if (IsEqual(password,DataBasePassword,DataBaseSalt))
                    {
                        int id = Convert.ToInt32(dataReader["user_Id"]);
                        string name = dataReader["name"].ToString();
                        int language = (int)dataReader["lang"];
                        bool verified = Convert.ToBoolean(dataReader["verified"]);
                        User.UpdateUserModel(id, email, name, language, verified);
                        passed = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                DBConnection.CloseConnection();
            }
            return passed;
        }

        /// <summary>
        /// generates a hash from inputted password en generated salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GenerateHash(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// checks if hashed password is equal to the password in database
        /// </summary>
        /// <param name="plainTextInput"></param>
        /// <param name="hashedInput"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public bool IsEqual(string plainTextInput, string hashedInput, string salt)
        {
            string newHashedPin = GenerateHash(plainTextInput, salt);
            return newHashedPin.Equals(hashedInput);
        }
    }
}
