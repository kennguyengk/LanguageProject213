using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LanguageProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.DAL
{
    public class DataContext : IdentityDbContext<User>
    {

        public DataContext() : base("DataContext",throwIfV1Schema: false) {

        }


        public static DataContext Create() {

            return new DataContext();

        }
        public DbSet<LanguageSkill> SecondLanguages { get; set; } 
        public DbSet<Course> Courses { get; set; }
        public DbSet<Languages> Languages { get; set; }
      


        public DbSet<Message> Messages { get; set; }
     
        public DbSet<CourseSession> CourseSessions { get; set; }

        public DbSet<ChatSession> ChatSessions { get; set; }
    }
}