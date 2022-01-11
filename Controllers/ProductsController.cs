using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausWebApp.Models;

namespace TilausWebApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
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
                List<Tuotteet> model = entities.Tuotteet.ToList();
                ViewBag.LoggedStatus = "In";
                entities.Dispose();
                return View(model);
            }
            
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tuotteet tuotteet = entities.Tuotteet.Find(id);
            if (tuotteet == null) return HttpNotFound();
            return View(tuotteet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TuoteID,Nimi,Ahinta")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                entities.Entry(tuotteet).State = EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuotteet);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuoteID,Nimi,Ahinta")] Tuotteet tuotteet)
        {
            if (ModelState.IsValid)
            {
                entities.Tuotteet.Add(tuotteet);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuotteet);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tuotteet tuotteet = entities.Tuotteet.Find(id);
            if (tuotteet == null) return HttpNotFound();
            return View(tuotteet);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tuotteet tuotteet = entities.Tuotteet.Find(id);
            entities.Tuotteet.Remove(tuotteet);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}