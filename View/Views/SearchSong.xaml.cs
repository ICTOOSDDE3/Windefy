using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for SearchSong.xaml
    /// </summary>
    public partial class SearchSong : UserControl
    {
        public SearchSong()
        {
            InitializeComponent();

            ApacheConnection.Initialize();

            List<TrackInfo> items = new List<TrackInfo>();
            DBConnection.OpenConnection();

            string query = "SELECT title, duration, image_path, name FROM track_artist join artist on track_artist.artistID = artist.artistID JOIN track on track_artist.trackID = track.trackID WHERE track.trackID < 500";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                TrackInfo trackInfo = new TrackInfo(Convert.ToString(dataReader["title"]), Convert.ToInt32(dataReader["duration"]), Convert.ToString(dataReader["image_path"]), Convert.ToString(dataReader["name"]));

                items.Add(trackInfo);
            }



            dataReader.Close();


            //items.Add(cmd.ExecuteScalar().ToString());

            DBConnection.CloseConnection();

            //items.Add("testdfsadfsajfhdsajfhjkdsahfjkdsahfjkdsahfjdksa" );
            //items.Add("test2ghdflspkgfdls;gkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk" );




            icTodoList.ItemsSource = items;
        }
    }

    public class TrackInfo
    {
        public string Title { get; set; }
        public string Duration { get; set; }
        public string ImagePath { get; set; }
        public string ArtistName { get; set; }

        public TrackInfo(string T, int D, string I, string A)
        {
            string seconds = (D % 60).ToString();
            if(seconds.Length == 1)
            {
                seconds = "0" + seconds;
            }

            Title = T;
            Duration = $"{ Math.Floor(Convert.ToDouble( D) / 60)}:{seconds}";
            ImagePath = $"{ApacheConnection.GetImageFullPath(I)}";
            ArtistName = A;
        }
    }
}
