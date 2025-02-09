using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using Attribute = net_moto_bot.Domain.Entities.Attribute;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/attribute")]
[ApiController]
//[Authorize]
public class AttributeController(
    IAttributeService _service) : CommonController
{
    [HttpPatch, Route("modify/change-state/{code}")]
    public async Task<IActionResult> ChangeStateAsync(
        [FromBody] bool active, string code)
    {
        await _service.ChangeStateAsync(code, active);

        return Ok(ResponseHandler.Ok());
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] Attribute attribute)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(attribute)));
    }

    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? value)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAsync(value ?? string.Empty)));
    }

    [HttpGet, Route("find/by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByIdAsync(id)));
    }

    [HttpPut, Route("update/{code}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] Attribute attribute, string code)
    {
        attribute.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(attribute)));
    }
}
