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
    public class FlottesController : Controller
    {
        private BDAgenceVoyageContext db = new BDAgenceVoyageContext();

        // GET: Flottes
        public ActionResult Index()
        {
            return View(db.Flottes.ToList());
        }

        // GET: Flottes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flotte flotte = db.Flottes.Find(id);
            if (flotte == null)
            {
                return HttpNotFound();
            }
            return View(flotte);
        }

        // GET: Flottes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flottes/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFlotte,Type,MatriculeFlotte")] Flotte flotte)
        {
            if (ModelState.IsValid)
            {
                db.Flottes.Add(flotte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flotte);
        }

        // GET: Flottes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flotte flotte = db.Flottes.Find(id);
            if (flotte == null)
            {
                return HttpNotFound();
            }
            return View(flotte);
        }

        // POST: Flottes/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFlotte,Type,MatriculeFlotte")] Flotte flotte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flotte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flotte);
        }

        // GET: Flottes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flotte flotte = db.Flottes.Find(id);
            if (flotte == null)
            {
                return HttpNotFound();
            }
            return View(flotte);
        }

        // POST: Flottes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flotte flotte = db.Flottes.Find(id);
            db.Flottes.Remove(flotte);
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
