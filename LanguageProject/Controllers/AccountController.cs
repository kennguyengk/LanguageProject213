using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace LanguageProject.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }


        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST : Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index","Home");
        }

        // POST : Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Models.LoginViewModel model, string returnUrl) {
            model.RememberMe = true;
            if (!ModelState.IsValid) {
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result) {

                case SignInStatus.Success:

                    
                    return RedirectToLocal(returnUrl);

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt");
                    return View(model);

            }


        }

        // GET: Account/Register
        [AllowAnonymous]
        public ActionResult Register() {

            return View();

        }


        // POST : Account/Regiser
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.StudentRegisterViewModel model) {

            if (ModelState.IsValid) {
                DAL.DataContext dt = new DAL.DataContext();
                Models.User newUser = new Models.User { UserName = model.Email, FName = model.FName, LName = model.LName, Email = model.Email, NativeLang = dt.Languages.Find(4), Balance = 0, Country = "Canada" };
                var result = await UserManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded) {

                   
                    await SignInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);
                    await UserManager.AddToRoleAsync(newUser.Id, "Student");
                    return RedirectToAction("Index", "Home");

                }
                AddErrors(result);

            }

            return View(model);

        }

        // GET /account/setting
        [HttpGet]
        public ActionResult Setting() {

            DAL.DataContext dt = new DAL.DataContext();

            Models.User currentUser = dt.Users.Find(User.Identity.GetUserId());
            Models.UserSettingViewModel model = new Models.UserSettingViewModel()
            {
                AvatarPath = currentUser.AvatarPath,
                FName = currentUser.FName,
                LName = currentUser.LName,
                Quote = currentUser.Quote,
                Email = currentUser.Email
            };

        ViewBag.User = currentUser;
            return View(model);
        }

        // POST /account/setting
        [HttpPost]
        public ActionResult Setting(Models.UserSettingViewModel model) {

            HttpPostedFileBase file = model.attachment;
            DAL.DataContext dt = new DAL.DataContext();
            Models.User currentUser = dt.Users.Find(User.Identity.GetUserId());

            currentUser.Email = model.Email;
            currentUser.FName = model.FName;
            currentUser.Quote = model.Quote;
            currentUser.LName = model.LName;
            if (file != null) {

                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/images/avatar_path"), pic);
                // file is uploaded
                file.SaveAs(path);

                string new_path = "/Content/images/avatar_path/" + pic;
                currentUser.AvatarPath = new_path;
               
            }
            model.AvatarPath = currentUser.AvatarPath;
            dt.SaveChanges();


            return View(model);

        }

    }
}