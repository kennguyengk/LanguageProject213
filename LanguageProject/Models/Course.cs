using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

      
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }

        public string ImagePanePath { get; set; }
        [Required]
        public virtual User Teacher { get; set; }
        [Required]
        public virtual Languages Language { get; set; }
        public int Cost { get; set; }
    }
}