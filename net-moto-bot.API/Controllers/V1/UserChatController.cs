using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Dtos.Public.Request;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/user-chat")]
[ApiController]
public class UserChatController(IUserChatService _service) : CommonController
{
    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync([FromBody] UserChat userChat)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(userChat, Token)));
    }

    [HttpPost, Route("user-query")]
    public async Task<IActionResult> CreateUserQueyAsync([FromBody] UserQueryRequestDto userQueryRequest)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateUserQueryAsync(userQueryRequest, Token)));
    }

    [HttpGet, Route("list/by-user")]
    public async Task<IActionResult> GetAllCustomByUserIdAsync()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllCustomByTokenAsync(Token)));
    }

    [HttpGet, Route("find/by-code/{code}")]
    public IActionResult GetByCode(string code)
    {
        return Ok(ResponseHandler.Ok(_service.GetByCode(code)));
    }

    [HttpPut, Route("update/{code}")]
    public async Task<IActionResult> UpdateAsync([FromBody] UserChat userChat, string code)
    {
        userChat.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(userChat)));
    }

    [HttpGet, Route("list/messages-by-chat/{code}")]
    public async  Task<IActionResult> GetAllMessagesByChatCodeAsync(string code)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllMessagesByChatCodeAsync(code)));
    }
}
