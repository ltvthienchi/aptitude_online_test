using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AptitudeTestOnline.Models;

namespace AptitudeTestOnline.Controllers
{
    public class PerformTestController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        // GET: PerformTest
        public ActionResult Index()
        {
            return View(db.ExamModels.ToList());
        }

        public ActionResult Begin()
        {
            return View();
        }

        // GET: PerformTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamModels examModels = db.ExamModels.Find(id);
            if (examModels == null)
            {
                return HttpNotFound();
            }
            return View(examModels);
        }

        // GET: PerformTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PerformTest/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamID,ExamName,Description")] ExamModels examModels)
        {
            if (ModelState.IsValid)
            {
                db.ExamModels.Add(examModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(examModels);
        }

        // GET: PerformTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamModels examModels = db.ExamModels.Find(id);
            if (examModels == null)
            {
                return HttpNotFound();
            }
            return View(examModels);
        }

        // POST: PerformTest/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamID,ExamName,Description")] ExamModels examModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(examModels);
        }

        // GET: PerformTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamModels examModels = db.ExamModels.Find(id);
            if (examModels == null)
            {
                return HttpNotFound();
            }
            return View(examModels);
        }

        // POST: PerformTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamModels examModels = db.ExamModels.Find(id);
            db.ExamModels.Remove(examModels);
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
