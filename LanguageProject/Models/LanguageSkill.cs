using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class LanguageSkill
    {
        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }
        public virtual Languages Language { get; set; }
        public int Level { get; set; }
    }
}