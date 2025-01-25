using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/product-file")]
[ApiController]
//[Authorize]
public class ProductFileController(IProductFileService _service) : CommonController
{
    [HttpPost, Route("bulk-create")]
    public async Task<IActionResult> BulkCreateAsync(
        [FromForm] List<IFormFile> files, 
        [FromQuery(Name ="product_id")] int productId)
    {
        return Ok(ResponseHandler.Ok(await _service.BulkCreateAsync(files, productId)));
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromForm] IFormFile file,
        [FromQuery(Name = "product_id")] int productId)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(file, productId)));
    }

    [HttpGet, Route("list/by-product-id")]
    public async Task<IActionResult> GetAllByProductIdAsync(
        [FromQuery(Name = "product_id")] int productId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllByProductIdAsync(productId)));
    }

    [HttpGet, Route("list/by-product-code")]
    public async Task<IActionResult> GetAllByProductCodeAsync(
       [FromQuery(Name = "product_code")] string productCode)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllByProductCodeAsync(productCode)));
    }

    [HttpPatch, Route("change-state")]
    public IActionResult ChangeState(
        [FromBody] bool state, [FromQuery] int id)
    {
        _service.ChangeState(id, state);
        return Ok(ResponseHandler.Ok());
    }
}
