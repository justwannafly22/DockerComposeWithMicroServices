using Microsoft.AspNetCore.Mvc;
using SenderApi.Services;

namespace SenderApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageBusinessLogic _messageBusinessLogic;
    private readonly ILogger<MessagesController> _logger;

    public MessagesController(IMessageBusinessLogic messageBusinessLogic, ILogger<MessagesController> logger)
    {
        _messageBusinessLogic = messageBusinessLogic;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage()
    {
        var response = await _messageBusinessLogic.CreateMessageAsync();

        return Ok(response);
    }

}
