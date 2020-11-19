using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            JObject data = JObject.Parse(File.ReadAllText("..\\..\\..\\..\\TEST.JSON"));

            var joe = data["Credentials"]["Passwords"]["DB"];

            Console.WriteLine(joe);
        }
    }
}
