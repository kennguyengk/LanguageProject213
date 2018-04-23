using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LanguageProject;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LanguageProject.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void TestUserModel() {

            Models.User user = new Models.User { FName = "Debora", LName = "Mayumi", Balance = 0, Country = "Canada" };
            Assert.AreSame("Debora", user.FName);
        }

        [TestMethod]
        public void TestLanguageModel() {
            Models.Languages lang = new Models.Languages { Name = "English", FlagImgPath = "/images/country_flag/gb.svg" };
            Assert.AreSame("English", lang.Name);

        }

        [TestMethod]
        public void TestCourse() {

            Models.Course cs = new Models.Course { Title = "IELTS Focus", Cost = 100 };
            Assert.AreSame("IELTS Focus", cs.Title);
        }

        [TestMethod]

        public void TestModelRelation() {
            Models.User user = new Models.User { FName = "Debora", LName = "Mayumi", Balance = 0, Country = "Canada" };
            Models.Languages lang = new Models.Languages { Name = "English", FlagImgPath = "/images/country_flag/gb.svg" };
            user.NativeLang = lang;

            Assert.AreSame(lang, user.NativeLang);
        }

        [TestMethod]
        public void TestLogin() {

            IWebDriver driver;
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:33010/Account/Login");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.CssSelector("input[name=\"Email\"]")).Clear();
            driver.FindElement(By.CssSelector("input[name=\"Email\"]")).SendKeys("yumi@gmail.com");
            driver.FindElement(By.CssSelector("input[name=\"Password\"]")).Clear();
            driver.FindElement(By.CssSelector("input[name=\"Password\"]")).SendKeys("123456");
            driver.FindElement(By.CssSelector("input[value=\"Sign In\"]")).Click();


        }

       
    }
}
