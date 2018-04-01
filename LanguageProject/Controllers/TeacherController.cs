using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace LanguageProject.Controllers
{
    public class TeacherController : BaseController
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
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