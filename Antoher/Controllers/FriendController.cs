using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Antoher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private UserManager<User> _userManager;
        private readonly IFriendRepo _friends;
        private readonly IRequestRepo _request;
        public FriendController(UserManager<User> userManager, IFriendRepo friends, IRequestRepo request)
        {
            _userManager = userManager;
            _friends = friends;
            _request = request;
        }

        [HttpGet]
        [Authorize]
        [Route("GetFriends")]
        public async Task<IActionResult> GetFriends()
        {
            var userId = User.Claims.First(x => x.Type == "UserID").Value;

            var friends = await _friends.GetFriendsAsync(userId);

            return Ok(friends);
        }
    }
}
