using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Classes;
using System.ComponentModel;

namespace Capstone.Web.Controllers
{

    public class HomeController : Controller
    {
        private IUserDAL dal;


        public HomeController(IUserDAL userDAL)
        {
            this.dal = userDAL;
        }

        // GET: Home
        public ActionResult Index([DefaultValue(0)]int logCode)
        {
            if (logCode == 1)
            {
                Session["user"] = null;
            }
            return View("Index");
        }

        public ActionResult Route()
        {
            PlacesDAL pdal = new PlacesDAL();
            if (Session["user"] != null)
            {
                UserModel user = Session["user"] as UserModel;
                ItineraryDAL idal = new ItineraryDAL();
                List<ItineraryPlacesModel> model = idal.GetAllItineraryPlacesForUser(user.UserId);

                return View("Route", model);
            }
            else
            {
                return RedirectToAction("LoginRegister");
            }
        }

        public ActionResult LoginRegister()
        {
            return View("LoginRegister");
        }

        [HttpPost]
        public ActionResult LoginRegister(UserModel newUser, int logCode)
        {
            ViewBag.ErrorMessage = null;
            if (logCode == 1)
            {
                if (ModelState.IsValid)
                {
                    HashSalt h = new HashSalt();
                    newUser.Password = h.HashPassword(newUser.Password);
                    newUser.PasswordSalt = h.SaltValue;

                    if (dal.SaveUser(newUser))
                    {
                        if (Session["user"] == null)
                        {
                            Session["user"] = newUser;
                        }
                        return RedirectToAction("Index", new { firstName = newUser.FirstName });
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "This username is unavailable";
                        return View("LoginRegister");
                    }

                }
                return View("LoginRegister");
            }
            if (logCode == 0)
            {
                UserModel user = dal.SelectUser(newUser.UserName);
                UserModel hashedUser = user;
                if (newUser.UserName == user.UserName)
                {
                    HashSalt login = new HashSalt();
                    if (login.VerifyPasswordMatch(user.Password, newUser.Password, user.PasswordSalt))
                    {
                        if (Session["user"] == null)
                        {
                            Session["user"] = user;
                        }
                        return RedirectToAction("Index", new { firstName = newUser.FirstName });
                    }
                }
            }

            return View("LoginRegister");
        }

        public ActionResult UserDashboard()
        {
            PlacesDAL pdal = new PlacesDAL();
            if (Session["user"] != null)
            {

                UserModel user = Session["user"] as UserModel;
                ItineraryDAL idal = new ItineraryDAL();
                List<ItineraryPlacesModel> model = idal.GetAllItineraryPlacesForUser(user.UserId);



                return View("UserDashboard", model);
            }
            else
            {
                return View("LoginRegister");
            }
        }

        [HttpPost]
        public ActionResult SavedItinerary(PlacesModel newPlaceForUser)
        {
            if (Session["user"] != null)
            {
                PlacesDAL pdal = new PlacesDAL();
                ItineraryDAL idal = new ItineraryDAL();
                UserModel user = Session["user"] as UserModel;
                newPlaceForUser.UserId = user.UserId;
                List<ItineraryModel> userItinerary = idal.GetAllItinerary(user.UserId);
                var save = pdal.CreatePlaceForUser(newPlaceForUser);
                var saveToItin = idal.AddPlaceToItinerary(userItinerary[0].Id, save);
                return Json(new { result = "OK" });
            }
            return View("LoginRegister");
        }

        [HttpPost]
        public ActionResult SavePlaceToItineraryFromList(int placeId)
        {
            if (Session["user"] != null)
            {
                PlacesDAL pdal = new PlacesDAL();
                ItineraryDAL idal = new ItineraryDAL();
                UserModel user = Session["user"] as UserModel;
                List<ItineraryModel> userItinerary = idal.GetAllItinerary(user.UserId);
                if (userItinerary.Count > 0) { 
                var saveToItin = idal.AddPlaceToItinerary(userItinerary[0].Id, placeId);

                return RedirectToAction("UserDashboard");
                }
                else
                {
                    return RedirectToAction("CreateItinerary");
                }
            }
            return View("LoginRegister");
        }

        public ActionResult CreateItinerary()
        {
            PlacesDAL pdal = new PlacesDAL();
            var places = pdal.GetAllPlaces();
            List<SelectListItem> listOfPlaces = new List<SelectListItem>();
            foreach (var place in places)
            {
                listOfPlaces.Add(new SelectListItem() { Text = place.PlaceName, Value = place.Id.ToString() });
            }
            ViewBag.Places = listOfPlaces;

            if (Session["user"] != null)
            {
                ItineraryModel model = new ItineraryModel();
                return View("CreateItinerary", model);
            }
            return View("LoginRegister");
        }

        [HttpPost]
        public ActionResult CreateItinerary(ItineraryModel newItinerary, string id)
        {
            PlacesDAL dal = new PlacesDAL();
            PlacesModel p = new PlacesModel();
            
            p = dal.GetSinglePlace(Convert.ToInt32(id));

            newItinerary.StartLocationLat = p.Latitude.ToString();
            newItinerary.StartLocationLong = p.Longitude.ToString();

            UserModel user = Session["user"] as UserModel;
            newItinerary.UserId = user.UserId;

            ItineraryDAL idal = new ItineraryDAL();
            idal.CreateNewItinerary(newItinerary);

            int neededIdNumber = idal.GetMostRecentlyCreatedItinerary(user.UserId);
            idal.AddPlaceToItinerary(neededIdNumber, Convert.ToInt32(id));

            return RedirectToAction("UserDashboard");
        }

        [HttpPost]
        public ActionResult DeleteItinerary(int itineraryId)
        {
            ItineraryDAL idal = new ItineraryDAL();
            idal.DeleteItinerary(itineraryId);

            return RedirectToAction("UserDashboard");
        }

        [HttpPost]
        public ActionResult DeletePlaceFromItinerary(int placeId, int itineraryId)
        {
            ItineraryDAL idal = new ItineraryDAL();
            idal.RemovePlaceFromItinerary(placeId, itineraryId);

            return RedirectToAction("UserDashboard");
        }

        [HttpPost]
        public ActionResult EditUserItinerary(int itineraryId, List<int> order)
        {
            UserModel user = Session["user"] as UserModel;
            ItineraryDAL idal = new ItineraryDAL();
            List<ItineraryPlacesModel> output = idal.GetAllItineraryPlacesForUser(user.UserId);
            List<PlacesModel> places = new List<PlacesModel>();
            List<PlacesModel> newOrderPlaces = new List<PlacesModel>();
            Dictionary<int, PlacesModel> placeDictionary = new Dictionary<int, PlacesModel>();
            
            foreach(var i in output)
            {
                if(i.Itinerary.Id == itineraryId)
                {
                    foreach(var p in i.Places)
                    {
                        int counter = 0;
                        places.Add(p);
                        placeDictionary.Add(order[counter], p);
                        counter++;
                    }
                    foreach (var p in i.Places)
                    {                  
                        idal.RemovePlaceFromItinerary(p.Id, itineraryId);
                    }
                    foreach(var kvp in placeDictionary)
                    {
                        for(int m = 1; m < placeDictionary.Count - 1; m++)
                        {
                            if(kvp.Key == m)
                            {
                                idal.AddPlaceToItinerary(itineraryId, kvp.Value.Id);
                            }
                        }
                    }

                }
            }
            return RedirectToAction("UserDashboard");
        }
    }
}