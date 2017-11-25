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
        private const string SQL_CreatePlace = "insert into places values(@streetAddress, @city, @state, @latitude, @longitude, @googleID, @detail, @placeName, @category, @zip)";
        private const string SQL_DeletePlace = "delete * from places where id = @id";
        private const string SQL_GetAllPlaces = "select * from places";
        private const string SQL_GetSinglePlace = "select * from places where id = @id";
        private const string SQL_UpdatePlace = "update places set  @column = @value where id = @id";
        private const string SQL_InsertUserPlace = "insert into places values(@streetAddress, @city, @state, @latitude, @longitude, @googleID, @detail,  @placeName, @category, @zip, @userId)";
        private object ViewBag;

        public bool CreatePlace(PlacesModel place)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CreatePlace, conn);
                    if (place.GoogleID == null)
                    {
                        cmd.Parameters.AddWithValue("@streetAddress", place.StreetAddress);
                        cmd.Parameters.AddWithValue("@city", place.City);
                        cmd.Parameters.AddWithValue("@state", place.State);
                        cmd.Parameters.AddWithValue("@latitude", place.Latitude);
                        cmd.Parameters.AddWithValue("@longitude", place.Longitude);
                        cmd.Parameters.AddWithValue("@googleID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@detail", place.City);
                        cmd.Parameters.AddWithValue("@placeName", place.State);
                        cmd.Parameters.AddWithValue("@category", place.Latitude);
                        cmd.Parameters.AddWithValue("@zip", place.Zip);

                        int rowsAffectedOne = cmd.ExecuteNonQuery();
                        return rowsAffectedOne > 0;
                    }
                    cmd.Parameters.AddWithValue("@streetAddress", place.StreetAddress);
                    cmd.Parameters.AddWithValue("@city", place.City);
                    cmd.Parameters.AddWithValue("@state", place.State);
                    cmd.Parameters.AddWithValue("@latitude", place.Latitude);
                    cmd.Parameters.AddWithValue("@longitude", place.Longitude);
                    cmd.Parameters.AddWithValue("@googleID", place.GoogleID);
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
                        if ((reader["googleID"] == DBNull.Value))
                        {
                            place.Id = Convert.ToInt32(reader["id"]);
                            place.StreetAddress = Convert.ToString(reader["streetAddress"]);
                            place.City = Convert.ToString(reader["city"]);
                            place.State = Convert.ToString(reader["state"]);
                            place.Latitude = Convert.ToDecimal(reader["latitude"]);
                            place.Longitude = Convert.ToDecimal(reader["longitude"]);
                            place.Detail = Convert.ToString(reader["detail"]);
                            place.PlaceName = Convert.ToString(reader["placeName"]);
                            place.Category = Convert.ToString(reader["category"]);
                            place.Zip = Convert.ToInt32(reader["zip"]);

                            places.Add(place);

                        }
                        else 
                        {
                            place.Id = Convert.ToInt32(reader["id"]);
                            place.StreetAddress = Convert.ToString(reader["streetAddress"]);
                            place.City = Convert.ToString(reader["city"]);
                            place.State = Convert.ToString(reader["state"]);
                            place.Latitude = Convert.ToDecimal(reader["latitude"]);
                            place.Longitude = Convert.ToDecimal(reader["longitude"]);
                            place.GoogleID = null;
                            place.Detail = Convert.ToString(reader["detail"]);
                            place.PlaceName = Convert.ToString(reader["placeName"]);
                            place.Category = Convert.ToString(reader["category"]);
                            place.Zip = Convert.ToInt32(reader["zip"]);

                            places.Add(place);
                        }
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

                    SqlCommand cmd = new SqlCommand(SQL_GetSinglePlace, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if ((reader["googleID"] == DBNull.Value))
                        {
                            place.Id = Convert.ToInt32(reader["id"]);
                            place.StreetAddress = Convert.ToString(reader["streetAddress"]);
                            place.City = Convert.ToString(reader["city"]);
                            place.State = Convert.ToString(reader["state"]);
                            place.Latitude = Convert.ToDecimal(reader["latitude"]);
                            place.Longitude = Convert.ToDecimal(reader["longitude"]);
                            place.Detail = Convert.ToString(reader["detail"]);
                            place.PlaceName = Convert.ToString(reader["placeName"]);
                            place.Category = Convert.ToString(reader["category"]);
                            place.Zip = Convert.ToInt32(reader["zip"]);

                            return place;
                        }
                        place.Id = Convert.ToInt32(reader["id"]);
                        place.StreetAddress = Convert.ToString(reader["streetAddress"]);
                        place.City = Convert.ToString(reader["city"]);
                        place.State = Convert.ToString(reader["state"]);
                        place.Latitude = Convert.ToDecimal(reader["latitude"]);
                        place.Longitude = Convert.ToDecimal(reader["longitude"]);
                        place.GoogleID = Convert.ToInt32(reader["googleID"]);
                        place.Detail = Convert.ToString(reader["detail"]);
                        place.PlaceName = Convert.ToString(reader["placeName"]);
                        place.Category = Convert.ToString(reader["category"]);
                        place.Zip = Convert.ToInt32(reader["zip"]);
                    }
                    return place;
                }
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

        public int CreatePlaceForUser(PlacesModel place)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_InsertUserPlace, conn);
                    if (place.GoogleID == null)
                    {
                        cmd.Parameters.AddWithValue("@streetAddress", place.StreetAddress);
                        cmd.Parameters.AddWithValue("@city", place.City);
                        cmd.Parameters.AddWithValue("@state", place.State);
                        cmd.Parameters.AddWithValue("@latitude", place.Latitude);
                        cmd.Parameters.AddWithValue("@longitude", place.Longitude);
                        cmd.Parameters.AddWithValue("@googleID", DBNull.Value);
                        cmd.Parameters.AddWithValue("@detail", DBNull.Value);
                        cmd.Parameters.AddWithValue("@placeName", place.PlaceName);
                        cmd.Parameters.AddWithValue("@Category", place.Category);
                        cmd.Parameters.AddWithValue("@zip", place.Zip);
                        cmd.Parameters.AddWithValue("@userId", place.UserId);

                        int rowsAffectedOne = cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("SELECT Max(Id) from places;", conn);
                        int newlyAddedPlaceIdOne = (int)(cmd.ExecuteScalar());

                        return newlyAddedPlaceIdOne;
                    }
                    cmd.Parameters.AddWithValue("@streetAddress", place.StreetAddress);
                    cmd.Parameters.AddWithValue("@city", place.City);
                    cmd.Parameters.AddWithValue("@state", place.State);
                    cmd.Parameters.AddWithValue("@latitude", place.Latitude);
                    cmd.Parameters.AddWithValue("@longitude", place.Longitude);
                    cmd.Parameters.AddWithValue("@googleID", place.GoogleID);
                    cmd.Parameters.AddWithValue("@detail", DBNull.Value);
                    cmd.Parameters.AddWithValue("@placeName", place.PlaceName);
                    cmd.Parameters.AddWithValue("@Category", place.Category);
                    cmd.Parameters.AddWithValue("@zip", place.Zip);
                    cmd.Parameters.AddWithValue("@userId", place.UserId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT Max(Id) from places;", conn);
                    int newlyAddedPlaceId = (int)(cmd.ExecuteScalar());

                    return newlyAddedPlaceId;
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}
