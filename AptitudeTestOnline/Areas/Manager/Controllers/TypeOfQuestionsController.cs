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
    [Authorize(Roles = "Admin, Manager")]
    public class TypeOfQuestionsController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        // GET: Manager/TypeOfQuestions
        public ActionResult Index()
        {
            return View(db.TypeOfQuestionModel.ToList());
        }

        // GET: Manager/TypeOfQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfQuestionModel typeOfQuestionModel = db.TypeOfQuestionModel.Find(id);
            if (typeOfQuestionModel == null)
            {
                return HttpNotFound();
            }
            return View(typeOfQuestionModel);
        }

        // GET: Manager/TypeOfQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/TypeOfQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeOfQuestion,NameTypeOfQuestion")] TypeOfQuestionModel typeOfQuestionModel)
        {
            if (ModelState.IsValid)
            {
                db.TypeOfQuestionModel.Add(typeOfQuestionModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeOfQuestionModel);
        }

        // GET: Manager/TypeOfQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfQuestionModel typeOfQuestionModel = db.TypeOfQuestionModel.Find(id);
            if (typeOfQuestionModel == null)
            {
                return HttpNotFound();
            }
            return View(typeOfQuestionModel);
        }

        // POST: Manager/TypeOfQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeOfQuestion,NameTypeOfQuestion")] TypeOfQuestionModel typeOfQuestionModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeOfQuestionModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeOfQuestionModel);
        }

        // GET: Manager/TypeOfQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfQuestionModel typeOfQuestionModel = db.TypeOfQuestionModel.Find(id);
            if (typeOfQuestionModel == null)
            {
                return HttpNotFound();
            }
            return View(typeOfQuestionModel);
        }

        // POST: Manager/TypeOfQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeOfQuestionModel typeOfQuestionModel = db.TypeOfQuestionModel.Find(id);
            db.TypeOfQuestionModel.Remove(typeOfQuestionModel);
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
