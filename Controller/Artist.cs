using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Controller
{
    public class Artist
    {
        private string name;
        private DateTime active_year_begin;
        private DateTime active_year_end;
        private string bio;
        private List<int> memberIDs;
        private string label;
        private string location;
    

        public Model.Artist GetArtist(int artistID)
        {
            Model.Artist artist = GetArtistFromDB(artistID);

            return artist;
            //naar view sturen
        }
        
        public List<Model.Artist> GetArtistsByList(List<int> artist_ids)
        {
            List<Model.Artist> list = new List<Model.Artist>();
            foreach(var item in artist_ids)
            {
                Model.Artist a = GetArtistFromDB(item);
                list.Add(a);
            }
            return list;
        }

        /// <summary>
        ///  Haalt alle gegevens van artist op uit de database en maakt hier een artist object van
        /// </summary>
        /// <param name="artistID"></param>
        /// <returns>een artist object</returns>
        private Model.Artist GetArtistFromDB(int artistID)
        {
            SqlConnection myConnection = new SqlConnection(); //open connectie met database, moet via Berkay's db connection class
            string dbString = $"Select * from artist where artistID = {artistID}";

            SqlCommand oCmd = new SqlCommand(dbString, myConnection);
            myConnection.Open();

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                name = reader["title"].ToString();
                active_year_begin = (DateTime)reader["active_year_begin"];
                active_year_end = (DateTime)reader["active_year_end"];
                bio = reader["bio"].ToString();
                label = reader["associated_labels"].ToString();
                location = reader["location"].ToString();
            }
            myConnection.Close();

            memberIDs = GetMemberIDs(artistID);

            Model.Artist artist = new Model.Artist(artistID, name, bio, memberIDs, label, location, active_year_begin, active_year_end);

            return artist;
        }

        /// <summary>
        /// haalt alle member id's op die horen bij een artiest
        /// </summary>
        /// <param name="artistID"></param>
        /// <returns>list van member id's</returns>
        private List<int> GetMemberIDs(int artistID)
        {
            List<int> IDsMembers = new List<int>();

            SqlConnection myConnection = new SqlConnection();
            string dbString = $"Select * from artist_member where artistID = {artistID}";

            SqlCommand oCmd = new SqlCommand(dbString, myConnection);
            myConnection.Open();

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    IDsMembers.Add((int)reader["memberID"]);
                }
            }
            myConnection.Close();

            return IDsMembers;
        }
    }
}

