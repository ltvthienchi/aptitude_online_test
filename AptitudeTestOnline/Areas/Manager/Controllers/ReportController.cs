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

namespace JoinAndViewModel.Models {
    public partial class Report
    {
        public int AccountID { get; set; }
        public int ExamID { get; set; }
        public string Username { get; set; }
        public string CandidateName { get; set; }
        public string TimeleftPartOne { get; set; }
        public string TimeleftPartTwo { get; set; }
        public string TimeleftPartThree { get; set; }
        public DateTime AddedDate { get; set; }
        public int Mark { get; set; }
        public DateTime Schedules { get; set; }
    }
}

namespace AptitudeTestOnline.Areas.Manager.Controllers
{
    public class ReportController : Controller
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
        private Accounts getUserAccounts(int? AccountID)
        {
            var userAccounts = db.AccountModels.Where(item => item.AccountID == AccountID).ToList();
            return userAccounts[0];
        }

        private DetailsRegistrations getUserDetails(int? AccountID)
        {
            var userDetails = db.DetailsRegistrations.Where(item => item.AccountID == AccountID).ToList();
            return userDetails[0];
        }

        private SchedulesModels getUserSchedules(int? ScheduleID)
        {
            SchedulesModels userSchedules = db.Schedules.Find(ScheduleID);
            return userSchedules;
        }
        // GET: Manager/Report
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            var reports = from perform in db.PerformTests

                        join account in db.AccountModels on perform.AccountID
                            equals account.AccountID where account.AccountID == perform.AccountID
                    
                        join details in db.DetailsRegistrations on perform.AccountID
                            equals details.AccountID where details.AccountID == perform.AccountID

                        join shedules in db.Schedules on details.ScheduleID
                            equals shedules.ScheduleID where shedules.ScheduleID == details.ScheduleID

                        select new JoinAndViewModel.Models.Report
                        {
                            AccountID = account.AccountID,
                            ExamID = perform.ExamID,
                            Username = account.Email,
                            CandidateName = account.Name,
                            TimeleftPartOne = perform.TimePartOne,
                            TimeleftPartTwo = perform.TimePartTwo,
                            TimeleftPartThree = perform.TimePartThree,
                            AddedDate = perform.AddedDate,
                            Mark = details.Mark,
                            Schedules = shedules.DateOfTime
                        };
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    questions = db.QuestionsModels.Where(s => s.QuestionsName.Contains(searchString));
            //}

            reports = reports.OrderByDescending(q => q.AddedDate);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(reports.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            GetData();
            //
            DetailsRegistrations userDetail = getUserDetails(id);
            SchedulesModels userSchedules = getUserSchedules(userDetail.ScheduleID);
            int ExamID = userSchedules.ExamID;
            List<int> MyList = new List<int>();
            //
            var ListDetailsQuestions = db.DetailsExamModels.Where(r => r.ExamID == ExamID).ToList();
            ViewData["DetailQuestions"] = ListDetailsQuestions;
            foreach (var item in ListDetailsQuestions)
            {
                MyList.Add(item.QuestionsID);
            }
            ViewBag.Test = ListDetailsQuestions;
            ViewData["MyQuestions"] = db.QuestionsModels.Where(r => MyList.Contains(r.QuestionsID)).ToList();
            ViewData["CandidateAnswer"] = db.CandidateAnswers.Where(r => r.AccountID == id);
            ViewData["PerformTest"] = db.PerformTests.Where(r => r.AccountID == id);
            return View();
        }
    }
}