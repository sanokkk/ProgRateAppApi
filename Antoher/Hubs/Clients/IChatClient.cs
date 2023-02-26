using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using System.Threading.Tasks;

namespace Antoher.Hubs.Clients
{
    public interface IChatClient
    {
        //Task ReceiveMessage(string message, string userName);
        Task ReceiveMessage(MessDto message);
        Task Notify(string userName);
    }
}
