using Antoher.DAL.Interfaces;
using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    public class FriendRepo: IFriendRepo
    {
        private readonly ApplicationDbContext _db;
        private UserManager<User> _userManager;
        public FriendRepo(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task AddFriendAsync(Friend entity)
        {
            await _db.friends.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteFriendAsync(string friendOne, string friendTwo)
        {
            var pair = await _db.friends.FirstOrDefaultAsync(x => x.friendOne_id == friendOne && x.friendTwo_id == friendTwo 
            || x.friendTwo_id == friendOne && x.friendOne_id == friendTwo);

            _db.friends.Remove(pair);

            await _db.SaveChangesAsync();
        }

        public async Task<List<GetUserDto>> GetFriendsAsync(string userId)
        {
            var friends = new List<GetUserDto>();

            var allPairs = await _db.friends.Where(x => x.friendOne_id == userId || x.friendTwo_id == userId).ToListAsync();
            foreach(var pair in allPairs)
            {
                var entityId = (pair.friendOne_id == userId) ? pair.friendTwo_id : pair.friendOne_id;
                var user = await _userManager.FindByIdAsync(entityId);
                var userDto = new GetUserDto()
                {
                    userId = user.Id,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Email = user.Email
                };
                friends.Add(userDto);
            }

            return friends;
        }

        public async Task<bool> IsFriendsAsync(string firstUserId, string secondUserId)
        {
            Friend friends = await _db.friends.FirstOrDefaultAsync(x => x.friendOne_id == firstUserId && x.friendTwo_id == secondUserId 
                                                                || x.friendOne_id == secondUserId && x.friendTwo_id == firstUserId);
            if (friends != null)
                return true;
            else
                return false;
        }
    }
}
