using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using LanguageProject.Models;
using System.Globalization;
namespace LanguageProject.Controllers
{
    
   
    public class MyApiController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Message(UserSendMessModel data) {
            DAL.DataContext dt = new DAL.DataContext();
            string current_user = User.Identity.GetUserId();
            string send_user = data.Send;
            //   var object = Request.Content.R
            string content = data.Content;

            ChatSession cs;
            cs = dt.ChatSessions.Where(u => u.ReceiveID == current_user || u.SenderID == current_user).FirstOrDefault();

            if (cs == null) {

                cs = new ChatSession();
                cs.SenderID = current_user;
                cs.ReceiveID = send_user;
                dt.ChatSessions.Add(cs);
                dt.SaveChanges();

            }
            User send = dt.Users.Where(u => u.Id == current_user).FirstOrDefault();
            User receiver = dt.Users.Where(u => u.Id == send_user).FirstOrDefault();

            Message mess = new Message();
            mess.Sender = send;
            mess.Receive = receiver;
            mess.Content = content;
            mess.when = DateTime.Now;
            mess.ChatSession = cs;
            dt.Messages.Add(mess);
            dt.SaveChanges();
            
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Test() {

            return Ok();
        }


        [HttpGet]
        public Dictionary<int, string> Courses(string id) {

            DAL.DataContext dt = new DAL.DataContext();

            Dictionary<int, string> result = new Dictionary<int, string>();

            foreach (var c in dt.Courses.Where(c => c.Teacher.Id == id)) {
                result.Add(c.Id, c.Title);
            }
            return result;
            
        }

        [HttpPost]

        public IHttpActionResult SaveCourseSession(UserSaveCourse cs) {

            DAL.DataContext dt = new DAL.DataContext();
            Course my_cs = dt.Courses.Include("Teacher").Where(c => c.Id == cs.CourseId).FirstOrDefault();
            CourseSession css = new CourseSession();
            css.Teacher = my_cs.Teacher;
            css.Course = my_cs;
            DateTime oDate = DateTime.ParseExact(cs.MyTime, "yyyy-MM-dd HH:mm", null);
            css.When = oDate;
            User current = dt.Users.Find(User.Identity.GetUserId());
            css.Student = current;
            css.Status = "Waiting Accept";
            dt.CourseSessions.Add(css);
            dt.SaveChanges();
            return Ok();

        }
    }
}
