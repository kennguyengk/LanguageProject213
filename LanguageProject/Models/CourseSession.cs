using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class CourseSession
    {
        [Key]
        public int Id { get; set; }

        public virtual User Teacher { get; set; }
        public virtual User Student { get; set; }
        public virtual Course Course { get; set; }
        public DateTime When { get; set; }
        public String Status { get; set; }
    }
}