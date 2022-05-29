using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Configuration;

namespace WeatherReports
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        //Constructor ff made To call in methods and Array lists from this Class
        Users ff = new Users();

        //Constructor dd made To call in methods and Array lists from this Class
        Reports dd = new Reports();

        //String made to store a user role that will be send to the 
        //main window that will be used there
        string userrole;

        //Boolean check value used as a checker in a loop in this window
        Boolean check = false;

        //Boolean error value used as a checker that will say if there are 
        //any errors made from the user side in this window
        Boolean error = false;

        //String made to store a edit user name so if the user decide to 
        //cancel there editing of a account then they still can 
        string edituser;

        int numuser;

        string connectionString = ConfigurationManager.AppSettings["connectionString"];
        SqlConnection cnn;
        SqlCommand cmd;

        public MainWindow()
		{
			InitializeComponent();
        }

        private void WeatherReports_Loaded(object sender, RoutedEventArgs e)
        {
            //if statement to fill the Weather reports array and the users array only ones
            if (dd.WeaterForecast1.Count == 0)
            {
                //Calls the details method to search throuh the text file reports and store it in the array
                ff.Details();

                //Calls the Values method to search throuh the text file users and store it in the array
                dd.Values();
            }
            
            //Hiding the following values because they their actions are diffrent for spesific tasks
            //------------------------------------------------------
            lblConfirmPassword.Visibility = Visibility.Hidden;
            txbConfirmPassword.Visibility = Visibility.Hidden;
            ckTypeUser.Visibility = Visibility.Hidden;
            lblForecasterPassword.Visibility = Visibility.Hidden;
            txbForcasterPassword.Visibility = Visibility.Hidden;
            btnSignUp2.Visibility = Visibility.Hidden;
            btnFinsh.Visibility = Visibility.Hidden;
            btnContinue.Visibility = Visibility.Hidden;
            //------------------------------------------------------
        }

        //Closing the program when the user pres the x on the window that will allow a user to gracefully exit the program
        //-------------------------------------------------------------
        private void WeatherReports_Closed(object sender, EventArgs e)
        {
            //Method that will shutdown the program
            Application.Current.Shutdown();
        }
        //-------------------------------------------------------------

        private void BtnLogIn_Click_1(object sender, RoutedEventArgs e)
        {
            //Calling the error method to see if the user made any errors
            Error();

            //if statement that wille check the boolean value to see if the user made any mistakes
            if (error == false)
            {
                //While loop that will go through the entire users array list
                int i = 0;
                while (i < ff.Users1.Count)
                {
                    //If statement that will compare a username in the textbox to the username in the array list 
                    if (txbUsername.Text.Equals(ff.Users1[i].Username))
                    {
                        //While loop that will go through the entire users array list
                        int j = 0;
                        while (j < ff.Users1.Count)
                        {
                            //If statement that will compare a password in the textbox to the password in the array list 
                            if (txbPassword.Password.GetHashCode().ToString().Equals(ff.Users1[j].Password))
                            {
                                //Opens up the main window for this program that users will be using if they are in signed up in the system
                                //Also sending the username to the wheater forecast window that will be used in favorites
                                //--------------------------------------------------------------------------------------
                                WeatherForecast newWindow = new WeatherForecast(ff.Users1[i].Username.ToString());
                                newWindow.DataContext = this;
                                this.Hide();
                                newWindow.ShowDialog();
                                //--------------------------------------------------------------------------------------
                            }
                            j++;
                        }
                    }
                    i++;
                }
            }
            else
            {
                //Turns the error boolean value of error to false so that the boolean can be used to check again for errors
                error = false;
            }
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            //This Button will change the window into a sing up page so a user can sign up to use the program
            //Hiding and showing texboxes and labels to let a user can sign up
            //----------------------------------------------------------------
            this.Title = "Sign Up";
            lblConfirmPassword.Visibility = Visibility.Visible;
            txbConfirmPassword.Visibility = Visibility.Visible;
            ckTypeUser.Visibility = Visibility.Visible;
            btnSignUp.Visibility = Visibility.Hidden;
            btnEditAccount.Content = "Back";
            btnLogIn.Visibility = Visibility.Hidden;
            btnSignUp2.Visibility = Visibility.Visible;
            lblWelcome.Visibility = Visibility.Hidden;
            lblWelcome2.Visibility = Visibility.Hidden;
            txbUsername.Clear();
            txbPassword.Clear();
            txbConfirmPassword.Clear();
            txbForcasterPassword.Clear();
            //----------------------------------------------------------------

            //Turns the error boolean value of error to false so that the boolean can be used to check again for errors
            error = false;
            
        }

        private void BtnEditAccount_Click(object sender, RoutedEventArgs e)
        {
            //if statement to check the button called edit account and where button called sign up is not visible
            if (btnEditAccount.Content.Equals("Back") || btnEditAccount.Content.Equals("Cancel") && btnSignUp.IsVisible == false)
            {
                //This Button will change the window into the base log in page so a user can log in 
                //Hiding and showing texboxes and labels to let a user can go back to the log in page
                //----------------------------------------------------------------
                this.Title = "Log In";
                lblConfirmPassword.Visibility = Visibility.Hidden;
                txbConfirmPassword.Visibility = Visibility.Hidden;
                txbForcasterPassword.Visibility = Visibility.Hidden;
                btnSignUp.Visibility = Visibility.Visible;
                ckTypeUser.Visibility = Visibility.Hidden;
                ckTypeUser.IsChecked = false;
                lblForecasterPassword.Visibility = Visibility.Hidden;
                btnSignUp2.Visibility = Visibility.Hidden;
                btnContinue.Visibility = Visibility.Hidden;
                btnFinsh.Visibility = Visibility.Hidden;
                btnLogIn.Visibility = Visibility.Visible;
                lblWelcome.Visibility = Visibility.Visible;
                lblWelcome2.Visibility = Visibility.Visible;
                btnEditAccount.Content = "Edit Account";
                lblUsername.Content = "Username :";
                lblPassword.Content = "Password :";
                txbUsername.Clear();
                txbPassword.Clear();
                txbConfirmPassword.Clear();
                txbForcasterPassword.Clear();
                //----------------------------------------------------------------

                //Turns the error boolean value of error to false so that the boolean can be used to check again for errors
                error = false;
            }
            else 
            {
                //This Button will change the window into edit account window so a user can choose to edit there username and password
                //Hiding and showing texboxes and labels to let a user edit there account
                //----------------------------------------------------------------
                this.Title = "Edit Account";
                btnSignUp.Visibility = Visibility.Hidden;
                lblUsername.Content = "Enter Old Username :";
                lblPassword.Content = "Enter Old Password :";
                btnEditAccount.Content = "Back";
                btnContinue.Visibility = Visibility.Visible;
                lblWelcome.Visibility = Visibility.Hidden;
                lblWelcome2.Visibility = Visibility.Hidden;
                btnLogIn.Visibility = Visibility.Hidden;
                txbUsername.Clear();
                txbPassword.Clear();
                txbConfirmPassword.Clear();
                txbForcasterPassword.Clear();
                //----------------------------------------------------------------
            }
        }

        private void BtnSignUp2_Click(object sender, RoutedEventArgs e)
        {
            //Calling the error method to see if the user made any errors
            Error();

            //if statement that wille check the boolean value to see if the user made any mistakes
            if (error == false)
            {
                //While loop that will go through the entire users array list
                int i = 0;
                while (i < ff.Users1.Count)
                {
                    //if statement to check if the username is already in the datebase
                    //So a user username has to be destinct
                    if (txbUsername.Text.Equals(ff.Users1[i].Username))
                    {
                        //Changing the boolean check value to true if the name is alredy taken
                        check = true;
                    }
                    i++;
                }

                // If the Check boolean value for check = true then that means the username is already taken 
                if (check == true)
                {
                    //message box to informe the user that the username is already taken
                    MessageBox.Show("Username is already taken please choose another username",
                    "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txbUsername.Clear();

                    //Change the boolean value back to false so that we can use it again
                    check = false;
                }
                //Else statement for if the username is not taken
                else
                {
                    //If staement to see if the confirm textbox is = to the password text box
                    //To make sure the user type the password correctly
                    if (txbPassword.Password.ToString().Equals(txbConfirmPassword.Password.ToString()))
                    {
                        //if statement that will see if the checkbox for weather forcaster is checked
                        if (ckTypeUser.IsChecked == true)
                        {
                            //if staement that will make sure that the forecaster password is correct
                            if (txbForcasterPassword.Password.GetHashCode().ToString().Equals("1217169917"))
                            {
                                //Changeing the user role to forecater if the password is correct
                                userrole = "forecaster";
                            }
                        }
                        else
                        {
                            //Changing the user to role to user if the user is just a normal user
                            userrole = "user";
                        }

                        //Writeing the users details to the users text file I am also getting the hash code of the password
                        //To make sure user passwords are secure
                        //----------------------------------------------------------------------------------------------------------------
                        try
                        {
                            int id = ff.Users1.Count + 1;
                            string username = txbUsername.Text;
                            string password = txbPassword.Password.GetHashCode().ToString();
                            string userrole1 = userrole;

                            cnn = new SqlConnection(connectionString);

                            //Inserting
                            cnn.Open();
                            string query = "insert into User_Table values (@username, @password, @userrole1);";
                            cmd = new SqlCommand(query, cnn);

                            cmd.Parameters.Add("@username", System.Data.SqlDbType.NChar);
                            cmd.Parameters.Add("@password", System.Data.SqlDbType.NChar);
                            cmd.Parameters.Add("@userrole1", System.Data.SqlDbType.NChar);

                            cmd.Parameters["@username"].Value = username;
                            cmd.Parameters["@password"].Value = password;
                            cmd.Parameters["@userrole1"].Value = userrole1;
                            cmd.ExecuteNonQuery();
                            cnn.Close();
                            ff.Users1.Add(new UserDetails(txbUsername.Text, txbPassword.Password.GetHashCode().ToString(), userrole));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Something went wrong");
                        }
                        //----------------------------------------------------------------------------------------------------------------
                        
                        //Message box to let the user know that everting is saved and to welcome the user
                        MessageBox.Show("Welcome " + txbUsername.Text + ", You have successfully signed up",
                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                        //Changing the texboxes and labeles to the log in page so the user can log in
                        //-------------------------------------------------------
                        lblWelcome.Visibility = Visibility.Visible;
                        lblWelcome2.Visibility = Visibility.Visible;
                        lblConfirmPassword.Visibility = Visibility.Hidden;
                        txbConfirmPassword.Visibility = Visibility.Hidden;
                        txbForcasterPassword.Visibility = Visibility.Hidden;
                        lblForecasterPassword.Visibility = Visibility.Hidden;
                        ckTypeUser.Visibility = Visibility.Hidden;
                        btnLogIn.Visibility = Visibility.Visible;
                        btnSignUp2.Visibility = Visibility.Hidden;
                        btnSignUp.Visibility = Visibility.Visible;
                        txbUsername.Clear();
                        txbPassword.Clear();
                        txbConfirmPassword.Clear();
                        txbForcasterPassword.Clear();
                        btnEditAccount.Content = "Edit Account";
                        //-------------------------------------------------------
                    }
                    else
                    {
                        //Message box to let the user know that the confirm password does not match
                        MessageBox.Show("Confirm Password does not match the password",
                        "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    //Changing the check value back to false so that it can be used later
                    check = false;
                }
            }
            else
            {
                //Changing the error value back to false so that it can be used later
                error = false;
            }
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            //Calling the error method to see if the user made any errors
            Error();

            //if statement that wille check the boolean value to see if the user made any mistakes
            if (error == false)
            {
                //While loop that will go through the entire users array list
                int j = 0;
                while (j < ff.Users1.Count)
                {
                    //Checks if the username textbox = username in the array
                    if (txbUsername.Text.Equals(ff.Users1[j].Username))
                    {
                        //While loop that will go through the entire users array list
                        int k = 0;
                        while (k < ff.Users1.Count)
                        {
                            //Checks if the password textbox = password in the array
                            if (txbPassword.Password.GetHashCode().ToString().Equals(ff.Users1[k].Password))
                            {
                                //Gets the username and store it in the edituser string
                                edituser = ff.Users1[j].Username.ToString();

                                //Changing the texboxes and labels to let the user can edit accounts
                                //-------------------------------------------------
                                lblUsername.Content = "Enter New Username :";
                                lblPassword.Content = "Enter New Password :";
                                lblConfirmPassword.Content = "Confirm New Password :";
                                btnEditAccount.Content = "Cancel";
                                ckTypeUser.Visibility = Visibility.Visible;
                                txbConfirmPassword.Visibility = Visibility.Visible;
                                lblConfirmPassword.Visibility = Visibility.Visible;
                                btnContinue.Visibility = Visibility.Hidden;
                                txbUsername.Clear();
                                txbPassword.Clear();
                                btnFinsh.Visibility = Visibility.Visible;
                                //-------------------------------------------------
                            }
                            k++;
                        }
                    }
                    j++;
                }
            }
            else
            {
                //Changing the error value back to false so that it can be used later
                error = false;
            }
        }

        private void BtnFinsh_Click(object sender, RoutedEventArgs e)
        {
            //Calling the error method to see if the user made any errors
            Error();

            //if statement that wille check the boolean value to see if the user made any mistakes
            if (error == false)
            {
                //While loop that will go through the entire users array list
                int i = 0;
                while (i < ff.Users1.Count)
                {
                    //Checks if the username textbox = username in the array
                    if (txbUsername.Text.Equals(ff.Users1[i].Username))
                    {
                        //Changing the boolean check value to true if the name is alredy taken
                        check = true;
                    }
                    i++;
                }

                // If the Check boolean value for check = true then that means the username is already taken 
                if (check == true)
                {
                    //message box to informe the user that the username is already taken
                    MessageBox.Show("Username is already taken please choose another username or you enterd your old username",
                    "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txbUsername.Clear();
                    check = false;
                }
                else
                {
                    //If staement to see if the confirm textbox is = to the password text box
                    //To make sure the user type the password correctly
                    if (txbPassword.Password.ToString().Equals(txbConfirmPassword.Password.ToString()))
                    {
                        //if statement that will see if the checkbox for weather forcaster is checked
                        if (ckTypeUser.IsChecked == true)
                        {
                            //if staement that will make sure that the forecaster password is correct
                            if (txbForcasterPassword.Password.GetHashCode().ToString().Equals("1217169917"))
                            {
                                //Changeing the user role to forecater if the password is correct
                                userrole = "forecaster";
                            }
                        }
                        else
                        {
                            //Changing the user to role to user if the user is just a normal user
                            userrole = "user";
                        }

                        //While loop that will go through the entire users array list
                        int u = 0;
                        while (u<ff.Users1.Count)
                        {
                            //Checks if the username in the array = to the edituser string
                            if (ff.Users1[u].Username == edituser)
                            {
                                //Removes the user if it is the same so the new data can be entered
                                ff.Users1.Remove(ff.Users1[u]);
                                numuser = u;
                            }
                            u++;
                        }

                        //Entering the new data into the array 
                        ff.Users1.Add(new UserDetails(txbUsername.Text, txbPassword.Password.GetHashCode().ToString(), userrole));

                        cnn = new SqlConnection(connectionString);

                        int id = (numuser + 1);
                        string username = txbUsername.Text;
                        string password = txbPassword.Password.GetHashCode().ToString();
                        string userrole1 = userrole;
                        try
                        {
                            //Save edit
                            cnn.Open();
                            string query = "update User_Table set Username = @username," +
                                "password = @password, UserRole = @userrole1 where Username = @username";
                            cmd = new SqlCommand(query, cnn);

                            cmd.Parameters.Add("@username", System.Data.SqlDbType.NChar);
                            cmd.Parameters.Add("@password", System.Data.SqlDbType.NChar);
                            cmd.Parameters.Add("@userrole1", System.Data.SqlDbType.NChar);

                            cmd.Parameters["@username"].Value = username;
                            cmd.Parameters["@password"].Value = password;
                            cmd.Parameters["@userrole1"].Value = userrole1;
                            cmd.ExecuteNonQuery();
                            cnn.Close();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Something went wrong");
                        }

                        //Changing the textboxes and labels back to the log in window so the user can log in
                        //----------------------------------------------------------------------------
                        MessageBox.Show("Account was Changed " + txbUsername.Text,
                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Title = "Log In";
                        lblUsername.Content = "Enter Username :";
                        lblPassword.Content = "Enter Password :";
                        btnEditAccount.Content = "Edit Account";
                        lblConfirmPassword.Visibility = Visibility.Hidden;
                        txbConfirmPassword.Visibility = Visibility.Hidden;
                        txbForcasterPassword.Visibility = Visibility.Hidden;
                        btnFinsh.Visibility = Visibility.Hidden;
                        btnLogIn.Visibility = Visibility.Visible;
                        lblWelcome.Visibility = Visibility.Visible;
                        lblWelcome2.Visibility = Visibility.Visible;
                        ckTypeUser.IsChecked = false;
                        ckTypeUser.Visibility = Visibility.Hidden;
                        lblForecasterPassword.Visibility = Visibility.Hidden;
                        btnContinue.Visibility = Visibility.Hidden;
                        txbUsername.Clear();
                        txbPassword.Clear();
                        txbConfirmPassword.Clear();
                        txbForcasterPassword.Clear();
                        btnSignUp.Visibility = Visibility.Visible;
                        //----------------------------------------------------------------------------
                    }
                    else
                    {
                        //Message box to let the user know that the confirm password does not match
                        MessageBox.Show("Confirm Password does not match the password",
                        "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    //Changing the boolean check value back to false to used it again
                    check = false;
                }
            }
            else
            {
                //Changing the boolean error value back to false to used it again
                error = false;
            }
        }

        private Boolean Error()
        {
            //if username textbox is empty then turn error boolean value to true
            //---------------------------------------------------------------
            if (txbUsername.Text.Equals(""))
            {
                //turn error value to true
                error = true;

                //Message box to display that username was not enter
                MessageBox.Show("Username was not enter",
                        "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //---------------------------------------------------------------

            //if password textbox is empty then turn error boolean value to true
            //---------------------------------------------------------------
            if (txbPassword.IsVisible == true && txbPassword.Password.Equals(""))
            {
                //turn error value to true
                error = true;

                //Message box to display that password was not enter
                MessageBox.Show("Password was not enter",
                "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //---------------------------------------------------------------

            //if confirm  password is visable and if the user did not input anything
            //---------------------------------------------------------------
            if (txbConfirmPassword.IsVisible == true && txbConfirmPassword.Password.Equals(""))
            {
                //turn error value to true
                error = true;

                //Message box to display that confirm password was not enter
                MessageBox.Show("Confirm Password was not enter",
                "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            //---------------------------------------------------------------

            //if statement to see if the password is not grater than 8
            //---------------------------------------------------------------
            if (this.Title == "Sign Up" && txbPassword.Password.Length < 8)
            {
                //turn error value to true
                error = true;

                //Message box to display if the password length is less than 8
                MessageBox.Show("Password must be at least 8 characters",
                "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);

                //Clear text boxes for password and confirm password
                //---------------------------------------------------------------
                txbPassword.Clear();
                txbConfirmPassword.Clear();
                //---------------------------------------------------------------
            }
            //---------------------------------------------------------------

            //if statement to see if the title page = log in
            if (this.Title == "Log In")
            {
                //While loop that gou through the users array list
                //---------------------------------------------------------------
                int i = 0;
                while (i < ff.Users1.Count)
                {
                    //If statement to see if the username textbox = username in the array
                    if (txbUsername.Text.Equals(ff.Users1[i].Username))
                    {
                        //If statement to see if the password textbox = password in the array
                        if (txbPassword.Password.GetHashCode().ToString().Equals(ff.Users1[i].Password))
                        {
                            //Change check value to true
                            check = true;
                        } 
                    }
                    i++;
                }
                //---------------------------------------------------------------

                //if statement to see if check = false
                if (check == false)
                {
                    //Message box to display that the username and password does not exist
                    MessageBox.Show("Username or Password does not exist",
                    "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);

                    //Clear text boxes for password and confirm password
                    //---------------------------------------------------------------
                    txbUsername.Clear();
                    txbPassword.Clear();
                    //---------------------------------------------------------------
                }

                //check if the check boolean value = true
                if (check == true)
                {
                    //change the check boolean value to false
                    check = false;
                }
            }

            //if statement to see if the title page = edit account 
            if (this.Title == "Edit Account" && btnContinue.IsVisible == true)
            {
                //While loop that gou through the users array list
                //---------------------------------------------------------------
                int i = 0;
                while (i < ff.Users1.Count)
                {
                    //If statement to see if the username textbox = username in the array
                    if (txbUsername.Text.Equals(ff.Users1[i].Username))
                    {
                        //If statement to see if the password textbox = password in the array
                        if (txbPassword.Password.GetHashCode().ToString().Equals(ff.Users1[i].Password))
                        {
                            //Change check value to true
                            check = true;
                        }
                    }
                    i++;
                }
                //---------------------------------------------------------------

                //if statement to see if check = false
                if (check == false)
                {
                    //Message box to display that the username and password does not exist
                    MessageBox.Show("Username or Password does not exist",
                    "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);

                    //Clear text boxes for password and confirm password
                    //---------------------------------------------------------------
                    txbUsername.Clear();
                    txbPassword.Clear();
                    //---------------------------------------------------------------

                    //Change check value to true
                    check = true;
                }

                //check if the check boolean value = true
                if (check == true)
                {
                    //Change check value to false
                    check = false;
                }
            }

            //Check if th checkbox for forecaster is checked = true
            if (ckTypeUser.IsChecked == true)
            {
                //if the password for forecaster password  does not = to the hrdcoded pasword in the system
                if (!txbForcasterPassword.Password.GetHashCode().ToString().Equals("1217169917"))
                {
                    //Change check value to true
                    error = true;

                    //Message box that disply to the user that the weather forecaster password is incorrect
                    MessageBox.Show("Forcaster Password is not enterd or incorrect",
                    "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            //returns the error boolean value
            return error;
        }

        private void CkTypeUser_Checked(object sender, RoutedEventArgs e)
        {
            //Will show that a password needs to be enterd if a user wants the role of forecaster
            //-------------------------------------------------------------
            if (ckTypeUser.IsChecked == true)
            {
                lblForecasterPassword.Visibility = Visibility.Visible;
                txbForcasterPassword.Visibility = Visibility.Visible;
            }
            //-------------------------------------------------------------
        }

        private void CkTypeUser_Unchecked(object sender, RoutedEventArgs e)
        {
            //Will show that a password needs to be enterd if a user wants the role of forecaster
            //-------------------------------------------------------------
            if (ckTypeUser.IsChecked == false)
            {
                lblForecasterPassword.Visibility = Visibility.Hidden;
                txbForcasterPassword.Visibility = Visibility.Hidden;
            }
            //-------------------------------------------------------------
        }
    }
}
