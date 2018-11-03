using AptitudeTestOnline.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptitudeTestOnline.Controllers
{
    public class ResultController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();
        // GET: Result
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var userAccounts = db.AccountModels.Where(item => item.UserID == userID).ToList();
            var accID = userAccounts[0].AccountID;
            ViewData["myname"] = userAccounts[0].Name;
            var mypftest = db.PerformTests.Where(item => item.AccountID == accID).ToList();
            ViewData["submitdate"] = "-";
            if (mypftest.Count != 0)
            {
                ViewData["submitdate"] = mypftest[0].AddedDate.ToString(string.Format("MM/dd/yyyy hh:mm:ss tt"));
            }
            var myAnswers = db.CandidateAnswers.Where(item => item.AccountID == accID).ToList();
            int count = 0;
            if (myAnswers.Count != 0)
            {
                foreach (var answer in myAnswers)
                {
                    var abc = db.QuestionsModels.Where(item => item.CorrectAnswer == answer.Answer && item.QuestionsID == answer.QuestionID).ToList();
                    if (abc.Count != 0) count++;
                }
                ViewData["dad"] = count;
            }
            else
            {
                ViewData["dad"] = "-";
            }
            var detailsRegis = db.DetailsRegistrations.Where(item => item.AccountID == accID).ToList();
            if (detailsRegis.Count == 0)
            {
                ViewData["mymark"] = "-";
                ViewData["examname"] = "-";
            }
            else
            {
                if (detailsRegis[0].Mark == -1)
                {
                    ViewData["mymark"] = "-";
                }
                else
                {
                    ViewData["mymark"] = detailsRegis[0].Mark;
                }

                var scheduleid = detailsRegis[0].ScheduleID;
                var myschedule = db.Schedules.Where(item => item.ScheduleID == scheduleid).ToList();
                int examID = myschedule[0].ExamID;
                var myexam = db.ExamModels.Where(item => item.ExamID == examID).ToList();
                ViewData["examname"] = myexam[0].ExamName;
            }
            return View();
        }
    }
}