using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/product")]
[ApiController]
//[Authorize]
public class ProductController(IProductService _service) : CommonController
{
    [HttpPatch, Route("modify/change-state/{code}")]
    public async Task<IActionResult> ChangeStateAsync([FromBody] bool active, string code)
    {
        await _service.ChangeStateAsync(code, active);

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

    [HttpGet, Route("list-by-category")]
    public IActionResult GetAllByCategory([FromQuery] int id)
    {
        return Ok(ResponseHandler.Ok(_service.GetAllByCategoryId(id)));
    }

    [HttpGet, Route("find/by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByIdAsync(id)));
    }

    [HttpPut, Route("update/{code}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] Product product, string code)
    {
        product.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(product)));
    }
}
