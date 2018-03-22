using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }


        public string Description { get; set; }

        public string Title { get; set; }
        public int Cost { get; set; }
        public User User { get; set; }

        public Languages Language { get; set; }
    }
}