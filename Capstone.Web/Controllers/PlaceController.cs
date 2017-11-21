using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Capstone.Web.Controllers
{
    public class PlaceController : Controller
    {
        private IPlacesDAL dal;

        public PlaceController(IPlacesDAL userDAL)
        {
            this.dal = userDAL;
        }

        public ActionResult Restaurant()
        {
            List<PlacesModel> model = dal.GetAllPlaces();
            return View("Restaurant", model);
        }

        public ActionResult Shopping()
        {
            List<PlacesModel> model = dal.GetAllPlaces();
            return View("Shopping", model);
        }

        public ActionResult Entertainment()
        {
            List<PlacesModel> model = dal.GetAllPlaces();
            return View("Entertainment", model);
        }

        public ActionResult Landmarks()
        {
            List<PlacesModel> model = dal.GetAllPlaces();
            return View("Landmarks", model);
        }

        public ActionResult Sports()
        {
            List<PlacesModel> model = dal.GetAllPlaces();
            return View("Sports", model);
        }

        public ActionResult ItinPartial()
        {
            List<ItineraryPlacesModel> model = new List<ItineraryPlacesModel>();
            UserModel user = Session["user"] as UserModel;
            ItineraryDAL itdal = new ItineraryDAL();
            List<ItineraryModel> itinerary = itdal.GetAllItinerary(user.UserId);
            List<int> listOfPlaceIds = itdal.GetItinerary(user.UserId);

            foreach (var i in itinerary)
            {
                ItineraryPlacesModel singleIntinForUser = new ItineraryPlacesModel();
                singleIntinForUser.Itinerary = i;
                List<PlacesModel> listOfPlacesInItin = new List<PlacesModel>();
                foreach (var singlePlaceId in listOfPlaceIds)
                {
                    PlacesModel newPlaceFromItin = new PlacesModel();
                    newPlaceFromItin = dal.GetSinglePlace(singlePlaceId);
                    listOfPlacesInItin.Add(newPlaceFromItin);
                }
                singleIntinForUser.Places = listOfPlacesInItin;
                model.Add(singleIntinForUser);
            }
            return View("ItinPartial", model);
        }

    }
}