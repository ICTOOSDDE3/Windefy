using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using System.Drawing;

namespace Test
{
    [TestFixture]
    class Controller_ApacheConnection
    {

        [Test]
        public void GetFullImagePath()
        {
            Assert.AreEqual(ApacheConnection.GetImageFullPath("0001.jpeg"), "http://localhost:8080/image_files/files/0001.jpeg");
        }

        [Test]
        public void GetFullAudioPath()
        {
            Assert.AreEqual(ApacheConnection.GetAudioFullPath("000/001"), "http://localhost:8080/audio_files/files/000/001.mp3");
        }
    }
}
