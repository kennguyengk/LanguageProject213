using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using LanguageProject.Models;
using Microsoft.AspNet.Identity;

namespace LanguageProject.Controllers
{
    public class TeacherController : BaseController
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TeacherSchedule()
        {

            DAL.DataContext dt = new DAL.DataContext();

            User current = dt.Users.Find(User.Identity.GetUserId());
            string current_id = User.Identity.GetUserId();
            List<CourseSession> cs = dt.CourseSessions.Include("Teacher").Include("Student").Where(c => c.Teacher.Id == current_id).ToList();
            ViewBag.Courses = cs;
            return View();
        }

        [HttpPost]
        public ActionResult ChangeStatus(TeacherChangeCourse cs) {

            DAL.DataContext dt = new DAL.DataContext();
            CourseSession myCs = dt.CourseSessions.Where(c => c.Id == cs.SeId).FirstOrDefault();
            myCs.Status = "Accepted";
            dt.SaveChanges();
            return RedirectToAction("TeacherSchedule", "Account");
        }

        public ActionResult Register() {

          
            return View();

        }

        // POST : Account/Regiser
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.StudentRegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                DAL.DataContext dt = new DAL.DataContext();
                Models.User newUser = new Models.User { UserName = model.Email, FName = model.FName, LName = model.LName, Email = model.Email, NativeLang = dt.Languages.Find(4), Balance = 0, Country = "Canada" };
                var result = await UserManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {


                    await SignInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);
                    await UserManager.AddToRoleAsync(newUser.Id, "Teacher");
                    return RedirectToAction("Index", "Home");

                }
                AddErrors(result);

            }

            return View(model);

        }
    }
}