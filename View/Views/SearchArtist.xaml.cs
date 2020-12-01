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
    /// Interaction logic for SearchArtist.xaml
    /// </summary>
    public partial class SearchArtist : UserControl
    {
        public SearchArtist()
        {
            InitializeComponent();

            List<string> items = new List<string>();
            DBConnection.OpenConnection();

            string query = "SELECT name FROM member WHERE memberID < 20";

            SqlCommand cmd = new SqlCommand(query, DBConnection.Connection);

            SqlDataReader dataReader = cmd.ExecuteReader();

            while(dataReader.Read())
            {
                items.Add(Convert.ToString(dataReader["name"]));
            }



             dataReader.Close();

            //items.Add(cmd.ExecuteScalar().ToString());

            DBConnection.CloseConnection();

            //items.Add("testdfsadfsajfhdsajfhjkdsahfjkdsahfjkdsahfjdksa" );
            //items.Add("test2ghdflspkgfdls;gkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk" );




            icTodoList.ItemsSource = items;


        }
    }

    public class TodoItem
    {
        public string Title { get; set; }
    }
}
