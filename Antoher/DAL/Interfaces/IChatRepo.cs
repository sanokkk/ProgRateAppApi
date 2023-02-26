using Antoher.Domain.Models;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface IChatRepo:IRepo<ChatMessage>
    {
        Task<ChatMessage[]> SelectAsync();

        Task<ChatMessage[]> SelectByGroupAsync(string groupName);

        Task<bool> isUserIn(string userId, string groupName);
    }
}
