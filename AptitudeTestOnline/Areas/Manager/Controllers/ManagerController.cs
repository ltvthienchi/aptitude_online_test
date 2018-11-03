using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using AptitudeTestOnline.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using PagedList;

namespace AptitudeTestOnline.Areas.Manager.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ManagerController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private ATODatabaseContext db = new ATODatabaseContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManagerController()
        {
        }

        public ManagerController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: Manager/Manager
        public ActionResult Index(int? page, string searchString, string currentFilter)

        {
            
           var Manager = from a in context.Users
                   where a.Roles.Any(r => r.RoleId == "1")
                   select a;
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
                Manager = Manager.Where(s => s.UserName.Contains(searchString));
            }
            Manager = Manager.OrderByDescending(q => q.Id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(Manager.ToPagedList(pageNumber, pageSize));
            

        }

        

        public ActionResult Delete(string Id)

        {

            var model = context.Users.Find(Id);

            return View(model);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        [ActionName("Delete")]

        public ActionResult DeleteConfirmed(string Id)

        {

            ApplicationUser model = null;

            try

            {

                model = context.Users.Find(Id);

                context.Users.Remove(model);

                context.SaveChanges();

                return RedirectToAction("Index");

            }

            catch (Exception ex)

            {

                ModelState.AddModelError("", ex.Message);

                return View("Delete", model);

            }

        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult Register()
        {
            
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            
            
            
            if (ModelState.IsValid)
            {


                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    db.SaveChanges();
                    
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    await UserManager.AddToRolesAsync(user.Id, "Manager");
                    return RedirectToAction("Index");
                    

                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}