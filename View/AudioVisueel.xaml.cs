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
		private MediaPlayer mediaPlayer = new MediaPlayer();
		private bool userIsDraggingSlider = false;

		public AudioVisueel()
		{
			InitializeComponent();
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
			{
				mediaPlayer.Open(new Uri(openFileDialog.FileName));
			}
			Song.Content = mediaPlayer.Source;
			DispatcherTimer timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;
			timer.Start();
		}

		void timer_Tick(object sender, EventArgs e)
		{
			if (mediaPlayer.Source != null && !userIsDraggingSlider && mediaPlayer.NaturalDuration.HasTimeSpan)
			{
				lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"hh\:mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
				TimeStatus.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
				TimeStatus.Value = mediaPlayer.Position.TotalSeconds;
				//TimeStatus.Foreground = Brushes.Red; 
			}
            else if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
				lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"hh\:mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
			}
			else
				lblStatus.Content = "No file selected...";
		}

		private void btnPlay_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Play();
		}

		private void btnPause_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Pause();
		}

		private void btnStop_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Stop();
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Position = TimeSpan.FromSeconds(mediaPlayer.Position.TotalSeconds + 5);
		}

		private void btnPrev_Click(object sender, RoutedEventArgs e)
		{
			mediaPlayer.Position = TimeSpan.FromSeconds(mediaPlayer.Position.TotalSeconds - 5);
		}

		private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			mediaPlayer.Volume = Volume.Value / 100;
		}
		
		private void btnRewind_Click(object sender, RoutedEventArgs e)
		{
			//moet nog
		}

        private void TimeStatus_DragCompleted(object sender, DragCompletedEventArgs e)
        {
			userIsDraggingSlider = false;
			mediaPlayer.Position = TimeSpan.FromSeconds(TimeStatus.Value);
		}

        private void TimeStatus_DragStarted(object sender, DragStartedEventArgs e)
        {
			userIsDraggingSlider = true;
		}
	}
}
