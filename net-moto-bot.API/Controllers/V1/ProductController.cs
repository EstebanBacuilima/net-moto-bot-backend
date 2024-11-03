using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/product")]
[ApiController]
[Authorize]
public class ProductController(IProductService _service) : CommonController
{
    [HttpPatch, Route("modify/change-state")]
    public async Task<IActionResult> ChangeStateAsync(
        [FromQuery] int id, [FromBody] bool active)
    {
        await _service.ChangeStateAsync(id, active);

        return Ok(ResponseHandler.Ok());
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] Product product)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(product)));
    }

    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAsync()));
    }

    [HttpGet, Route("by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByIdAsync(id)));
    }

    [HttpPut, Route("update/{id}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] Product product, int id)
    {
        product.Id = id;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(product)));
    }
}
