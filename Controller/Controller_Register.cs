using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Controller
{
    class Controller_Register
    {
        //Method to register an account (validate and save to db)
        public void RegisterAccount(string email, string name, string pw1, string pw2)
        {
            if (IsValidEmail(email))
            {
                if (IsEmailUnique(email))
                {
                    if (ArePasswordsEqual(pw1, pw2))
                    {
                        //TODO: Create the account in the database when the db connection is made

                        byte[] bytePassword = PasswordToByte(pw1);
                        var salt = GenerateSalt(20); // maybe a random number between 20 - 30?
                        byte[] genratedPasswordHash = GenerateHash(bytePassword, salt, 10, 10); // maybe these numbers random generate aswell?

                        //INSERT INTO Users (User_Email, User_Name, User_Password(GenratedPasswordHash)) 
                        //we also need to store the salt (generated above) and the iterations and workfactor (which are the two 10's in GenerateHash())
                        //VALUES (email, name, generatedPasswordHash)
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
            //TODO: Add code to check if email already excists in database when db connection is written
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

    }
}

