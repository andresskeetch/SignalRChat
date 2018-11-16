using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSignalRChat.Models
{
    public class User
    {
        public string ConnectionID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Enums.StateUser State { get; set; }
    }
}