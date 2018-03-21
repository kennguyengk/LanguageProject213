using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
    }
}