using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models
{
    public class UserViewModel
    {
    }
    public class BalanceViewModel {


        public int Amount { get; set; }

    }

    public class UserSaveCourse {

        public int CourseId { get; set; }
        public string MyTime { get; set; }


    }

    public class TeacherChangeCourse
    {

        public int SeId { get; set; }
        public string SeStatus { get; set; }

    }

    public class CourseViewModel {

        public string Title { get; set; }
        public string Des { get; set; }
        public int Cost { get; set; }

    }
    public class UserSendMessModel {

        public string Send { get; set; }
        public string Content { get; set; }

    }

    
    public class UserSettingViewModel {



        public string UserId { get; set; }

        public string AvatarPath { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Email { get; set; }

        public int Balance { get; set; }

        public string Quote { get; set;}

        public List<Message> Messages { get; set; }
        public HttpPostedFileBase attachment { get; set; }

        public List<ChatSession> ChatSessions { get; set; }

        public ChatBoxSend ChatBox { get; set; }

    }

    public class ChatBoxSend {

        public string ReceiveId { get; set; }
        public int SessionId { get; set; }
        public string Content { get; set; }
    }
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }


    public class StudentRegisterViewModel {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100,ErrorMessage = "The {0} must be at last {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "First name")]
        public string FName { get; set;}

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at last {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "Last name")]
        public string LName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }





    }




}