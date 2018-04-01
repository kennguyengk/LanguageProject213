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


        public ActionResult Student() {

            DAL.DataContext dt = new DAL.DataContext();

            List<Models.User> result;
            IdentityRole rl = new IdentityRole();
            rl = dt.Roles.Where(r => r.Name == "Student").FirstOrDefault();
            result = dt.Users.Where(u => u.Roles.Any(r => r.RoleId == rl.Id)).ToList();
            ViewBag.Students = result;

            return View();

        }

        public ActionResult     Teacher() {

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