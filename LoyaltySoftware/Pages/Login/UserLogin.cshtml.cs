using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2016.Excel;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        private const int MaxFailedAttempts = 3;
        private const double LockoutDurationMinutes = 30.0;

        [BindProperty]
        public UserAccount UserAccount { get; set; }

        [BindProperty]
        public Userdbo UserRec { get; set; }


        public string IPAddress { get; set; }
        public string Message1 { get; set; }
        public string Message2 { get; set; }

        public string SessionID;

        public static bool ActiveSession { get; set; }







        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {


            if (ActiveSession == true)
            {
                Message2 = "You are already logged in, please logout first";
                return Page();
            }


            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(UserAccount.username);
            Console.WriteLine(UserAccount.password);


            //1. check if the fields are empty
            //2. check if the username exists
            //3. check if the status is active
            //4. check if the passowrd is correct
            //5. check if the role is admin or member

            // Check if the account is locked out
            if (UserAccount.isLockedOut(UserAccount.username))
            {
                // Check if lockout duration has expired

                // Get lockout time of account
                DateTime lockouttime = UserAccount.getLockoutTime(UserAccount.username);

                // Get curent time
                DateTime current = DateTime.Now;

                // Get span time in minutes
                TimeSpan span = current - lockouttime;

                double totalMinutes = span.TotalMinutes;

                if (totalMinutes < LockoutDurationMinutes)
                {
                    // Account is still locked out, return appropriate message
                    Message2 = "Your account is locked. Please try again later.";
                    return Page();
                }
                else
                {
                    // Lockout duration has expired, unlock the account
                    UserAccount.IsLockedOut = false;
                    UserAccount.FailedLoginAttempts = 0;
                    UserAccount.LockoutTime = new DateTime(1753, 01, 01); ;
                    UserAccount.updateUserAccount(UserAccount.username, UserAccount.IsLockedOut, UserAccount.FailedLoginAttempts, UserAccount.LockoutTime); // Update user account in the database
                }
            }
            // new code
            int currentFailedLoginAttempt = UserAccount.getFailedLoginCount(UserAccount.username);
            if (currentFailedLoginAttempt > 3)
            {
                // Account is still locked out, return appropriate message
                Message2 = "Your account is locked. Please try again later.";
                // Lockout duration has expired, unlock the account
                UserAccount.IsLockedOut = true;
                UserAccount.FailedLoginAttempts = currentFailedLoginAttempt;
                UserAccount.LockoutTime = DateTime.Now;
                UserAccount.updateUserAccount(UserAccount.username, UserAccount.IsLockedOut, UserAccount.FailedLoginAttempts, UserAccount.LockoutTime); // Update user account in the database

                return Page();
            }



            if (string.IsNullOrEmpty(UserAccount.username)) // check if username field is empty
            {
                Message1 = "Please enter a username!";
                return Page();
            }
            else if (string.IsNullOrEmpty(UserAccount.password)) // check if password field is empty
            {
                Message2 = "Please enter a password!";
                return Page();

            }

            else // if the username and passwordvis not empty
            {
                //check if the username is valid
                if (UserAccount.checkIfUsernameExists(UserAccount.username)) // check if the username entered exists
                {

                    //if the username exists...

                    SessionID = HttpContext.Session.Id;
                    HttpContext.Session.SetString("sessionID", SessionID);
                    HttpContext.Session.SetString("username", UserAccount.username);

                    // now check the status

                    if (UserAccount.checkStatus(UserAccount.username) == "suspended") //if the status is suspended
                    {
                        Message2 = "Your account has been suspended.";
                        return Page();
                    }
                    else if (UserAccount.checkStatus(UserAccount.username) == "revoked") // if the status is revoked
                    {
                        Message2 = "Your account has been revoked.";
                        return Page();
                    }
                    else //if the status is active
                    {
                        //check the password field

                        if (!UserAccount.checkPassword(UserAccount.username, UserAccount.password))  // check that the passowrd is correct
                        {
                            Message2 = "Password does not match!"; // if the password does not match display message
                            // new code
                            UserAccount.addFailLoginCount(UserAccount.username, currentFailedLoginAttempt);
                            return Page();
                        }
                        else // the password does match
                        {
                            //check the role
                            if (UserAccount.checkRole(UserAccount.username) == "member")
                            {
                                ActiveSession = true;
                                return RedirectToPage("/Member/MemberDashboard");
                            }
                            else
                            {
                                ActiveSession = true;
                                return RedirectToPage("/Admin/AdminDashboard");
                            }

                        }

                    }

                }
                else // if the username does not exist
                {
                    // display error message
                    Message1 = "Username does not exist!";
                    return Page();
                }

            }

        }


    }

}
