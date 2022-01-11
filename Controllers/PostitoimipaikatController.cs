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
    public class PostitoimipaikatController : Controller
    {
        // GET: Postitoimipaikat
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
                List<Postitoimipaikat> model = entities.Postitoimipaikat.ToList();
                ViewBag.LoggedStatus = "In";
                entities.Dispose();
                return View(model);
            }
         
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Postitoimipaikat ptp = entities.Postitoimipaikat.Find(id);
            if (ptp == null) return HttpNotFound();
            return View(ptp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat ptp)
        {
            if (ModelState.IsValid)
            {
                entities.Entry(ptp).State = EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ptp);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Postinumero,Postitoimipaikka")] Postitoimipaikat ptp)
        {
            if (ModelState.IsValid)
            {
                entities.Postitoimipaikat.Add(ptp);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ptp);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Postitoimipaikat ptp = entities.Postitoimipaikat.Find(id);
            if (ptp == null) return HttpNotFound();
            return View(ptp);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Postitoimipaikat ptp = entities.Postitoimipaikat.Find(id);
            entities.Postitoimipaikat.Remove(ptp);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}