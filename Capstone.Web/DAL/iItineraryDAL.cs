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
        bool RemovePlaceFromItinerary(int id);
        bool UpdateStartLocation(decimal lat, decimal longg, int id);
        bool UpdateStartDate(DateTime startDate, int id);
        bool UpdateName(string name, int id);
        List<PlacesModel> GetItinerarty(int id);
        List<ItineraryModel> GetAllItinerary(int userId);
    }
}