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
