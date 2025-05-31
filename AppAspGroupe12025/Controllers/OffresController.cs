using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppAspGroupe12025.Models;

namespace AppAspGroupe12025.Controllers
{
    public class OffresController : Controller
    {
        private BDAgenceVoyageContext db = new BDAgenceVoyageContext();

        // GET: Offres
        public ActionResult Index()
        {
            return View(db.Offres.ToList());
        }

        // GET: Offres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // GET: Offres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Offres/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOffre,Description,Prix,Disponibilité")] Offre offre)
        {
            if (ModelState.IsValid)
            {
                db.Offres.Add(offre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offre);
        }

        // GET: Offres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // POST: Offres/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOffre,Description,Prix,Disponibilité")] Offre offre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(offre);
        }

        // GET: Offres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            return View(offre);
        }

        // POST: Offres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offre offre = db.Offres.Find(id);
            db.Offres.Remove(offre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
