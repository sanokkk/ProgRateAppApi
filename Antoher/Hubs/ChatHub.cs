using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Antoher.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Antoher.Hubs
{
    /// <summary>
    /// Класс хаба для чата, имплементирует интерфейс IChatClient
    /// </summary>
    public class ChatHub : Hub<IChatClient>
    {
        /// <summary>
        /// Метод, присоединяющий клиента к группе
        /// </summary>
        /// <param name="group">Название группы (хранится в бд)</param>
        /// <returns></returns>
        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        
        /// <summary>
        /// Метод, отправляющий сообщение в группу
        /// </summary>
        /// <param name="message">Дто сообщения</param>
        /// <returns></returns>
        public async Task SendToGroup(MessDto message)
        {
            await Clients.Group(message.GroupName).ReceiveMessage(message);

        }

    }
}
