using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class User
    {
        public int User_ID { get; set; }
        public string User_Email { get; set; }
        public string User_Name { get; set; }
        public string User_Password { get; set; }
        public int User_Language { get; set; }

        public User(int user_ID, string user_Email, string user_Name, string user_Password, int user_Language)
        {
            User_ID = user_ID;
            User_Email = user_Email;
            User_Name = user_Name;
            User_Password = user_Password;
            User_Language = user_Language;
        }
    }
}
