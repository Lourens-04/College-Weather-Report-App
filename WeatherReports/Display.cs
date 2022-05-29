using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WeatherReports
{
	class Display
	{
        //Declaring the variables for the vlaues the user chose to view
        //------------------------------------------------------
		private string dcity;
		private string ddate;
		private string dminTemp;
		private string dmaxTemp;
		private string dprecipitation;
		private string dhumidity;
		private string dwindSpeed;
		private BitmapImage dimage;
        //------------------------------------------------------

        //Gets and Sets for the variables the user chose to view
        //------------------------------------------------------
        public string Dcity { get => dcity; set => dcity = value; }
		public string Ddate { get => ddate; set => ddate = value; }
		public string DminTemp { get => dminTemp; set => dminTemp = value; }
		public string DmaxTemp { get => dmaxTemp; set => dmaxTemp = value; }
		public string Dprecipitation { get => dprecipitation; set => dprecipitation = value; }
		public string Dhumidity { get => dhumidity; set => dhumidity = value; }
		public string DwindSpeed { get => dwindSpeed; set => dwindSpeed = value; }
		public BitmapImage Dimage { get => dimage; set => dimage = value; }
        //------------------------------------------------------

        //Constructor that will set the variables the user chose
        public Display(string city, string date, string minTemp, string maxTemp, string precipitation, string humidity, string windSpeed, BitmapImage Dimage)
        {
            //Sets the variables being sent from the other class to set the variables in this class
            //----------------------------------------------
            this.Dcity = city;
            this.Ddate = date;
            this.DminTemp = minTemp;
            this.DmaxTemp = maxTemp;
            this.Dprecipitation = precipitation;
            this.Dhumidity = humidity;
            this.DwindSpeed = windSpeed;
            this.Dimage = Dimage;
            //----------------------------------------------
        }
    }
}
