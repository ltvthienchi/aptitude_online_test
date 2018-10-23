using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AptitudeTestOnline.Models;

namespace AptitudeTestOnline.Areas.Manager.Controllers
{
    public class ExamController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        // GET: Manager/Exam
        public ActionResult Index()
        {
            return View(db.ExamModels.ToList());
        }

        // GET: Manager/Exam/Details/5
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

        // GET: Manager/Exam/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Exam/Create
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

        // GET: Manager/Exam/Edit/5
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

        // POST: Manager/Exam/Edit/5
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

        // GET: Manager/Exam/Delete/5
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

        // POST: Manager/Exam/Delete/5
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
