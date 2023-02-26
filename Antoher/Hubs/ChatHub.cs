using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Antoher.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Antoher.Hubs
{
    //public class ChatHub: Hub<IChatClient>
    //{
    //    public async Task SendMessage(ChatMessage message)
    //    {
    //        await Clients.All.ReceiveMessage(message);
    //    }

    //    public async Task Enter(string UserName, string GroupName)
    //    {
    //        await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
    //        await Clients.All.
    //    }
    //}

    public class ChatHub : Hub<IChatClient>
    {
        
        //public async Task SendMessage(string message, string userName, string groupName)
        //{
        //    //await Clients.Group(groupName).SendAsync("Receive", message, userName);
        //    await Clients.Group(groupName).ReceiveMessage(message, userName);
        //}

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        //public async Task SendToGroup(string group, string message, string user)
        //{
        //    await Clients.Group(group).ReceiveMessage(message, user);

        //}

        public async Task SendToGroup(MessDto message)
        {
            await Clients.Group(message.GroupName).ReceiveMessage(message);

        }

    }
}
