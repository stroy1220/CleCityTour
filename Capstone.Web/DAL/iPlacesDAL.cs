using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;


namespace Capstone.Web.DAL
{
    public interface IIPlacesDAL
    {
        List<PlacesModel> GetAllPlaces();
        PlacesModel GetSinglePlace(int id);
        PlacesModel UpdatePlace(PlacesModel place);
        bool CreatePlace(PlacesModel place);
        bool DeletePlace(int id);

    }
}