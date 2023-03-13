using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Rewards
{
    public class BrowseRewardsModel : PageModel
    {
        public List<Reward> RewardRec { get; set; }
        public void OnGet()
        {
            DBConnection dbstring = new DBConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Reward";

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                RewardRec = new List<Reward>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    Reward record = new Reward(); //a local var to hold a record temporarily
                    record.rewardId = reader.GetInt32(0); //getting the first field from the table
                    record.rewardName = reader.GetString(1); //getting the second field from the table
                    record.pointsToClaim = reader.GetInt32(2); //getting the third field from the table
                    record.rewardImageSrc = reader.GetString(3);
                    record.rewardDescription = reader.GetString(4);

                    RewardRec.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();

            }
        }
    }
}