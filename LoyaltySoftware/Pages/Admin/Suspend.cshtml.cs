using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Login;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Admin
{
    public class SuspendModel : PageModel
    {

        [BindProperty]
        public UserAccount UserAccountRec { get; set; }

        public string Message { get; set; }


        public void OnGet()
        {

        }


        public IActionResult OnPost()
        {

            string InputUsername = Request.Form["suspend"];


            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            // check username exists

            if (UserAccount.checkIfUsernameExists(InputUsername))
            {
                // if username exists then update the member status

                string username = InputUsername;

                SuspendMember(username);

                return RedirectToPage("/Admin/Confirmation/ConfirmSuspend");
            }
            else
            {
                Message = "That username does not exist";
                return Page();
            }

        }

        public static string SuspendMember(string username)
        {

            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = @"UPDATE UserAccount SET status = @sts WHERE username = @Uname";


                    command.Parameters.AddWithValue("@uname", username);
                    command.Parameters.AddWithValue("@sts", UserAccount.UserStatuses[1]);


                    command.ExecuteReader();
                }
            return  UserAccount.checkStatus(username);
            }

    }
}
