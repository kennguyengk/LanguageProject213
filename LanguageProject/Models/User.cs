using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace LanguageProject.Models
{
    public class User: IdentityUser
    {
      
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }

       
       
        public int Balance { get; set; }
        public virtual Languages NativeLang { get; set; }
        public virtual ICollection<LanguageSkill> SecondLang { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public string AvatarPath { get; set; }
   
        public string YouTubeLink { get; set; }
       
        public string Quote { get; set; }
       
        public string TimeZone { get; set; }
        

        [Required]
        public string Country { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}