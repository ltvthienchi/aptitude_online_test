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
    
    public class AccountsController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        // GET: Accounts
        

        // GET: Accounts/Details/5
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

        // GET: Accounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountID,Name,Education,Experience,Interest,Email,UserID,Dateofbirth")] Accounts accounts)
        {
            // check validate cua ngay thang
            DateTime check = DateTime.Parse(accounts.Dateofbirth.ToString(),
                          System.Globalization.CultureInfo.InvariantCulture);
            
            if (check.Year>DateTime.Now.Year || check.Year<1755)
            {
                ViewBag.errorDate = "Year Of Birth out range 1755 to 2018";
                return View(accounts);

            }else if (ModelState.IsValid)
            {
                db.AccountModels.Add(accounts);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(accounts);
        }

        // GET: Accounts/Edit/5
        

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
