using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public static class User
    {
        public static void UpdateUserModel(int id, string email, string name, int language, bool verified)
        {
            Model.User.UserID = id;
            Model.User.Email = email;
            Model.User.Name = name;
            Model.User.Language = language;
            Model.User.Verified = verified;
        }
    }
}
