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
    public class QuestionsController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        

        // GET: Manager/Questions
        public ActionResult Index()
        {
            return View(db.QuestionsModels.ToList());
        }

        // GET: Manager/Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionsModels questionsModels = db.QuestionsModels.Find(id);
            if (questionsModels == null)
            {
                return HttpNotFound();
            }
            return View(questionsModels);
        }

        // GET: Manager/Questions/Create
        public ActionResult Create()
        {
            ViewBag.TypeOfQuestion = db.TypeOfQuestionModel.ToList();
            ViewBag.Test = "Test";

            return View();
        }

        // POST: Manager/Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionsID,QuestionsName,TypeOfQuestion,AnswerOne,AnswerTwo,AnswerThree,AnswerFour,CorrectAnswer,Mark")] QuestionsModels questionsModels)
        {
            if (ModelState.IsValid)
            {
                db.QuestionsModels.Add(questionsModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionsModels);
        }

        // GET: Manager/Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionsModels questionsModels = db.QuestionsModels.Find(id);
            if (questionsModels == null)
            {
                return HttpNotFound();
            }
            return View(questionsModels);
        }

        // POST: Manager/Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionsID,QuestionsName,TypeOfQuestion,AnswerOne,AnswerTwo,AnswerThree,AnswerFour,CorrectAnswer,Mark")] QuestionsModels questionsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionsModels);
        }

        // GET: Manager/Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionsModels questionsModels = db.QuestionsModels.Find(id);
            if (questionsModels == null)
            {
                return HttpNotFound();
            }
            return View(questionsModels);
        }

        // POST: Manager/Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionsModels questionsModels = db.QuestionsModels.Find(id);
            db.QuestionsModels.Remove(questionsModels);
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
