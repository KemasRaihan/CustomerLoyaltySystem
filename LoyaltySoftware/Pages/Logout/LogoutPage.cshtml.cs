using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Pages.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Logout
{
    public class Logout_PageModel : PageModel
    {


        public void OnGet()
        {
            UserLoginModel.ActiveSession = false;
            HttpContext.Session.Clear();
        }

    }
}
