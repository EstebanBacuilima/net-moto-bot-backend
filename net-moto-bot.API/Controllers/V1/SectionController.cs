using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;


[Route("api/v1/section")]
[ApiController]
//[Authorize]
public class SectionController(
    ISectionService _service) : CommonController
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
        [FromBody] Section section)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(section)));
    }

    [HttpGet, Route("list")]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? value)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllAsync(value ?? string.Empty)));
    }

    [HttpGet, Route("list/including-product")]
    public async Task<IActionResult> GetAllIncludingProductsAsync()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllIncludingProductsAsync()));
    }

    [HttpGet, Route("find/by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByIdAsync(id)));
    }

    [HttpPut, Route("update/{code}")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] Section section, string code)
    {
        section.Code = code;
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(section)));
    }

    [HttpGet, Route("count/product-by-id/{id}")]
    public IActionResult ProductQuantityBySection(short id)
    {
        return Ok(ResponseHandler.Ok(_service.ProductQuantityBySection(id)));
    }
}