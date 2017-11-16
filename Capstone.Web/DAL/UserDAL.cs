using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Configuration;
using Capstone.Web.DAL;

namespace Capstone.Web.DAL
{
    public class UserDAL : IUserDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CityTour"].ConnectionString;
        private const string SQL_SaveUser = "insert into userInfo values(@userName, @firstName, @lastName, @password, @passwordSalt, 0)";
        private const string SQL_DeleteUser = "delete * from userInfo where userName = @userName and password = @password";
        private const string SQL_UpdatePassword = "update password from userInfo where userName = @userName";
        private const string SQL_SelectUser = "select * from userInfo where userName = @userName";
        private const string SQL_CheckUserName = "select count(*) from userInfo where userName = @userName";

        public bool SaveUser(UserModel user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd2 = new SqlCommand(SQL_CheckUserName, conn);
                    int numberOfRows = (int)(cmd2.ExecuteScalar());

                    if (numberOfRows > 0)
                    {
                        return false;
                    }

                    SqlCommand cmd = new SqlCommand(SQL_SaveUser, conn);

                    cmd.Parameters.AddWithValue("@userName", user.UserName);
                    cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", user.LastName);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@passwordSalt", user.PasswordSalt);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public bool DeleteUser(string userName, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_DeleteUser, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    cmd.Parameters.AddWithValue("@password", password);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool UpdatePassword(string userName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_UpdatePassword, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public UserModel SelectUser(string userName)
        {
            UserModel user = new UserModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SelectUser, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.UserName = Convert.ToString(reader["userName"]);
                        user.FirstName = Convert.ToString(reader["firstName"]);
                        user.LastName = Convert.ToString(reader["lastName"]);
                        user.Password = Convert.ToString(reader["password"]);
                        user.PasswordSalt = Convert.ToString(reader["passwordSalt"]);
                        user.Admin = Convert.ToBoolean(reader["admin"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
            return user;
        }


    }
}