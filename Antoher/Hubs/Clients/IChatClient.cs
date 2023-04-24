using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using System.Threading.Tasks;

namespace Antoher.Hubs.Clients
{
    /// <summary>
    /// Интерфейс для хаба.
    /// Нужен для абстракции (чтобы не хардкодить названия метода в классе хаба)
    /// </summary>
    public interface IChatClient
    {
        
        Task ReceiveMessage(MessDto message);
        Task Notify(string userName);
    }
}
