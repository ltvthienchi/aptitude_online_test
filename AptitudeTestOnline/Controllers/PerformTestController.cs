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
            return userAccounts[0];
        }

        private DetailsRegistrations getUserDetails()
        {
            int AccountID = getUserAccounts().AccountID;
            var userDetails = db.DetailsRegistrations.Where(item => item.AccountID == AccountID).ToList();
            return userDetails[0];
        }

        private SchedulesModels getUserSchedules()
        {
            int ScheduleID = getUserDetails().ScheduleID;
            SchedulesModels userSchedules = db.Schedules.Find(ScheduleID);
            return userSchedules;
        }
        // GET: PerformTest

        public ActionResult Index()
        {
            SchedulesModels userSchedules = getUserSchedules();
            DateTime Now = DateTime.Now;
            DateTime ScheduleTime = userSchedules.DateOfTime;
            //var TempScheduleTime = ScheduleTime.ToString("DD/MM/YYYY");
            //DateTime MyScheduleTime = DateTime.ParseExact(TempScheduleTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (Now.Date <= ScheduleTime.Date)
            {
                ViewBag.CheckDate = true;
                ViewBag.CheckText = "You have start your test";
            } else
            {
                ViewBag.CheckDate = false;
                ViewBag.CheckText = "You don't start your test";
            }
            return View();
        }


        public ActionResult Begin()
        {
            SchedulesModels userSchedules = getUserSchedules();
            int ExamID = int.Parse(userSchedules.ExamID);
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
            return View(db.ExamModels.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Begin(int? id)
        {
            List<int> MyList = new List<int>();
            var ListDetailsQuestions = db.DetailsExamModels.Where(r => r.ExamID == 1).ToList();
            ViewData["DetailQuestions"] = ListDetailsQuestions;
            foreach (var item in ListDetailsQuestions) { MyList.Add(item.QuestionsID); }
            var ListMyQuestions = db.QuestionsModels.Where(r => MyList.Contains(r.QuestionsID)).ToList();

            int CandidateMark = 0, totalMark = 0;
            foreach (var item in ListMyQuestions)
            {
                int name = item.QuestionsID;
                var temp = int.Parse(Request.Form[name]);
                if (temp == item.CorrectAnswer) CandidateMark += item.Mark;
                totalMark += item.Mark;
            }
            int userMark = (CandidateMark * 100) / totalMark;
            DetailsRegistrations userDetail = getUserDetails();
            userDetail.Mark = userMark;
            db.Entry(userDetail).State = EntityState.Modified;
            db.SaveChanges();
            //string name = "T" + 1 + "Q" + 1;
            //string value = Request.Form[name];
            //string valueTwo = Request.Form["T1Q2"];
            return Redirect("Result");
        }

        public ActionResult Result()
        {
            DetailsRegistrations userDetails = getUserDetails();
            return View(userDetails);
        }
    }
}
