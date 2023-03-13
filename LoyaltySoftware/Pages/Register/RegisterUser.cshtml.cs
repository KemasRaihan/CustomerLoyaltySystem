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


        public IActionResult OnPost()
        {
            // if user does not enter all of their personal details
            if(string.IsNullOrEmpty(UserDBORecord.first_name) || string.IsNullOrEmpty(UserDBORecord.last_name) || string.IsNullOrEmpty(UserDBORecord.dob) || string.IsNullOrEmpty(UserDBORecord.telephone) || string.IsNullOrEmpty(UserDBORecord.email))
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

                DBConnection dbstring = new DBConnection();
                string DbConnection = dbstring.DatabaseString();

                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();



                using (SqlCommand command = new SqlCommand())
                {
                  command.Connection = conn;

                  command.CommandText = @"INSERT INTO UserAccount (username, password, status, user_role) VALUES (@uname, @pword, @sts, @urole)";

                  command.Parameters.AddWithValue("@uname", UserAccountRecord.username);
                  command.Parameters.AddWithValue("@pword", UserAccountRecord.password);
                  command.Parameters.AddWithValue("@sts", "active"); // set to "active" by default
                  command.Parameters.AddWithValue("@urole", "member"); // set to "member" by default

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


 
