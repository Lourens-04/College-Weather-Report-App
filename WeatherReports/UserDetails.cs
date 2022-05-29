using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherReports
{
    class UserDetails
    {
        //Declaring the variables that will set the reports values
        //------------------------------------------------------
        string username;
        string password;
        string userrole;
        //------------------------------------------------------

        //Constructor that will set the variables so it can be enterd into the array
        public UserDetails(string username, string password, string userrole)
        {
            //Sets the variables being sent from the other class to set the variables in this class
            //----------------------------------------------
            this.Username = username;
            this.Password = password;
            this.Userrole = userrole;
            //----------------------------------------------
        }

        //Gets and Sets for the variables so it can be enterd into the array
        //------------------------------------------------------------------------
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Userrole { get => userrole; set => userrole = value; }
        //------------------------------------------------------------------------
    }
}
