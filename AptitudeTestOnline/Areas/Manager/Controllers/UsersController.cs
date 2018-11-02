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

namespace AptitudeTestOnline.Areas.Manager.Controllers
{
    public class UsersController : Controller
    {
        private ATODatabaseContext db = new ATODatabaseContext();
        // GET: Manager/Users
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            var ListResult = from q in db.AccountModels select q;
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
                ListResult = db.AccountModels.Where(s => s.Name.Contains(searchString));
            }


            ListResult = ListResult.OrderByDescending(q => q.AccountID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            ViewData["ListAccount"] = db.AccountModels.ToList();
            ViewData["ListRegister"] = db.DetailsRegistrations.OrderByDescending(a => a.Mark).ToList();
            return View(ListResult.ToPagedList(pageNumber, pageSize));
            
        }
    }
}