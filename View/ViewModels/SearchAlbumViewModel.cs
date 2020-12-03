using Caliburn.Micro;
using Controller;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace View.ViewModels
{
    public class SearchAlbumViewModel
    {
        public List<PlaylistInfo> items { get; set; }
        public SearchAlbumViewModel()
        {
            items = new List<PlaylistInfo>();

            ApacheConnection.Initialize();

            
            DBConnection.OpenConnection();

            // string query = "SELECT title, duration, image_path, name FROM track_artist join artist on track_artist.artistID = artist.artistID JOIN track on track_artist.trackID = track.trackID WHERE track.trackID < 500";
            string query = "SELECT title, COUNT(trackID) as trackCount, name FROM playlist_track join playlist on playlist_track.playlistID = playlist.playlistID join artist_album on playlist_track.playlistID = artist_album.playlistID join artist on artist_album.artistID = artist.artistID  WHERE playlist.playlistID < 500  GROUP BY title, name";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                PlaylistInfo playlistInfo = new PlaylistInfo(Convert.ToString(dataReader["title"]), Convert.ToString(dataReader["trackCount"]), Convert.ToString(dataReader["name"]));

                items.Add(playlistInfo);

            }



                dataReader.Close();


                //items.Add(cmd.ExecuteScalar().ToString());

                DBConnection.CloseConnection();

                //items.Add("testdfsadfsajfhdsajfhjkdsahfjkdsahfjkdsahfjdksa" );
                //items.Add("test2ghdflspkgfdls;gkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk" );
            

        }

        public class PlaylistInfo
        {
            public string Title { get; set; }
            public string Quantity { get; set; }
            public string ArtistName { get; set; }

            public PlaylistInfo(string T, string Q, string A)
            {

                Title = T;
                Quantity = Q;
                ArtistName = A;
            }
        }
    }
}
