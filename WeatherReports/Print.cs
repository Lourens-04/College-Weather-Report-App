using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WeatherReports
{
    class Print
    {
        //declareing the print array list
        public static IList<Display> print = new List<Display>();

        //Get and Set for the print array list
        internal IList<Display> Print1 { get => print; set => print = value; }
        
        //Methods used to transform data into an html file that will open in you default browser
        //some of the code in this method is my own work like the design colours and that I am not 
        //exporting a data table but a datagrid but the hole layout I got from the internet
        //--------------------------------------------------------Code Attribution--------------------------------------------------------------
        //Method was taken from c-sharpcorner
        //Author: Devesh Omar
        //link: https://www.c-sharpcorner.com/UploadFile/deveshomar/export-datatable-to-html-in-C-Sharp/
        public string ExportDataGridToHtml(DataGrid dt, string date1, string date2, string html)
        {
            StringBuilder strHTMLBuilder = new StringBuilder();
            strHTMLBuilder.Append("<html >");
            strHTMLBuilder.Append("<center><head>");
            strHTMLBuilder.Append("<p style='font-family:Garamond; font-size:30'>" );
            strHTMLBuilder.Append("<strong>Weather Forecast</strong>");
            strHTMLBuilder.Append("</p></head></center>");
            strHTMLBuilder.Append(" <body bgcolor=' #3c8484'> ");

            //This paragraph is also my own work to display the details of the data the user requested
            //---------------------------------------------------------------------------------------------
            strHTMLBuilder.Append("<center><p style='font-family:Garamond; font-size:30'><strong>");
            strHTMLBuilder.Append("Weather Report Between " + date1 + " - " + date2 );
            strHTMLBuilder.Append("</strong></p></center>");
            //---------------------------------------------------------------------------------------------

            //I also customize the table to my liking
            strHTMLBuilder.Append("<center><table style = 'width: 100%'  border='1' cellpadding='0' cellspacing='0' bgcolor='White' style='font-family:Garamond; font-size:30'>");

            strHTMLBuilder.Append("<tr > ");
            foreach (DataGridColumn myColumn in dt.Columns)
            {
                strHTMLBuilder.Append("<td align='center' font-size:'30' bgcolor='Aqua'> <strong>");
                strHTMLBuilder.Append(myColumn.Header);
                strHTMLBuilder.Append(" </strong> </td>");
            }
            strHTMLBuilder.Append("</tr>");

            //The way I read in the row from the data grid is my own work
            //I use a fore loop to go through all the items in the data grid
            //Also Ading the pictures depending on the parcipitation and as well colouring 
            //the row depending on the max temp
            //----------------------------------------------------------------------------------------------------------------------------------
            for (int j = 0; j < dt.Items.Count; j++)
            {
                strHTMLBuilder.Append("<tr >");
                if (Convert.ToInt32(Print1[j].DmaxTemp) >= 0 && Convert.ToInt32(Print1[j].DmaxTemp) <= 18)
                {
                    strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                    strHTMLBuilder.Append(Print1[j].Dcity);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                    strHTMLBuilder.Append(Print1[j].Ddate);
                    strHTMLBuilder.Append("</td></center>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                    strHTMLBuilder.Append(Print1[j].DminTemp);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                    strHTMLBuilder.Append(Print1[j].DmaxTemp);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                    strHTMLBuilder.Append(Print1[j].Dprecipitation);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                    strHTMLBuilder.Append(Print1[j].Dhumidity);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                    strHTMLBuilder.Append(Print1[j].DwindSpeed);
                    strHTMLBuilder.Append("</td>");

                    if (Convert.ToUInt16(Print1[j].Dprecipitation) >= 80)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/thunderstorm-98541_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) >= 50 && Convert.ToInt32(Print1[j].Dprecipitation) <= 79)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/rain-98538_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) >= 20 && Convert.ToInt32(Print1[j].Dprecipitation) <= 49)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/warm-98534_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) <= 19)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='LightBlue'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/warm-98532_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                }

                if (Convert.ToInt32(Print1[j].DmaxTemp) >= 19 && Convert.ToInt32(Print1[j].DmaxTemp) <= 26)
                {
                    strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                    strHTMLBuilder.Append(Print1[j].Dcity);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                    strHTMLBuilder.Append(Print1[j].Ddate);
                    strHTMLBuilder.Append("</td></center>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                    strHTMLBuilder.Append(Print1[j].DminTemp);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                    strHTMLBuilder.Append(Print1[j].DmaxTemp);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                    strHTMLBuilder.Append(Print1[j].Dprecipitation);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                    strHTMLBuilder.Append(Print1[j].Dhumidity);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                    strHTMLBuilder.Append(Print1[j].DwindSpeed);
                    strHTMLBuilder.Append("</td>");

                    if (Convert.ToUInt16(Print1[j].Dprecipitation) >= 80)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/thunderstorm-98541_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) >= 50 && Convert.ToInt32(Print1[j].Dprecipitation) <= 79)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/rain-98538_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) >= 20 && Convert.ToInt32(Print1[j].Dprecipitation) <= 49)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/warm-98534_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) <= 19)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='Yellow'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/warm-98532_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                }

                if (Convert.ToInt32(Print1[j].DmaxTemp) >= 27)
                {
                    strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                    strHTMLBuilder.Append(Print1[j].Dcity);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                    strHTMLBuilder.Append(Print1[j].Ddate);
                    strHTMLBuilder.Append("</td></center>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                    strHTMLBuilder.Append(Print1[j].DminTemp);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                    strHTMLBuilder.Append(Print1[j].DmaxTemp);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                    strHTMLBuilder.Append(Print1[j].Dprecipitation);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                    strHTMLBuilder.Append(Print1[j].Dhumidity);
                    strHTMLBuilder.Append("</td>");

                    strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                    strHTMLBuilder.Append(Print1[j].DwindSpeed);
                    strHTMLBuilder.Append("</td>");

                    if (Convert.ToUInt16(Print1[j].Dprecipitation) >= 80)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/thunderstorm-98541_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) >= 50 && Convert.ToInt32(Print1[j].Dprecipitation) <= 79)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/rain-98538_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) >= 20 && Convert.ToInt32(Print1[j].Dprecipitation) <= 49)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/warm-98534_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                    if (Convert.ToInt32(Print1[j].Dprecipitation) <= 19)
                    {
                        strHTMLBuilder.Append("<td align='center' bgcolor='OrangeRed'>");
                        strHTMLBuilder.Append("<img src='https://cdn.pixabay.com/photo/2013/04/01/09/22/warm-98532_960_720.png' alt='Tunder' style='width: 80px; height: 80px;'> ");
                        strHTMLBuilder.Append("</td>");
                    }
                }
                strHTMLBuilder.Append("</tr>");
                //-------------------------------------------------------------------------------------------------------------------------------------
            }

            //Close tags. 
            strHTMLBuilder.Append("</table></center>");
            strHTMLBuilder.Append("<center><p style='font-family:Garamond; font-size:20'>");
            strHTMLBuilder.Append("<strong><u>Lowest and Highest Forecast Results</u><br/>");
            //Also Adding the lowest minimum value of the data and the highest maximum value is my own work
            //-----------------------------------------------------------
            strHTMLBuilder.Append(html);
            //-----------------------------------------------------------
            strHTMLBuilder.Append("</strong></p></center>");
            strHTMLBuilder.Append("</body></html>");
            string Htmltext = strHTMLBuilder.ToString();
            return Htmltext;
        }
        //----------------------------------------------------------------------End-----------------------------------------------------------------------------------------
    }
}
