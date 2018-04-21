using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using LanguageProject.Models;

namespace LanguageProject.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Search
        public ActionResult Index()
        {

            DAL.DataContext dt = new DAL.DataContext();
          
            int lang_id = 0;
            string my_id = User.Identity.GetUserId();
            
            dt.Configuration.LazyLoadingEnabled = false;
            List<Models.User> result = new List<Models.User>();
            string type = Request.QueryString["type"];
            string language = Request.QueryString["language"];
            if (type != null && language != null) {

                lang_id = Int32.Parse(language);
                Languages lang = dt.Languages.Where(l => l.Id == lang_id).FirstOrDefault();
               
                int mlanguage = Int32.Parse(language);
                IdentityRole rl = new IdentityRole();
           
                rl = (type == "teacher" ? dt.Roles.Where(r => r.Name == "Teacher").FirstOrDefault() : dt.Roles.Where(r => r.Name == "Student").FirstOrDefault());
                result = this.UserManager.Users.Where(u=>u.SecondLang.Any(m=>m.Language.Id == lang.Id )).Where(u => u.Roles.Any(r => r.RoleId == rl.Id)).Where( i =>i.Id != my_id).ToList();

                
                 
            }
            ViewBag.Results = result;
            ViewBag.LangId = lang_id;
            ViewBag.Type = type;
            ViewBag.Languages = dt.Languages.ToList();
            return View();
        }
    }
}