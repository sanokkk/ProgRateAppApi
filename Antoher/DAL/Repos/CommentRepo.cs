using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.DAL.Repos
{
    public class CommentRepo: ICommentRepo
    {
        private readonly ApplicationDbContext _db;
        public CommentRepo(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Comment entity)
        {
            await _db.comments.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var comment = await _db.comments.FirstOrDefaultAsync(x => x.commentId == Id);
            _db.comments.Remove(comment);
            await _db.SaveChangesAsync();
        }

        public async Task<Comment> GetAsync(int id)
        {
            var comment = await _db.comments.FirstOrDefaultAsync(x => x.commentId == id);
            return comment;
        }

        public async Task<List<Comment>> SelectPostCommentsAsync(int postId)
        {
            var comments = await _db.comments.Where(x => x.postId == postId).ToListAsync();
            return comments;
        }

        public async Task UpdateAsync(Comment entity)
        {
            await _db.SaveChangesAsync();
        }
    }
}
