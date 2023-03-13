using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Login;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Member
{
    public class ProfilePageModel : PageModel
    {
        public string Username;
        public int AccountID;
        public const string SessionKeyName1 = "username";
        public Userdbo UserRec { get; set; }


        public IActionResult OnGet()
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            UserRec = new Userdbo();
            Username = HttpContext.Session.GetString(SessionKeyName1);
            AccountID = UserAccount.findAccountID(Username);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Userdbo WHERE account_id = @AID";

                command.Parameters.AddWithValue("@AID", AccountID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserRec.user_id = reader.GetInt32(0);
                    UserRec.first_name = reader.GetString(1);
                    UserRec.last_name = reader.GetString(2);
                    UserRec.dob = reader.GetString(3);
                    UserRec.telephone = reader.GetString(4);
                    UserRec.email = reader.GetString(5);
                }
            }

            conn.Close();

            return Page();


        }
     }
}
