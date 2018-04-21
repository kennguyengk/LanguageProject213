namespace LanguageProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LanguageProject.Models;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;


    internal sealed class Configuration : DbMigrationsConfiguration<LanguageProject.DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LanguageProject.DAL.DataContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false) {
                System.Diagnostics.Debugger.Launch();
            }
              
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            ApplicationRoleManager role = new ApplicationRoleManager(context);
            role.CreateRoles();

            List<Languages> languages = Seeds.FirstDemo.Lanuages();
            foreach (Languages la in languages) {

                var old = context.Languages.Where(s => s.Name == la.Name).FirstOrDefault();
                if (old == null)
                {
                    context.Languages.AddOrUpdate(la);
                    

                }

            }
            context.SaveChanges();

            
           
           
            
           
            User one = new User { UserName= "paula@gmail.com", FName = "Paula", LName = "Caroline", Email = "paula@gmail.com", NativeLang = context.Languages.Find(4), Balance = 0,Country="Canada" };
            User two = new User { UserName= "yumi@gmail.com", FName = "Debora", LName = "Mayumi", Email = "yumi@gmail.com", NativeLang = context.Languages.Find(5), Balance = 0,Country="Canada" };

            User admin = new User { UserName = "admin@gmail.com", FName = "Debora", LName = "Mayumi", Email = "admin@gmail.com", NativeLang = context.Languages.Find(5), Balance = 0, Country = "Canada" };
            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<User>(context));
          
            userManager.PasswordValidator = new PasswordValidator
            {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
            };
            var create_one = userManager.Create(one, "123456");
            if (create_one.Succeeded) {

                userManager.AddToRole(one.Id, "Teacher");

            }
            var create_two = userManager.Create(two, "123456");

            if (create_two.Succeeded) {

                userManager.AddToRole(two.Id, "Teacher");

            }
            var create_admin = userManager.Create(admin, "123456");

            if (create_admin.Succeeded)
            {

                userManager.AddToRole(admin.Id, "Admin");

            }
            List<int> costs = new List<int> { 10, 19, 20, 25, 22, 18, 10 };

            List<User> teachers = Seeds.FirstDemo.Teachers();

            List<string> courseName = Seeds.FirstDemo.courseName;

            Random rnd = new Random();
        
            foreach (User te in teachers)
            {

                var te_create = userManager.Create(te, "123456");
                if (te_create.Succeeded)
                {

                    userManager.AddToRole(te.Id, "Student");

                }

                for (int i = 0; i <= 5; i++)
                {


                    int r = rnd.Next(courseName.Count);
                    int rCost = rnd.Next(costs.Count);
                    Course course = new Course { Description = "", Language = context.Languages.Find(1), Title = courseName[rnd.Next(courseName.Count)], Cost = costs[rnd.Next(costs.Count)] };
                    course.Teacher = te;
                    context.Courses.Add(course);
                    context.SaveChanges();

                }



            }


            // context.Users.Add(one);
            //context.Users.Add(two);
            //users.ForEach(s => context.Users.AddOrUpdate(s));


            context.SaveChanges();

             LanguageSkill language_one = new LanguageSkill { Language = languages[5], User = one, Level = 9 };
             LanguageSkill langue_two = new LanguageSkill { Language = languages[4], User = two, Level = 8 };
             context.SecondLanguages.AddOrUpdate(language_one);
             context.SecondLanguages.AddOrUpdate(langue_two);
            context.SaveChanges();
          //  Seeds.FirstDemo.Teachers();

        }
    }
}
