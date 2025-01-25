using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using Service = net_moto_bot.Domain.Entities.Service;


namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/service")]
[ApiController]
//[Authorize]
public class ServiceController(
    IServiceService _service) : CommonController
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
        [FromBody] Service service)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(service)));
    }

    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAsync()));
    }

    [HttpGet, Route("find/by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByIdAsync(id)));
    }

    [HttpPut, Route("update/{code}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] Service service, string code)
    {
        service.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(service)));
    }
}
