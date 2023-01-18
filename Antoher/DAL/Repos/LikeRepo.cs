using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    public class LikeRepo : ILikeRepo
    {
        private readonly ApplicationDbContext _db;
        public LikeRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Like entity)
        {
            await _db.likes.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var like = await _db.likes.FirstOrDefaultAsync(x => x.likeId == id);
            _db.likes.Remove(like);
            await _db.SaveChangesAsync();
        }

        public Task<Like> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Like entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
