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
    public class UsersController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();
        // GET: Manager/Users
        public ActionResult Index()
        {
            var result = from a in db.AccountModels
                         join b in db.DetailsRegistrations
                         on a.AccountID equals b.AccountID
                         select new
                         {
                             UserName = a.Name,
                             Education = a.Education,
                             Experience = a.Experience,
                             Mark = b.Mark

                         };
            ViewData["ListResult"] = result.OrderByDescending(a => a.Mark).ToList();
            return View();
            
        }
    }
}