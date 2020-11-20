using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Controller
{
    public static class Passwords
    {
        //Get a specifick password from the Password JSON FIle
        public static string GetPassword(string type)
        {
            //Create the JSON object in the class from the JSON File
            JObject data = JObject.Parse(File.ReadAllText("..\\..\\..\\..\\Model/Passwords.JSON"));
            //Get the matched data
            var returnValue = data["Credentials"]["Passwords"][type];

            //Check if data is not null
            if (returnValue != null)
            {
                //Return the data
                return returnValue.ToString();
            }
            else
            {
                //Return null if there is no password found
                return null;
            }
        }
    }
}
