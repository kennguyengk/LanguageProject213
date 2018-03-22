using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            DAL.DataContext dt = new DAL.DataContext();
            dt.Configuration.LazyLoadingEnabled = false;
            ViewBag.Courses = dt.Courses.Include("Teacher").Include("Language").OrderBy(r => Guid.NewGuid()).Take(8).ToList();
          //  List<Models.Course> course = 
            return View();
        }


        // Login

        public ActionResult Login() {

            return View();
        }

        // Register
        public ActionResult Registers() {

            return View();

        }
    }
}