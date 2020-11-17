using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

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

                        //Hash the password

                        //INSERT INTO Users (User_Email, User_Name, User_Password)
                        //VALUES (email, name, pw1)
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

        public string PasswordToHash(string password)
        {
            string hash = "";
            return hash;
        }
    }
}

