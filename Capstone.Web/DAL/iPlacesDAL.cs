using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;


namespace Capstone.Web.DAL
{
    public interface IPlacesDAL
    {
        List<PlacesModel> GetAllPlaces();
        PlacesModel GetSinglePlace(int id);
        bool UpdatePlace(int id, string value, string column);
        bool CreatePlace(PlacesModel place);
        bool DeletePlace(int id);
        int CreatePlaceForUser(PlacesModel place);

    }
}