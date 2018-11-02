using AptitudeTestOnline.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptitudeTestOnline.Controllers
{
    public class ScheduleController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();
        // GET: Schedule
        public ActionResult Index()
        {
             var userID = User.Identity.GetUserId();
            var userAccounts = db.AccountModels.Where(item => item.UserID == userID).ToList();
            var accID = userAccounts[0].AccountID;
            ViewData["myname"] = userAccounts[0].Name;
            var detailsRegis = db.DetailsRegistrations.Where(item => item.AccountID == accID).ToList();
            var scheduleid = detailsRegis[0].ScheduleID;
            var myschedule = db.Schedules.Where(item => item.ScheduleID == scheduleid).ToList();
            ViewData["mytimeschedule"] = myschedule[0].DateOfTime;
            return View();
        }
    }
}