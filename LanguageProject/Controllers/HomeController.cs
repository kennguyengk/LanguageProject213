﻿using System;
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
            return View();
        }


        // Login

        public ActionResult Login() {

            return View();
        }

        // Register
        public ActionResult Register() {

            return View();

        }

        // SearchResultFound
        public ActionResult SearchResultFound()
        {
            return View();
        }

        // TeacherInfo
        public ActionResult TeacherInfo()
        {
            return View();
        }
    }
}