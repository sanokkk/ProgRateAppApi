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
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepo _requests;
        private UserManager<User> _userManager;
        private readonly IFriendRepo _freiends;
        public RequestController(IRequestRepo requests, UserManager<User> userManager, IFriendRepo freiends)
        {
            _requests = requests;
            _userManager = userManager;
            _freiends = freiends;
        }

        [HttpGet]
        [Authorize]
        [Route("GetRequests")]
        public async Task<IActionResult> GetRequests()
        {
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            var requests = await _requests.GetRequestsAsync(user.Id);

            var issuers = await _requests.GetIssuersAsync(requests);

            return Ok(requests);
        }

        [HttpPost]
        [Authorize]
        [Route("Reject")]
        public async Task<IActionResult> RejectRequest([FromQuery] int requestId)
        {
            var request = await _requests.GetByIdAsync(requestId);
            if (request != null)
            {
                await _requests.DeleteRequestAsync(requestId);
                return Ok();
            }
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("AddRequest")]
        public async Task<IActionResult> AddRequest([FromQuery] string targerUserId)
        {
            var userId = User.Claims.First(x => x.Type == "UserID").Value;
            if(userId == targerUserId)
                return BadRequest();
            if (await _freiends.IsFriends(userId, targerUserId) || await _requests.IsRequestedAsync(userId, targerUserId))
                return BadRequest("Уже в дркзьях / запросил дружбу");
            var request = new Request()
            {
                issuer_id = userId,
                target_id = targerUserId
            };
            await _requests.AddRequestAsync(request);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("Accept")]
        public async Task<IActionResult> AcceptRequest([FromQuery] int requestId)
        {
            var request = await _requests.GetByIdAsync(requestId);
            if (request == null)
                return BadRequest();
            var friendPair = new Friend()
            {
                friendOne_id = request.issuer_id,
                friendTwo_id = request.target_id
            };
            await _freiends.AddFriendAsync(friendPair);
            await _requests.DeleteRequestAsync(requestId);

            return Ok();
        }


    }
}
