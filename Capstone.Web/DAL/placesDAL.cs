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
    public class PlacesDAL : IPlacesDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CityTour"].ConnectionString;
        private const string SQL_CreatePlace = "insert into places values(@streetAddress, @city, @state, @latitude, @longitude, @googleID, @detail, @placeName, @category)";
        private const string SQL_DeletePlace = "delete * from places where id = @id";
        private const string SQL_GetAllPlaces = "select * from places";
        private const string SQL_GetSinglePlace = "select * from places where id = @id";
        private const string SQL_UpdatePlace = "update places set  @column = @value where id = @id";
        private object ViewBag;

        public bool CreatePlace(PlacesModel place)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreatePlace, conn);

                    cmd.Parameters.AddWithValue("@streetAddress", place.StreetAddress);
                    cmd.Parameters.AddWithValue("@city", place.City);
                    cmd.Parameters.AddWithValue("@state", place.State);
                    cmd.Parameters.AddWithValue("@latitude", place.Latitude);
                    cmd.Parameters.AddWithValue("@longitude", place.Longitude);
                    //cmd.Parameters.AddWithValue("@googleID", place.StreetAddress);
                    cmd.Parameters.AddWithValue("@detail", place.City);
                    cmd.Parameters.AddWithValue("@placeName", place.State);
                    cmd.Parameters.AddWithValue("@category", place.Latitude);
                    cmd.Parameters.AddWithValue("@zip", place.Zip);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool DeletePlace(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_DeletePlace, conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<PlacesModel> GetAllPlaces()
        {
            List<PlacesModel> places = new List<PlacesModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllPlaces, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        PlacesModel place = new PlacesModel();
                        place.Id = Convert.ToInt32(reader["id"]);
                        place.StreetAddress = Convert.ToString(reader["streetAddress"]);
                        place.City = Convert.ToString(reader["city"]);
                        place.State = Convert.ToString(reader["state"]);
                        place.Latitude = Convert.ToDecimal(reader["latitude"]);
                        place.Longitude = Convert.ToDecimal(reader["longitude"]);
                        //place.GoogleID = Convert.ToInt32(reader["googleID"]);
                        place.Detail = Convert.ToString(reader["detail"]);
                        place.PlaceName = Convert.ToString(reader["placeName"]);
                        place.Category = Convert.ToString(reader["category"]);
                        place.Zip = Convert.ToInt32(reader["zip"]);

                        places.Add(place);
                    }
                }
                return places;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public PlacesModel GetSinglePlace(int id)
        {
            try
            {
                PlacesModel place = new PlacesModel();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllPlaces, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        place.Id = Convert.ToInt32(reader["id"]);
                        place.StreetAddress = Convert.ToString(reader["streetAddress"]);
                        place.State = Convert.ToString(reader["state"]);
                        place.Latitude = Convert.ToDecimal(reader["latitude"]);
                        place.Longitude = Convert.ToDecimal(reader["longitude"]);
                        //place.GoogleID = Convert.ToInt32(reader["googleID"]);
                        place.Detail = Convert.ToString(reader["detail"]);
                        place.PlaceName = Convert.ToString(reader["placeName"]);
                        place.Category = Convert.ToString(reader["category"]);
                        place.Zip = Convert.ToInt32(reader["zip"]);

                      
                    }
                }
                return place;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool UpdatePlace(int id, string value, string column)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_UpdatePlace, conn);

                    cmd.Parameters.AddWithValue("@column", column);
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
