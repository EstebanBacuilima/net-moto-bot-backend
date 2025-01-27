using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Interfaces.Public;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(IUserService _service) : CommonController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        return Ok(ResponseHandler.Ok(await _service.SignInAsync(loginRequestDto)));
    }

    [HttpPost("register")]
    public async Task<IActionResult> ResgisterAsync([FromBody] RegisterRequest registerRequest)
    {
        return Ok(ResponseHandler.Ok(await _service.ResgisterAsync(registerRequest)));
    }
}