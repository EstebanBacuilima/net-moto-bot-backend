using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/product-attribute")]
[ApiController]
//[Authorize]
public class ProductAttributeController(
    IProductAttributeService _service) : CommonController
{
    [HttpPatch, Route("modify/change-state")]
    public async Task<IActionResult> ChangeStateAsync(
        [FromBody] bool active,
        [FromQuery(Name = "pro_id")] int proId,
        [FromQuery(Name = "att_id")] short attId)
    {
        await _service.ChangeStateAsync(proId, attId, active);

        return Ok(ResponseHandler.Ok());
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] ProductAttribute productAttribute)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(productAttribute)));
    }

    [HttpGet, Route("list-by-product")]
    public async Task<IActionResult> GetAllAsync([FromQuery] int id)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllByProductIdAsync(id)));
    }

    [HttpGet, Route("find/by-product-and-attribute")]
    public async Task<IActionResult> GetByProductAndAttributeAsync(
        [FromQuery(Name = "pro_id")] int proId,
        [FromQuery(Name = "att_id")] short attId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetByProductIdAndAttibuteIdAsync(proId, attId)));
    }

    [HttpPut, Route("update")]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] ProductAttribute productAttribute)
    {
        return Ok(ResponseHandler.Ok(await _service.UpdateAsync(productAttribute)));
    }
}
