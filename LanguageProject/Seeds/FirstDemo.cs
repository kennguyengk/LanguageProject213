using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LanguageProject.Models;
namespace LanguageProject.Seeds
{
    public static class FirstDemo
    {

        public static List<Languages> Lanuages() {

            List<Languages> list = new List<Languages> {

                new Languages {Name = "English",FlagImgPath="/images/country_flag/gb.svg" },
                new Languages {Name = "Spanish",FlagImgPath="/images/country_flag/es.svg" },
                new Languages {Name = "Portuguese",FlagImgPath="/images/country_flag/es.svg"},
                new Languages {Name = "Chinese (Mandarin)",FlagImgPath="/images/country_flag/cn.svg"},
                new Languages {Name = "French",FlagImgPath="/images/country_flag/fr.svg"},
                new Languages {Name = "German",FlagImgPath="/images/country_flag/de.svg" },
                new Languages {Name = "Portuguese",FlagImgPath="/images/country_flag/pt.svg" },
                new Languages {Name = "Japanese",FlagImgPath="/images/country_flag/jp.svg" },
                new Languages {Name = "Korean",FlagImgPath="/images/country_flag/kp.svg" },
                new Languages {Name = "Arabic",FlagImgPath="/images/country_flag/sa.svg" },
                new Languages {Name = "Hindi",FlagImgPath="/images/country_flag/in.svg" },
                new Languages {Name = "Italian",FlagImgPath="/images/country_flag/it.svg" },
                new Languages {Name = "Russian",FlagImgPath="/images/country_flag/ru.svg" }


            };

            return list;


        }

        public static List<Role> Roles() {

            List<Role> list = new List<Role> {

                new Role {Id = 1,Name ="Admin" },
                new Role {Id = 2, Name ="Student" },
                new Role {Id = 3,Name = "Teacher" }
            };
            return list;
        }

        public static List<User> Users() {

            List<Role> roles = Roles();
            List<Languages> langagues = Lanuages();
           // Role admin = this:Roles.Item[1];
            
            

         

            //o.SecondLang.Add(language_one);


            List<User> list = new List<User>();
           // list.Add(one);
            //list.Add(two);

            return list;
        }


        public static List<Course> Course() {

            DAL.DataContext dt = new DAL.DataContext();

            List<Course> courses = new List<Course>
            {
                new Course {Language = dt.Languages.Find(1),Title ="IELTS Focus",Cost=100 }


            };
            return courses;

        }

        public static List<User> Teachers() {
            DAL.DataContext dt = new DAL.DataContext();

            List<string> courseName = new List<string> {
                "IELTS Focus",
                "Academic Engish",
                "TOEIC",
                "IELTS Test Prep",
                "IELTS Preparation"
            };

            List<int> costs = new List<int> { 10, 19, 20, 25, 22, 18, 10 };

           
            List<User> teachers = new List<User> {

             new User { FName = "Chris", LName = "Cook", Email = "chries22@gmail.com",AvatarPath="/images/user_avatar/chris22.jpg", Role = dt.Roles.Find(2), NativeLang = dt.Languages.Find(1), Balance = 0 },
             new User { FName = "Mike", LName = "Warren", Email = "warrent2@gmail.com",AvatarPath="/images/user_avatar/mike22.jpg", Role = dt.Roles.Find(2), NativeLang = dt.Languages.Find(2), Balance = 0 },
             new User { FName = "Florencia", LName = "Ferreira", Email = "ferrerirs2@gmail.com",AvatarPath="/images/user_avatar/floren22.jpg", Role = dt.Roles.Find(2), NativeLang = dt.Languages.Find(3), Balance = 0 },
             new User { FName = "Sophie", LName = "Horrocks", Email = "sophie22@gmail.com",AvatarPath="/images/user_avatar/sophie.jpg", Role = dt.Roles.Find(2), NativeLang = dt.Languages.Find(3), Balance = 0 }

            };
            Random rnd = new Random();
            foreach (User te in teachers) {


                //dt.Users.

                dt.Users.Add(te);
              
                for (int i = 0; i <= 5; i++)
                {

                 
                    int r = rnd.Next(courseName.Count);
                    int rCost = rnd.Next(costs.Count);
                    Course course = new Course { Description="",Language = dt.Languages.Find(1), Title = courseName[rnd.Next(courseName.Count)], Cost = costs[rnd.Next(costs.Count)] };
                    course.Teacher = te;
                    dt.Courses.Add(course);

               }
              


            }
            dt.SaveChanges();

            return teachers;
        }


        

    }
}