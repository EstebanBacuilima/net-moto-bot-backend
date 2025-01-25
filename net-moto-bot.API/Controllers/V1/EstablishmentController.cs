using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/establishment")]
[ApiController]
//[Authorize]
public class EstablishmentController(
    IEstablishmentService _service) : CommonController
{
    [HttpPatch, Route("modify/change-state/{code}")]
    public async Task<IActionResult> ChangeStateAsync(
        [FromBody] bool active, string code)
    {
        return Ok(ResponseHandler.Ok(await _service.UpdateActiveAsync(code, active)));
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] Establishment establishment)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(establishment)));
    }

    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] bool? active, 
        [FromQuery] string name = "", 
        [FromQuery] string description = "")
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAsync(active, name, description)));
    }

    [HttpPut, Route("update/{code}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] Establishment establishment, string code)
    {
        establishment.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(establishment)));
    }
}
