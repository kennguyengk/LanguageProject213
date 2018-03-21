using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LanguageProject.Models;
namespace LanguageProject.DAL
{
    public class DataContext : DbContext
    {

        public DataContext() : base("DataContext") {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SecondLanguages> SecondLanguages { get; set; } 

        public DbSet<Languages> Languages { get; set; }
        //public DbSet<User>
    }
}