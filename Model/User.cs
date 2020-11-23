using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public static class User
    {
        public static int UserID { get; set; }
        public static string Email { get; set; }
        public static string Name { get; set; }
        public static int Language { get; set; }
        public static bool Verified { get; set; }

        public static void UpdateEmail(string newEmai)
        {
            //Validate if email is correct
            //Validate if email is not in use yet
            //Update the email in the object & on the server
        }
        public static void ChangeName(string newName)
        {
            //Update the name in the object & on the server
            //Update name in db
            /* 
             *UPDATE users 
             *SET name = newName
             *WHERE user_Id = UserID
             */
        }

        public static void UpdatePassword(string newPassword)
        {
            //Validate if the two passwords are correct
            //Update the password on the server
        }
        public static bool IsUniqueMail(string mail)
        {
            /*
             * COUNT = 
             * 
             * SELECT COUNT(*)
             * FROM users
             * WHERE email = mail
             * 
             * 
             * if(count == 0) {
             *  return true;
             * }
             */
            return true;
        }

        public static string GetLanguage()
        {
            switch (Language)
            {
                case 1:
                    return "Dutch";
               default:
                    return "English";
            }

        }
    }
}
