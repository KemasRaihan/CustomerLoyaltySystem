using LoyaltySoftware.Pages.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Models
{
    
    public class UserAccount
    {
        public static int account_id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        [Display(Name = "Status")]
        public static string status { get; set; }
        [Required]
        [Display(Name = "User Role")]
        public static string user_role { get; set; }

        public static string[] UserRoles = new string[] { "member", "admin" };
        public static string[] UserStatuses = new string[] { "active", "suspended", "revoked" };

        public static string checkStatus(string username)
        {
            string currentStatus = "";
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT username, status FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    currentStatus = reader.GetString(1);
                }

                return currentStatus;
            }
        }


        public static string checkRole(string username)
        {
            string currentRole = "";
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();
                string DbConnection = dbstring.DatabaseString();
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT username, user_role FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    currentRole = reader.GetString(1);
                }

                return currentRole;
            }

        }

        public static bool checkIfUsernameExists(string inputUsername)
        {
            string userName = "";
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT username FROM userAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", inputUsername);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userName = reader.GetString(0);
                }

                if (!string.IsNullOrEmpty(userName))
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public static bool checkPassword(string inputUsername, string inputPassword)
        {
            string passWord = "";
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT username, password FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", inputUsername);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    passWord = reader.GetString(1);
                }

                if (inputPassword != passWord)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        public static int findAccountID(string inputUsername)
        {
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT account_id FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", inputUsername);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    account_id = reader.GetInt32(0);
                }

                return account_id;

            }
        }

    }

}


