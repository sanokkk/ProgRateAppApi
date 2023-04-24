using Antoher.Domain.Models;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для чатов (имплементируется в ChatRepo).
    /// Наследуется от IRepo
    /// </summary>
    public interface IChatRepo:IRepo<ChatMessage>
    {
        Task<ChatMessage[]> SelectAsync();

        Task<ChatMessage[]> SelectByGroupAsync(string groupName);

        Task<bool> isUserIn(string userId, string groupName);
    }
}
