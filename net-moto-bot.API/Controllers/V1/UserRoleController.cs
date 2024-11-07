using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;


[Route("api/v1/user-rol")]
[ApiController]
public class UserRoleController(IUserRoleService _service) : CommonController
{
    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync([FromBody] UserRole userRole)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(userRole)));
    }
}
