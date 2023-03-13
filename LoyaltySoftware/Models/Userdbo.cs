using LoyaltySoftware.Pages.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    public class Userdbo
    {
        [Display(Name = "User ID")]
        public int user_id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public string dob { get; set; }

        [Required]
        [Display(Name = "Telephone Number")]
        public string telephone { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Total Points")]
        public int total_points { get; set; }

        public static int getUserId(int account_id)
        {

            int id = 0;
            using (SqlCommand command = new SqlCommand())
            {

                DBConnection dbstring = new DBConnection();
                string DbConnection = dbstring.DatabaseString();
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT user_id FROM Userdbo WHERE account_id = @AID";

                command.Parameters.AddWithValue("@AID", account_id);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }

                return id;


            }
        }





        public static int getTotalPoints(int accountID)
        {
            using (SqlCommand command = new SqlCommand())
            {
                int points = -1;
                DBConnection dbstring = new DBConnection();
                string DbConnection = dbstring.DatabaseString();
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT points FROM Userdbo WHERE account_id = @AID";

                command.Parameters.AddWithValue("@AID", accountID);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    points = reader.GetInt32(0);
                }

                return points;
            }
        }


    }
}
