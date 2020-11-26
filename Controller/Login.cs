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
            DBConnection.Initialize();
            DBConnection.OpenConnection();
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

                    if (AreEqual(password,DataBasePassword,DataBaseSalt))
                    {
                        int id = Convert.ToInt32(dataReader["user_Id"]);
                        string name = dataReader["name"].ToString();
                        int language = 1;
                        bool verified = Convert.ToBoolean(dataReader["verified"]);
                        User.UpdateUserModel(id, email, name, language, verified);
                        return true;
                    }
                }

                dataReader.Close();
                DBConnection.CloseConnection();
                
            }
            catch (Exception e)
            {
                DBConnection.CloseConnection();
                return false;
            }
            return false;
        }

        public string GenerateHash(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public byte[] PasswordToByte(string password)
        {
            byte[] bytePassword = Encoding.ASCII.GetBytes(password);
            return bytePassword;
        }
        public bool AreEqual(string plainTextInput, string hashedInput, string salt)
        {
            string newHashedPin = GenerateHash(plainTextInput, salt);
            return newHashedPin.Equals(hashedInput);
        }
    }
}
