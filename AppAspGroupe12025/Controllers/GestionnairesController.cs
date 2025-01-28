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
    public class GestionnairesController : Controller
    {
        private BDAgenceVoyageContext db = new BDAgenceVoyageContext();

        // GET: Gestionnaires
        public ActionResult Index()
        {
            var gestionnaires = db.gestionnaires.Include(g => g.Agence);
            return View(gestionnaires.ToList());
        }

        // GET: Gestionnaires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestionnaire gestionnaire = db.gestionnaires.Find(id);
            if (gestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(gestionnaire);
        }

        // GET: Gestionnaires/Create
        public ActionResult Create()
        {
            ViewBag.IdAgence = new SelectList(db.Agences, "IdAgence", "NineaAgence");
            return View();
        }

        // POST: Gestionnaires/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,TelUtilisateur,CNIGestionnaire,IdAgence")] Gestionnaire gestionnaire)
        {
            if (ModelState.IsValid)
            {
                db.utilisateurs.Add(gestionnaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdAgence = new SelectList(db.Agences, "IdAgence", "NineaAgence", gestionnaire.IdAgence);
            return View(gestionnaire);
        }

        // GET: Gestionnaires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestionnaire gestionnaire = db.gestionnaires.Find(id);
            if (gestionnaire == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAgence = new SelectList(db.Agences, "IdAgence", "NineaAgence", gestionnaire.IdAgence);
            return View(gestionnaire);
        }

        // POST: Gestionnaires/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,TelUtilisateur,CNIGestionnaire,IdAgence")] Gestionnaire gestionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gestionnaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdAgence = new SelectList(db.Agences, "IdAgence", "NineaAgence", gestionnaire.IdAgence);
            return View(gestionnaire);
        }

        // GET: Gestionnaires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestionnaire gestionnaire = db.gestionnaires.Find(id);
            if (gestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(gestionnaire);
        }

        // POST: Gestionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gestionnaire gestionnaire = db.gestionnaires.Find(id);
            db.utilisateurs.Remove(gestionnaire);
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
