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
        public List<string> items { get; set; }

        public SearchArtistViewModel(string q)
        {
            items = new List<string>();
            DBConnection.OpenConnection();

            SqlCommand cmd = new SqlCommand(null, DBConnection.Connection);
            cmd.CommandText = "SELECT name FROM artist " +
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
                items.Add(Convert.ToString(dataReader["name"]));
            }

            dataReader.Close();

            DBConnection.CloseConnection();
        }
    }
}
