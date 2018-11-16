using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSignalRChat.Models
{
    public class Chat
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<User> UsersConected{ get; set; }
        public List<User> UsersOwners { get; set; }
        public Enums.VisibilityChat Visibility { get; set; }
        public List<Message> messages { get; set; }

    }
}