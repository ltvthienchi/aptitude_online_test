﻿using System;
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
    public class ExamController : Controller
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

        // GET: Manager/Exam
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {

            GetData();
            var exam = from q in db.ExamModels select q;
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
                exam = db.ExamModels.Where(s => s.ExamName.Contains(searchString));
            }

            exam = exam.OrderByDescending(q => q.ExamID);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(exam.ToPagedList(pageNumber, pageSize));
        }
        

        // GET: Manager/Exam/Create
        public ActionResult Create()
        {
            GetData();
            return View();
        }

        // POST: Manager/Exam/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamID,ExamName,Description")] ExamModels examModels)
        {
            var result = db.ExamModels.Add(examModels);
            db.SaveChanges();
            for (var i = 1; i <= 3; i++)
            {
                for (var j = 1; j <= 5; j++)
                {
                    string name = "T" + i + "Q" + j;
                    string value = Request.Form[name];
                    DetailsExamModels newDetail = new DetailsExamModels();
                    newDetail.ExamID = result.ExamID;
                    newDetail.QuestionsID = int.Parse(value);
                    db.DetailsExamModels.Add(newDetail);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Manager/Exam/Edit/5
        public ActionResult Edit(int? id)
        {
            GetData();
            List<int> MyList = new List<int>();
            var ListDetailsQuestions = db.DetailsExamModels.Where(r => r.ExamID == id).ToList();
            ViewData["DetailQuestions"] = ListDetailsQuestions;
            foreach (var item in ListDetailsQuestions)
            {
                MyList.Add(item.QuestionsID);
            }
            ViewData["MyQuestions"] = db.QuestionsModels.Where(r => MyList.Contains(r.QuestionsID)).ToList();
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
                var ListDetailsQuestions = db.DetailsExamModels.Where(r => r.ExamID == examModels.ExamID).ToList();
                foreach (var item in ListDetailsQuestions)
                {
                    string name = item.DetailsID + "";
                    string value = Request.Form[name];
                    DetailsExamModels oldDetail = db.DetailsExamModels.Find(item.DetailsID);
                    oldDetail.QuestionsID = int.Parse(value);
                    db.Entry(oldDetail).State = EntityState.Modified;
                }
                db.Entry(examModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(examModels);
        }
        
    }
}
