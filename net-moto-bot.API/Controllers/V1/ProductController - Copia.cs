using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/rol")]
[ApiController]
[Authorize]
public class RolController(IRoleService _service) : CommonController
{
    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAsync()));
    }
}
