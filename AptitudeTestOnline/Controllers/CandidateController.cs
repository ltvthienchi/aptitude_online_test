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
    public class CandidateController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        // GET: Candidate
        public ActionResult Index()
        {
            return View(db.AccountModels.ToList());
        }

        // GET: Candidate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountModel = db.AccountModels.Find(id);
            if (accountModel == null)
            {
                return HttpNotFound();
            }
            return View(accountModel);
        }

        // GET: Candidate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Candidate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,Education,Experience,Interest")] AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                db.AccountModels.Add(accountModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountModel);
        }

        // GET: Candidate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountModel = db.AccountModels.Find(id);
            if (accountModel == null)
            {
                return HttpNotFound();
            }
            return View(accountModel);
        }

        // POST: Candidate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Education,Experience,Interest")] AccountModel accountModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountModel);
        }

        // GET: Candidate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountModel accountModel = db.AccountModels.Find(id);
            if (accountModel == null)
            {
                return HttpNotFound();
            }
            return View(accountModel);
        }

        // POST: Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountModel accountModel = db.AccountModels.Find(id);
            db.AccountModels.Remove(accountModel);
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
