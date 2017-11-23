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
                        newPlaceFromItin = pdal.GetSinglePlace(singlePlaceId);
                        listOfPlacesInItin.Add(newPlaceFromItin);
                    }
                    singleIntinForUser.Places = listOfPlacesInItin;
                    model.Add(singleIntinForUser);
                }

                return View("UserDashboard", model);
            }
            else
            {
                return View("LoginRegister");
            }
        }

        //public ActionResult CreateItinerary()
        //{
        //    PlacesDAL pdal = new PlacesDAL();
        //    var places = pdal.GetAllPlaces();
        //    List<SelectListItem> listOfPlaces = new List<SelectListItem>();
        //    foreach (var place in places)
        //    {
        //        //string latLong = place.Latitude + "|" + place.Longitude;
        //        listOfPlaces.Add(new SelectListItem() { Text = place.PlaceName, Value = place.Id });
        //    }
        //    ViewBag.Places = listOfPlaces;

        //    if (Session["user"] != null)
        //    {
        //        ItineraryModel model = new ItineraryModel();
        //        return View("CreateItinerary", model);
        //    }
        //    return View("LoginRegister");
        //}

        [HttpPost]
        public ActionResult SavedItinerary(PlacesModel newPlaceForUser)
        {
            if (Session["user"] != null)
            {
                PlacesDAL pdal = new PlacesDAL();
                UserModel user = Session["user"] as UserModel;
                newPlaceForUser.UserId = user.UserId;
                var save = pdal.CreatePlaceForUser(newPlaceForUser);
                return Json(new { result = "OK" });
            }
            return View("LoginRegister");
        }


        [HttpPost]
        public ActionResult CreateItinerary(ItineraryModel newItinerary, int id)
        {

            newItinerary.StartLocation = id;

            UserModel user = Session["user"] as UserModel;
            newItinerary.UserId = user.UserId;

            ItineraryDAL idal = new ItineraryDAL();
            idal.CreateNewItinerary(newItinerary);

            return RedirectToAction("UserDashboard");
        }
    }
}