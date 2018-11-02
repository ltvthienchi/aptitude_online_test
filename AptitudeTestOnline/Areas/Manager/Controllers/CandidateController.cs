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
    public class CandidateController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        // GET: Manager/Candidate
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            var Candidate = from q in db.AccountModels select q;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                Candidate = db.AccountModels.Where(s => s.Name.Contains(searchString));
            }


            Candidate = Candidate.OrderByDescending(q => q.AccountID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Candidate.ToPagedList(pageNumber, pageSize));
        }

        // GET: Manager/Candidate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.AccountModels.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // GET: Manager/Candidate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Candidate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,Name,Education,Experience,Interest,Email,UserID,Dateofbirth")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.AccountModels.Add(accounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accounts);
        }

        // GET: Manager/Candidate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.AccountModels.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Manager/Candidate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Name,Education,Experience,Interest,Email,UserID,Dateofbirth")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accounts);
        }

        // GET: Manager/Candidate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.AccountModels.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Manager/Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accounts accounts = db.AccountModels.Find(id);
            db.AccountModels.Remove(accounts);
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

        public ActionResult Schedule(int id)
        {
            ViewData["Accounts"] = db.AccountModels.Where(item => item.AccountID == id);
            ViewData["Schedules"] = db.Schedules.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Schedule([Bind(Include = "")] DetailsRegistrations detailsRegistrations)
        {
            if (ModelState.IsValid)
            {
                detailsRegistrations.Mark = -1;
                db.DetailsRegistrations.Add(detailsRegistrations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(detailsRegistrations);
        }
    }
}
