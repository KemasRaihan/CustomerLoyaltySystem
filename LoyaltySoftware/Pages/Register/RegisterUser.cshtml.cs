using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Login;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Text; // modified code
using System.Text.RegularExpressions;  // modified code

namespace LoyaltySoftware.Pages.Register
{
    public class RegisterUserModel : PageModel
    {

        [BindProperty]
        public Userdbo UserDBORecord { get; set; }

        [BindProperty]
        public UserAccount UserAccountRecord { get; set; }

        public string Message1 { get; set; }
        public string Message2 { get; set; }
        public string Message3 { get; set; }



        public void OnGet()
        {
        }

        // modified code
        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public IActionResult OnPost()
        {
            // if user does not enter all of their personal details
            if (string.IsNullOrEmpty(UserDBORecord.first_name) || string.IsNullOrEmpty(UserDBORecord.last_name) || string.IsNullOrEmpty(UserDBORecord.dob) || string.IsNullOrEmpty(UserDBORecord.telephone) || string.IsNullOrEmpty(UserDBORecord.email))
            {
                Message1 = "Please fill in your personal details!";
            }
            if (string.IsNullOrEmpty(UserAccountRecord.username) && string.IsNullOrEmpty(UserAccountRecord.password)) // if user does not enter their username and password
            {
                Message2 = "Please enter a username!";
                Message3 = "Please enter a password!";
                return Page();
            }
            else if (string.IsNullOrEmpty(UserAccountRecord.username)) // if user does not enter their username
            {
                Message2 = "Please enter a username!";
                return Page();
            }
            else if (string.IsNullOrEmpty(UserAccountRecord.password)) // if user does not enter their password
            {
                Message3 = "Please enter a password!";
                return Page();
            }
            else if (UserAccount.checkIfUsernameExists(UserAccountRecord.username))
            {
                Message2 = "Username already exists! Please try again.";
                return Page();
            }
            else
            {
                // Modified code
                ////////////////////////
                // Check for password strength
                string password = UserAccountRecord.password;


                // Check for minimum length
                if (password.Length < 8)
                {
                    Message3 = "Password is too short, must have a minimum length of 8 characters";
                    return Page();
                }


                // Check for uppercase letters
                if (!Regex.IsMatch(password, "[A-Z]"))
                {
                    Message3 = "Password must have at least one uppercase character";
                    return Page();
                }

                // Check for lowercase letters
                if (!Regex.IsMatch(password, "[a-z]"))
                {
                    Message3 = "Password must have at least one lowercase character";
                    return Page();
                }

                // Check for digits
                if (!Regex.IsMatch(password, "\\d"))
                {
                    Message3 = "Password must have at least one digit character";
                    return Page();
                }

                // Check for special characters
                if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
                {
                    Message3 = "Password must have at least one special character (such as ‘£’ or ‘@’ etc.).";
                    return Page();
                }

                ////////////////////////




                DBConnection dbstring = new DBConnection();
                string DbConnection = dbstring.DatabaseString();

                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();



                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;

                    command.CommandText = @"INSERT INTO UserAccount (username, password, status, user_role) 
                    VALUES (@uname, @pword, @sts, @urole)";
            
                    command.Parameters.AddWithValue("@uname", UserAccountRecord.username);
                    command.Parameters.AddWithValue("@pword", UserAccountRecord.password);
                    command.Parameters.AddWithValue("@sts", "active"); // set to "active" by default
                    command.Parameters.AddWithValue("@urole", "member"); // set to "member" by default
                    //command.Parameters.AddWithValue("@isLockedOut", false); // set to "false" by default
                    //command.Parameters.AddWithValue("@failedLoginCount", 0); // set to 0 by default
                    //command.Parameters.AddWithValue("@LockoutTime", null); // set to null by default



                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO Userdbo (first_name, last_name, dob, telephone, email, account_id) VALUES (@fname, @lname, @dob, @tphone, @email, (SELECT account_id FROM UserAccount WHERE username = @uname))";

                    command.Parameters.AddWithValue("@fname", UserDBORecord.first_name);
                    command.Parameters.AddWithValue("@lname", UserDBORecord.last_name);
                    command.Parameters.AddWithValue("@dob", UserDBORecord.dob);
                    command.Parameters.AddWithValue("@tphone", UserDBORecord.telephone);
                    command.Parameters.AddWithValue("@email", UserDBORecord.email);

                    command.ExecuteNonQuery();

                    // insert username/password into useraccount table
                    // status, role, user_id are not user input --> status is active, role is member by default.

                }


                conn.Close();
                return RedirectToPage("/Register/DataProtection");
            }
        }
    }

        

}


 
