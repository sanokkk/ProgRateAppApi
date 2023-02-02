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
        public Task<bool> IsFriendsAsync(string firstUserId, string secondUserId);
        public Task DeleteFriendAsync(string friendOne, string friendTwo);
    }
}
