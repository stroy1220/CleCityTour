using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public interface IItineraryDAL
    {
        bool CreateNewItinerary(ItineraryModel itinerary);
        bool DeleteItinerary(int id);
        bool AddPlaceToItinerary(int itineraryId, int placeId);
        bool RemovePlaceFromItinerary(int placeId, int itineraryId);
        bool UpdateStartLocation(decimal lat, decimal longg, int id);
        bool UpdateStartDate(DateTime startDate, int id);
        bool UpdateName(string name, int id);
        List<int> GetItinerary(int id);
        List<ItineraryModel> GetAllItinerary(int userId);
        List<ItineraryPlacesModel> GetAllItineraryPlacesForUser(int userId);
        bool EditItineraryOrder(int itineraryId);
        ItineraryModel GetAllItineraryInfo(int itineraryId);
        int GetMostRecentlyCreatedItinerary(int userId);

    }
}