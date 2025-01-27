using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Custom;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/token")]
[ApiController]
public class TokenController(IJWTService _service) : CommonController
{
    [HttpGet("validate")]
    public IActionResult ValidateTokenAsync()
    {
        return Ok(ResponseHandler.Ok(_service.ValidateToken(Token)));
    }
}