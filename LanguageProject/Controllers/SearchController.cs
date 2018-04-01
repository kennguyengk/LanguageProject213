using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace LanguageProject.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {

            DAL.DataContext dt = new DAL.DataContext();
            // Models.User currentUser = dt.Users.Find(User.Identity.GetUserId());
            //List<Models.User> list = new List<Models.User>();
            //list.Add(currentUser);
            string my_id = User.Identity.GetUserId();
            dt.Configuration.LazyLoadingEnabled = false;
            List<Models.User> result = new List<Models.User>();
            string type = Request.QueryString["type"];
            string language = Request.QueryString["language"];
            if (type != null && language != null) {
                
                IdentityRole rl = new IdentityRole();

                rl = (type == "teacher" ? dt.Roles.Where(r => r.Name == "Teacher").FirstOrDefault() : dt.Roles.Where(r => r.Name == "Student").FirstOrDefault());
                result = dt.Users.Where(u => u.Roles.Any(r => r.RoleId == rl.Id)).Where( i =>i.Id != my_id).ToList();
                
                 
            }
            ViewBag.Results = result;
            ViewBag.Languages = dt.Languages.ToList();
            return View();
        }
    }
}