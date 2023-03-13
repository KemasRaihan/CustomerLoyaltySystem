using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Login
{
    public class MemberDashboardModel : PageModel
    {
        public string Username;
        public const string SessionKeyName1 = "username";
        public string SessionID;
        public const string SessionKeyName2 = "sessionID";
        public int AccountID;
        public int TotalPoints;

        public IActionResult OnGet()
        {

            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();


            Username = HttpContext.Session.GetString(SessionKeyName1);
            SessionID = HttpContext.Session.GetString(SessionKeyName2);

            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Login/UserLogin");
            }

            AccountID = UserAccount.findAccountID(Username);
            TotalPoints = Userdbo.getTotalPoints(AccountID);


            return Page();
        }
    }
}

