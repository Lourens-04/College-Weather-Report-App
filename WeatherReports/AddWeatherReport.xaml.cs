using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;
using System.Data.SqlClient;
using System.Configuration;

namespace WeatherReports
{
	/// <summary>
	/// Interaction logic for AddWeatherReport.xaml
	/// </summary>
	public partial class AddWeatherReport : Window
	{
		//error cheaker for error cheaking
		private Boolean error = false;
        //error cheaker for cheaking
        private Boolean check = false;
        //error cheaker for cheaking
        private Boolean check2 = true;
        //Accessing the Reports class to get the array list
        Reports dd = new Reports();
		//Variable for min temp, is a string
		string minTemp;
		//Variable for max temp, is a string
		string maxTemp;
        //String that holds the users name
        string user;

        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        SqlConnection cnn;
        SqlCommand cmd;

        public AddWeatherReport(string user)
		{
			InitializeComponent();

            //This string user will = to the username that was send to this window
            this.user = user;
        }

        //Cutton to add a new forecast
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Error method checks for error that can occur
            Error();
            //if statement to see if there was an error or not
            if (error == false)
            {
                if (btnAdd.Content.Equals("Update"))
                {
                    //for loop that will loop the entire array list
                    for (int i = 0; i < dd.WeaterForecast1.Count; i++)
                    {
                        //if statement to see if the array list city equel to the enterd city ignored case
                        if (dd.WeaterForecast1[i].City.Equals(txbCity.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Change the enterd item to that cit that is equeled
                            txbCity.Text = dd.WeaterForecast1[i].City;
                        }
                    }

                    //for loop that will loop the entire array list
                    for (int i = 0; i < dd.WeaterForecast1.Count; i++)
                    {
                        //if statement to see if the city textbox = city value in the array
                        if (txbCity.Text.Equals(dd.WeaterForecast1[i].City))
                        {
                            //if statement to see if the date textbox = date value in the array
                            if (dpDate.Text.Equals(dd.WeaterForecast1[i].Date))
                            {
                                cnn = new SqlConnection(connectionString);

                                int id = (i + 1);
                                string city = txbCity.Text;
                                string date = dpDate.Text;
                                string minTemp1 = minTemp;
                                string maxTemp2 = maxTemp;
                                string precipitation = txbPrecipitation.Text;
                                string humidity = txbHumidity.Text;
                                string windSpeed = txbWindSpeed.Text;
                                try
                                {
                                    //Save edit
                                    cnn.Open();
                                    string query = "update Forecast_Table set City = @city, " +
                                        "Date = @date, MinTemp = @minTemp1, MaxTemp = @maxTemp2," +
                                        "Precipitation = @precipitation, Humidity = @humidity, WindSpeed = @windSpeed" +
                                        " where ForecastID = @id";
                                    cmd = new SqlCommand(query, cnn);

                                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
                                    cmd.Parameters.Add("@city", System.Data.SqlDbType.VarChar);
                                    cmd.Parameters.Add("@date", System.Data.SqlDbType.VarChar);
                                    cmd.Parameters.Add("@minTemp1", System.Data.SqlDbType.Int);
                                    cmd.Parameters.Add("@maxTemp2", System.Data.SqlDbType.Int);
                                    cmd.Parameters.Add("@precipitation", System.Data.SqlDbType.Int);
                                    cmd.Parameters.Add("@humidity", System.Data.SqlDbType.Int);
                                    cmd.Parameters.Add("@windSpeed", System.Data.SqlDbType.Int);

                                    cmd.Parameters["@id"].Value = id;
                                    cmd.Parameters["@city"].Value = city;
                                    cmd.Parameters["@date"].Value = date;
                                    cmd.Parameters["@minTemp1"].Value = minTemp1;
                                    cmd.Parameters["@maxTemp2"].Value = maxTemp2;
                                    cmd.Parameters["@precipitation"].Value = precipitation;
                                    cmd.Parameters["@humidity"].Value = humidity;
                                    cmd.Parameters["@windSpeed"].Value = windSpeed;
                                    cmd.ExecuteNonQuery();
                                    cnn.Close();
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Something went wrong");
                                }

                                //Changing the check value to true
                                check = true;
                            }
                        }
                    }
                }

                //if the content of the add button = Add
                if (btnAdd.Content.Equals("Add"))
                {
                    //for loop that will loop the entire array list
                    for (int i = 0; i < dd.WeaterForecast1.Count; i++)
                    {
                        //if statement to see if the array list city equel to the enterd city ignored case
                        if (dd.WeaterForecast1[i].City.Equals(txbCity.Text, StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Change the enterd item to that city that is equeled
                            txbCity.Text = dd.WeaterForecast1[i].City;
                        }
                    }

                    try
                    {
                        int id = dd.WeaterForecast1.Count + 1;
                        string city = txbCity.Text;
                        string date = dpDate.Text;
                        string minTemp1 = minTemp;
                        string maxTemp2 = maxTemp;
                        string precipitation = txbPrecipitation.Text;
                        string humidity = txbHumidity.Text;
                        string windSpeed = txbWindSpeed.Text;

                        cnn = new SqlConnection(connectionString);

                        //Inserting
                        cnn.Open();
                        string query = "insert into Forecast_Table values (@id, @city, @date, @minTemp1, @maxTemp2, @precipitation, @humidity, @windSpeed);";
                        cmd = new SqlCommand(query, cnn);

                        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@city", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@date", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@minTemp1", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@maxTemp2", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@precipitation", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@humidity", System.Data.SqlDbType.Int);
                        cmd.Parameters.Add("@windSpeed", System.Data.SqlDbType.Int);

                        cmd.Parameters["@id"].Value = id;
                        cmd.Parameters["@city"].Value = city;
                        cmd.Parameters["@date"].Value = date;
                        cmd.Parameters["@minTemp1"].Value = minTemp1;
                        cmd.Parameters["@maxTemp2"].Value = maxTemp2;
                        cmd.Parameters["@precipitation"].Value = precipitation;
                        cmd.Parameters["@humidity"].Value = humidity;
                        cmd.Parameters["@windSpeed"].Value = windSpeed;
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Something went wrong");
                    }

                    //Changing yhe check value to true
                    check = true;
                }
            }
            else
            {
                //changeing the error cheaker to false if there was an error
                error = false;
            }

            //if statement the check value = true 
            if (check == true)
            {
                //if statement this title = Add Weather Report
                if (this.Title == "Add Weather Report")
                {
                    //A message to tell the user the data enterd is eneterd
                    MessageBox.Show("The city " + txbCity.Text + " weather forcast are sucsessfully captured on the date " + dpDate.Text,
                   "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //if statement this title = Update Weather Report
                if (this.Title == "Update Weather Report")
                {
                    //A message to tell the user the data enterd is updated
                    MessageBox.Show("The city " + txbCity.Text + " weather forcast is sucsessfully updated for the date " + dpDate.Text,
                   "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //Clearing all the values if the updating or adding process is finished
                //Also opening back to the main home page so the user can view the data
                //--------------------------------------------------------------------
                check = false;
                txbCity.Clear();
                txbHumidity.Clear();
                txbMaxTemp.Clear();
                txbMinTemp.Clear();
                txbPrecipitation.Clear();
                txbWindSpeed.Clear();
                sMaxTemp.Value = 0;
                sMinTemp.Value = 0;
                dpDate.Text = "";
                txbCity.Clear();
                WeatherForecast WeatherForecast = new WeatherForecast(user);
                WeatherForecast.DataContext = WeatherForecast;
                this.Hide();
                WeatherForecast.ShowDialog();
                //--------------------------------------------------------------------
            }
        }

		private void BtnBack_Click(object sender, RoutedEventArgs e)
		{
            //Button to go back to the main window where a user can start to view weather reports
            //-------------------------------------------------------------------
            WeatherForecast WeatherForecast = new WeatherForecast(user);
            WeatherForecast.DataContext = WeatherForecast;
			this.Hide();
            WeatherForecast.ShowDialog();
            //-------------------------------------------------------------------
        }

        //Min slider getting the string value from the slider
        private void SMinTemp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			//if statement to see if the slider is lodeded
			if (IsLoaded)
			{
				//Changing the min slider value to string and assigning it to the mintemp variable
				minTemp = sMinTemp.Value.ToString();
				//Changing the min slider value to string and assigning it to the mintemp textbox to view to the user
				txbMinTemp.Text = sMinTemp.Value.ToString();
			}
			else
			{
                //Making the error cheaker true if nothing is selected
                error = true;
			}

		}

		//Max slider getting the string value from the slider
		private void SMaxTemp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			//if statement to see if the slider is lodeded
			if (IsLoaded)
			{
				//Changing the max slider value to string and assigning it to the mintemp variable
				maxTemp = sMaxTemp.Value.ToString();
				//Changing the max slider value to string and assigning it to the mintemp textbox to view to the user
				txbMaxTemp.Text = sMaxTemp.Value.ToString();
			}
			else
			{
				//Making the error cheaker true if nothing is selected
				error = true;
			}

		}

		//Method to cheak if an error was made
		private Boolean Error()
		{
			//Checks that the entry for Precipitation to make sure it is an int
			try
			{
				if (Convert.ToInt32(txbPrecipitation.Text) == Convert.ToInt32(txbPrecipitation.Text))
				{
					//It is an int
				}
			}
			//Catch a FormatException for when the user did not entered an int 
			catch (FormatException)
			{
				MessageBox.Show("Please check your that your Precipitation is an interger value",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
				error = true;
			}

			//Checks that the entry for Humidity to make sure it is an int
			try
			{
				if (Convert.ToInt32(txbHumidity.Text) == Convert.ToInt32(txbHumidity.Text))
				{
					//It is an int
				}
			}
			//Catch a FormatException for when the user did not entered an int
			catch (FormatException)
			{
				MessageBox.Show("Please check your that your Humidity is an interger value",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
				error = true;
			}

			//Checks that the entry for Wind Speed to make sure it is an int
			try
			{
				if (Convert.ToInt32(txbWindSpeed.Text) == Convert.ToInt32(txbWindSpeed.Text))
				{
					//It is an int
				}
			}
			//Catch a FormatException for when the user did not entered an int
			catch (FormatException)
			{
				MessageBox.Show("Please check your that your Wind Speed is an interger value",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
				error = true;
			}

			//try and catch to check cities
			try
			{
				//checks that the entry for cities is not an int
				if (Convert.ToInt32(txbCity.Text) > 0 || Convert.ToInt32(txbCity.Text) < 0)
				{
					error = true;
					MessageBox.Show("Please make sure that you enter the city correctly",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			//Catch a FormatException for when the user did not entered an String
			catch (FormatException)
			{
				//It is a string
			}

			//Cheacks that the min temp value is not grater than the max temp
			if (sMaxTemp.Value < sMinTemp.Value)
			{
				error = true;
				MessageBox.Show("Please check your tempreture because min tempreture can not be higher than the max tempreture",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			//Cheak if the city is entered
			if (txbCity.Text.Equals(""))
			{
				error = true;
				MessageBox.Show("Please enter a City",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			//Cheak if the Date is entered
			if (dpDate.Text.Equals(""))
			{
				error = true;
				MessageBox.Show("Please enter a Date",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			//Cheak if the Precipitation is entered
			if (txbPrecipitation.Text.Equals(""))
			{
				error = true;
				MessageBox.Show("Please enter the Precipitation",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			//Cheak if the Humidity is entered
			if (txbHumidity.Text.Equals(""))
			{
				error = true;
				MessageBox.Show("Please enter the Humidity",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			//Cheak if the Wind Speed is entered
			if (txbWindSpeed.Text.Equals(""))
			{
				error = true;
				MessageBox.Show("Please enter the Wind Speed",
				"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

            if (btnAdd.Content.Equals("Update"))
            {
                //Date can go back in time to update dates
            }
            else
            {
                //Make sure a user can't enter a prvious date forecast
                if (dpDate.SelectedDate < DateTime.Today)
                {
                    error = true;
                    MessageBox.Show("You can not enterd previous dates",
                    "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

			try
			{
				//Cheak if the Precipitation is grater than a 100
				if (Convert.ToInt32(txbPrecipitation.Text) > 100)
				{
					error = true;
					MessageBox.Show("Precipitation can't be grater than a 100%",
					"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
				}

				//Cheak if the Humidity grater than a 100
				if (Convert.ToInt32(txbHumidity.Text) > 100)
				{
					error = true;
					MessageBox.Show("Humidity can't be grater than a 100%",
					"WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
			}
			catch (FormatException)
			{
				//Nothing is imputed
			}

            if (btnAdd.Content.Equals("Update"))
            {
                //Date can be the same as in the array and text file
            }
            else
            {
                //For loop to loop the entire array list to see if the city already have the user entry date exists
                for (int i = 0; i < dd.WeaterForecast1.Count; i++)
                {
                    //if statement if the city array value = textbox value
                    if (dd.WeaterForecast1[i].City.Equals(txbCity.Text))
                    {
                        //if staement to check if the array date value = textbox value
                        if (dd.WeaterForecast1[i].Date.Equals(dpDate.Text))
                        {
                            //error =true
                            error = true;

                            //message box to tell the user that the date enterd is already enterd
                            MessageBox.Show("The date enterd is already enterd in the system",
                            "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
            }

            //Loop that will loop the amount of the weather reports array
            for (int i = 0; i < dd.WeaterForecast1.Count; i++)
            {
                //if statement if the city array value = textbox value and that button = Update
                if (dd.WeaterForecast1[i].City.Equals(txbCity.Text) && btnAdd.Content.Equals("Update"))
                {
                    //if staement to check if the array date value = textbox value And button = Update
                    if (dd.WeaterForecast1[i].Date.Equals(dpDate.Text) && btnAdd.Content.Equals("Update"))
                    {
                        //Change check2 to false
                        check2 = false;
                    }
                }
            }
            //if the check2 = true and button = Update 
            if (check2 == true && btnAdd.Content.Equals("Update"))
            {
                //Tells the user that the city or date was not find in the database
                MessageBox.Show("The City or Date was not found in the database",
                "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //change Check2 = true
            check2 = true;

            //return the error value
            return error;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            //Method to fill all the values in quickly used for testing
            //-------------------------------------------------------
            txbCity.Text = "Durban";
			dpDate.Text = "5/11/2019";
			sMinTemp.Value = 18;
			sMaxTemp.Value = 24;
			txbPrecipitation.Text = "23";
			txbHumidity.Text = "34";
			txbWindSpeed.Text = "44";
            //-------------------------------------------------------
        }

        public void UpdateOrAdd(int num)
        {
            //change the button content to update
            btnAdd.Content = "Update";
            //Change the title to Update Weather Report
            this.Title = "Update Weather Report";
        } 

		private void Window_Closed(object sender, EventArgs e)
		{
            //Method to end a the program when the user exits it
            //--------------------------------------
            Application.Current.Shutdown();
            //--------------------------------------
        }
    }
}
