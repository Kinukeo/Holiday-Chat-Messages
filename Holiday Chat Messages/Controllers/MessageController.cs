using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Holiday_Chat_Messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpPost("UserMessage")]
        public IActionResult UserMessage()
        {
            return Ok("User Message is OK");
        }
    }
}
