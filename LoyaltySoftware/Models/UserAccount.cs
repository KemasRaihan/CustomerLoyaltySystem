using LoyaltySoftware.Pages.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        [Required]
        [Display(Name = "Failed Login Attempts")]
        public int FailedLoginAttempts { get; set; }
        [Required]
        [Display(Name = "Is Locked Out")]
        public bool IsLockedOut { get; set; }

        public DateTime? LockoutTime { get; set; }

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
        // new code
        public static void addFailLoginCount(string username, int currentFailedLoginCount)
        {
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                currentFailedLoginCount+=1; // update currentFailedLoginCount
                command.CommandText = @"UPDATE UserAccount SET failed_login_count = @failedLoginCount WHERE username = @UName";
                command.Parameters.AddWithValue("@UName", username);
                command.Parameters.AddWithValue("@failedLoginCount", currentFailedLoginCount); // modify login count

                command.ExecuteNonQuery();


                SqlDataReader reader = command.ExecuteReader();
            }
        }

        // new code
        public bool isLockedOut(string username)
        {
            bool isLockedOut = false;

            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT is_locked_out FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    isLockedOut = reader.GetBoolean(0);
                }

                return isLockedOut;

            }
        }

        // new code
        public int getFailedLoginCount(string username)
        {
            int loginCount = 0;
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT failed_login_count FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loginCount = reader.GetInt32(0);
                }
                return loginCount;

            }
    
        }

        // new code
        public DateTime getLockoutTime(string username)
        {
            DateTime lockouttime = new DateTime(1753, 01, 01); // initialise DateTime variable
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
                command.CommandText = @"SELECT lockout_time FROM UserAccount WHERE username = @UName";

                command.Parameters.AddWithValue("@UName", username);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lockouttime = reader.GetDateTime(0);
                }
                return lockouttime;

            }

        }

        // new code
        public static void updateUserAccount(string username, bool isLockedOut, int FailedLoginAttempts, DateTime? LockoutTime)
        {
            using (SqlCommand command = new SqlCommand())
            {
                DBConnection dbstring = new DBConnection();      //creating an object from the class
                string DbConnection = dbstring.DatabaseString(); //calling the method from the class
                SqlConnection conn = new SqlConnection(DbConnection);
                conn.Open();

                command.Connection = conn;
              
                command.CommandText = @"UPDATE UserAccount
                      SET is_locked_out = @isLockedOut, failed_login_count = @failedLoginCount, lockout_time = @LockoutTime
                      WHERE username = @UName";
                command.Parameters.AddWithValue("@UName", username);
                command.Parameters.AddWithValue("@isLockedOut", isLockedOut);
                command.Parameters.AddWithValue("@failedLoginCount", FailedLoginAttempts);
                command.Parameters.AddWithValue("@LockoutTime", LockoutTime);

                command.ExecuteNonQuery();

            }

        }

    }

}


