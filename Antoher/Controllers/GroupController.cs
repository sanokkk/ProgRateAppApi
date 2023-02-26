using Antoher.DAL.Interfaces;
using Antoher.Domain.Models;
using Antoher.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Antoher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepo _group;
        private readonly UserManager<User> _userManager;

        public GroupController( IGroupRepo group, UserManager<User> userManager)
        {
            _group = group;
            _userManager = userManager; 

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAsync([FromQuery] string groupName)
        {
            var isExist = await _group.IsExistAsync(groupName);
            if (isExist)
                return BadRequest();
            await _group.AddAsync(new ChatGroup() { GroupName = groupName});
            return Ok(groupName);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync()
        {
            var groups = await _group.SelectAsync();
            return Ok(groups); 
        }
    }
}
