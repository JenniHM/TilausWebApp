using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TilausWebApp.Models;


namespace TilausWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Out";
            }
            else ViewBag.LoggedStatus = "In";
            ViewBag.Selain = Request.UserAgent;
            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Yhtiön perustietojen kuvailua";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Yhteystiedot";

            return View();
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Kirjautuminen";

            return View();
        }
        public ActionResult Authorize(Logins LoginModel)
        {
            TilausDBEntities1 entities = new TilausDBEntities1();
            
            var LoggedUser = entities.Logins.SingleOrDefault(x => x.UserName == LoginModel.UserName && x.PassWord == LoginModel.PassWord);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                Session["UserName"] = LoggedUser.UserName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home");
        }
    }
}