using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ItineraryDAL : IItineraryDAL
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CityTour"].ConnectionString;
        private const string SQL_AddPlaceToItinerary = "insert into itineraryPlaces values(@itineraryId, @placesId)";
        private const string SQL_CreateNeweItinerary = "insert into itinerary values(@userId, @name, @startLocation, @date)";
        private const string SQL_DeleteItinerary = "delete * from itinerary where id = @id";
        private const string SQL_RemovePlaceFromItinerary = "delete * from itineraryPlaces where placeId = @placeId";
        private const string SQL_GetItinerary = "select placeId from itineraryPlaces where itineraryID = (select max(id) from itinerary where userId = @userId)";
        private const string SQL_UpdateName = "update itinerary set name = @name where id = @id";
        private const string SQL_StartDate = "update itinerary set startDate = @startDate where id = @id";
        private const string SQL_UpdateLocation = "update itinerary set startLocation = @startLocation where id = @id";
        private const string SQL_GetAllItinerary = "select * from itinerary where userId = @userId order by id desc";

        public bool AddPlaceToItinerary(int itineraryId, int placeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_AddPlaceToItinerary, conn);
                    cmd.Parameters.AddWithValue("@itineraryId", itineraryId);
                    cmd.Parameters.AddWithValue("@placesId", placeId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool CreateNewItinerary(ItineraryModel itinerary)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_CreateNeweItinerary, conn);
                cmd.Parameters.AddWithValue("@userId", itinerary.UserId);
                cmd.Parameters.AddWithValue("@name", itinerary.Name);
                cmd.Parameters.AddWithValue("@startLocation", itinerary.StartLocation);
                cmd.Parameters.AddWithValue("@date", itinerary.Date);


                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        catch (SqlException ex)
        {
            throw;
        }
    }

    public bool DeleteItinerary(int id)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_DeleteItinerary, conn);
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

    public bool RemovePlaceFromItinerary(int id)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_RemovePlaceFromItinerary, conn);
                cmd.Parameters.AddWithValue("@placeId", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        catch (SqlException ex)
        {
            throw;
        }
    }

<<<<<<< HEAD
    public List<PlacesModel> GetItinerarty(int id)
    {
        List<PlacesModel> output = new List<PlacesModel>();
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
=======
        public List<int> GetItinerary(int id)
        {
            List<int> output = new List<int>();
            try
>>>>>>> c870ccf93847f6ff4141973d7919f4c197138e8c
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_GetItinerary, conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
<<<<<<< HEAD
                    PlacesModel p = new PlacesModel();
                    p.Category = Convert.ToString(reader["category"]);
                    p.City = Convert.ToString(reader["city"]);
                    p.StreetAddress = Convert.ToString(reader["streetAddress"]);
                    p.State = Convert.ToString(reader["state"]);
                    p.Latitude = Convert.ToDecimal(reader["latitude"]);
                    p.Longitude = Convert.ToDecimal(reader["longitude"]);
                    p.GoogleID = Convert.ToInt32(reader["googleID"]);
                    p.Detail = Convert.ToString(reader["detail"]);
                    p.PlaceName = Convert.ToString(reader["placeName"]);
                    p.Zip = Convert.ToInt32(reader["zip"]);
                    p.Id = Convert.ToInt32(reader["id"]);

                    output.Add(p);
=======
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetItinerary, conn);
                    cmd.Parameters.AddWithValue("@userId", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int p = 0;
                        p = Convert.ToInt32(reader["placeId"]);

                        output.Add(p);
                    }
>>>>>>> c870ccf93847f6ff4141973d7919f4c197138e8c
                }
            }
            return output;
        }
        catch (SqlException ex)
        {
            throw;
        }
    }

    public bool UpdateName(string name, int id)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_UpdateName, conn);
                cmd.Parameters.AddWithValue("@name", name);
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

    public bool UpdateStartDate(DateTime startDate, int id)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_StartDate, conn);
                cmd.Parameters.AddWithValue("@startDate", startDate);
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

    public bool UpdateStartLocation(decimal lat, decimal longg, int id)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_UpdateLocation, conn);
                cmd.Parameters.AddWithValue("@latitude", lat);
                cmd.Parameters.AddWithValue("@longitude", longg);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        catch (SqlException ex)
        {
<<<<<<< HEAD
            throw;
        }
    }
    public List<ItineraryModel> GetAllItinerary(int userId)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

=======
            List<ItineraryModel> output = new List<ItineraryModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllItinerary, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ItineraryModel p = new ItineraryModel();
                        p.Id = Convert.ToInt32(reader["Id"]);
                        p.UserId = Convert.ToInt32(reader["UserId"]);
                        p.Name = Convert.ToString(reader["Name"]);
                        p.StartLocation = Convert.ToString(reader["StartLocation"]);
                        p.Date = Convert.ToDateTime(reader["Date"]);

                        output.Add(p);
                    }
                }

                return output;
            }
            catch (SqlException ex)
            {
                throw;
>>>>>>> c870ccf93847f6ff4141973d7919f4c197138e8c
            }
        }
        catch (SqlException ex)
        {
            throw;
        }
    }
}
}