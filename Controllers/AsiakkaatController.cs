using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausWebApp.Models;
using System.Data.Entity;

namespace TilausWebApp.Controllers
{
    public class AsiakkaatController : Controller
    {
        // GET: Asiakkaat
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
                List<Asiakkaat> model = entities.Asiakkaat.ToList();
                ViewBag.LoggedStatus = "In";
                entities.Dispose();
                return View(model);
            }
        }
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Asiakkaat asiakkaat = entities.Asiakkaat.Find(id);
            if (asiakkaat == null) return HttpNotFound();
            return View(asiakkaat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                entities.Entry(asiakkaat).State = EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakkaat);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AsiakasID,Nimi,Osoite,Postinumero")] Asiakkaat asiakkaat)
        {
            if (ModelState.IsValid)
            {
                entities.Asiakkaat.Add(asiakkaat);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asiakkaat);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Asiakkaat asiakkaat = entities.Asiakkaat.Find(id);
            if (asiakkaat == null) return HttpNotFound();
            return View(asiakkaat);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asiakkaat asiakkaat = entities.Asiakkaat.Find(id);
            entities.Asiakkaat.Remove(asiakkaat);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
