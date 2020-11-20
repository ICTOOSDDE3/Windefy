﻿using System;
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
        private bool mediaPlaying = false;
        private bool rewind = false;

        public AudioVisueel()
        {
            InitializeComponent();

            Song.Content = mediaPlayer.Source;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null && !userIsDraggingSlider && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"hh\:mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
                TimeStatus.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                TimeStatus.Value = mediaPlayer.Position.TotalSeconds;
                TimeStatus.Foreground = Brushes.Red;
            }
            else if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                lblStatus.Content = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"hh\:mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss"));
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            //naar volgend liedje
        }
        private void btnPrev_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //naar vorig liedje
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();

            if (mediaPlaying)
            {
                mediaPlayer.Play();
            }
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaPlayer.Volume = Volume.Value / 100;
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (rewind)
            {
                mediaPlayer.Stop();
                mediaPlayer.Play();
            }
            else
            {
                //Volgend nummer 
            }
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

        private void TogglePlay(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
            tbPlayPause.Content = "Pause";
            mediaPlaying = true;
        }

        private void TogglePause(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
            tbPlayPause.Content = "Play";
            mediaPlaying = false;
        }

        private void btnRewind_Checked(object sender, RoutedEventArgs e)
        {
            rewind = true;
            //Volgend nummer wordt het huidige nummer
        }

        private void btnRewind_Unchecked(object sender, RoutedEventArgs e)
        {
            rewind = false;
        }
    }
}
