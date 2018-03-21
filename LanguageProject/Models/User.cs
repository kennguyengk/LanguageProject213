using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }

        public virtual Role Role { get; set; }

        public int Balance { get; set; }
        public virtual Languages NativeLang { get; set; }
        public ICollection<SecondLanguages> SecondLang { get; set; }
        public string AvatarPath { get; set; }
        [Required]
        public int Gender { get; set; }
        public string Quote { get; set; }

        [Required]
        public string Email { get; set; }
    }
}