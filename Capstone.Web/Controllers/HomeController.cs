using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Classes;

namespace Capstone.Web.Controllers
{

    public class HomeController : Controller
    {
        private IUserDAL dal;
        

        public HomeController(IUserDAL userDAL)
        {
            this.dal = userDAL;
        }

        public ActionResult Restaurant()
        {
            PlacesDAL pdal = new PlacesDAL();
            List<PlacesModel> model =  pdal.GetAllPlaces();
            return View("Restaurant", model);
        }

        // GET: Home
        public ActionResult Index(string firstName = "")
        {
            ViewBag.FirstName = firstName;
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
                        ViewBag.FirstName = newUser.FirstName;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "This username is unavailable";
                        return View("LoginRegister");
                    }

                }
                //ViewBag.ErrorMessage = "This username is unavailable";
                return View("LoginRegister");
            }
            UserModel user = dal.SelectUser(newUser.UserName);
            UserModel hashedUser = user;
            if (newUser.UserName == user.UserName)
            {
                HashSalt login = new HashSalt();
                if (login.VerifyPasswordMatch(user.Password, newUser.Password, user.PasswordSalt))
                {
                   
                    return RedirectToAction("Index", new { firstName = newUser.FirstName });
                }
            }
           
            return View("LoginRegister");
        }
    }
}