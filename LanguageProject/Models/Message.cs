using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanguageProject.Models
{
    public class Message
    {

        [Key]
        public int Id { get; set; }

        public string SenderID { get; set; }

        public string ReceiveID { get; set; }

        [ForeignKey("SenderID")]
        public virtual User Sender { get; set; }

        [ForeignKey("ReceiveID")]
        public virtual User Receive { get; set; }

        public string Content { get; set; }
        public DateTime when { get; set; }

        public virtual ChatSession ChatSession { get; set; }
    }
}