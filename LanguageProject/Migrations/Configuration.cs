namespace LanguageProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LanguageProject.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<LanguageProject.DAL.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LanguageProject.DAL.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            List<Languages> languages = Seeds.FirstDemo.Lanuages();
            List<Role> roles = Seeds.FirstDemo.Roles();
           

            foreach (Languages la in languages) {

                var old = context.Languages.Where(s => s.Name == la.Name).FirstOrDefault();
                if (old == null)
                {
                    context.Languages.AddOrUpdate(la);

                }

            }
            context.SaveChanges();

            foreach (Role rl in roles) {

                var old = context.Roles.Where(s => s.Name == rl.Name).FirstOrDefault();
                if (old == null)
                {
                    context.Roles.AddOrUpdate(rl);

                }

            }
           
            context.SaveChanges();
            Console.WriteLine("fadsfsa");
           
            User one = new User { FName = "Paula", LName = "Caroline", Email = "paula@gmail.com", Role = roles[1], NativeLang = context.Languages.Find(4), Balance = 0 };
            User two = new User { FName = "Debora", LName = "Mayumi", Email = "yumi@gmail.com", Role = roles[1], NativeLang = context.Languages.Find(5), Balance = 0 };

            context.Users.Add(one);
            context.Users.Add(two);
            //users.ForEach(s => context.Users.AddOrUpdate(s));


            context.SaveChanges();

             LanguageSkill language_one = new LanguageSkill { Language = languages[5], User = one, Level = 9 };
             LanguageSkill langue_two = new LanguageSkill { Language = languages[4], User = two, Level = 8 };
             context.SecondLanguages.AddOrUpdate(language_one);
             context.SecondLanguages.AddOrUpdate(langue_two);
             context.SaveChanges();
            Seeds.FirstDemo.Teachers();

        }
    }
}
