using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public static class User
    {

        public static string Email { get; set; }
        public static string Name { get; set; }
        public static int Language { get; set; }
        public static bool Verificated { get; set; }

        public static void UpdateEmail(string newEmai)
        {
            //Validate if email is correct
            //Validate if email is not in use yet
            //Update the email in the object & on the server
        }
        public static void ChangeName(string newName)
        {
            //Update the name in the object & on the server
        }

        public static void ChangePassword(string newPassword)
        {
            //Validate if the two passwords are correct
            //Update the password on the server
        }
    }
}
