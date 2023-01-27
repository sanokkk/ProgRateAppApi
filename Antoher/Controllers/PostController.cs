﻿using Antoher.DAL;
using Antoher.DAL.Interfaces;
using Antoher.Domain.DTO;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Antoher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IPostRepo _post;
        public PostController(UserManager<User> userManager, ApplicationDbContext db,
            IPostRepo post)
        {
            _userManager = userManager;
            _db = db;
            _post = post;
        }

        [Route("SelectAll")]
        [HttpGet]
        public IActionResult SelectAll()
        {
            var response = _db.posts.ToList();
            //var response = _post.SelectAsync();
            return Ok(response);
        }

        [Route("Select")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Select()
        {
            string userId = User.Claims.First(x => x.Type == "UserID").Value;

            var posts = await _post.SelectAsync();
            var userPosts = posts.Where(x => x.userId == userId);
            //var posts = _db.posts.Where(x => x.userId == userId);
            return Ok(userPosts);
        }

        [Route("CreatePost")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePost model)
        {
            string IdUser = User.Claims.First(x => x.Type == "UserID").Value;
            var post = new Post()
            {
                userId = IdUser,
                title = model.title,
                plot = model.plot,
                likes = 0
            };

            await _post.AddAsync(post);
            //await _db.posts.AddAsync(post);
            //await _db.SaveChangesAsync();
            return Ok(post);
        }

        [Route("DeletePost")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeletePost([FromQuery] int postId)
        {
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            var posts = await _post.SelectAsync();
            var post = posts.FirstOrDefault(x => x.postId == postId && x.userId == user.Id);

            //если пост существует -> удаляем
            if (post != null)
            {
                await _post.DeleteAsync(post.postId);
                return Ok();
            }
            return NoContent();
        }

        [HttpGet]
        [Route("SelectUserPosts")]
        public async Task<IActionResult> GetUserPost(string userId)
        {
            var posts = await _post.SelectUserPostsAsync(userId);
            return Ok(posts);
        }


    }
}
