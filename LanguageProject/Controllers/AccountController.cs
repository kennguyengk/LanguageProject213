using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using LanguageProject.Models;
namespace LanguageProject.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public ActionResult Balance() {
            DAL.DataContext dt = new DAL.DataContext();

            User current = dt.Users.Find(User.Identity.GetUserId());

            BalanceViewModel ba = new BalanceViewModel()
            {
                Amount = current.Balance
            };
            return View(ba);

        }

        [HttpPost]
        public ActionResult ChangeStatus(TeacherChangeCourse cs) {
            DAL.DataContext dt = new DAL.DataContext();

            CourseSession myCss = dt.CourseSessions.Where(c => c.Id == cs.SeId).First();
            int fee = myCss.Course.Cost;

            User teacher = myCss.Teacher;
            User stu = myCss.Student;
            myCss.Status = "Completed";
            teacher.Balance = teacher.Balance + fee;
            stu.Balance = stu.Balance - fee;
            dt.SaveChanges();

            return RedirectToAction("MySchedule","Account");
        }

        public ActionResult MySchedule() {

            DAL.DataContext dt = new DAL.DataContext();
            string current_id = User.Identity.GetUserId();
            List<CourseSession> cs = dt.CourseSessions.Include("Teacher").Include("Student").Where(c => c.Student.Id == current_id).ToList();
            ViewBag.Courses = cs;
            return View();

        }
       

        [HttpPost]

        public ActionResult SaveBalance(BalanceViewModel ba) {

            DAL.DataContext dt = new DAL.DataContext();

            User current = dt.Users.Find(User.Identity.GetUserId());
            int balance = current.Balance;
            balance += ba.Amount;
            current.Balance = balance;
            dt.SaveChanges();
            return RedirectToAction("Balance", "Account");
        }

        [HttpGet]
        public ActionResult Language() {

            DAL.DataContext dt = new DAL.DataContext();
            string my_id = User.Identity.GetUserId();
            dt.Configuration.LazyLoadingEnabled = false;
            List<Models.LanguageSkill> langs;
            langs = dt.SecondLanguages.Include("Language").Where(u => u.User.Id == my_id).ToList();
            var filter = dt.SecondLanguages.Select(l => l.Language.Id).ToList();
            List<Models.Languages> allLangs = dt.Languages.Where(f =>!filter.Contains(f.Id)).ToList();

            ViewBag.AllLangs = allLangs;
            ViewBag.Langs = langs;
            return View();
        }

        [HttpPost]
        public ActionResult SaveLanguage() {
            DAL.DataContext dt = new DAL.DataContext();

            int lang_id = Int32.Parse(Request.Form["lang"]);
            int level = Int32.Parse(Request.Form["level"]);
            LanguageSkill lang = new LanguageSkill();
            lang.Language = dt.Languages.Where(l => l.Id == lang_id).FirstOrDefault();
            string user_id = User.Identity.GetUserId();
            lang.User = dt.Users.Where(u => u.Id == user_id).FirstOrDefault();
            lang.Level = level;
            dt.SecondLanguages.Add(lang);
            dt.SaveChanges();
            
            return RedirectToAction("Language", "Account");

        }

        
        public ActionResult Mailbox(int id) {


            int session_id = id; 
           DAL.DataContext dt = new DAL.DataContext();
           dt.Configuration.LazyLoadingEnabled = false;
           User currentUser = dt.Users.Find(User.Identity.GetUserId());
          
            List<ChatSession> cs = dt.ChatSessions.Include("Sender").Include("Receive").Where(c => c.ReceiveID == currentUser.Id || c.SenderID == currentUser.Id).ToList();

        
            Models.UserSettingViewModel model = new Models.UserSettingViewModel()
            {
                Balance = currentUser.Balance,
         
                FName = currentUser.FName,
                LName = currentUser.LName,
                UserId = User.Identity.GetUserId(),
                ChatSessions = cs,
                
    
                
                
                
            };
            List<Message> mymess = new List<Message>();

            if (session_id == 0 && cs.Count > 0)
            {
                int cs_id = cs[0].Id;
                session_id = cs_id;
                mymess = dt.Messages.Where(c => c.ChatSession.Id == cs_id).ToList();
            }
            else if (session_id != 0) {

                mymess = dt.Messages.Where(c => c.ChatSession.Id == session_id).ToList();
            }
            ViewBag.MyMess = mymess;
            ViewBag.SessionId = session_id;
            return View(model);


        }
        [HttpGet]

        public ActionResult ListCourses() {

            DAL.DataContext dt = new DAL.DataContext();
            dt.Configuration.LazyLoadingEnabled = false;
          
            string current_id = User.Identity.GetUserId();
            List<Course> cs = dt.Courses.Where(c => c.Teacher.Id == current_id).ToList();
            ViewBag.Course = cs;
            return View();

        }

        [HttpPost]

        public ActionResult SaveCourse(CourseViewModel cs) {

            DAL.DataContext dt = new DAL.DataContext();
            Models.User currentUser = dt.Users.Find(User.Identity.GetUserId());
            Course course = new Course();
            course.Description = cs.Des;
            course.Title = cs.Title;
            course.Cost = cs.Cost;
            course.Language = dt.Languages.FirstOrDefault();
            course.Teacher = currentUser;

            dt.Courses.Add(course);
            dt.SaveChanges();

            return RedirectToAction("ListCourses", "Account");
        }

        [HttpPost]

        public ActionResult SaveChat(ChatBoxSend box) {

            int session_id = box.SessionId;
            string my_id = User.Identity.GetUserId();
            DAL.DataContext dt = new DAL.DataContext();
            ChatSession cs = dt.ChatSessions.Where(c => c.Id == session_id).FirstOrDefault();
            string receiver_id = (cs.SenderID == my_id ? cs.ReceiveID : cs.SenderID);

            Message ms = new Message();
            ms.SenderID = my_id;
            ms.ReceiveID = receiver_id;
            ms.Content = box.Content;
            ms.ChatSession = cs;
            ms.when = DateTime.Now;
            dt.Messages.Add(ms);
            dt.SaveChanges();


            return RedirectToAction("MailBox","Account", new { id = session_id });
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
       
        public ActionResult LogOff()
        {
            //SignInManager.
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