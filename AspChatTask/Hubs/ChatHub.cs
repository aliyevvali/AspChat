using AspChatTask.DAL;
using AspChatTask.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspChatTask.Hubs
{
    public class ChatHub:Hub
    {
        private AppDbContext _context;

        public ChatHub(AppDbContext context)
        {
            _context = context;
        }
        public async Task SendMessage(string message,string userName)
        {
            AppUser appUser = _context.Users.SingleOrDefault(x => x.UserName == userName);
            await Clients.Clients(appUser.ConnectionId,Context.ConnectionId).SendAsync("ReceiveMessage", message,userName);
        }
        public override Task OnConnectedAsync()
        {
            AppUser appUser = _context.Users.SingleOrDefault(x => x.UserName == Context.User.Identity.Name);
            appUser.ConnectionId = Context.ConnectionId;
            _context.SaveChanges();
            Clients.All.SendAsync("Connected",Context.User.Identity.Name);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            AppUser appUser = _context.Users.SingleOrDefault(x => x.UserName == Context.User.Identity.Name);
            appUser.ConnectionId =null;
            _context.SaveChanges();
            Clients.All.SendAsync("DisConnected", Context.User.Identity.Name);
            return base.OnDisconnectedAsync(exception); 
        }
    }
}
