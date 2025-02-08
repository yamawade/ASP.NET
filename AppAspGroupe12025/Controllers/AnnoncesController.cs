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
    public class AnnoncesController : Controller
    {
        private BDAgenceVoyageContext db = new BDAgenceVoyageContext();

        // GET: Annonces
        public ActionResult Index()
        {
            var annonces = db.Annonces.Include(a => a.Gestionnaire);
            return View(annonces.ToList());
        }

        // GET: Annonces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            return View(annonce);
        }

        // GET: Annonces/Create
        public ActionResult Create()
        {
            ViewBag.IdGestionnaire = new SelectList(db.utilisateurs, "IdUtilisateur", "NomUtilisateur");
            return View();
        }

        // POST: Annonces/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAnnonce,Description,Statut,DateDepart,DateArrivé,Localité,IdGestionnaire")] Annonce annonce)
        {
            if (ModelState.IsValid)
            {
                db.Annonces.Add(annonce);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGestionnaire = new SelectList(db.utilisateurs, "IdUtilisateur", "NomUtilisateur", annonce.IdGestionnaire);
            return View(annonce);
        }

        // GET: Annonces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGestionnaire = new SelectList(db.utilisateurs, "IdUtilisateur", "NomUtilisateur", annonce.IdGestionnaire);
            return View(annonce);
        }

        // POST: Annonces/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAnnonce,Description,Statut,DateDepart,DateArrivé,Localité,IdGestionnaire")] Annonce annonce)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annonce).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGestionnaire = new SelectList(db.utilisateurs, "IdUtilisateur", "NomUtilisateur", annonce.IdGestionnaire);
            return View(annonce);
        }

        // GET: Annonces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Annonce annonce = db.Annonces.Find(id);
            if (annonce == null)
            {
                return HttpNotFound();
            }
            return View(annonce);
        }

        // POST: Annonces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Annonce annonce = db.Annonces.Find(id);
            db.Annonces.Remove(annonce);
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
