using Microsoft.AspNetCore.Mvc;
using net_moto_bot.API.Handlers;
using net_moto_bot.Application.Interfaces.Public;
using net_moto_bot.Domain.Entities;

namespace net_moto_bot.API.Controllers.V1;


[Route("api/v1/product-section")]
[ApiController]
//[Authorize]
public class ProductSectionController(
    IProductSectionService _service) : CommonController
{
    [HttpPost, Route("bulk-create")]
    public async Task<IActionResult> ChangeStateAsync(
        [FromBody] List<int> productIds, [FromQuery(Name = "section_id")] short sectionId)
    {
        return Ok(ResponseHandler.Ok(await _service.BulkCreateAsync(productIds, sectionId)));
    }

    [HttpGet, Route("List")]
    public async Task<IActionResult> GetAllIncludingProductsAsync()
    {
        return Ok(ResponseHandler.Ok(await _service.GetAllIncludingProductsAsync()));
    }
}