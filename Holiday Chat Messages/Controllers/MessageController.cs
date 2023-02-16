using Holiday_Chat_Messages.Models.Requests;
using Holiday_Chat_Messages.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Holiday_Chat_Messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private UserMessageService _userMessageService;

        public MessageController(UserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }

        [HttpPost("UserMessage")]
        public IActionResult UserMessage([FromBody] UserMessageRequest request)
        {
            var processMessageSuccess = _userMessageService.ProcessMessage(request);
            if (!processMessageSuccess)
            {
                return Ok("Unable to process message, please try again");
            }

            return Ok(_userMessageService.GetCurrentQuestion(request.Sender));
        }
    }
}
