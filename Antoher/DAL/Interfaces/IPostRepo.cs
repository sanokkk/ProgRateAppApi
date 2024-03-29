﻿using Antoher.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Antoher.DAL.Interfaces
{
    public interface IPostRepo: IRepo<Post>
    {
        Task<List<Post>> SelectAsync();
        Task<List<Post>> SelectUserPostsAsync(string userId);

        Task<Post> SelectPostByIdAsync(int id);

        Task<List<Post>> SelectByTitleAsync(string title);
        Task<Post> SelectByUserAndIdAsync(string userId, int postId);
        
    }
}
