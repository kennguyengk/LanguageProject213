using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LanguageProject.Models

{
    public class ChatSession
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<Message> Mess { get; set; }

        public string SenderID { get; set; }

        public string ReceiveID { get; set; }

        [ForeignKey("SenderID")]
        public virtual User Sender { get; set; }

        [ForeignKey("ReceiveID")]
        public virtual User Receive { get; set; }

   
        public bool Owner(string id) {

            return (this.SenderID == id ? true : false);

        } 
    }
}