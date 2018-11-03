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

        ATODatabaseContext db = new ATODatabaseContext();

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
    }
}