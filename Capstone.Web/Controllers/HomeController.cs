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
            if(logCode == 1)
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
    }
}