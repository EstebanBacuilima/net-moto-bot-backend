using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/user-chat")]
[ApiController]
public class UserChatController(IUserChatService _service) : CommonController
{
    [HttpGet, Route("list/by-user")]
    public async Task<IActionResult> GetAllCustomByUserIdAsync(
        [FromQuery(Name = "user_id")] long userId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllCustomByUserIdAsync(userId)));
    }
}
