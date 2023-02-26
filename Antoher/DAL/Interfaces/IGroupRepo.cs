using Antoher.Domain.Models;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface IGroupRepo: IRepo<ChatGroup>
    {
        Task<bool> IsExistAsync(string groupName);
        Task<ChatGroup[]> SelectAsync();
    }
}
