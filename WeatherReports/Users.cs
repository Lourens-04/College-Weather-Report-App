using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;

namespace WeatherReports
{
    class Users
    {
        //declareing the users and usersBackup array list
        //--------------------------------------------------------------------------------
        public static IList<UserDetails> users = new List<UserDetails>();
        //--------------------------------------------------------------------------------

        //Gets and Sets for the users and usersBackup array list 
        //--------------------------------------------------------------------------------
        internal IList<UserDetails> Users1 { get => users; set => users = value; }
        //--------------------------------------------------------------------------------

        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        SqlConnection cnn;
        SqlCommand cmd;

        public void Details()
        {
            //Reding in the data from the Users text file and putting it into a class and then into an array
            //--------------------------------------------------------------------------------
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                cmd = new SqlCommand("select * from User_Table", cnn);
                SqlDataReader sqlReader = cmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    string username = sqlReader.GetValue(0) + "";
                    string password = sqlReader.GetValue(1) + "";
                    string userrole = sqlReader.GetValue(2) + "";
                    UserDetails c = new UserDetails(username.Replace(" ",""), password.Replace(" ", ""), userrole.Replace(" ", ""));
                    Users1.Add(c);
                }
                sqlReader.Close();
                cmd.Dispose();
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error! (Maybe connection cannot be open)");
            }
            //--------------------------------------------------------------------------------
        }
    }
}
