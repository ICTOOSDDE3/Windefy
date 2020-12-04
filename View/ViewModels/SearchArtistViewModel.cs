using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using View.Views;

namespace View.ViewModels
{
    public class SearchArtistViewModel
    {
        public List<ArtistInfo> items { get; set; }

        public SearchArtistViewModel(string q)
        {
            items = new List<ArtistInfo>();
            DBConnection.OpenConnection();

            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);
            cmd.CommandText = "SELECT name, artistID FROM artist " +
                "WHERE name LIKE '%' + @que + '%' " +
                "ORDER BY artistID " +
                "OFFSET 0 ROWS " +
                "FETCH NEXT 50 ROWS ONLY";

            SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255);
            que.Value = q;

            cmd.Parameters.Add(que);

            cmd.Prepare();

            SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                items.Add(new ArtistInfo(Convert.ToString(dataReader["name"]),
                    Convert.ToInt32(dataReader["artistID"])));
            }

            dataReader.Close();

            DBConnection.CloseConnection();
        }
    }

    public class ArtistInfo
    {
        public string Name { get; set; }
        public int ArtistID { get; set; }

        public ArtistInfo(string name, int id)
        {
            Name = name;
            ArtistID = id;
        }
    }
}
