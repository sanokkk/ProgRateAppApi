using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Antoher.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Antoher.Hubs
{
    

    public class ChatHub : Hub<IChatClient>
    {
        
        

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        

        public async Task SendToGroup(MessDto message)
        {
            await Clients.Group(message.GroupName).ReceiveMessage(message);

        }

    }
}
