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

                new Languages {Name = "English" },
                new Languages {Name = "Spanish" },
                new Languages {Name = "Portuguese"},
                new Languages {Name = "Chinese (Mandarin)"},
                new Languages {Name = "French"},
                new Languages {Name = "German" }


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


        

    }
}