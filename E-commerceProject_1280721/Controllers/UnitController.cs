using E_commerceProject_1280721.DAL;
using E_commerceProject_1280721.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace E_commerceProject_1280721.Controllers
{
    public class UnitController : Controller
    {
        private EcommerceContext db = new EcommerceContext();

        public ActionResult Index()
        {
            return View(db.Units.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UnitId,UnitName")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Units.Add(unit);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Unit Added successfully";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error Adding Unit. Please try again.";
            return View(unit);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = db.Units.Find(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnitId,UnitName")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Unit Updated successfully";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error updating Unit. Please try again.";
            return View(unit);
        }

        public ActionResult Delete(int? id)
        {
            Unit unit = db.Units.Find(id);
            db.Entry(unit).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["DeleteMessage"] = "Unit deleted successfully.";
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