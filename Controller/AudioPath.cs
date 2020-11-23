using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public class AudioPath
    {
        /// <summary>
        /// Gets full audio file path from static AppacheConnection class
        /// </summary>
        /// <param name="File_path"></param>
        /// <returns>Full audio file path</returns>
        public string GetAudioPath(string File_path)
        {
            ApacheConnection.Initialize();
            
            string audio_full_path = ApacheConnection.GetAudioFullPath(File_path);

            return audio_full_path;
        }
    }
}
