using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Linq;
using System.Data.SqlClient;


namespace Controller
{
    public class Register
    {
        private static Random random = new Random();
        private Mail VerificationMail = new Mail();

        //Method to register an account (validate and save to db)
        public void RegisterAccount(string email, string name, string pw1, string pw2)
        {
            DBConnection.Initialize();
            if (IsValidEmail(email))
            {
                if (IsEmailUnique(email))
                {
                    if (ArePasswordsEqual(pw1, pw2))
                    {
                        //TODO: Create the account in the database when the db connection is made

                        byte[] bytePassword = PasswordToByte(pw1);
                        byte[] salt = GenerateSalt(20); // maybe a random number between 20 - 30?
                        byte[] genratedPasswordHash = GenerateHash(bytePassword, salt, 10, 10);// maybe these numbers random generate aswell?
                        string verificationCode = CreateCode();

                        //database connection / inserting new user in database
                        DBConnection.OpenConnection();

                        SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);
                        cmd.CommandText = $"INSERT INTO users (email, name, password, lang, verificationCode, verified, saltcode, iteration) " +
                            $"VALUES(@email, @name, @password, @lang, @verificationCode, @verified, @saltcode, @iteration)";


                        SqlParameter paramEmail = new SqlParameter("@email", System.Data.SqlDbType.Text, 255);
                        SqlParameter paramName = new SqlParameter("@name", System.Data.SqlDbType.Text, 255);
                        SqlParameter paramPassword = new SqlParameter("@password", System.Data.SqlDbType.Text, 255);
                        SqlParameter paramLang = new SqlParameter("@lang", System.Data.SqlDbType.Int, 1);
                        SqlParameter paramVerification = new SqlParameter("@verificationCode", System.Data.SqlDbType.Text, 255);
                        SqlParameter paramVerified = new SqlParameter("@verified", System.Data.SqlDbType.Int, 1);
                        SqlParameter paramSalt = new SqlParameter("@saltcode", System.Data.SqlDbType.Text, 255);
                        SqlParameter paramIteration = new SqlParameter("@iteration", System.Data.SqlDbType.Int, 100);

                        paramEmail.Value = email;
                        paramName.Value = name;
                        paramPassword.Value = Encoding.UTF8.GetString(genratedPasswordHash, 0, genratedPasswordHash.Length);
                        paramLang.Value = 0;
                        paramVerification.Value = verificationCode;
                        paramVerified.Value = 0;
                        paramSalt.Value = Encoding.UTF8.GetString(salt, 0, salt.Length);
                        paramIteration.Value = 10;

                        cmd.Parameters.Add(paramEmail);
                        cmd.Parameters.Add(paramName);
                        cmd.Parameters.Add(paramPassword);
                        cmd.Parameters.Add(paramLang);
                        cmd.Parameters.Add(paramVerification);
                        cmd.Parameters.Add(paramVerified);
                        cmd.Parameters.Add(paramSalt);
                        cmd.Parameters.Add(paramIteration);

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();

                        DBConnection.CloseConnection();
                        VerificationMail.SendValidationMail(email, verificationCode);

                    }
                }
            }
        }

        //Check if email is valid
        public bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        //Check if two passwords are equal
        public bool ArePasswordsEqual(string str1, string str2)
        {
            return str1.Equals(str2);
        }

        //Check if email is already in use
        public bool IsEmailUnique(string email)
        {
            DBConnection.OpenConnection();

            string query = "SELECT email FROM users";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                if (email.Equals(dataReader["email"].ToString()))
                {
                    DBConnection.CloseConnection();
                    return false;
                }
            }
            dataReader.Close();
            DBConnection.CloseConnection();
            return true;
        }

        /// <summary>
        ///generate a byte array from a password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>bytePassword</returns>
        public byte[] PasswordToByte(string password)
        {
            byte[] bytePassword = Encoding.ASCII.GetBytes(password);
            return bytePassword;
        }

        /// <summary>
        /// generate a salt byte array
        /// </summary>
        /// <param name="length">amount of bytes</param>
        /// <returns>byte array with the given length</returns>
        public byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// Generates a hash with the bytepassword and the byte salt combined
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="length"></param>
        /// <returns>a byte array of the bytepassword combined with the byte salt</returns>
        public byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }

        /// <summary>
        /// creates a verification code
        /// </summary>
        /// <returns>returns a code string</returns>
        public string CreateCode()
        {
            //Linq statement to create random string based on the given chars and amount
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string code = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            Console.WriteLine(code);
            return code;
        }

        // method get verifcation code
        public string GetUserVerificationCode(int userID)
        {
            string verificationCode = "";// search verificationcode in database filtered on userID
            return verificationCode;
        }

        // method check verification code
        public bool IsVerificationCodeCorrect(string verificationCode, string email)
        {

            DBConnection.OpenConnection();

            string query = $"SELECT verificationCode FROM users where email = '{email}'";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            string code = cmd.ExecuteScalar().ToString();
            DBConnection.CloseConnection();
            if (code.Equals(verificationCode))
            {

                DBConnection.OpenConnection();

                string query2 = $"UPDATE users SET verified = 1 where email = '{email}'";

                SqlCommand cmd2 = new SqlCommand(query2, DBConnection.Connection);
                cmd2.ExecuteNonQuery();
                DBConnection.CloseConnection();
                return true;
            }
            return false;
        }


        // method resend verification code
        public void ResendVerificationCode(int userID)
        {
            string userMail = "";
            string newCode = CreateCode();
            //resendVerificationMail.SendValidationMail(userMail, newCode);
        }
    }
}

