using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
namespace net_moto_bot.API.Controllers.V1;

[Route("api/v1/product-file")]
[ApiController]
//[Authorize]
public class ProductFileController(IProductFileService _service) : CommonController
{
    [HttpPost, Route("bulk-create")]
    public async Task<IActionResult> BulkCreateAsync(
        [FromForm] List<IFormFile> files,
        [FromQuery(Name = "product_id")] int productId)
    {
        return Ok(ResponseHandler.Ok(await _service.BulkCreateAsync(files, productId)));
    }

    [HttpPatch, Route("modify/change-state/{code}")]
    public IActionResult ChangeState(
        [FromBody] bool active, string code)
    {
        _service.ChangeState(code, active);
        return Ok(ResponseHandler.Ok());
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateAsync(
        [FromForm] IFormFile file,
        [FromQuery(Name = "product_id")] int productId)
    {
        return Ok(ResponseHandler.Ok(await _service.CreateAsync(file, productId)));
    }

    [HttpDelete, Route("delete/{code}")]
    public IActionResult DeleteByCode(string code)
    {
        _service.DeleteByCode(code);
        return Ok(ResponseHandler.Ok());
    }

    [HttpGet, Route("list/by-product-code")]
    public async Task<IActionResult> GetAllByProductCodeAsync(
       [FromQuery(Name = "code")] string productCode)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllByProductCodeAsync(productCode)));
    }

    [HttpGet, Route("list/by-product-id")]
    [Authorize]
    public async Task<IActionResult> GetAllByProductIdAsync(
        [FromQuery(Name = "id")] int productId)
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllByProductIdAsync(productId)));
    }
}
