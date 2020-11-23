using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private bool userIsDraggingSlider = false;
        private bool mediaPlaying = false;
        private bool rewind = false;
        private Model.Track CurrentTrack;

        public MainWindow() {
            InitializeComponent();
            Controller.Track trackController = new Controller.Track();
            
            DataContext = trackController.GetTrack(5);
            CurrentTrack = (Model.Track)this.DataContext;

            Controller.Artist artistController = new Controller.Artist();
            //icArtistList.ItemsSource = artistController.GetArtistsByList(CurrentTrack.ArtistIDs);
            icArtistList.ItemsSource = CurrentTrack.ArtistIDs;


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
                CurrentTime.Content = mediaPlayer.Position.ToString(@"mm\:ss");
                TotalTime.Content = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                TimeStatus.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                TimeStatus.Value = mediaPlayer.Position.TotalSeconds;
                //TimeStatus.Foreground = Brushes.Red;
            }
            else if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                CurrentTime.Content = mediaPlayer.Position.ToString(@"mm\:ss");
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

