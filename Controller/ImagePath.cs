using System;
using System.Collections.Generic;
using System.Text;

namespace Controller
{
    public class ImagePath
    {
        /// <summary>
        /// Gets full image file path from static AppacheConnection class
        /// </summary>
        /// <param name="Image_path"></param>
        /// <returns>Full image file path</returns>
        public string GetImagePath(string Image_path)
        {
            ApacheConnection.Initialize();

            string audio_full_path = ApacheConnection.GetImageFullPath(Image_path);

            return audio_full_path;
        }
    }
}
