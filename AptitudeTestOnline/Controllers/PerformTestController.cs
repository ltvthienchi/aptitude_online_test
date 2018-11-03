using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AptitudeTestOnline.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace AptitudeTestOnline.Controllers
{
    
    public class PerformTestController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();
        private ApplicationDbContext dbApp = new ApplicationDbContext();
        public List<QuestionsModels> GetQuestions()
        {
            //
            return db.QuestionsModels.ToList();
        }
       
        public void GetData()
        {
            ViewData["Questions"] = GetQuestions();
        }
        private Accounts getUserAccounts()
        {
            var userID = User.Identity.GetUserId();
            var userAccounts = db.AccountModels.Where(item => item.UserID == userID).ToList();
            if (userAccounts != null)
            {
                return userAccounts[0];
            }
            return null;
        }

        private DetailsRegistrations getUserDetails()
        {
            Accounts accounts = getUserAccounts();
            if (accounts != null)
            {
                int AccountID = accounts.AccountID;
                var userDetails = db.DetailsRegistrations.Where(item => item.AccountID == AccountID).ToList();
                if (userDetails.Count() != 0)
                {
                    return userDetails[0];
                }
                return null;
            }
            return null;
        }

        private SchedulesModels getUserSchedules()
        {
            DetailsRegistrations detailsRegistrations = getUserDetails();
            if (detailsRegistrations != null)
            {
                int ScheduleID = detailsRegistrations.ScheduleID;
                SchedulesModels userSchedules = db.Schedules.Find(ScheduleID);
                return userSchedules;
            }
            return null;
        }
        // GET: PerformTest

        public bool CheckTime()
        {
            SchedulesModels userSchedules = getUserSchedules();
            DetailsRegistrations userDetailsReg = getUserDetails();
            if(userDetailsReg != null && userSchedules != null)
            {
                DateTime Now = DateTime.Now;
                DateTime ScheduleTime = userSchedules.DateOfTime;
                if (Now.Date > ScheduleTime.Date)
                {
                    ViewBag.CheckDate = false;
                    ViewBag.CheckText = "Exam has been over!";
                    return false;
                }
                else if (userDetailsReg.Mark != -1)
                {
                    ViewBag.CheckDate = false;
                    ViewBag.CheckText = "You have already test! click under link to view the results!";
                    ViewBag.CheckLink = "Go to result!";
                    return false;
                }
                else
                {
                    ViewBag.CheckDate = true;
                    ViewBag.CheckText = "You have start your test";
                }
                return true;
            }
            ViewBag.CheckDate = false;
            ViewBag.CheckText = "Your current exam schedule is not available, please try again later!";
            return false;
        }

        public ActionResult Index()
        {
            CheckTime();
            return View();
        }


        public ActionResult Begin()
        {
            bool check = CheckTime();
            if (check == true)
            {
                SchedulesModels userSchedules = getUserSchedules();
                int ExamID = userSchedules.ExamID;
                GetData();
                List<int> MyList = new List<int>();
                var ListDetailsQuestions = db.DetailsExamModels.Where(r => r.ExamID == ExamID).ToList();
                ViewData["DetailQuestions"] = ListDetailsQuestions;
                foreach (var item in ListDetailsQuestions)
                {
                    MyList.Add(item.QuestionsID);
                }
                ViewBag.Test = ListDetailsQuestions;
                ViewData["MyQuestions"] = db.QuestionsModels.Where(r => MyList.Contains(r.QuestionsID)).ToList();
                DetailsRegistrations userDetail = getUserDetails();
                userDetail.Mark = 0;
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return View(db.ExamModels.ToList());
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Begin(int? id)
        {
            int AcountID = getUserAccounts().AccountID;
            int ExamID = getUserSchedules().ExamID;
            List<int> MyList = new List<int>();
            var ListDetailsQuestions = db.DetailsExamModels.Where(r => r.ExamID == 1).ToList();
            ViewData["DetailQuestions"] = ListDetailsQuestions;
            foreach (var item in ListDetailsQuestions) { MyList.Add(item.QuestionsID); }
            var ListMyQuestions = db.QuestionsModels.Where(r => MyList.Contains(r.QuestionsID)).ToList();
            //update candidate mark's

            int CandidateMark = 0, totalMark = 0;
            foreach (var item in ListMyQuestions)
            {
                //logic mark
                string name = "T" + item.TypeOfQuestion + item.QuestionsID;
                var temp = int.Parse(Request.Form[name]);
                if (temp == item.CorrectAnswer) CandidateMark += item.Mark;
                totalMark += item.Mark;
                //add candidate answer
                int value = int.Parse(Request.Form[name]);
                CandidateAnswer NewCanAnswer = new CandidateAnswer();
                NewCanAnswer.QuestionID = item.QuestionsID;
                NewCanAnswer.AccountID = AcountID;
                NewCanAnswer.Answer = value;
                db.CandidateAnswers.Add(NewCanAnswer);
            }
            //add mark
            int userMark = (CandidateMark * 100) / totalMark;
            DetailsRegistrations userDetail = getUserDetails();
            userDetail.Mark = userMark;
            db.Entry(userDetail).State = EntityState.Modified;
            //add record candidate test
            PerformTest record = new PerformTest();
            record.AccountID = AcountID;
            record.AddedDate = DateTime.Now;
            record.ExamID = ExamID;
            record.TimePartOne = Request.Form["timeRemaingOne"];
            record.TimePartTwo = Request.Form["timeRemaingTwo"];
            record.TimePartThree = Request.Form["timeRemaingThree"];
            db.PerformTests.Add(record);
            //
            db.SaveChanges();
            return Redirect("Result");
        }

        public ActionResult Result()
        {
            DetailsRegistrations userDetails = getUserDetails();
            return View(userDetails);
        }
    }
}
