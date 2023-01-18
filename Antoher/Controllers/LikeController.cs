using Antoher.DAL;
using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly ILikeRepo _likes;
        private readonly IPostRepo _post;
        public LikeController(UserManager<User> userManager, SignInManager<User> signInManager, 
            ApplicationDbContext db, ILikeRepo likes, IPostRepo post)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
            _likes = likes;
            _post = post;
        }

        [HttpPost]
        [Authorize]
        [Route("LikeAction")]
        public async Task<IActionResult> LikeAction([FromQuery] /*System.Text.Json.JsonElement*/ int postid)
        {
            //var postid = int.Parse(Idpost.GetProperty("Idpost").ToString()); //id полученного поста
            
            //данные о текущем юзере (айдишник и сущность)
            string user = User.Claims.First(x => x.Type == "UserID").Value; 
            var userEntity = await _userManager.FindByIdAsync(user);

            //проверяем, есть ли выбранный пост в бд
            var post = await _db.posts.FirstOrDefaultAsync(x => x.postId == postid);
            
            if (post == null)
                return NoContent();     //204
            
            //пост существует - дальше проверка на то, есть ли лайк у пользователя
            else
            {
                var postLike = await _db.likes.FirstOrDefaultAsync(x => x.userId == userEntity.Id && x.postId == postid);

                if (postLike != null)
                {
                    //лайк уже стоит -> убираем лайк
                    post.likes--;
                    await _likes.DeleteAsync(postLike.likeId);
                    //_db.likes.Remove(postLike);
                    //await _db.SaveChangesAsync();
                    return Ok(userEntity.UserName + " убрал лайк с поста " + post.postId);
                }
                else
                {
                    //лайка нет в бд -> ставим лайк
                    //создаю объект лайка для добавления в бд
                    var like = new Like()
                    {
                        postId = post.postId,
                        userId = userEntity.Id
                    };
                    post.likes++;
                    await _likes.AddAsync(like);
                    //_db.likes.Add(like);
                    //await _db.SaveChangesAsync();
                    return Ok(userEntity.UserName + " поставил лайк посту " + post.postId);
                }
            }
            
        }
    }


    
}
