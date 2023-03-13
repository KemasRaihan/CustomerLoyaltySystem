using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LoyaltySoftware.Models;
using LoyaltySoftware.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoyaltySoftware.Pages.Shop
{
    public class ShopModel : PageModel
    {
        public List<Product> ProductRec { get; set; }
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
                command.CommandText = @"SELECT * FROM Product";

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                ProductRec = new List<Product>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    Product record = new Product(); //a local var to hold a record temporarily
                    record.productId = reader.GetInt32(0); //getting the first field from the table
                    record.productName = reader.GetString(1); //getting the second field from the table
                    record.productPrice = (double)reader.GetDecimal(2); //getting the third field from the table
                    record.productImageSrc = reader.GetString(3);

                    ProductRec.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();

            }
        }
    }
}
