using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SocialNetworkWithSignalR.Entities;
using SocialNetworkWithSignalR.Helpers;
using System.Runtime.CompilerServices;

namespace SocialNetworkWithSignalR.Hubs
{
    public class ChatHub : Hub
    {
        private UserManager<CustomIdentityUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        private CustomIdentityDbContext _context;

        public ChatHub(UserManager<CustomIdentityUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async override Task OnConnectedAsync()
        {
            var userItem = _context.Users.SingleOrDefault(x => x.Id == user.Id);
            if (userItem != null)
            {
                userItem.IsOnline = true;
                await _context.SaveChangesAsync();
            }
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            UserHelper.ActiveUsers.Add(user);
            string info = $"{user.UserName} connected successfully";
            await Clients.Others.SendAsync("Connect", info);
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var userItem = _context.Users.SingleOrDefault(x => x.Id == user.Id);
            if (userItem != null)
            {
                userItem.IsOnline = false;
                await _context.SaveChangesAsync();
            }
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            if (user != null)
            {
                UserHelper.ActiveUsers.RemoveAll(u => u.Id == user.Id);
                string info = $"{user.UserName} disconnected";
                await Clients.Others.SendAsync("Disconnect", info);
            }
        }
    }
}
