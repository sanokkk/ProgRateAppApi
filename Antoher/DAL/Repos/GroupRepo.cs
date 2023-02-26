using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    public class GroupRepo : IGroupRepo
    {
        private readonly ApplicationDbContext _db;
        public GroupRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(ChatGroup entity)
        {
            await _db.chatgroups.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ChatGroup> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsExistAsync(string groupName)
        {
            return await _db.chatgroups.AnyAsync(x => x.GroupName == groupName);
        }

        public async Task<ChatGroup[]> SelectAsync()
        {
            return await _db.chatgroups.ToArrayAsync();
        }

        public Task UpdateAsync(ChatGroup entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
