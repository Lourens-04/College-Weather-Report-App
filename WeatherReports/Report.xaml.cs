using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
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

namespace WeatherReports
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        //------------------Code Attribution------------------
        //All pictures was taken from pixabay.com
        //link: https://pixabay.com/vectors/warm-sunny-sun-cloudless-summer-98532/
        //link: https://pixabay.com/vectors/rain-heavy-rain-cloudy-rainy-drops-98538/
        //link: https://pixabay.com/vectors/thunderstorm-lightning-thunder-rain-98541/
        //link: https://pixabay.com/vectors/warm-sunny-cloudy-clouds-sun-98534/
        //-----------------------end------------------------

        //String that will store the username
        string user;

        //Constructor made that will be used to get methods and arrays from the print class
        Print gg = new Print();

        //Constructor made that will be used to get methods and arrays from the reports class
        Reports ee = new Reports();

        //String that will store the start date the user choose
        string date1;

        //String that will store the end date the user choose
        string date2;

        //String that will store the highest and lowest values between all the dates and diffrent cities chosen by 
        //the user that will be displayed in html
        string html = " ";

        public Report(string user)
        {
            InitializeComponent();

            //This string user will = to the username that was send to this window
            this.user = user;
        }

        //Method to display the weather forecast to the user
        public void Display(string city, string date, string minTemp, string maxTemp, string precipitation, string humidity, string windSpeed)
        {
            //if the precipitation grater or equel to 80 the weather picture will be Tunder
            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            if (Convert.ToInt32(precipitation) >= 80)
            {
                //setting the image to bitmap
                BitmapImage Dimage = toBitmap(File.ReadAllBytes(System.IO.Path.GetFileName("Resources/Tunder.png")));
                //putting the display results into the array list
                gg.Print1.Add(new Display(city, Convert.ToDateTime(date).ToString("d MMM yyyy"), minTemp, maxTemp, precipitation, humidity, windSpeed, Dimage));
                //Taking the results and putting into the dataGrid
                dgData.ItemsSource = gg.Print1.ToList();
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            //if the precipitation grater or equel to 50 and less than equel 79 the weather picture will be Rainy
            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            else if (Convert.ToInt32(precipitation) >= 50 && Convert.ToInt32(precipitation) <= 79)
            {
                //setting the image to bitmap
                BitmapImage Dimage = toBitmap(File.ReadAllBytes(System.IO.Path.GetFileName("Resources/Rainy.png")));
                //putting the display results into the array list
                gg.Print1.Add(new Display(city, Convert.ToDateTime(date).ToString("d MMM yyyy"), minTemp, maxTemp, precipitation, humidity, windSpeed, Dimage));
                //Taking the results and putting into the dataGrid
                dgData.ItemsSource = gg.Print1.ToList();
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            //if the precipitation grater or equel to 20 and less than equel 49 the weather picture will be Cloudy
            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            else if (Convert.ToInt32(precipitation) >= 20 && Convert.ToInt32(precipitation) <= 49)
            {
                //setting the image to bitmap
                BitmapImage Dimage = toBitmap(File.ReadAllBytes(System.IO.Path.GetFileName("Resources/Cloudy.png")));
                //putting the display results into the array list
                gg.Print1.Add(new Display(city, Convert.ToDateTime(date).ToString("d MMM yyyy"), minTemp, maxTemp, precipitation, humidity, windSpeed, Dimage));
                //Taking the results and putting into the dataGrid
                dgData.ItemsSource = gg.Print1.ToList();
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            //if the precipitation less than equel 19 the weather picture will be Sunny
            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            else if (Convert.ToInt32(precipitation) <= 19)
            {
                //setting the image to bitmap
                BitmapImage Dimage = toBitmap(File.ReadAllBytes(System.IO.Path.GetFileName("Resources/Sunny.png")));
                //putting the display results into the array list
                gg.Print1.Add(new Display(city, Convert.ToDateTime(date).ToString("d MMM yyyy"), minTemp, maxTemp, precipitation, humidity, windSpeed, Dimage));
                //Taking the results and putting into the dataGrid
                dgData.ItemsSource = gg.Print1.ToList();
            }
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            // Letting the data that was just eneterd into the data grid go through the dgData_LoadingRow to colour in the row
            dgData.LoadingRow += DgData_LoadingRow;
        }

        //Methods used where an image will be turen into a bitmap and then put into an byte array
        //------------------Code Attribution------------------
        //Method was taken from stack overflow
        //Author: Syed Taimoor Hussain
        //link: https://stackoverflow.com/questions/15683314/wpf-add-datagrid-image-column-possible/15683394
        public static BitmapImage toBitmap(Byte[] value)
        {
            if (value != null && value is byte[])
            {
                byte[] ByteArray = value as byte[];
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(ByteArray);
                bmp.EndInit();
                return bmp;
            }
            return null;
        }
        //-----------------------end------------------------

        
        private void DgData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //Method for when a row is created  it will check DmaxTemp value to convert the row clour to red, yellow or blue
            //------------------------------------------------------------------------------------------------
            var row = e.Row;
            var Display = row.DataContext as Display;

            if (sender == dgData)
            {
                //if the value is less or = than 18 and grater or = to 0 then colour will be LightBlue
                if (Convert.ToInt32(Display.DmaxTemp) >= 0 && Convert.ToInt32(Display.DmaxTemp) <= 18)
                {
                    row.Background = new SolidColorBrush(Colors.LightBlue);
                }
                //else if the value is less or = than 26 and grater or = to 19 then colour will be Yellow
                else if (Convert.ToInt32(Display.DmaxTemp) >= 19 && Convert.ToInt32(Display.DmaxTemp) <= 26)
                {
                    row.Background = new SolidColorBrush(Colors.Yellow);
                }
                //else if the value is grater or = to 27 then colour will be OrangeRed
                else if (Convert.ToInt32(Display.DmaxTemp) >= 27)
                {
                    row.Background = new SolidColorBrush(Colors.OrangeRed);
                }
            }
            //------------------------------------------------------------------------------------------------
        }

        
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            //Button to go back to the main window where a user can start to view weather reports
            //------------------------------------------------------------------------------------------------
            WeatherForecast WeatherForecast = new WeatherForecast(user);
            gg.Print1.Clear();
            WeatherForecast.DataContext = WeatherForecast;
            this.Hide();
            WeatherForecast.ShowDialog();
            //------------------------------------------------------------------------------------------------
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            //Button to view the data in an html format so the user can the print it from there
            //------------------------------------------------------------------------------------------------
            string HtmlBody = gg.ExportDataGridToHtml(dgData, date1 , date2, html);
            System.IO.File.WriteAllText("UserReport.html", HtmlBody);
            System.Diagnostics.Process.Start("UserReport.html");
            //------------------------------------------------------------------------------------------------
        }

        public void Ranges(DateTime startDate, DateTime endDate)
        {
            //Used to display the lowest minimum temperature, as well as the highest maximum temperature, precipitation, humidity, and wind speed for one city
            //for loop to loop all the days the user selected
            for (DateTime day1 = startDate; day1 <= endDate; day1 = day1.AddDays(1))
            {
                //Declareing all the values that will be used to display the lowest minimum value
                //values of the dates chosen and the highest maximum values
                //--------------------------------
                long highMin = -66;
                long lowMin = 66;
                long highMax = -66;
                long lowMax = 66;
                long highprecipitation = -1;
                long lowprecipitation = 101;
                long highhumidity = -1;
                long lowhumidity = 101;
                long highwindSpeed = -1;
                long lowwindSpeed = 101;
                //--------------------------------

                //Foreach that will loop through all the reports that were chosen by the user
                foreach (var item in ee.WeaterForecast1)
                {
                    //if the array list date = to the date that was send to this method
                    if (Convert.ToDateTime(item.Date) == day1)
                    {
                        //if the array mintemp grater than the highmintemp variable
                        if (Convert.ToInt64(item.MinTemp) > highMin)
                        {
                            highMin = Convert.ToInt64(item.MinTemp);
                        }
                        //if the array mintemp less than the lowMin variable
                        if (Convert.ToInt64(item.MinTemp) < lowMin)
                        {
                            lowMin = Convert.ToInt64(item.MinTemp);
                        }
                        //if the array MaxTemp grater than the highMax variable
                        if (Convert.ToInt64(item.MaxTemp) > highMax)
                        {
                            highMax = Convert.ToInt64(item.MaxTemp);
                        }
                        //if the array MaxTemp less than the lowMax variable
                        if (Convert.ToInt64(item.MaxTemp) < lowMax)
                        {
                            lowMax = Convert.ToInt64(item.MaxTemp);
                        }
                        //if the array Precipitation grater than the highprecipitation variable
                        if (Convert.ToInt64(item.Precipitation) > highprecipitation)
                        {
                            highprecipitation = Convert.ToInt64(item.Precipitation);
                        }
                        //if the array Precipitation less than the lowprecipitation variable
                        if (Convert.ToInt64(item.Precipitation) < lowprecipitation)
                        {
                            lowprecipitation = Convert.ToInt64(item.Precipitation);
                        }
                        //if the array Humidity grater than the highhumidity variable
                        if (Convert.ToInt64(item.Humidity) > highhumidity)
                        {
                            highhumidity = Convert.ToInt64(item.Humidity);
                        }
                        //if the array Humidity less than the lowhumidity variable
                        if (Convert.ToInt64(item.Humidity) < lowhumidity)
                        {
                            lowhumidity = Convert.ToInt64(item.Humidity);
                        }
                        //if the array WindSpeed grater than the highwindSpeed variable
                        if (Convert.ToInt64(item.WindSpeed) > highwindSpeed)
                        {
                            highwindSpeed = Convert.ToInt64(item.WindSpeed);
                        }
                        //if the array WindSpeed less than the lowwindSpeed variable
                        if (Convert.ToInt64(item.WindSpeed) < lowwindSpeed)
                        {
                            lowwindSpeed = Convert.ToInt64(item.WindSpeed);
                        }
                    }
                }
                //if the variables declared in this method does not contain there defualt values then they can be print into the rich text field
                if (lowMin != 66 && highMax != -66 && lowMax != 66 && highprecipitation != -1 && highhumidity != -1 && highwindSpeed != -1 && lowwindSpeed != 101 && lowhumidity != 101 && lowprecipitation != 101 && highMin != -66)
                {
                    //Desplay the results in a rich text field
                    txbRanges.AppendText("------------------------------------------------------------------------------------------------------\n" 
                    + day1.ToString("d MMMM yyyy")
                    + "\n------------------------------------------------------------------------------------------------------"
                    + "\nLowest Minimum Temperature: " + lowMin.ToString() +"\t\t"+ "Highest Minimum Temperature: " + highMin.ToString()
                    + "\n\n" + "Lowest Maximum Temperature: " + lowMax.ToString() + "\t\t" + "Highest Maximum Temperature: " + highMax.ToString() 
                    + "\n\n" + "Lowest Precipitation: " + lowprecipitation.ToString() + "\t\t\t\t" + "Highest Precipitation: " + highprecipitation.ToString() 
                    + "\n\n" + "Lowest Humidity: " + lowhumidity.ToString() + "\t\t\t\t" + "Highest Humidity: " + highhumidity.ToString() 
                    + "\n\n" + "Lowest Wind Speed: " + lowwindSpeed.ToString() + "\t\t\t\t" + "Highest Wind Speed: " + highwindSpeed.ToString() 
                    + "\n\n");
                }
            }
        }

        //Used to display the lowest minimum temperature, as well as the highest maximum temperature, precipitation, humidity, and wind speed for multible Cities
        public void Ranges2(DateTime startDate, DateTime endDate, string city)
        {
            //Declareing all the values that will be used to display the lowest minimum value
            //values of the dates chosen and the highest maximum values
            //---------------------------------------
            long highMin = -66;
            long lowMin = 66;
            long highMax = -66;
            long lowMax = 66;
            long highprecipitation = -1;
            long lowprecipitation = 101;
            long highhumidity = -1;
            long lowhumidity = 101;
            long highwindSpeed = -1;
            long lowwindSpeed = 101;
            //---------------------------------------

            //for loop to loop all the days the user selected
            for (DateTime day1 = startDate; day1 <= endDate; day1 = day1.AddDays(1))
            {
                //Foreach that will loop through all the reports that were chosen by the user
                foreach (var item in ee.WeaterForecast1)
                {
                    //if the array list date = to the date that was send to this method
                    if (Convert.ToDateTime(item.Date) == day1)
                    {
                        //if array city = to the city variable that was send to this window
                        if (item.City == city)
                        {
                            //if the array mintemp grater than the highmintemp variable
                            if (Convert.ToInt64(item.MinTemp) > highMin)
                            {
                                highMin = Convert.ToInt64(item.MinTemp);
                            }
                            //if the array mintemp less than the lowMin variable
                            if (Convert.ToInt64(item.MinTemp) < lowMin)
                            {
                                lowMin = Convert.ToInt64(item.MinTemp);
                            }
                            //if the array MaxTemp grater than the highMax variable
                            if (Convert.ToInt64(item.MaxTemp) > highMax)
                            {
                                highMax = Convert.ToInt64(item.MaxTemp);
                            }
                            //if the array MaxTemp less than the lowMax variable
                            if (Convert.ToInt64(item.MaxTemp) < lowMax)
                            {
                                lowMax = Convert.ToInt64(item.MaxTemp);
                            }
                            //if the array Precipitation grater than the highprecipitation variable
                            if (Convert.ToInt64(item.Precipitation) > highprecipitation)
                            {
                                highprecipitation = Convert.ToInt64(item.Precipitation);
                            }
                            //if the array Precipitation less than the lowprecipitation variable
                            if (Convert.ToInt64(item.Precipitation) < lowprecipitation)
                            {
                                lowprecipitation = Convert.ToInt64(item.Precipitation);
                            }
                            //if the array Humidity grater than the highhumidity variable
                            if (Convert.ToInt64(item.Humidity) > highhumidity)
                            {
                                highhumidity = Convert.ToInt64(item.Humidity);
                            }
                            //if the array Humidity less than the lowhumidity variable
                            if (Convert.ToInt64(item.Humidity) < lowhumidity)
                            {
                                lowhumidity = Convert.ToInt64(item.Humidity);
                            }
                            //if the array WindSpeed grater than the highwindSpeed variable
                            if (Convert.ToInt64(item.WindSpeed) > highwindSpeed)
                            {
                                highwindSpeed = Convert.ToInt64(item.WindSpeed);
                            }
                            //if the array WindSpeed less than the lowwindSpeed variable
                            if (Convert.ToInt64(item.WindSpeed) < lowwindSpeed)
                            {
                                lowwindSpeed = Convert.ToInt64(item.WindSpeed);
                            }
                        }
                    }
                }
            }
            //if the variables declared in this method does not contain there defualt values then they can be print into the rich text field
            if (lowMin != 66 && highMax != -66 && lowMax != 66 && highprecipitation != -1 && highhumidity != -1 && highwindSpeed != -1 && lowwindSpeed != 101 && lowhumidity != 101 && lowprecipitation != 101 && highMin != -66)
            {
                //Desplay the results in a rich text field
                txbRanges.AppendText("\nLowest Minimum Temperature: " + lowMin.ToString() + "\t\t" + "Highest Minimum Temperature: " + highMin.ToString()
                + "\n\n" + "Lowest Maximum Temperature: " + lowMax.ToString() + "\t\t" + "Highest Maximum Temperature: " + highMax.ToString()
                + "\n\n" + "Lowest Precipitation: " + lowprecipitation.ToString() + "\t\t\t\t" + "Highest Precipitation: " + highprecipitation.ToString()
                + "\n\n" + "Lowest Humidity: " + lowhumidity.ToString() + "\t\t\t\t" + "Highest Humidity: " + highhumidity.ToString()
                + "\n\n" + "Lowest Wind Speed: " + lowwindSpeed.ToString() + "\t\t\t\t" + "Highest Wind Speed: " + highwindSpeed.ToString()
                + "\n\n");
            }
        }

        public void HtmlRanges(DateTime startDate, DateTime endDate)
        {
            //Date1 is = startdate that the user chose 
            date1 = startDate.ToString("d MMMM yyyy");

            //Date2 is = enddate that the user chose 
            date2 = endDate.ToString("d MMMM yyyy");

            //for loop to loop all the days the user selected
            for (DateTime day1 = startDate; day1 <= endDate; day1 = day1.AddDays(1))
            {
                //Declareing all the values that will be used to display the lowest minimum value
                //values of the dates chosen and the highest maximum values
                //---------------------------------------
                long highMin = -66;
                long lowMin = 66;
                long highMax = -66;
                long lowMax = 66;
                long highprecipitation = -1;
                long lowprecipitation = 101;
                long highhumidity = -1;
                long lowhumidity = 101;
                long highwindSpeed = -1;
                long lowwindSpeed = 101;
                //---------------------------------------

                //Foreach that will loop through all the reports that were chosen by the user
                foreach (var item in ee.WeaterForecast1)
                {
                    //if the array list date = to the date that was send to this method
                    if (Convert.ToDateTime(item.Date) == day1)
                    {
                        //if the array mintemp grater than the highmintemp variable
                        if (Convert.ToInt64(item.MinTemp) > highMin)
                        {
                            highMin = Convert.ToInt64(item.MinTemp);
                        }
                        //if the array mintemp less than the lowMin variable
                        if (Convert.ToInt64(item.MinTemp) < lowMin)
                        {
                            lowMin = Convert.ToInt64(item.MinTemp);
                        }
                        //if the array MaxTemp grater than the highMax variable
                        if (Convert.ToInt64(item.MaxTemp) > highMax)
                        {
                            highMax = Convert.ToInt64(item.MaxTemp);
                        }
                        //if the array MaxTemp less than the lowMax variable
                        if (Convert.ToInt64(item.MaxTemp) < lowMax)
                        {
                            lowMax = Convert.ToInt64(item.MaxTemp);
                        }
                        //if the array Precipitation grater than the highprecipitation variable
                        if (Convert.ToInt64(item.Precipitation) > highprecipitation)
                        {
                            highprecipitation = Convert.ToInt64(item.Precipitation);
                        }
                        //if the array Precipitation less than the lowprecipitation variable
                        if (Convert.ToInt64(item.Precipitation) < lowprecipitation)
                        {
                            lowprecipitation = Convert.ToInt64(item.Precipitation);
                        }
                        //if the array Humidity grater than the highhumidity variable
                        if (Convert.ToInt64(item.Humidity) > highhumidity)
                        {
                            highhumidity = Convert.ToInt64(item.Humidity);
                        }
                        //if the array Humidity less than the lowhumidity variable
                        if (Convert.ToInt64(item.Humidity) < lowhumidity)
                        {
                            lowhumidity = Convert.ToInt64(item.Humidity);
                        }
                        //if the array WindSpeed grater than the highwindSpeed variable
                        if (Convert.ToInt64(item.WindSpeed) > highwindSpeed)
                        {
                            highwindSpeed = Convert.ToInt64(item.WindSpeed);
                        }
                        //if the array WindSpeed less than the lowwindSpeed variable
                        if (Convert.ToInt64(item.WindSpeed) < lowwindSpeed)
                        {
                            lowwindSpeed = Convert.ToInt64(item.WindSpeed);
                        }
                    }
                }
                //if the variables declared in this method does not contain there defualt values then they can be print into the html format
                if (lowMin != 66 && highMax != -66 && lowMax != 66 && highprecipitation != -1 && highhumidity != -1 && highwindSpeed != -1 && lowwindSpeed != 101 && lowhumidity != 101 && lowprecipitation != 101 && highMin != -66)
                {
                    //Desplay the results in a html using a string caled html
                    html = html + ("------------------------------------------------------------------------------------------------------</Br>"
                    + day1.ToString("d MMMM yyyy")
                    + "</Br>------------------------------------------------------------------------------------------------------"
                    + "</Br>Lowest Minimum Temperature: " + lowMin.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Minimum Temperature: " + highMin.ToString()
                    + "</Br>" + "Lowest Maximum Temperature: " + lowMax.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Maximum Temperature: " + highMax.ToString()
                    + "</Br>" + "Lowest Precipitation: " + lowprecipitation.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Precipitation: " + highprecipitation.ToString()
                    + "</Br>" + "Lowest Humidity: " + lowhumidity.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Humidity: " + highhumidity.ToString()
                    + "</Br>" + "Lowest Wind Speed: " + lowwindSpeed.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Wind Speed: " + highwindSpeed.ToString()
                    + "</Br>");
                }
            } 
        }

        public void HtmlRanges2(DateTime startDate, DateTime endDate, string city)
        {
            //Date1 is = startDate that the user chose 
            date1 = startDate.ToString("d MMMM yyyy");

            //Date2 is = enddate that the user chose 
            date2 = endDate.ToString("d MMMM yyyy");

            //Declareing all the values that will be used to display the lowest minimum value
            //values of the dates chosen and the highest maximum values
            //---------------------------------------
            long highMin = -66;
            long lowMin = 66;
            long highMax = -66;
            long lowMax = 66;
            long highprecipitation = -1;
            long lowprecipitation = 101;
            long highhumidity = -1;
            long lowhumidity = 101;
            long highwindSpeed = -1;
            long lowwindSpeed = 101;
            //---------------------------------------

            //for loop to loop all the days the user selected
            for (DateTime day1 = startDate; day1 <= endDate; day1 = day1.AddDays(1))
            {
                //Foreach that will loop through all the reports that were chosen by the user
                foreach (var item in ee.WeaterForecast1)
                {
                    //if the array list date = to the date that was send to this method
                    if (Convert.ToDateTime(item.Date) == day1)
                    {
                        //if array city = to the city variable that was send to this window
                        if (item.City == city)
                        {
                            //if the array mintemp grater than the highmintemp variable
                            if (Convert.ToInt64(item.MinTemp) > highMin)
                            {
                                highMin = Convert.ToInt64(item.MinTemp);
                            }
                            //if the array mintemp less than the lowMin variable
                            if (Convert.ToInt64(item.MinTemp) < lowMin)
                            {
                                lowMin = Convert.ToInt64(item.MinTemp);
                            }
                            //if the array MaxTemp grater than the highMax variable
                            if (Convert.ToInt64(item.MaxTemp) > highMax)
                            {
                                highMax = Convert.ToInt64(item.MaxTemp);
                            }
                            //if the array MaxTemp less than the lowMax variable
                            if (Convert.ToInt64(item.MaxTemp) < lowMax)
                            {
                                lowMax = Convert.ToInt64(item.MaxTemp);
                            }
                            //if the array Precipitation grater than the highprecipitation variable
                            if (Convert.ToInt64(item.Precipitation) > highprecipitation)
                            {
                                highprecipitation = Convert.ToInt64(item.Precipitation);
                            }
                            //if the array Precipitation less than the lowprecipitation variable
                            if (Convert.ToInt64(item.Precipitation) < lowprecipitation)
                            {
                                lowprecipitation = Convert.ToInt64(item.Precipitation);
                            }
                            //if the array Humidity grater than the highhumidity variable
                            if (Convert.ToInt64(item.Humidity) > highhumidity)
                            {
                                highhumidity = Convert.ToInt64(item.Humidity);
                            }
                            //if the array Humidity less than the lowhumidity variable
                            if (Convert.ToInt64(item.Humidity) < lowhumidity)
                            {
                                lowhumidity = Convert.ToInt64(item.Humidity);
                            }
                            //if the array WindSpeed grater than the highwindSpeed variable
                            if (Convert.ToInt64(item.WindSpeed) > highwindSpeed)
                            {
                                highwindSpeed = Convert.ToInt64(item.WindSpeed);
                            }
                            //if the array WindSpeed less than the lowwindSpeed variable
                            if (Convert.ToInt64(item.WindSpeed) < lowwindSpeed)
                            {
                                lowwindSpeed = Convert.ToInt64(item.WindSpeed);
                            }
                        }
                    }
                }
            }
            //if the variables declared in this method does not contain there defualt values then they can be print into the html format
            if (lowMin != 66 && highMax != -66 && lowMax != 66 && highprecipitation != -1 && highhumidity != -1 && highwindSpeed != -1 && lowwindSpeed != 101 && lowhumidity != 101 && lowprecipitation != 101 && highMin != -66)
            {
                //Desplay the results in a html using a string caled html
                html = ("</Br>Lowest Minimum Temperature: " + lowMin.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Minimum Temperature: " + highMin.ToString()
                    + "</Br>" + "Lowest Maximum Temperature: " + lowMax.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Maximum Temperature: " + highMax.ToString()
                    + "</Br>" + "Lowest Precipitation: " + lowprecipitation.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Precipitation: " + highprecipitation.ToString()
                    + "</Br>" + "Lowest Humidity: " + lowhumidity.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Humidity: " + highhumidity.ToString()
                    + "</Br>" + "Lowest Wind Speed: " + lowwindSpeed.ToString() + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + "Highest Wind Speed: " + highwindSpeed.ToString()
                    + "</Br>");
            }
        }

        
        private void Window_Closed(object sender, EventArgs e)
        {
            //Method to end a the program when the user exits it
            //---------------------------------------------
            Application.Current.Shutdown();
            //---------------------------------------------
        }
    }
}
