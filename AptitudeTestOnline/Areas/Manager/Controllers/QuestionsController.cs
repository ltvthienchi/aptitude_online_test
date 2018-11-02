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
    public class QuestionsController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();

        public List<QuestionsModels> GetQuestions()
        {
            return db.QuestionsModels.ToList();
        }

        public List<TypeOfQuestionModel> GetTypeOfQuestions()
        {
            return db.TypeOfQuestionModel.ToList();
        }

        public void GetData()
        {
            ViewData["Questions"] = GetQuestions();
            ViewData["TypeOfQuestion"] = GetTypeOfQuestions();
        }

        // GET: Manager/Questions
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {

            GetData();
            var questions = from q in db.QuestionsModels select q;
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
                questions = db.QuestionsModels.Where(s => s.QuestionsName.Contains(searchString));
            }

            questions = questions.OrderByDescending(q => q.TypeOfQuestion);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(questions.ToPagedList(pageNumber, pageSize));
        }
        

        public PartialViewResult GetPagination(int? page)
        {
            GetData();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return PartialView("_Pagination", db.QuestionsModels.ToList().ToPagedList(pageNumber, pageSize));
        }
        
        // GET: Manager/Questions/Create
        public ActionResult Create()
        {
            GetData();
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
            GetData();
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
        
    }
}
