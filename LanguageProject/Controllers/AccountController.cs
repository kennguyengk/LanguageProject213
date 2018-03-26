﻿using System;
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

                   /* Models.User curent_user = UserManager.Find(model.Email, model.Password);
                    IList<string> roles = UserManager.GetRoles(curent_user.Id);

                    HttpCookie userCookie = CustomIdentity.Create(curent_user,roles);
                    Response.Cookies.Add(userCookie);*/
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
    }
}