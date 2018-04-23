using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LanguageProject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace LanguageProject.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestSearchStudentMethod()
        {

            IWebDriver driver;
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:33010/Search");
            driver.Manage().Window.Maximize();
            var searchBox = driver.FindElement(By.CssSelector("select[name=\"language\"]"));
            var selectType = new SelectElement(driver.FindElement(By.CssSelector("select[name=\"type\"]")));
            var selectValue = new SelectElement(searchBox);
            selectValue.SelectByValue("12");
            selectType.SelectByValue("Student");
            driver.FindElement(By.CssSelector("input[value=\"Search\"]")).Click();

        }
        [TestMethod]
        public void TestSearchTeachertMethod()
        {

            IWebDriver driver;
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:33010/Search");
            driver.Manage().Window.Maximize();
            var searchBox = driver.FindElement(By.CssSelector("select[name=\"language\"]"));
            var selectType = new SelectElement(driver.FindElement(By.CssSelector("select[name=\"type\"]")));
            var selectValue = new SelectElement(searchBox);
            selectValue.SelectByValue("14");
            selectType.SelectByValue("teacher");
            driver.FindElement(By.CssSelector("input[value=\"Search\"]")).Click();

        }

        [TestMethod]
        public void TestAdminSection() {

            IWebDriver driver;
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:33010/Account/Login");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector("input[name=\"Email\"]")).Clear();
            driver.FindElement(By.CssSelector("input[name=\"Email\"]")).SendKeys("yumi@gmail.com");
            driver.FindElement(By.CssSelector("input[name=\"Password\"]")).Clear();
            driver.FindElement(By.CssSelector("input[name=\"Password\"]")).SendKeys("123456");
            driver.FindElement(By.CssSelector("input[value=\"Sign In\"]")).Click();
            driver.Navigate().GoToUrl("http://localhost:33010/admin/index");


        }
        [TestMethod]
        public void TestStudentCanAddCourseSession()
        {


            Models.CourseSession s = new Models.CourseSession();
            DateTime now = new DateTime();
            s.When = now;
            Models.User user = new Models.User { FName = "Debora", LName = "Mayumi", Balance = 0, Country = "Canada" };
            s.Student = user;
            Assert.AreSame("Debora", s.Student.FName);

        }

        [TestMethod]
        public void TestTeacherCanAddCourseSession()
        {


            Models.CourseSession s = new Models.CourseSession();
            DateTime now = new DateTime();
            s.When = now;
            Models.User user = new Models.User { FName = "Jack", LName = "London", Balance = 0, Country = "Canada" };
            s.Teacher = user;
            Assert.AreSame("Jack", s.Teacher.FName);

        }



    }
}
