using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface IFriendRepo
    {
        Task AddFriendAsync(Friend entity);
        Task<List<GetUserDto>> GetFriendsAsync(string userId);
        public Task<bool> IsFriends(string firstUserId, string secondUserId);
    }
}
