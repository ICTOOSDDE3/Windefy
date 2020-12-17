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
        public event EventHandler<int> ArtistClickEvent;
        public List<ArtistInfo> items { get; set; }

        private string _NoResultsVisibility;

        public SearchArtistViewModel()
        {
            items = new List<ArtistInfo>();
            NoResultsVisibility = "Hidden";
        }

        public string NoResultsVisibility
        {
            get { return _NoResultsVisibility; }
            set { _NoResultsVisibility = value; }
        }

        public void OnArtistClick(int artistId)
        {
            ArtistClickEvent?.Invoke(this, artistId);
        }

        public SearchArtistViewModel(string q)
        {
            items = new List<ArtistInfo>();
            DBConnection.OpenConnection();
            
            // Fetch all artists based on the search query
            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                CommandText = "SELECT name, artistID FROM artist " +
                "WHERE name LIKE '%' + @que + '%' " +
                "ORDER BY artistID " +
                "OFFSET 0 ROWS " +
                "FETCH NEXT 50 ROWS ONLY"
            };

            SqlParameter que = new SqlParameter("@que", System.Data.SqlDbType.VarChar, 255)
            {
                Value = q
            };

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

            if (items.Count > 0)
            {
                NoResultsVisibility = "Hidden";
            }
            else
            {
                NoResultsVisibility = "Visible";
            }
        }

        internal void GetFavourites(int userID)
        {
            items = new List<ArtistInfo>();
            DBConnection.OpenConnection();

            // Fetch all artists based on the search query
            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection)
            {
                CommandText = "SELECT name, artistID FROM artist " +
                "WHERE artistID IN (" +
                "   SELECT artistID " +
                "   FROM user_favourite_artist " +
                "   WHERE userID = @ID " +
                ") " +
                "ORDER BY artistID " +
                "OFFSET 0 ROWS " +
                "FETCH NEXT 50 ROWS ONLY"
            };

            SqlParameter id = new SqlParameter("@ID", System.Data.SqlDbType.VarChar, 255)
            {
                Value = userID
            };

            cmd.Parameters.Add(id);
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

    // Data template for artist info on the search screen
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
