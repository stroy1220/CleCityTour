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
        private const string SQL_CreateNeweItinerary = "insert into itinerary values(@userId, @name, @date, @StartLocationLong, @StartLocationLat)";
        private const string SQL_DeleteItinerary = "delete * from itinerary where id = @id";
        private const string SQL_RemovePlaceFromItinerary = "delete * from itineraryPlaces where placeId = @placeId";
        private const string SQL_UpdateName = "update itinerary set name = @name where id = @id";
        private const string SQL_StartDate = "update itinerary set startDate = @startDate where id = @id";
        private const string SQL_UpdateLocation = "update itinerary set startLocation = @startLocation where id = @id";
        private const string SQL_GetAllItinerary = "select * from itinerary where userId = @userId order by id desc";
        private const string SQL_GetItinerary = "select placeId from itineraryPlaces where itineraryID = (select max(id) from itinerary where userId = @userId)";
        private const string SQL_GetAllItineraryPlaces = "select * from itineraryPlaces where itineraryID = @itineraryId";



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
                    cmd.Parameters.AddWithValue("@date", itinerary.Date);
                    cmd.Parameters.AddWithValue("@StartLocationLong", itinerary.StartLocationLong);
                    cmd.Parameters.AddWithValue("@StartLocationLat", itinerary.StartLocationLat);


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

        public List<int> GetItinerary(int id)
        {
            List<int> output = new List<int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
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
                throw;
            }
        }
        public List<ItineraryModel> GetAllItinerary(int userId)
        {
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
                        p.Date = Convert.ToDateTime(reader["Date"]);
                        p.StartLocationLat = Convert.ToString(reader["startLocationLat"]);
                        p.StartLocationLong = Convert.ToString(reader["startLocationLong"]);


                        output.Add(p);
                    }
                }

                return output;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<ItineraryPlacesModel> GetAllItineraryPlacesForUser(int userId)
        {
            try
            {
                List<ItineraryPlacesModel> output = new List<ItineraryPlacesModel>();
                PlacesDAL pdal = new PlacesDAL();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    ItineraryDAL idal = new ItineraryDAL();
                    List<ItineraryModel> listOfItineraries = idal.GetAllItinerary(userId);
                    for (int i = 0; i < listOfItineraries.Count; i++)
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(SQL_GetAllItineraryPlaces, conn);
                        cmd.Parameters.AddWithValue("@itineraryId", listOfItineraries[i].Id);

                        SqlDataReader reader = cmd.ExecuteReader();
                        ItineraryPlacesModel it = new ItineraryPlacesModel();
                        it.Itinerary = listOfItineraries[i];
                        List<PlacesModel> placesList = new List<PlacesModel>();


                        while (reader.Read())
                        {
                            PlacesModel place = new PlacesModel();
                            int placeId = Convert.ToInt32(reader["placeId"]);
                            place = pdal.GetSinglePlace(placeId);
                        
                            placesList.Add(place);
                        }
                        it.Places = placesList;
                        output.Add(it);
                        conn.Close();
                    }
                }
                return output;
            }













            //ItineraryDAL idal = new ItineraryDAL();
            //List<ItineraryModel> itineraryModels = idal.GetAllItinerary(userId);

            //try
            //{
            //    List<int> output = new List<int>();
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {

            //        conn.Open();
            //        SqlCommand cmd2 = new SqlCommand(SQL_GetAllItineraryPlaces, conn);
            //        SqlDataReader reader2 = cmd2.ExecuteReader();

            //        foreach (var i in itineraryModels)
            //        {
            //            cmd2.Parameters.AddWithValue("@itineraryId", i.Id);

            //            while(reader2.Read())
            //            {
            //                int placeId;
            //                placeId = Convert.ToInt32(reader2["id"]);                     
            //                output.Add(placeId);
            //            }


            //        }


            //    }
            //    return output;
            //}
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}