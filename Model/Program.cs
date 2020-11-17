using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;

namespace Model
{
    class Program
    {
        static void Main(string[] args)
        {
            PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo("145.44.235.109", "student", "S3q8yCze6NL");
            connectionInfo.Timeout = TimeSpan.FromSeconds(30);
            var client = new SshClient(connectionInfo);
            client.Connect();
            ForwardedPortLocal portFwld = new ForwardedPortLocal("127.0.0.1", Convert.ToUInt32(1433), "127.0.0.1", Convert.ToUInt32(1433)); client.AddForwardedPort(portFwld);
            portFwld.Start();
            // using Renci.sshNet 
            var connection = new MySqlConnection("server = " + "127.0.0.1" + "; Database = TestDB; password = S3q8yCze6NL; UID = SA; Port = 1433");
            connection.Open();
        }
    }
}
