using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AptitudeTestOnline.Models;
using PagedList;

namespace AptitudeTestOnline.Areas.Manager.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class SchedulesController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        // GET: Manager/Schedules
        public ActionResult Index(int? page)
        {
            GetData();
            var schedules = from q in db.Schedules select q;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            schedules = schedules.OrderByDescending(q => q.ScheduleID);
            return View(schedules.ToPagedList(pageNumber, pageSize));
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
        public ActionResult Create([Bind(Include = "ScheduleID,ExamID,DateOfTime")] SchedulesModels schedulesModels)
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
        public ActionResult Edit([Bind(Include = "ScheduleID,ExamID,DateOfTime")] SchedulesModels schedulesModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedulesModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schedulesModels);
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
        public List<ExamModels> GetExam()
        {
            return db.ExamModels.ToList();
        }

        public void GetData()
        {
            ViewData["Exam"] = GetExam();
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
