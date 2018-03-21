using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class SecondLanguages
    {
        [Key]
        public int Id { get; set; }

        public User User { get; set; }
        public Languages Language { get; set; }
        public int Level { get; set; }
    }
}