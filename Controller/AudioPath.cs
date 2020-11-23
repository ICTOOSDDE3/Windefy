using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Controller
{
    public class AudioPath
    {
        public string GetAudioPath(string File_path)
        {
            ApacheConnection.Initialize();
            
            string audio_full_path = ApacheConnection.GetAudioFullPath(File_path);

            return audio_full_path;
        }
    }
}
