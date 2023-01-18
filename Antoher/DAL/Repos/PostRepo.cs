using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    public class PostRepo : IPostRepo
    {
        private readonly ApplicationDbContext _db;
        public PostRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Post entity)
        {
            await _db.posts.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _db.posts.FirstOrDefaultAsync(x => x.postId == id);
            _db.posts.Remove(post);
            await _db.SaveChangesAsync();
        }

        public async Task<Post> GetAsync(int id)
        {
            var post = await _db.posts.FirstOrDefaultAsync(x => x.postId == id);
            return post;
        }

        public Task<List<Post>> SelectAsync()
        {
            var posts = _db.posts.ToListAsync();
            return posts;
        }

        public async Task UpdateAsync(Post entity)
        {
            await _db.SaveChangesAsync();
        }
    }
}
