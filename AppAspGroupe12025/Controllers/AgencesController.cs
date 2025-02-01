using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppAspGroupe12025.Models;
using PagedList;

namespace AppAspGroupe12025.Controllers
{
    public class AgencesController : Controller
    {
        private BDAgenceVoyageContext db = new BDAgenceVoyageContext();
        const int pageSize = 1;
        // GET: Agences

        
        public ActionResult Index(string Adresse, string ninea, string rccm, int? page)
        {
            //var agences = db.Agences.Include(a => a.Gestionnaire);

            ViewBag.Addresse = Adresse!=null? Adresse: string.Empty;
            ViewBag.ninea = ninea!=null? ninea: string.Empty;
            ViewBag.rccm = rccm!=null? rccm: string.Empty;

            var liste = db.Agences.ToList();

            if(!string.IsNullOrEmpty(Adresse))
            {
                liste=liste.Where(a=>a.AdresseAgence.ToLower().Contains(Adresse.ToLower())).ToList();
            }
            if(!string.IsNullOrEmpty(ninea))
            {
                liste=liste.Where(a=>a.NineaAgence.ToLower().Contains(ninea.ToLower())).ToList();
            }
            if(!string.IsNullOrEmpty(rccm))
            {
                liste=liste.Where(a=>a.RccmAgence.ToLower().Contains(rccm.ToLower())).ToList();
            }

            page = page.HasValue ? page : 1;
            int pageNumber=(int)page;

            return View(liste.ToPagedList(pageNumber,pageSize));
        }

        // GET: Agences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // GET: Agences/Create
        public ActionResult Create()
        {
            ViewBag.IdGestionnaire = new SelectList(db.gestionnaires, "IdUtilisateur", "CNIGestionnaire");
            return View();
        }

        // POST: Agences/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAgence,NineaAgence,AdresseAgence,Longitude,Latitude,RccmAgence,IdGestionnaire")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                db.Agences.Add(agence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdGestionnaire = new SelectList(db.gestionnaires, "IdUtilisateur", "CNIGestionnaire", agence.IdGestionnaire);
            return View(agence);
        }

        // GET: Agences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdGestionnaire = new SelectList(db.gestionnaires, "IdUtilisateur", "CNIGestionnaire", agence.IdGestionnaire);
            return View(agence);
        }

        // POST: Agences/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAgence,NineaAgence,AdresseAgence,Longitude,Latitude,RccmAgence,IdGestionnaire")] Agence agence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdGestionnaire = new SelectList(db.gestionnaires, "IdUtilisateur", "CNIGestionnaire", agence.IdGestionnaire);
            return View(agence);
        }

        // GET: Agences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agence agence = db.Agences.Find(id);
            if (agence == null)
            {
                return HttpNotFound();
            }
            return View(agence);
        }

        // POST: Agences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agence agence = db.Agences.Find(id);
            db.Agences.Remove(agence);
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
