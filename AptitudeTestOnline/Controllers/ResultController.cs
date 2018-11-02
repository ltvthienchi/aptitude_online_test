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
            var detailsRegis = db.DetailsRegistrations.Where(item => item.AccountID == accID).ToList();
            if (detailsRegis.Count == 0)
            {
                ViewData["mymark"] = "You haven't done the test yet";
            }
            else
            {
                ViewData["mymark"] = "Your mark is: " + detailsRegis[0].Mark;
            }
            return View();
        }
    }
}