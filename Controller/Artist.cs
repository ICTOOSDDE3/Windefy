using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Controller
{
    public class Artist
    {
        public Artist()
        {

            //DBConnection.Initialize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artistID"></param>
        /// <returns></returns>
        public Model.Artist GetArtist(int artistID)
        {
            Model.Artist artist = GetArtistFromDB(artistID);
            return artist;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="artist_ids"></param>
        /// <returns></returns>
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
            DBConnection.OpenConnection();
            string query = $"Select * from artist where artistID = {artistID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            Model.Artist artist = new Model.Artist();
            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    
                    artist.Name = reader["name"].ToString();
                    if(reader["active_year_begin"] != null) artist.active_year_begin = (DateTime)reader["active_year_begin"];
                    if (reader["active_year_end"] != null) artist.active_year_end = (DateTime)reader["active_year_end"];
                    artist.Bio = reader["bio"].ToString();
                    artist.Associated_Labels = reader["associated_labels"].ToString();
                    artist.Location = reader["location"].ToString();
                }
            }
            DBConnection.CloseConnection();
            artist.MemberIDs = GetMemberIDs(artistID);

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

            DBConnection.OpenConnection();

            string query = $"Select * from artist_member where artistID = {artistID}";

            SqlCommand oCmd = new SqlCommand(query, DBConnection.Connection);

            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    IDsMembers.Add((int)reader["memberID"]);
                }
            }
            DBConnection.CloseConnection();

            return IDsMembers;
        }
    }
}

