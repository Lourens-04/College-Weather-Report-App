using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Windows;
using System.Configuration;

namespace WeatherReports
{
	class Reports
	{
        //declareing the WeaterForecast and backUpWeaterForecast array list
        //--------------------------------------------------------------------------------
        public static IList<AddReport> WeaterForecast = new List<AddReport>();
        //--------------------------------------------------------------------------------

        //Gets and Sets for the WeaterForecast and backUpWeaterForecast array list 
        //--------------------------------------------------------------------------------
        internal IList<AddReport> WeaterForecast1 { get => WeaterForecast; set => WeaterForecast = value;}
        //--------------------------------------------------------------------------------

        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        SqlConnection cnn;
        SqlCommand cmd;

        public void Values()
        {
            //Reding in the data from the Users text file and putting it into a class and then into an array
            //--------------------------------------------------------------------------------
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                cmd = new SqlCommand("select * from Forecast_Table", cnn);
                SqlDataReader sqlReader = cmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    int id = Convert.ToInt16(sqlReader.GetValue(0));
                    string city = sqlReader.GetValue(1) + "";
                    string date = sqlReader.GetValue(2) + "";
                    string minTemp = sqlReader.GetValue(3) + "";
                    string maxTemp = sqlReader.GetValue(4) + "";
                    string precipitation = sqlReader.GetValue(5) + "";
                    string humidity = sqlReader.GetValue(6) + "";
                    string windSpeed = sqlReader.GetValue(7) + "";
                    AddReport c = new AddReport(city.Replace(" ", ""), date.Replace(" ", ""), minTemp.Replace(" ", ""), maxTemp.Replace(" ", ""), precipitation.Replace(" ", ""), humidity.Replace(" ", ""), windSpeed.Replace(" ", ""));
                    WeaterForecast1.Add(c);
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
