using Renci.SshNet;
using Renci.SshNet.Common;
using System;

namespace Controller
{
    public static class DBConnection
    {
        private static ForwardedPortLocal portFwld = new ForwardedPortLocal("127.0.0.1", 1433, "127.0.0.1", 1433);
        private static PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo("145.44.235.109", "student", "S3q8yCze6NL");
        
        public static void Initialize()
        {
            connectionInfo.Timeout = TimeSpan.FromSeconds(30);

            var client = new SshClient(connectionInfo);

                try
                {
                    Console.WriteLine("Trying SSH connection...");
                    client.Connect();
                    if (client.IsConnected)
                    {
                        Console.WriteLine("SSH connection is active: {0}", client.ConnectionInfo.ToString());
                    }
                    else
                    {
                        Console.WriteLine("SSH connection has failed: {0}", client.ConnectionInfo.ToString());
                    }

                    Console.WriteLine("\r\nTrying port forwarding...");

                    client.AddForwardedPort(portFwld);
                    portFwld.Start();
                    if (portFwld.IsStarted)
                    {
                        Console.WriteLine("Port forwarded: {0}", portFwld.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Port forwarding has failed.");
                    }

                    Console.WriteLine("\r\nTrying database connection...");
                    

                }
                catch (SshException e)
                {
                    Console.WriteLine("SSH client connection error: {0}", e.Message);
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    Console.WriteLine("Socket connection error: {0}", e.Message);
                }



            
        }



    }
}
