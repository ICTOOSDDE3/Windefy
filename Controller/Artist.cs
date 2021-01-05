using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Controller
{
    public class Artist
    {
        /// <summary>
        /// Calls methods to create artist object
        /// </summary>
        /// <param name="artistID"></param>
        /// <returns>artist object</returns>
        public Model.Artist GetArtist(int artistID)
        {
            DBConnection.OpenConnection();
            string query = $"SELECT name, active_year_begin, active_year_end, bio, associated_labels, location FROM artist WHERE artistID = {artistID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            Model.Artist artist = new Model.Artist();
            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        artist.Name = reader["name"].ToString();
                        artist.ArtistID = artistID;
                        if (reader["active_year_begin"].GetType().GetProperties().Length > 0) artist.ActiveYearBegin = (DateTime)reader["active_year_begin"];
                        if (reader["active_year_end"].GetType().GetProperties().Length > 0) artist.ActiveYearEnd = (DateTime)reader["active_year_end"];
                        artist.Bio = reader["bio"].ToString();
                        artist.Associated_Labels = reader["associated_labels"].ToString();
                        artist.Location = reader["location"].ToString();
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                    return null;
                }
            }
            DBConnection.CloseConnection();
            artist.MemberIDs = GetMemberIDs(artistID);

            return artist;
        }
        /// <summary>
        /// Fetches all tracks of an Artist
        /// </summary>
        /// <param name="artistID"></param>
        /// <returns></returns>
        public List<Model.Track> GetArtistTracks(int artistID)
        {
            DBConnection.OpenConnection();
            string query = $"SELECT * FROM track where trackID IN (SELECT trackID from track_artist where artistID = {artistID})";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            List<Model.Track> tracks = new List<Model.Track>();

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tracks.Add(new Model.Track()
                        {
                            TrackID = (int)reader["trackID"],
                            Title = reader["title"].ToString(),
                            Listens = (int)reader["listens"]
                        });          
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                    return null;
                }
            }
            DBConnection.CloseConnection();

            tracks.ForEach(track =>
            {
                track.Artists = GetTrackArtists(track.TrackID);
            });
            return tracks;
        }

        private List<Model.Artist> GetTrackArtists(int trackID)
        {
            DBConnection.OpenConnection();
            string query2 = $"SELECT * FROM artist WHERE artistID IN (SELECT artistID FROM track_artist WHERE trackID = {trackID})";

            SqlCommand oCmd2 = new SqlCommand(query2, DBConnection.Connection);
            List<Model.Artist> artists = new List<Model.Artist>();

            using (SqlDataReader reader2 = oCmd2.ExecuteReader())
            {
                while (reader2.Read())
                {
                    artists.Add(new Model.Artist()
                    {
                        ArtistID = (int)reader2["artistID"],
                        Name = reader2["name"].ToString()
                    });
                }
                reader2.Close();
            }
            DBConnection.CloseConnection();

            return artists;
        }

        /// <summary>
        /// Gets member id's from database
        /// </summary>
        /// <param name="artistID"></param>
        /// <returns>list of member ID's</returns>
        private List<int> GetMemberIDs(int artistID)
        {
            List<int> IDsMembers = new List<int>();

            DBConnection.OpenConnection();
            string query = $"SELECT memberID FROM artist_member WHERE artistID = {artistID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IDsMembers.Add((int)reader["memberID"]);
                    }
                }
                else
                {
                    DBConnection.CloseConnection();
                    return null;
                }
            }
            DBConnection.CloseConnection();

            return IDsMembers;
        }
    }
}

