using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSignalRChat.Models
{
    public class Message
    {
        public string Id { get; set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        public string message { get; set; }
    }
}