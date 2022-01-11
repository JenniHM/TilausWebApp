using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TilausWebApp.Models;
using System.Data.Entity;
using System.Net;

namespace TilausWebApp.Controllers
{
    public class TilauksetController : Controller
    {
        // GET: Tilaukset
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
                List<Tilaukset> model = entities.Tilaukset.ToList();
                ViewBag.LoggedStatus = "In";
                entities.Dispose();
                return View(model);
            }
           
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilaukset orders = entities.Tilaukset.Find(id);
            if (orders == null) return HttpNotFound();
            return View(orders);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset orders)
        {
            if (ModelState.IsValid)
            {
                entities.Entry(orders).State = EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orders);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Toimitusosoite,Postinumero,Tilauspvm,Toimituspvm")] Tilaukset orders)
        {
            if (ModelState.IsValid)
            {
                entities.Tilaukset.Add(orders);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orders);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Tilaukset orders = entities.Tilaukset.Find(id);
            if (orders == null) return HttpNotFound();
            return View(orders);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset orders = entities.Tilaukset.Find(id);
            entities.Tilaukset.Remove(orders);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}