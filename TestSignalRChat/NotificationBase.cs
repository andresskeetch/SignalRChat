using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using TestSignalRChat.Repository;
namespace TestSignalRChat
{
    [Authorize]
    public class NotificationBase : Hub
    {

        public override Task OnConnected()
        {

            Data.usersConnected.Add(Context.User.Identity.Name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (Data.usersConnected.ContainsKey(Context.User.Identity.Name))
            {
                Data.usersConnected.Remove(Context.User.Identity.Name);
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            if (!Data.usersConnected.ContainsKey(Context.User.Identity.Name))
            {
                Data.usersConnected.Add(Context.User.Identity.Name, Context.ConnectionId);
            }
            return base.OnReconnected();
        }
        public void Register(Models.User user)
        {
            foreach (var item in Data.users)
            {
                if (item.ConnectionID == Context.ConnectionId)
                {
                    item.State = Enums.StateUser.Connected;
                    item.Name = user.Name;
                    item.LastName = user.LastName;
                    item.UserName = user.UserName;
                    Clients.All.addNewUser(user);
                    Clients.Caller.myUser(item);
                    break;
                }
            }

        }

        public void CreateChat(Models.Chat newChat)
        {
            newChat.Id = Guid.NewGuid().ToString();
            Data.allChats.Add(newChat);

            if (newChat.Visibility == Enums.VisibilityChat.publicChat)
            {
                Clients.All.addChat(newChat);
            }
            else if (newChat.Visibility == Enums.VisibilityChat.privateChat)
            {
                Clients.Clients(newChat.UsersConected.Select(f => f.ConnectionID).ToList()).addChat(newChat);
            }
        }

        public void deleteChat(Models.Chat chat)
        {
            var chatRemove = Data.allChats.FindIndex(f => f.Id == chat.Id);
            if (chatRemove > -1)
            {
                Data.allChats.RemoveAt(chatRemove);
            }
        }

        public void newMessage(string message, string chatID)
        {
            var newMessage = new Models.Message()
            {
                Chat = Data.allChats.Where(f => f.Id == chatID).FirstOrDefault(),
                Id = Guid.NewGuid().ToString(),
                message = message,
                User = Data.users.Where(f => f.ConnectionID == Context.ConnectionId).FirstOrDefault()
            };

            Data.allChats.ForEach(f =>
            {
                if (f.Id == chatID)
                {
                    f.messages.Add(newMessage);
                    if (f.Visibility == Enums.VisibilityChat.privateChat)
                    {
                        Clients.Clients(f.UsersConected.Select(u => u.ConnectionID).ToList()).addMessage(message);
                    }
                    else if (f.Visibility == Enums.VisibilityChat.publicChat)
                    {
                        Clients.All.addMessage(newMessage);
                    }
                }
            });

        }
    }
}