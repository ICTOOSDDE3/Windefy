using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public static class ApacheConnection
    {

        private static ForwardedPortLocal PortFwld = new ForwardedPortLocal("127.0.0.1", 8080, "127.0.0.1", 80);
        private static PasswordConnectionInfo ConnectionInfo = new PasswordConnectionInfo("145.44.235.109", "student", Passwords.GetPassword("SSH"));

        public static void Initialize()
        {
            ConnectionInfo.Timeout = TimeSpan.FromSeconds(30);

            var client = new SshClient(ConnectionInfo);

            try
            {
                client.Connect();
                client.AddForwardedPort(PortFwld);
                PortFwld.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// Returns full path to audio files based on file path in database
        /// </summary>
        /// <param name="AudioPath">Path in database</param>
        /// <returns>Full path to audio file</returns>
        public static string GetAudioFullPath(string AudioPath)
        {
            return "http://localhost:8080/audio_files/files/" + AudioPath + ".mp3";
        }

        /// <summary>
        /// Returns full path to image files based on file path in database
        /// </summary>
        /// <param name="ImagePath">Path in database</param>
        /// <returns>Full path to image file</returns>
        public static string GetImageFullPath(string ImagePath)
        {
            return "http://localhost:8080/image_files/files/" + ImagePath;
        }
    }
}

