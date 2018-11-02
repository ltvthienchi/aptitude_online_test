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
            
            ViewData["ListAccount"] = db.AccountModels.ToList();
            ViewData["ListRegister"] = db.DetailsRegistrations.OrderByDescending(a => a.Mark).ToList();
            return View();
            
        }
    }
}