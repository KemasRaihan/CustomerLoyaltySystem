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

namespace LoyaltySoftware.Pages.AdminPages
{
    public class AdminDashboardModel : PageModel
    {
        public List<Userdbo> UserRec { get; set; }

        public Userdbo UserRecord { get; set; }

        public string Username;

        public const string SessionKeyName1 = "username";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";

        public IActionResult OnGet()
        {
            Username = HttpContext.Session.GetString(SessionKeyName1);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Login/UserLogin");
            }
        
                return Page();
        }
    }
}
