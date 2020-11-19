using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;

namespace View
{
	/// <summary>
	/// Interaction logic for AudioVisueel.xaml
	/// </summary>
	public partial class AudioVisueel : Window
	{
		public MediaPlayer mediaPlayer = new MediaPlayer();
		private bool IsDragging = false;

		public AudioVisueel()
		{
			InitializeComponent();
			/* OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
			{
				mediaPlayer.Open(new Uri(openFileDialog.FileName));
			}
			*/
			mediaPlayer.Open(new Uri("https://www.learningcontainer.com/wp-content/uploads/2020/02/Kalimba.mp3"));
			Song.Content = mediaPlayer.Source;
            mediaPlayer.MediaEnded += MusicControl
            DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;
			timer.Start();
		}

		void timer_Tick(object sender, EventArgs e)
		{
			if (mediaPlayer.Source != null && !IsDragging)
			{
				lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"hh\:mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
				pbStatus.Value = (mediaPlayer.Position.TotalSeconds / mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds) * 100;
				sliderStatus.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
				sliderStatus.Value = mediaPlayer.Position.TotalSeconds;
			}
			else
				lblStatus.Content = "No file selected...";
		}
		public void InsertSong() //Parameter Song class
        {

        }
		private void TogglePlay(object sender,RoutedEventArgs e)
        {
			mediaPlayer.Play();
			tbPlayPause.Content = "Pause";
		}
		private void TogglePause(object sender, RoutedEventArgs e)
        {
			mediaPlayer.Pause();
			tbPlayPause.Content = "Play";
        }

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Position = TimeSpan.FromSeconds(mediaPlayer.Position.TotalSeconds + 5);
		}

		private void btnPrev_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Position = TimeSpan.FromSeconds(mediaPlayer.Position.TotalSeconds - 5);
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			mediaPlayer.Volume = Volume.Value / 100;
		}
		
		private void btnRewind_Click(object sender, RoutedEventArgs e)
		{
			//moet nog
		}

        private void sliderStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void sliderStatus_DragLeave(object sender, DragEventArgs e)
        {
			IsDragging = false;
			mediaPlayer.Position = TimeSpan.FromSeconds(sliderStatus.Value);
        }

        private void sliderStatus_DragEnter(object sender, DragEventArgs e)
        {
			IsDragging = true;
        }
		
    }
}
