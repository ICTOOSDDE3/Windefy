using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Controller
{
    public class Program
    {
        static void Main(string[] args)
        {
            DBConnection.Initialize();
            DBConnection.OpenConnection();
            Playlist playlist = new Playlist();

            Model.User.UserID = 1;

            SideBarList.SetAllPlaylistsFromUser();

            Console.WriteLine(SideBarList.sideBarList);
        }
    }
}
