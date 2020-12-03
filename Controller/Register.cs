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
        private static Random _random = new Random();
        private Mail _verificationMail = new Mail();
        private Login _account = new Login();

        //Method to register an account (validate and save to db)
        public void RegisterAccount(string email, string name, string pw1, string pw2)
        {
            

            if (IsValidEmail(email))
            {
                if (IsEmailUnique(email))
                {
                    if (IsPasswordEqual(pw1, pw2))
                    {
                        //TODO: Create the account in the database when the db connection is made

                        
                        string salt = GenerateSalt(20); // maybe a random number between 20 - 30?
                        string genratedPasswordHash = GenerateHash(pw1, salt);// maybe these numbers random generate aswell?
                        string verificationCode = CreateCode();

                        //database connection / inserting new user in database
                        DBConnection.OpenConnection();

                        SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);
                        cmd.CommandText = $"INSERT INTO users (email, name, password, verificationCode, verified, saltcode) " + //if language is going to be implemented in the registration add lang to this statement
                            $"VALUES(@email, @name, @password, @verificationCode, @verified, @saltcode)";// if language is goint to be implemented in the registration add lang to this statement


                        SqlParameter paramEmail = new SqlParameter("@email", System.Data.SqlDbType.Text, 255);
                        SqlParameter paramName = new SqlParameter("@name", System.Data.SqlDbType.Text, 255);
                        SqlParameter paramPassword = new SqlParameter("@password", System.Data.SqlDbType.Text, 255);
                        //SqlParameter paramLang = new SqlParameter("@lang", System.Data.SqlDbType.Int, 1);      //uncomment when lang is goint to be implemented
                        SqlParameter paramVerification = new SqlParameter("@verificationCode", System.Data.SqlDbType.Text, 255);
                        SqlParameter paramVerified = new SqlParameter("@verified", System.Data.SqlDbType.Int, 1);
                        SqlParameter paramSalt = new SqlParameter("@saltcode", System.Data.SqlDbType.Text, 255);

                        paramEmail.Value = email;
                        paramName.Value = name;
                        paramPassword.Value = genratedPasswordHash;
                        //paramLang.Value = ;      //uncomment when lang is goint to be implemented
                        paramVerification.Value = verificationCode;
                        paramVerified.Value = 0;
                        paramSalt.Value = salt;

                        cmd.Parameters.Add(paramEmail);
                        cmd.Parameters.Add(paramName);
                        cmd.Parameters.Add(paramPassword);
                        //cmd.Parameters.Add(paramLang);      //uncomment when lang is goint to be implemented
                        cmd.Parameters.Add(paramVerification);
                        cmd.Parameters.Add(paramVerified);
                        cmd.Parameters.Add(paramSalt);

                        cmd.Prepare();
                        cmd.ExecuteNonQuery();

                        DBConnection.CloseConnection();
                        _verificationMail.SendValidationMail(email, verificationCode);
                        _account.IsLogin(email, pw1);
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
        public bool IsPasswordEqual(string str1, string str2)
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
        /// generate a salt byte array
        /// </summary>
        /// <param name="length">amount of bytes</param>
        /// <returns>byte array with the given length</returns>
        public string GenerateSalt(int length)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[length];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Generates a hash with the bytepassword and the byte salt combined
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="length"></param>
        /// <returns>a byte array of the bytepassword combined with the byte salt</returns>
        public string GenerateHash(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
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
              .Select(s => s[_random.Next(s.Length)]).ToArray());
            return code;
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
        public void ResendVerificationCode(string email)
        {
            string newCode = CreateCode();
            _verificationMail.SendValidationMail(email, newCode);
            DBConnection.OpenConnection();

            string query2 = $"UPDATE users SET verificationCode = '{newCode}' where email = '{email}'";

            SqlCommand cmd2 = new SqlCommand(query2, DBConnection.Connection);
            cmd2.ExecuteNonQuery();
            DBConnection.CloseConnection();
        }
    }
}

