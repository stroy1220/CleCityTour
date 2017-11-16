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

        // GET: Home
        public ActionResult Index()
        {
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
            newUser = user;
            if (ModelState.IsValid)
            {
                HashSalt login = new HashSalt();
                if (login.VerifyPasswordMatch(user.Password, newUser.Password, user.PasswordSalt))
                {
                    return RedirectToAction("Index");
                }
            }

            return View("LoginRegister");
        }
    }
}