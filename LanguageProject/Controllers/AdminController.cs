using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LanguageProject.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyUser(string id) {

            DAL.DataContext dt = new DAL.DataContext();

            Models.User currentUser = dt.Users.Find(id);
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
        public ActionResult Student() {

            DAL.DataContext dt = new DAL.DataContext();

            List<Models.User> result;
            IdentityRole rl = new IdentityRole();
            rl = dt.Roles.Where(r => r.Name == "Student").FirstOrDefault();
            result = dt.Users.Where(u => u.Roles.Any(r => r.RoleId == rl.Id)).ToList();
            ViewBag.Students = result;

            return View();

        }

        public ActionResult Teacher() {

            DAL.DataContext dt = new DAL.DataContext();

            List<Models.User> result;
            IdentityRole rl = new IdentityRole();
            rl = dt.Roles.Where(r => r.Name == "Teacher").FirstOrDefault();
            result = dt.Users.Where(u => u.Roles.Any(r => r.RoleId == rl.Id)).ToList();
            ViewBag.Students = result;

            return View();



        }
    }
}