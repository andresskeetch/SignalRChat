using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSignalRChat.Repository
{
    public class Data
    {
        public static List<Models.User> users = new List<Models.User>();
        public static List<Models.Chat> allChats = new List<Models.Chat>();
        public static Dictionary<string, string> usersConnected = new Dictionary<string, string>();
    }
}