using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptitudeTestOnline.Areas.Manager.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Manager/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}