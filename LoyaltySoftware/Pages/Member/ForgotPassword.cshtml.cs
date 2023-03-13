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

namespace LoyaltySoftware.Pages.Member
{
    public class ForgotPasswordModel : PageModel
    {

        [BindProperty]
        public UserAccount UserAccountRecord { get; set; }
        [BindProperty]
        public Userdbo UserDBORecord { get; set; }

        public static string uname { get; set; }


        public string Message { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            // check username exists

            if (UserAccount.checkIfUsernameExists(UserAccountRecord.username))
            {
                // if username exists then redirect to reset passsword
                uname = UserAccountRecord.username;
                return RedirectToPage("/Member/ResetPassword");
            }
            else
            {
                Message = "That username does not exist";
                return Page();
            }

        }
    }

}

    
