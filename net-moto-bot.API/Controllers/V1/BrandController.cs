using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/brand")]
[ApiController]
//[Authorize]
public class BrandController(
    IBrandService _service) : CommonController
{
    [HttpPatch, Route("modify/change-state/{code}")]
    public async Task<IActionResult> ChangeStateAsync(
        [FromBody] bool active, string code)
    {
        return Ok(ResponseHandler.Ok(await _service.UpdateActveAsync(code, active)));
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] Brand brand)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(brand)));
    }

    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAsync()));
    }

    [HttpPut, Route("update/{code}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] Brand category, string code)
    {
        category.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(category)));
    }
}

