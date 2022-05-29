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
using System.Data.SqlClient;
using System.Configuration;

namespace WeatherReports
{
    /// <summary>
    /// Interaction logic for WeatherForecast.xaml
    /// </summary>
    public partial class WeatherForecast : Window
    {
        //------------------Code Attribution------------------
        //favorite pictures was taken from pixabay.com
        //link: https://cdn.pixabay.com/photo/2017/06/13/12/54/star-2398792_960_720.png
        //link: https://cdn.pixabay.com/photo/2017/06/13/12/54/star-2398796_960_720.png
        //-----------------------end------------------------

        //Checker used for when a user select Multiple cities
        private Boolean check = false;

        //error cheaker for error cheaking
        private Boolean error = false;

        //Accessing the Reports class to get the array list
        Reports dd = new Reports();
        
        //String made to store the username for the user that is log in curently
        string user;

        //Declareing an array list that will be used to store favorites
        List<string> favlist = new List<string>();

        //Constructor made to call methods from the users class
        Users ff = new Users();

        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        SqlConnection cnn;
        SqlCommand cmd;

        public WeatherForecast(string user)
        {
            InitializeComponent();

            //takeing the username that was pass to this class = to the string called user in this window
            this.user = user;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Clearing the weatherForecast array 
            dd.WeaterForecast1.Clear();

            //Calling the method to take all the reports from the text file and put it back into the array
            dd.Values();

            //Made to Display the Date 
            txbDate.Text = DateTime.Now.ToString();
            //Used to make the listbox allow multible cities
            lbCities.SelectionMode = SelectionMode.Multiple;
            //Used to make the listbox allow multible cities
            lbFavorites.SelectionMode = SelectionMode.Multiple;

            //if statement to start fill the listBox with the cities
            if (dd.WeaterForecast1.Count > 0)
            {
                //while loop to start puting cities into the listbox
                int i = 0;
                while (i < dd.WeaterForecast1.Count)
                {
                    //foreach loop to loop all the items in the listbox
                    foreach (var item in lbCities.Items)
                    {
                        //if statement to see if the city is equel to a city in the array an if yes change checker to true
                        if (item.ToString() == dd.WeaterForecast1[i].City)
                        {
                            //Check Boolean value = true
                            check = true;
                            break;
                        }
                    }
                    //if check = false then put the city into the listbox
                    if (check == false)
                    {
                        //Adding the cities to the lbcities listbox
                        lbCities.Items.Add(dd.WeaterForecast1[i].City);
                    }
                    //Change check back to false
                    check = false;
                    i++;
                }

                //While loop that goes through all the users in the user array list
                int o = 0;
                while (o<ff.Users1.Count)
                {
                    //if the username in the arraylist = to the string user that holds the username that was send
                    //from the log in page
                    if (ff.Users1[o].Username == user)
                    {
                        //if the user role = user hide buttons to add and update
                        //------------------------------------------------------
                        if(ff.Users1[o].Userrole == "user")
                        {
                            btnUpdate.Visibility = Visibility.Hidden;
                            btnAdd.Visibility = Visibility.Hidden;
                        }
                        //------------------------------------------------------
                    }
                    o++;
                }
            }

            //Clear the array list for favorites
            favlist.Clear();

            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                cmd = new SqlCommand("select * from favourite_Table", cnn);
                SqlDataReader sqlReader = cmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    string id = sqlReader.GetValue(0) + "";
                    string username = sqlReader.GetValue(1) + "";
                    string city = sqlReader.GetValue(2) + "";
                    favlist.Add(username + "," + city);
                }
                sqlReader.Close();
                cmd.Dispose();
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error! (Maybe connection cannot be open)");
            }

            //LinQ statment to get the city names of the user that log in 
            var fav = from f in favlist where f.Contains(user + ",") select f.Substring(f.IndexOf(",") + 1);

            //Clear the lbFavorites listbox
            lbFavorites.Items.Clear();

            //Foreach loop that will add all the cities in the favorites cities into the favorites list box
            foreach (var item in fav)
            {
                //Adding the city to the list box called favorites
                lbFavorites.Items.Add(item);
            }
        }

        //Button to generate a weather report to display to a user
        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            Report newWindow2 = new Report(user);
            //Error method checks for error that can occur
            Error();
            //if statement to see if there was an error or not
            if (error == false)
            {
                //Takes the first date and converts it to a date time named start date
                DateTime startDate = Convert.ToDateTime(dpDate1.Text);
                //Takes the second date and converts it to a date time named end date
                DateTime endDate = Convert.ToDateTime(dpDate2.Text);

                //if statement to see if the listbox total number of items is grater than -1
                if (lbCities.Items.Count > -1)
                {
                    //i declared for a while loop
                    int i = 0;
                    //while loop that will loop the amount of items selected in the listbox
                    while (i < lbCities.SelectedItems.Count)
                    {
                        //for loop that will loop the entire array list
                        for (int j = 0; j < dd.WeaterForecast1.Count; j++)
                        {
                            //Takes the dtae that is in the array and is converted into a date time named compare date
                            DateTime compareDate = Convert.ToDateTime(dd.WeaterForecast1[j].Date);
                            //if statement to see if the selected item in the listbox is equel to the city in the array list
                            if (lbCities.SelectedItems[i].Equals(dd.WeaterForecast1[j].City))
                            {
                                //if statement to see if the compare date is equel to the date in the array list
                                if (compareDate.Date.Date >= startDate && compareDate.Date.Date <= endDate)
                                {
                                    //Send the array list items that was true to a method in the report window
                                    newWindow2.Display(dd.WeaterForecast1[j].City, dd.WeaterForecast1[j].Date,
                                    dd.WeaterForecast1[j].MinTemp, dd.WeaterForecast1[j].MaxTemp,
                                    dd.WeaterForecast1[j].Precipitation, dd.WeaterForecast1[j].Humidity,
                                    dd.WeaterForecast1[j].WindSpeed);
                                }
                            }
                        }
                        i++;
                    }
                }

                //if statement to see if the listbox total number of items is grater than -1
                if (lbFavorites.Items.Count > -1)
                {
                    //i declared for a while loop
                    int i = 0;
                    //while loop that will loop the amount of items selected in the listbox
                    while (i < lbFavorites.SelectedItems.Count)
                    {
                        //for loop that will loop the entire array list
                        for (int j = 0; j < dd.WeaterForecast1.Count; j++)
                        {
                            //Takes the dtae that is in the array and is converted into a date time named compare date
                            DateTime compareDate = Convert.ToDateTime(dd.WeaterForecast1[j].Date);
                            //if statement to see if the selected item in the listbox is equel to the city in the array list
                            if (lbFavorites.SelectedItems[i].Equals(dd.WeaterForecast1[j].City))
                            {
                                //if statement to see if the compare date is equel to the date in the array list
                                if (compareDate.Date.Date >= startDate && compareDate.Date.Date <= endDate)
                                {
                                    //Send the array list items that was true to a method in the report window
                                    newWindow2.Display(dd.WeaterForecast1[j].City, dd.WeaterForecast1[j].Date,
                                    dd.WeaterForecast1[j].MinTemp, dd.WeaterForecast1[j].MaxTemp,
                                    dd.WeaterForecast1[j].Precipitation, dd.WeaterForecast1[j].Humidity,
                                    dd.WeaterForecast1[j].WindSpeed);
                                }
                            }
                        }
                        i++;
                    }
                }

                //if the comparing is done then a the report window will open to show the report
                newWindow2.DataContext = newWindow2;

                //sends start date and end date to ranges if more than one city was selected for the lbcities listbox
                if (lbCities.SelectedItems.Count > 1)
                {
                    //Sends it to the new window for a ritch text box
                    newWindow2.Ranges(startDate, endDate);

                    //Sends it to the new window to be able to open in html
                    newWindow2.HtmlRanges(startDate, endDate);
                }

                //sends start date and end date to ranges2 if only one city was selected for the lbcities listbox
                if (lbCities.SelectedItems.Count == 1)
                {
                    //Sends it to the new window for a ritch text box
                    newWindow2.Ranges2(startDate, endDate, lbCities.SelectedItem.ToString());

                    //Sends it to the new window to be able to open in html
                    newWindow2.HtmlRanges2(startDate, endDate, lbCities.SelectedItem.ToString());
                }

                //sends start date and end date to ranges if more than one city was selected for the lbFavorites listbox
                if (lbFavorites.SelectedItems.Count > 1)
                {
                    //Sends it to the new window for a ritch text box
                    newWindow2.Ranges(startDate, endDate);

                    //Sends it to the new window to be able to open in html
                    newWindow2.HtmlRanges(startDate, endDate);
                }

                //sends start date and end date to ranges2 if only one city was selected for the lbFavorites listbox
                if (lbFavorites.SelectedItems.Count == 1)
                {
                    //Sends it to the new window for a ritch text box
                    newWindow2.Ranges2(startDate, endDate, lbFavorites.SelectedItem.ToString());

                    //Sends it to the new window to be able to open in html
                    newWindow2.HtmlRanges2(startDate, endDate, lbFavorites.SelectedItem.ToString());
                }
                //Hides the window
                this.Hide();
                //opens the new window
                newWindow2.ShowDialog();
            }
            else
            {
                //changeing the error cheaker to false if there was an error
                error = false;
            }
        }

        //Button that will open the AddweatherReport window to allow a user to add a new forecast
        //--------------------------------------------------------------------------
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWeatherReport newWindow = new AddWeatherReport(user);
            newWindow.DataContext = this;
            this.Hide();
            newWindow.ShowDialog();
        }
        //--------------------------------------------------------------------------

        //Method to cheak if an error was made
        private Boolean Error()
        {
            //Checks if the dpDate1 and dpDate2 is filled and if yes then turn error cheaker to true
            //--------------------------------------------------------------------------
            if (dpDate1.Text.Equals("") || dpDate2.Text.Equals(""))
            {
                error = true;
                MessageBox.Show("Please insure that you picked your dates to view the weather forecast",
                "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //--------------------------------------------------------------------------
            else
            {
                //try and catch for the checkStartDate and checkEndDate to see if its in the correct format
                try
                {
                    //Declareing checkStartDate as DateTime 
                    DateTime checkStartDate = Convert.ToDateTime(dpDate1.Text);
                    //Declareing checkEndDate as DateTime 
                    DateTime checkEndDate = Convert.ToDateTime(dpDate2.Text);

                    //Declareing int sum1
                    int sum1 = 0;
                    //Declareing int sum2
                    int sum2 = 0;

                    //Checks if the range between the dates have data enterd
                    //--------------------------------------------------------------------------
                    for (int i = 0; i < dd.WeaterForecast1.Count; i++)
                    {
                        if (checkStartDate.Date != Convert.ToDateTime(dd.WeaterForecast1[i].Date).Date)
                        {
                            sum1 = sum1 + 1;
                        }
                        if (checkEndDate.Date != Convert.ToDateTime(dd.WeaterForecast1[i].Date).Date)
                        {
                            sum2 = sum2 + 1;
                        }
                    }
                    //--------------------------------------------------------------------------

                    //Checks if the range between the dates have data enterd
                    //--------------------------------------------------------------------------
                    if (sum1 == dd.WeaterForecast1.Count && sum2 == dd.WeaterForecast1.Count)
                    {
                        error = true;
                        MessageBox.Show("There is no data between this range of dates",
                        "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    //--------------------------------------------------------------------------

                    //Checks if the start day is grater than the end day and if yes then turn error cheaker to true
                    //--------------------------------------------------------------------------
                    if (checkStartDate.Day > checkEndDate.Day)
                    {
                        error = true;
                        MessageBox.Show("Please insure that the dates you selected are correct",
                        "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    //--------------------------------------------------------------------------
                }
                //catch FormatException 
                catch (FormatException)
                {
                    MessageBox.Show("Please insure that you picked your dates to view the weather forecast",
                    "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            //Checks if no city was selected and if yes then turn error cheaker to true
            //--------------------------------------------------------------------------
            if (lbCities.SelectedIndex == -1 && lbFavorites.SelectedIndex == -1)
            {
                error = true;
                MessageBox.Show("Please insure that you selected a city",
                "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //--------------------------------------------------------------------------

            //Returns error boolean value
            return error;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //Button that will take the forecaster to the update page
            int num = 1;

            //Open the AddWeatherReport window but it will be for updateing
            //--------------------------------------------------------------------------
            AddWeatherReport newWindow = new AddWeatherReport(user);
            newWindow.UpdateOrAdd(num);
            newWindow.DataContext = this;
            this.Hide();
            newWindow.ShowDialog();
            //--------------------------------------------------------------------------
        }

        private void BtnFav_Click(object sender, RoutedEventArgs e)
        {
            //LinQ statment to get the city names of the user that log in 
            var fav = from f in favlist where f.Contains(user + ",") select f.Substring(f.IndexOf(",") + 1);

            //Foreach statement of all the selected items in the listbox cities
            foreach (string i in lbCities.SelectedItems)
            {
                //if Fav is the same as the selected items from the listbox cities
                if (!fav.Contains(i))
                {
                    //Stream writer to write into favorites text file
                    //------------------------------------------------------------
                    try
                    {
                        string id = user + i;
                        string username = user;
                        string city = i;

                        cnn = new SqlConnection(connectionString);

                        //Inserting
                        cnn.Open();
                        string query = "insert into favourite_Table values (@id, @username, @city);";
                        cmd = new SqlCommand(query, cnn);

                        cmd.Parameters.Add("@id", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar);
                        cmd.Parameters.Add("@city", System.Data.SqlDbType.VarChar);

                        cmd.Parameters["@id"].Value = id;
                        cmd.Parameters["@username"].Value = username;
                        cmd.Parameters["@city"].Value = city;
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Something went wrong");
                    }

                    //Adding the username and the cities they chose and adding it to the list to store all the favorites
                    favlist.Add(user + "," + i);
                    //Adding the cities to the favorites listbox
                    lbFavorites.Items.Add(i);

                    MessageBox.Show(i + " is added to favorites",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    //------------------------------------------------------------
                }
                else
                {
                    //message box to tell the user that the cities were added to favorites
                    MessageBox.Show(i + " Already added to favorite",
                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BtnUnfav_Click(object sender, RoutedEventArgs e)
        {
            //LinQ statment to get the city names of the user that log in
            var fav = from f in favlist where f.Contains(user + ",") select f.Substring(f.IndexOf(",") + 1);

            //Foreach statement of all the selected items in the listbox cities
            foreach (string i in lbFavorites.SelectedItems)
            {
                //if Fav is the same as the selected items from the listbox cities
                if (fav.Contains(i))
                {
                    try
                    {
                        //Deleting
                        cnn.Open();
                        string id = user+i;
                        string query = "delete from favourite_Table where favouriteID = @id;";
                        cmd = new SqlCommand(query, cnn);
                        cmd.Parameters.Add("@id", System.Data.SqlDbType.VarChar);

                        cmd.Parameters["@id"].Value = id;

                        cmd.ExecuteNonQuery();
                        cnn.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Something went wrong");
                    }
                    //Message box to tell the user that the cities were unvavorite
                    MessageBox.Show(i + " is Unfavorited",
                   "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    //------------------------------------------------------------
                }
            }
            
            //Clear the listbox for favorites
            lbFavorites.Items.Clear();
            favlist.Clear();

            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                cmd = new SqlCommand("select * from favourite_Table", cnn);
                SqlDataReader sqlReader = cmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    string id = sqlReader.GetValue(0) + "";
                    string username = sqlReader.GetValue(1) + "";
                    string city = sqlReader.GetValue(2) + "";
                    favlist.Add(username + "," + city);
                }
                sqlReader.Close();
                cmd.Dispose();
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error! (Maybe connection cannot be open)");
            }

            //LinQ statment to get the city names of the user that log in
            fav = from f in favlist where f.Contains(user + ",") select f.Substring(f.IndexOf(",") + 1);

            //Foreach loop that will populate the favorites listbox 
            //------------------------------------------------------------
            foreach (var item in fav)
            {
                //adding cities to favorites listbox 
                lbFavorites.Items.Add(item);
            }
            //------------------------------------------------------------
        }
        
        private void Window_Closed(object sender, EventArgs e)
        {
            //Method to end a the program when the user exits it
            //------------------------------------------------------------
            Application.Current.Shutdown();
            //------------------------------------------------------------
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            //Opening back to the log in page if this button is click
            //-----------------------------------
            MainWindow main = new MainWindow();
            main.DataContext = this;
            this.Hide();
            main.ShowDialog();
            //-----------------------------------
        }

        private void LbCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if staement to see if the list box for cities selected items is more than -1
            //------------------------------------------------------------
            if (lbCities.SelectedIndex>-1)
            {
                //Unselecting the selected items in the listbox for Favorites
                lbFavorites.SelectedIndex = -1;
            }
            //------------------------------------------------------------
        }

        private void LbFavorites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if staement to see if the list box for Favorites selected items is more than -1
            //------------------------------------------------------------
            if (lbFavorites.SelectedIndex > -1)
            {
                //Unselecting the selected items in the listbox for cities
                lbCities.SelectedIndex = -1;
            }
            //------------------------------------------------------------
        }
    }
}


