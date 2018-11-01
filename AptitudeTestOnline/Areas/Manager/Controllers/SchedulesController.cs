﻿using System;
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
    public class SchedulesController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        // GET: Manager/Schedules
        public ActionResult Index()
        {
            return View(db.Schedules.ToList());
        }

        // GET: Manager/Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulesModels schedulesModels = db.Schedules.Find(id);
            if (schedulesModels == null)
            {
                return HttpNotFound();
            }
            return View(schedulesModels);
        }

        // GET: Manager/Schedules/Create
        public ActionResult Create()
        {
            GetData();
            return View();
        }

        // POST: Manager/Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Schedule,ExamID,DateOfTime")] SchedulesModels schedulesModels)
        {
            if (ModelState.IsValid)
            {
                db.Schedules.Add(schedulesModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schedulesModels);
        }

        // GET: Manager/Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            GetData();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulesModels schedulesModels = db.Schedules.Find(id);
            if (schedulesModels == null)
            {
                return HttpNotFound();
            }
            return View(schedulesModels);
        }

        // POST: Manager/Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Schedule,ExamID,DateOfTime")] SchedulesModels schedulesModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedulesModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedulesModels);
        }
        public List<ExamModels> GetExam()
        {
            return db.ExamModels.ToList();
        }

        public void GetData()
        {
            ViewData["Exam"] = GetExam();
        }

        // GET: Manager/Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchedulesModels schedulesModels = db.Schedules.Find(id);
            if (schedulesModels == null)
            {
                return HttpNotFound();
            }
            return View(schedulesModels);
        }

        // POST: Manager/Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchedulesModels schedulesModels = db.Schedules.Find(id);
            db.Schedules.Remove(schedulesModels);
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