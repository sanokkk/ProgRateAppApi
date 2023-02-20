using Antoher.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Antoher.Chat.Hubs
{
    [Authorize]
    public class ChatHub: Hub
    {
        private readonly UserManager<User> _userManager;
        public ChatHub(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public  Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveOne", user, message);
        }
    }
}
