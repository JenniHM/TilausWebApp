using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TilausWebApp.Models;

namespace TilausWebApp.Controllers
{
    public class TilausrivitController : Controller
    {
        // GET: Tilausrivit
        TilausDBEntities1 entities = new TilausDBEntities1();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                ViewBag.LoggedStatus = "Out";
                return RedirectToAction("login", "home");
            }
            else
            {
                List<Tilausrivit> model = entities.Tilausrivit.ToList();
                ViewBag.LoggedStatus = "In";
                entities.Dispose();
                return View(model);
            }
        }
    }  
}