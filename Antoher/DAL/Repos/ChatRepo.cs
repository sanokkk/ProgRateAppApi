using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    public class ChatRepo : IChatRepo
    {
        private readonly ApplicationDbContext _db;
        public ChatRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(ChatMessage entity)
        {
            await _db.messages.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ChatMessage> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> isUserIn(string userId, string groupName)
        {
            return await _db.messages.AnyAsync(x => x.UserId == userId && x.GroupName == groupName);
        }

        public async Task<ChatMessage[]> SelectAsync()
        {
            return await _db.messages.ToArrayAsync();
        }

        public async Task<ChatMessage[]> SelectByGroupAsync(string groupName)
        {
            return await _db.messages.Where(x => x.GroupName == groupName).ToArrayAsync();
        }

        public Task UpdateAsync(ChatMessage entity)
        {
            throw new System.NotImplementedException();
        }


    }
}
