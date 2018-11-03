using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AptitudeTestOnline.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;




namespace AptitudeTestOnline.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ATODatabaseContext dbATO = new ATODatabaseContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public async Task<ActionResult> Index()
        {
            if(db.Roles.Count()==0)
            {
                IdentityRole Admin = new IdentityRole();
                Admin.Id = "0";
                Admin.Name = "Admin";
                db.Roles.Add(Admin);
                db.SaveChanges();
                IdentityRole Manager = new IdentityRole();
                Manager.Id = "1";
                Manager.Name = "Manager";
                db.Roles.Add(Manager);
                db.SaveChanges();
                IdentityRole User = new IdentityRole();
                User.Id = "2";
                User.Name = "User";
                db.Roles.Add(User);
                db.SaveChanges();
                if(db.Users.Count()==0)
                {
                    var user = new ApplicationUser { UserName = "Admin@gmail.com", Email = "Admin@gmail.com" };
                    var result = await UserManager.CreateAsync(user, "admin123");
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                        await UserManager.AddToRoleAsync(user.Id, "Admin");

                        if (dbATO.TypeOfQuestionModel.ToList().Count == 0)
                        {
                            TypeOfQuestionModel GeneralKnowledge = new TypeOfQuestionModel() { };
                            GeneralKnowledge.TypeOfQuestion = 1;
                            GeneralKnowledge.NameTypeOfQuestion = "General Knowledge";

                            TypeOfQuestionModel Mathematics = new TypeOfQuestionModel() { };
                            Mathematics.TypeOfQuestion = 2;
                            Mathematics.NameTypeOfQuestion = "Mathematics";

                            TypeOfQuestionModel ComputerTechnology = new TypeOfQuestionModel() { };
                            ComputerTechnology.TypeOfQuestion = 3;
                            ComputerTechnology.NameTypeOfQuestion = "Computer Technology";

                            dbATO.TypeOfQuestionModel.Add(GeneralKnowledge);
                            dbATO.TypeOfQuestionModel.Add(Mathematics);
                            dbATO.TypeOfQuestionModel.Add(ComputerTechnology);
                            dbATO.SaveChanges();


                            return View();
                        }
                    }

                }




            }
            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}