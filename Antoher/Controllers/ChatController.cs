using Antoher.Chat.Dto;
using Antoher.Chat.Hubs;
using Antoher.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Antoher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<User> _userManager;
        public ChatController(IHubContext<ChatHub> hubContext, UserManager<User> userManager)
        {
            _hubContext = hubContext;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("send")]
        public IActionResult SendRequest([FromBody] MessageDto msg)
        {
            _hubContext.Clients.All.SendAsync("ReceiveOne", msg.user, msg.message);
            return Ok();
        }
    }
}
