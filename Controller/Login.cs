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
            // check if there is an email similar to the given email in the database
            //password to byte
            // check if passwords are equal
            // return true if so


            //
            //DBConnection.Initialize();
            DBConnection.OpenConnection();
            try
            {
                string query = $"SELECT email, password, saltcode, iteration FROM users WHERE email = '{email}'";

                SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
                SqlDataReader dataReader = cmd.ExecuteReader();

                var temp = dataReader["email"].ToString();

                string tempPassword = dataReader["password"].ToString();
                byte[] tempSalt = Encoding.UTF8.GetBytes(dataReader["saltcode"].ToString());
                int tempIteration = (int)dataReader["iteration"];

                byte[] currentPassword = PasswordToByte(password);
                string currentHash = Convert.ToString(GenerateHash(currentPassword, tempSalt, tempIteration, tempIteration));

                if (currentHash.Equals(tempPassword))
                {
                    return true;
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

        public byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }

        public byte[] PasswordToByte(string password)
        {
            byte[] bytePassword = Encoding.ASCII.GetBytes(password);
            return bytePassword;
        }
    }
}
